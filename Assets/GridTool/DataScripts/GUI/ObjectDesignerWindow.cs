#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Texture2D = UnityEngine.Texture2D;

namespace GridTool.DataScripts.GUI
{
    public class ObjectDesignerWindow : EditorWindow
    {
        private Rect _headerSection;
        private Rect _loadSection;
        private Rect _mainSection;
        private Rect _spriteSection;

        private Texture2D _headerSectionTexture;
        private Texture2D _mainSectionTexture;
        private Color _headerSectionColor = new Color(0.5f, 0.5f, 0.5f);
        private Color _mainSectionColor = new Color(0.2f, 0.2f, 0.2f);

        private static ObjectData _objectData;
        private static ObjectData _loadObjectData;
        private static ObjectData _overrideData;

        private static string _lastPath = "";

        private static int _maxAnimFrames = 12;

        #region Initialization

        [MenuItem("Window/art Designer")]
        private static void OpenWindow()
        {
            ObjectDesignerWindow window = (ObjectDesignerWindow)GetWindow(typeof(ObjectDesignerWindow), false, "art Designer");
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        public static void OpenWindow(ObjectData data)
        {
            ObjectDesignerWindow window = (ObjectDesignerWindow)GetWindow(typeof(ObjectDesignerWindow), false, "art Designer");
            window.minSize = new Vector2(500, 500);
            _overrideData = data;
            window.Show();
            _objectData = ScriptableObject.Instantiate(data);
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
        }

        private void InitData()
        {
            _objectData = (ObjectData)ScriptableObject.CreateInstance(typeof(ObjectData));
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
            DrawSprites();
        }

        // Defines rect values and paints textures
        private void DrawLayouts()
        {
            int headerHeight = 60;
            int loadWidth = 160;
            int halfPadding = 4;
            int halfWidth = Screen.width / 2;

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
            _mainSection.width = halfWidth - halfPadding * 3;
            _mainSection.height = Screen.height - headerHeight - halfPadding * 8;

            _spriteSection.x = halfWidth + halfPadding;
            _spriteSection.y = headerHeight + halfPadding * 2;
            _spriteSection.width = halfWidth - halfPadding * 3;
            _spriteSection.height = Screen.height - headerHeight - halfPadding * 8;

            UnityEngine.GUI.DrawTexture(headerFill, _headerSectionTexture);
            UnityEngine.GUI.DrawTexture(_mainSection, _mainSectionTexture);
            UnityEngine.GUI.DrawTexture(_spriteSection, _mainSectionTexture);
        }

        #endregion

        #region Header

        private void DrawHeader()
        {
            GUILayout.BeginArea(_headerSection);
            GUIStyle titleStyle = new GUIStyle { fontSize = 25, stretchHeight = true, alignment = TextAnchor.MiddleLeft };
            GUILayout.Label(" art Designer", titleStyle);
            GUILayout.EndArea();
        }

        private void DrawLoad()
        {
            GUILayout.BeginArea(_loadSection);
            EditorGUILayout.BeginVertical();
            _loadObjectData = (ObjectData)EditorGUILayout.ObjectField(_loadObjectData, typeof(ObjectData), false);
            if (GUILayout.Button("Load Existing")) {
                LoadExistingAsset();
            }
            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private void LoadExistingAsset()
        {
            if (_loadObjectData != null) {
                _objectData = ScriptableObject.Instantiate(_loadObjectData);
                _overrideData = _loadObjectData;
                _loadObjectData = null;
            } else {
                //EditorUtility.OpenFilePanel("Select Existing art", "", "asset");
            }
        }

        #endregion

        #region Main

        private void DrawMain()
        {
            GUILayout.BeginArea(_mainSection);

            GUILayout.Label("art Settings");
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name");
            _objectData.Name = EditorGUILayout.TextField(_objectData.Name);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            GUILayout.Label("Visual Settings");
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Base Texture");
            _objectData.Texture = (Texture2D)EditorGUILayout.ObjectField(_objectData.Texture, typeof(Texture2D), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Sorting Priority");
            _objectData.SortingPriority = EditorGUILayout.DelayedIntField(_objectData.SortingPriority);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Mix Color");
            _objectData.MixColor = EditorGUILayout.ColorField(_objectData.MixColor);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (string.IsNullOrEmpty(_objectData.Name)) {
                EditorGUILayout.HelpBox("Please specify a [Name] for this object.", MessageType.Warning);
            } else if (_objectData.Static[0] == null) {
                EditorGUILayout.HelpBox("Please specify a [Sprite] for this object.", MessageType.Warning);
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
                    _overrideData.Name = _objectData.Name;
                    _overrideData.Texture = _objectData.Texture;
                    _overrideData.SortingPriority = _objectData.SortingPriority;
                    _overrideData.SpriteType = _objectData.SpriteType;
                    _overrideData.SpriteAnimationFrames = _objectData.SpriteAnimationFrames;
                    _overrideData.Static = _objectData.Static;
                    _overrideData.Up = _objectData.Up;
                    _overrideData.Left = _objectData.Left;
                    _overrideData.Down = _objectData.Down;
                }
                EditorGUILayout.ObjectField(_overrideData, typeof(ObjectData), false);
            } else {
                if (GUILayout.Button("Create", GUILayout.Height(40))) {
                    string projectPath = string.IsNullOrEmpty(_lastPath) ? Application.dataPath : _lastPath;
                    string fullPath = EditorUtility.OpenFolderPanel("Select folder to save " + _objectData.Name + " to", projectPath, "");
                    if (fullPath.Length < projectPath.Length) {
                        return;
                    }
                    _lastPath = fullPath;
                    string path = "Assets" + fullPath.Remove(0, projectPath.Length);
                    Debug.Log(fullPath);
                    AssetDatabase.CreateAsset(_objectData, path + "/" + _objectData.Name + ".asset");
                    ClearDesigner();
                }
            }
        }

        private void ClearDesigner()
        {
            _objectData = null;
            InitData();
        }

        #endregion

        #region Sprites

        private void DrawSprites()
        {
            GUILayout.BeginArea(_spriteSection);

            GUILayout.Label("Sprite Settings");
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Mode");
            _objectData.SpriteType = (ObjectSpriteType)EditorGUILayout.EnumPopup(_objectData.SpriteType);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Animation Frames");
            int animFrames = EditorGUILayout.DelayedIntField(_objectData.SpriteAnimationFrames);
            EditorGUILayout.EndHorizontal();

            animFrames = Mathf.Clamp(animFrames, 1, _maxAnimFrames);
            if (animFrames != _objectData.SpriteAnimationFrames) {
                _objectData.SpriteAnimationFrames = animFrames;
                _objectData.Static = ResizeSpriteArray(_objectData.Static, animFrames);
                _objectData.Up = ResizeSpriteArray(_objectData.Up, animFrames);
                _objectData.Left = ResizeSpriteArray(_objectData.Left, animFrames);
                _objectData.Down = ResizeSpriteArray(_objectData.Down, animFrames);
            }

            EditorGUILayout.Separator();

            switch (_objectData.SpriteType) {
                case ObjectSpriteType.Static:
                    _objectData.Static = DrawSpriteArray(_objectData.Static);
                    break;
                case ObjectSpriteType.Directional:
                    EditorGUILayout.Separator();
                    GUILayout.Label("Facing Right");
                    _objectData.Static = DrawSpriteArray(_objectData.Static);
                    GUILayout.Label("Facing Upwards");
                    _objectData.Up = DrawSpriteArray(_objectData.Up);
                    EditorGUILayout.Separator();
                    GUILayout.Label("Facing Left");
                    _objectData.Left = DrawSpriteArray(_objectData.Left);
                    EditorGUILayout.Separator();
                    GUILayout.Label("Facing Downwards");
                    _objectData.Down = DrawSpriteArray(_objectData.Down);
                    break;
            }

            GUILayout.EndArea();
        }

        private Sprite[] ResizeSpriteArray(Sprite[] arr, int size)
        {
            Sprite[] newArr = new Sprite[size];
            for (int i = 0; i < size; i++) {
                if (i < arr.Length) {
                    newArr[i] = arr[i];
                } else {
                    newArr[i] = null;
                }
            }
            return newArr;
        }

        private static Sprite[] DrawSpriteArray(Sprite[] arr)
        {
            int len = arr.Length;
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < len; ++i) {
                EditorGUILayout.BeginHorizontal();
                if (len > 1) {
                    GUILayout.Label(i.ToString());
                }
                arr[i] = (Sprite)EditorGUILayout.ObjectField(arr[i], typeof(Sprite), false);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            return arr;
        }

        #endregion
    }
}

#endif