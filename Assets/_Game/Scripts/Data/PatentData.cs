using System.Collections.Generic;
using Scripts.Enums;
using Scripts.Mechanics;
using Scripts.Structs;
using Scripts.UI;
using UnityEngine;
using Utility.Buttons;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Scripts.Data
{
    public class PatentData : ScriptableObject
    {
        [Header("Patent Info")]
        public string Name = "New Patent";
        public int Honor;
        [Header("Cost")]
        public PatentCost Cost1;
        public PatentCost Cost2;
        [Header("Requirements")]
        public PatentConstraint Constraint1;
        public PatentConstraint Constraint2;
        [Header("Tags")]
        public PatentResourceType Alt1 = PatentResourceType.None;
        public PatentResourceType Alt2 = PatentResourceType.None;
        public PatentResourceType Alt3 = PatentResourceType.None;
        [Header("Effects")]
        public PatentEffect Effect1;
        public PatentEffect Effect2;
        public PatentEffect Effect3;
        public PatentEffect Effect4;

        private PatentCollection _collection;

        public List<Sprite> GetAltSprites(IconData icons)
        {
            var list = new List<Sprite>();
            var icon1 = icons.GetIcon(Alt1);
            if (icon1 != null) list.Add(icon1);
            var icon2 = icons.GetIcon(Alt2);
            if (icon2 != null) list.Add(icon2);
            var icon3 = icons.GetIcon(Alt3);
            if (icon3 != null) list.Add(icon3);
            return list;
        }

        public bool AnyActivate(GameData gameData)
        {
            return Activate(gameData, CanActivate(gameData, true));
        }

        public bool Activate(GameData gameData, bool alt)
        {
            if (!CanActivate(gameData, alt)) return false;
            if (!gameData.CurrentPlayer.RemoveResource(alt ? Cost2.Resource : Cost1.Resource, alt ? Cost2.Amount : Cost1.Amount)) {
                return false;
            }
            if (Effect1.Active) ActivateEffect(Effect1, gameData);
            if (Effect2.Active) ActivateEffect(Effect2, gameData);
            if (Effect3.Active) ActivateEffect(Effect3, gameData);
            if (Effect4.Active) ActivateEffect(Effect4, gameData);
            gameData.CurrentPlayer.AddHonor(Honor);
            gameData.CurrentPlayer.CompletePatent(this);
            AnnouncementController.Instance.Announce(gameData.CurrentPlayer.PlayerName + " Activated " + Name, GetEffectsReadable(), 0, 3);
            return true;
        }

        private static void ActivateEffect(PatentEffect effect, GameData gameData)
        {
            switch (effect.Type) {
                case PatentEffectType.Lose:
                    gameData.CurrentPlayer.RemoveResource(effect.Resource, effect.Amount);
                    break;
                case PatentEffectType.Damage:
                    gameData.CurrentPlayer.RemoveResource(effect.Resource, effect.Amount, true);
                    break;
                case PatentEffectType.Build:
                    PlayerStandardProjects.ForcePlaceTile(effect.Tile, !gameData.CurrentPlayer.UserControlled);
                    break;
                case PatentEffectType.Increase:
                    gameData.Planet.IncreaseStatus(effect.Status);
                    break;
                case PatentEffectType.Earn:
                    gameData.CurrentPlayer.AddResource(effect.Resource, effect.Amount);
                    break;
                case PatentEffectType.LevelUp:
                    gameData.CurrentPlayer.AddResource(effect.Resource, effect.Amount, true);
                    break;
                case PatentEffectType.Sabotage:
                    gameData.OtherPlayer.RemoveResource(effect.Resource, effect.Amount, true);
                    break;
                case PatentEffectType.EarnPatents:
                    gameData.CurrentPlayer.AddPatents(gameData.PatentCollection.GetRandom(2));
                    break;
            }
        }

        public bool CanActivate(GameData gameData, bool alt)
        {
            if (alt && !Cost2.Active) return false;
            if (!gameData.CurrentPlayer.HasResource(alt ? Cost2.Resource : Cost1.Resource, alt ? Cost2.Amount : Cost1.Amount)) {
                return false;
            }
            if (Constraint1.Active && !CheckConstraint(Constraint1, gameData)) {
                return false;
            }
            if (Constraint2.Active && !CheckConstraint(Constraint2, gameData)) {
                return false;
            }
            return true;
        }

        private static bool CheckConstraint(PatentConstraint constraint, GameData gameData)
        {
            int required = constraint.Amount;
            int actual = constraint.Type switch {
                PatentConstraintType.PlanetOxygen  => gameData.Planet.GetLevel(PlanetStatusType.Oxygen),
                PatentConstraintType.PlanetHeat    => gameData.Planet.GetLevel(PlanetStatusType.Heat),
                PatentConstraintType.PlanetWater   => gameData.Planet.GetLevel(PlanetStatusType.Water),
                PatentConstraintType.IronLevel     => gameData.CurrentPlayer.GetResource(ResourceType.Iron, true),
                PatentConstraintType.TitaniumLevel => gameData.CurrentPlayer.GetResource(ResourceType.Titanium, true),
                _                                  => 0
            };

            return constraint.Comparison switch {
                ComparisonType.GreaterThan          => actual > required,
                ComparisonType.GreaterThanOrEqualTo => actual >= required,
                ComparisonType.LessThan             => actual < required,
                ComparisonType.LessThanOrEqualTo    => actual <= required,
                ComparisonType.EqualTo              => actual == required,
                _                                   => false
            };
        }

        public string GetConstraintsReadable()
        {
            if (!Constraint1.Active) return "";
            return GetConstraintReadable(Constraint1.Type) + " must be " + GetComparisonReadable(Constraint1.Comparison) + " " + Constraint1.Amount;
        }

        public string GetEffectsReadable()
        {
            string output = "";
            if (Effect1.Active) output += GetEffectReadable(Effect1);
            if (Effect2.Active) output += ", " + GetEffectReadable(Effect2);
            if (Effect3.Active) output += ", " + GetEffectReadable(Effect3);
            if (Effect4.Active) output += ", " + GetEffectReadable(Effect4);
            return output;
        }

        public string GetEffectReadable(PatentEffect effect)
        {
            string output = effect.Type.ToString();
            switch (effect.Type) {
                case PatentEffectType.Build:
                    output += " one " + effect.Tile;
                    break;
                case PatentEffectType.Increase:
                    output += " Planet " + effect.Status;
                    break;
                default:
                    output += " " + effect.Amount + " " + effect.Resource;
                    break;
            }
            return output;
        }

        public static string GetConstraintReadable(PatentConstraintType type)
        {
            return type switch {
                PatentConstraintType.PlanetOxygen   => "Planet Oxygen",
                PatentConstraintType.PlanetHeat     => "Planet Heat",
                PatentConstraintType.PlanetWater    => "Planet Water",
                PatentConstraintType.IronLevel      => "Iron Level",
                PatentConstraintType.TitaniumLevel  => "Titanium Level",
                PatentConstraintType.SciencePatents => "Science Patents",
                _                                   => ""
            };
        }

        public static string GetComparisonReadable(ComparisonType type)
        {
            return type switch {
                ComparisonType.GreaterThan          => "Greater Than",
                ComparisonType.GreaterThanOrEqualTo => "Greater Than Or Equal To",
                ComparisonType.LessThan             => "Less Than",
                ComparisonType.LessThanOrEqualTo    => "Less Than Or Equal To",
                ComparisonType.EqualTo              => "Equal To",
                _                                   => ""
            };
        }

        public List<Sprite> GetEffectSprites(IconData icons)
        {
            var list = new List<Sprite>();
            if (Effect1.Active && Effect1.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect1.Resource));
            }
            if (Effect2.Active && Effect2.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect2.Resource));
            }
            if (Effect3.Active && Effect3.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect3.Resource));
            }
            if (Effect4.Active && Effect4.Resource != ResourceType.None) {
                list.Add(icons.GetResource(Effect4.Resource));
            }
            return list;
        }

#if UNITY_EDITOR
        public void Init(PatentCollection collection)
        {
            _collection = collection;
        }

        [ContextMenu("Delete this")]
        [Button("Delete This", Spacing = 25)]
        private void EditorDeleteThis()
        {
            if (_collection != null) {
                _collection.EditorDeletePatent(this);
            } else {
                Undo.DestroyObjectImmediate(this);
                AssetDatabase.SaveAssets();
            }
        }

        [Button("Update Patent Name")]
        private void EditorRename()
        {
            name = Name;
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}