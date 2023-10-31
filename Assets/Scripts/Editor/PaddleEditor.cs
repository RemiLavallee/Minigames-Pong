using UnityEditor;

namespace Core.Pong.Editor
{
    [CustomEditor(typeof(Paddle))]
    public class PaddleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Age");
        }
    }
}

