using UnityEditor;
using UnityEngine;

namespace _game.Scripts.Editor
{
    public class LevelEditor : EditorWindow
    {
        public GameObject Platform, WaterGroundPlatform, EndLevelPlatform;
        public GameObject CubeObstacle, SphereObstacle, UnbreakableObstacle;
        public GameObject SpeedBoost, TrampolineBoost;
        
        private static Transform _segmentParent;
        private static Transform _obstacleParent;
        private static Transform _level;
        private Vector2 _scroll;
        private float _totalZ;
        private GameObject _lastPlatform;
        private UnityEditor.Editor _editor;
        
        [MenuItem("SliceItAll/LevelEditor")]
        private static void ShowEditor()
        {
            // Get existing open window or if none, make a new one:
            var window = (LevelEditor)GetWindow(typeof(LevelEditor));
            window.titleContent = new GUIContent("Level Editor");
            window.Show();
        }
        
        private void OnGUI()
        {
            _scroll = GUILayout.BeginScrollView(_scroll);
            LevelRect();
            LevelSegments();
            GUILayout.Space(16);
            SaveRect();
            GUILayout.EndScrollView();
        }

        private void SaveRect()
        {
            if (GUILayout.Button("SavePrefab"))
            {
                SavePrefab();
            }
        }

        private static void SavePrefab()
        {
            if (_level == null) return;
            
            string path;
            path = PrefabUtility.IsAnyPrefabInstanceRoot(_level.gameObject) ? 
                PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(_level.gameObject) : AssetDatabase.GenerateUniqueAssetPath("Assets/_game/Prefabs/Levels/_level.prefab");

            PrefabUtility.SaveAsPrefabAssetAndConnect(_level.gameObject, path, InteractionMode.UserAction, out var success);
            Debug.Log($"Prefab saved {success}");
            EditorUtility.SetDirty(_level);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
        
        
        private void LevelRect()
        {
            EditorGUI.BeginChangeCheck();
            _level = (Transform) EditorGUILayout.ObjectField("Level", _level, typeof(Transform), true);
            if (GUILayout.Button("Create Level"))
            {
                _totalZ = 0;
                CreateNewLevel();
            }
            if (EditorGUI.EndChangeCheck()) UpdateParents();
        }

        private void LevelSegments()
        {
            EditorGUILayout.LabelField("---- Level Objects----");
            Platform = (GameObject) EditorGUILayout.ObjectField("Platform", Platform,  typeof(GameObject), true);
            WaterGroundPlatform = (GameObject) EditorGUILayout.ObjectField("Water Ground", WaterGroundPlatform,  typeof(GameObject), true);
            EditorGUILayout.LabelField("---- Obstacle Objects----");
            CubeObstacle = (GameObject) EditorGUILayout.ObjectField("CubeObstacle", CubeObstacle,  typeof(GameObject), true);
            SphereObstacle = (GameObject) EditorGUILayout.ObjectField("SphereObstacle", SphereObstacle,  typeof(GameObject), true);
            UnbreakableObstacle = (GameObject) EditorGUILayout.ObjectField("UnbreakableObstacle", UnbreakableObstacle,  typeof(GameObject), true);
            EditorGUILayout.LabelField("---- Boost Objects----");
            SpeedBoost = (GameObject) EditorGUILayout.ObjectField("SpeedBoost", SpeedBoost,  typeof(GameObject), true);
            TrampolineBoost = (GameObject) EditorGUILayout.ObjectField("TrampolineBoost", TrampolineBoost,  typeof(GameObject), true);
            EditorGUILayout.LabelField("---- Platform----");
            if (GUILayout.Button("Add Platform"))
            {
                PlaceAtTheEndOfLevel(Platform);
            }
            
            if (GUILayout.Button("Add WaterGroundPlatform"))
            {
                PlaceAtTheEndOfLevel(WaterGroundPlatform);
            }
            EndLevelPlatform = (GameObject) EditorGUILayout.ObjectField("Finish", EndLevelPlatform,  typeof(GameObject), true);
            if (GUILayout.Button("Add Finish"))
            {
                PlaceAtTheEndOfLevel(EndLevelPlatform, true);
            }
        }

        private void PlaceAtTheEndOfLevel(GameObject obj, bool isFinish=false)
        {
            var platform = Instantiate(obj).transform;
            platform.SetParent(_segmentParent);
            _totalZ += _lastPlatform.transform.localScale.z;
            platform.localPosition = isFinish ? new Vector3(_lastPlatform.transform.localPosition.x,_lastPlatform.transform.localPosition.y+35f,_totalZ) 
                : new Vector3(_lastPlatform.transform.localPosition.x,_lastPlatform.transform.localPosition.y,_totalZ);
            _lastPlatform = platform.gameObject;
        }


        private void CreateNewLevel()
        {
            _level = new GameObject("New Level").transform;
            _segmentParent = new GameObject("Platforms").transform;
            _segmentParent.SetParent(_level);
            _obstacleParent = new GameObject("Obstacles").transform;
            _obstacleParent.SetParent(_level);
            _lastPlatform = (GameObject) PrefabUtility.InstantiatePrefab(Platform, _level);
            _lastPlatform.transform.SetParent(_segmentParent);
        }

        private void UpdateParents()
        {
            _segmentParent = _level.Find("Platforms");
            _obstacleParent = _level.Find("Obstacles");
        }
    }
}
