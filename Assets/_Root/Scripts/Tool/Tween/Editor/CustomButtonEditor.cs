using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Tool.Tween.Editor
{
    [CustomEditor(typeof(CustomButtonByInheritance_Obsolete))]
    internal class CustomButtonEditor : ButtonEditor
    {
        private SerializedProperty m_InteractableProperty;

        protected override void OnEnable()
        {
            m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
        }

        // Новый способ редактирования представления инспектора
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var animationType = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance_Obsolete.AnimationTypeName));
            var curveEase = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance_Obsolete.CurveEaseName));
            var duration = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance_Obsolete.DurationName));
            var vibrato = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance_Obsolete.VibratoName));
            var randomness = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance_Obsolete.RandomnessName));
            var snapping = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance_Obsolete.SnappingName));
            var fadeOut = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance_Obsolete.FadeOutName));

            var tweenGeneralSettingsLabel = new Label("General settings of Tween Animation");
            var tweenSpecificSettings = new Label("\nSettings for change rotation/position animations");
            var intractableLabel = new Label("\nInteractable");

            root.Add(tweenGeneralSettingsLabel);
            root.Add(animationType);
            root.Add(curveEase);
            root.Add(duration);
            root.Add(vibrato);
            root.Add(tweenSpecificSettings);
            root.Add(randomness);
            root.Add(snapping);
            root.Add(fadeOut);
            
            root.Add(intractableLabel);
            root.Add(new IMGUIContainer(OnInspectorGUI));

            return root;
        }

        // Старый способ представления инспектора
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_InteractableProperty);

            EditorGUI.BeginChangeCheck();
            EditorGUI.EndChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
