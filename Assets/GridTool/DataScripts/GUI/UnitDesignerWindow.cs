#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GridTool.DataScripts.GUI
{
    public class UnitDesignerWindow : EditorWindow
    {
        private Rect _headerSection;
        private Rect _loadSection;
        private Rect _mainSection;
        private Rect _objectSection;

        private Texture2D _headerSectionTexture;
        private Texture2D _mainSectionTexture;
        private Color _headerSectionColor = new Color(0.5f, 0.5f, 0.5f);
        private Color _mainSectionColor = new Color(0.2f, 0.2f, 0.2f);

        private Texture2D _moveTexture;
        private Texture2D _attackTexture;
        private Texture2D _moveAndAttackTexture;

        private static int _moveOrAttack;

        private static UnitData _unitData;
        private static UnitData _loadUnitData;
        private static UnitData _overrideData;

        private static string _lastPath = "";

        private static int _minPixels = 32;
        private static int _maxPixels = 128;

        #region Initialization

        [MenuItem("Window/Unit Designer")]
        private static void OpenWindow()
        {
            UnitDesignerWindow window = (UnitDesignerWindow)GetWindow(typeof(UnitDesignerWindow), false, "Unit Designer");
            window.minSize = new Vector2(800, 600);
            window.Show();
        }

        public static void OpenWindow(UnitData data)
        {
            UnitDesignerWindow window = (UnitDesignerWindow)GetWindow(typeof(UnitDesignerWindow), false, "Unit Designer");
            window.minSize = new Vector2(800, 500);
            _overrideData = data;
            window.Show();
            _unitData = ScriptableObject.Instantiate(data);
        }

        private void OnEnable()
        {
            InitTextures();
            InitData();
        }

        // Initializes Texture 2D values
        private void InitTextures()
        {
            _headerSectionTexture = new Texture2D(1, 1);
            _headerSectionTexture.SetPixel(0, 0, _headerSectionColor);
            _headerSectionTexture.Apply();

            _mainSectionTexture = new Texture2D(1, 1);
            _mainSectionTexture.SetPixel(0, 0, _mainSectionColor);
            _mainSectionTexture.Apply();

            _moveTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/GridTool/Sprites/MoveIcon.png", typeof(Texture2D));
            _moveTexture.Apply();

            _attackTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/GridTool/Sprites/AttackIcon.png", typeof(Texture2D));
            _attackTexture.Apply();

            _moveAndAttackTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/GridTool/Sprites/MoveAndAttackIcon.png", typeof(Texture2D));
            _moveAndAttackTexture.Apply();
        }

        private static void InitData()
        {
            _unitData = (UnitData)ScriptableObject.CreateInstance(typeof(UnitData));
            _overrideData = null;
        }

        #endregion

        #region Setup

        // Called once or more during interactions with user
        private void OnGUI()
        {
            DrawLayouts();
            DrawHeader();
            DrawLoad();
            DrawMain();
            DrawObjectSection();
        }

        // Defines rect values and paints textures
        private void DrawLayouts()
        {
            int headerHeight = 60;
            int loadWidth = 160;
            int halfPadding = 4;
            int separator = 3 * Screen.width / 4;

            Rect headerFill = new Rect(0, 0, Screen.width, headerHeight);

            _headerSection.x = halfPadding * 2;
            _headerSection.y = halfPadding * 2;
            _headerSection.width = Screen.width - loadWidth - halfPadding * 3;
            _headerSection.height = headerHeight - halfPadding * 4;

            _loadSection.x = Screen.width - loadWidth + halfPadding;
            _loadSection.y = halfPadding * 2;
            _loadSection.width = loadWidth - halfPadding * 3;
            _loadSection.height = headerHeight - halfPadding * 4;

            _mainSection.x = halfPadding * 2;
            _mainSection.y = headerHeight + halfPadding * 2;
            _mainSection.width = separator - halfPadding * 3;
            _mainSection.height = Screen.height - headerHeight - halfPadding * 8;

            _objectSection.x = separator + halfPadding;
            _objectSection.y = headerHeight + halfPadding * 2;
            _objectSection.width = Screen.width - separator - halfPadding * 3;
            _objectSection.height = Screen.height - headerHeight - halfPadding * 8;

            UnityEngine.GUI.DrawTexture(headerFill, _headerSectionTexture);
            UnityEngine.GUI.DrawTexture(_mainSection, _mainSectionTexture);
            UnityEngine.GUI.DrawTexture(_objectSection, _mainSectionTexture);
        }

        #endregion

        #region Header

        private void DrawHeader()
        {
            GUILayout.BeginArea(_headerSection);
            GUIStyle titleStyle = new GUIStyle { fontSize = 25, stretchHeight = true, alignment = TextAnchor.MiddleLeft };
            GUILayout.Label(" Unit Designer" + (string.IsNullOrEmpty(_unitData.Name) ? " " : " (" + _unitData.Name + ")"), titleStyle);
            GUILayout.EndArea();
        }

        private void DrawLoad()
        {
            GUILayout.BeginArea(_loadSection);
            EditorGUILayout.BeginVertical();
            _loadUnitData = (UnitData)EditorGUILayout.ObjectField(_loadUnitData, typeof(UnitData), false);
            if (GUILayout.Button("Load Existing")) {
                LoadExistingAsset();
            }
            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private static void LoadExistingAsset()
        {
            if (_loadUnitData != null) {
                _unitData = ScriptableObject.Instantiate(_loadUnitData);
                _overrideData = _loadUnitData;
                _loadUnitData = null;
            } else {
                //EditorUtility.OpenFilePanel("Select Existing art", "", "asset");
            }
        }

        #endregion

        #region Unit Map

        private void DrawMain()
        {
            GUILayout.BeginArea(_mainSection);

            int size = Mathf.RoundToInt(Mathf.Clamp((_mainSection.width - 4 * _unitData.UnitOptionLength) / _unitData.UnitOptionLength, _minPixels, _maxPixels));

            _unitData.Verify();

            GUILayout.BeginHorizontal();
            for (int x = 0; x < _unitData.UnitOptionLength; x++) {
                GUILayout.BeginVertical();
                for (int y = 0; y < _unitData.UnitOptionLength; y++) {
                    if (x == _unitData.MaxMoveDistance && y == _unitData.MaxMoveDistance) {
                        GUILayout.Box(GUIContent.none, GUILayout.Width(size), GUILayout.Height(size - 4));
                        continue;
                    }

                    int unitValue = _unitData.UnitOptions[x, y].GetValue();
                    var tex = unitValue switch
                    {
                        1 => _moveTexture,
                        2 => _attackTexture,
                        3 => _moveAndAttackTexture,
                        _ => Texture2D.blackTexture
                    };

                    if (GUILayout.RepeatButton(tex, GUILayout.Width(size), GUILayout.Height(size))) {
                        _unitData.UnitOptions[x, y].SetNewData(_moveOrAttack);
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        #endregion

        #region Settings

        private void DrawObjectSection()
        {
            GUILayout.BeginArea(_objectSection);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            _unitData.Name = EditorGUILayout.TextField(_unitData.Name);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();


            GUILayout.Label("Set Move and Attack Data");
            int size = Mathf.RoundToInt(_objectSection.width / 4 - 4);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(Texture2D.blackTexture, GUILayout.Width(size), GUILayout.Height(size))) {
                _moveOrAttack = 0;
            }
            if (GUILayout.Button(_moveTexture, GUILayout.Width(size), GUILayout.Height(size))) {
                _moveOrAttack = 1;
            }
            if (GUILayout.Button(_attackTexture, GUILayout.Width(size), GUILayout.Height(size))) {
                _moveOrAttack = 2;
            }
            if (GUILayout.Button(_moveAndAttackTexture, GUILayout.Width(size), GUILayout.Height(size))) {
                _moveOrAttack = 3;
            }
            GUILayout.EndHorizontal();

            switch (_moveOrAttack) {
                case 1:
                    GUILayout.Label("Set Move");
                    break;
                case 2:
                    GUILayout.Label("Set Attack");
                    break;
                case 3:
                    GUILayout.Label("Set Move and Attack");
                    break;
                default:
                    GUILayout.Label("Set Nothing");
                    break;
            }


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (string.IsNullOrEmpty(_unitData.Name)) {
                EditorGUILayout.HelpBox("Please specify a [Name] for this object.", MessageType.Warning);
            } else {
                DrawCreateButton();
            }

            GUILayout.EndArea();
        }

        private void DrawCreateButton()
        {
            if (_overrideData != null) {
                if (GUILayout.Button("Save Asset", GUILayout.Height(40))) {
                    // TODO: Better way to copy information
                    _overrideData.Name = _unitData.Name;
                    _overrideData.UnitOptions = _unitData.UnitOptions;
                }
                EditorGUILayout.ObjectField(_overrideData, typeof(ObjectData), false);
            } else {
                if (GUILayout.Button("Create", GUILayout.Height(40))) {
                    string projectPath = string.IsNullOrEmpty(_lastPath) ? Application.dataPath : _lastPath;
                    string fullPath = EditorUtility.OpenFolderPanel("Select folder to save " + _unitData.Name + " to", projectPath, "");
                    if (fullPath.Length < projectPath.Length) {
                        return;
                    }
                    _lastPath = fullPath;
                    string path = "Assets" + fullPath.Remove(0, projectPath.Length);
                    AssetDatabase.CreateAsset(_unitData, path + "/" + _unitData.Name + ".asset");
                    ClearDesigner();
                }
            }
        }

        private void ClearDesigner()
        {
            _unitData = null;
            InitData();
        }

        #endregion
    }
}

#endif