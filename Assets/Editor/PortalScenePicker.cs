using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(Portal), true)]
    public class PortalScenePicker : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var picker = target as Portal;
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.nextLevel);

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUILayout.ObjectField("Next Level", oldScene, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                var scenePathProperty = serializedObject.FindProperty("nextLevel");
                scenePathProperty.stringValue = newPath;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
