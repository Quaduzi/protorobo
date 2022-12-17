using UI;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(UIButton), true)]
    public class UIButtonScenePicker : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var picker = target as UIButton;
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.loadedLevel);

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUILayout.ObjectField("Loaded Level", oldScene, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                var scenePathProperty = serializedObject.FindProperty("loadedLevel");
                scenePathProperty.stringValue = newPath;
            }
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();

        }
    }
}