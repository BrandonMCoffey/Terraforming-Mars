#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Utility.Buttons.Editor
{
    public abstract class Button
    {
        public readonly MethodInfo method;
        public readonly string displayName;
        private readonly bool _disabled;

        // Create a new button
        internal static Button Create(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            var parameters = method.GetParameters();

            if (parameters.Length == 0) {
                return new ButtonWithoutParams(method, buttonAttribute);
            }
            return new ButtonWithParams(method, buttonAttribute, parameters);
        }

        // Constructor used by inherited classes (this is abstract)
        protected Button(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            this.method = method;
            displayName = string.IsNullOrEmpty(buttonAttribute.name) ? ObjectNames.NicifyVariableName(method.Name) : buttonAttribute.name;

            _disabled = buttonAttribute.Mode switch
            {
                ButtonMode.Always       => false,
                ButtonMode.NotPlaying   => !EditorApplication.isPlaying,
                ButtonMode.WhilePlaying => EditorApplication.isPlaying,
                _                       => true
            };
        }

        // Draw the button
        public void Draw(IEnumerable<object> targets)
        {
            EditorGUI.BeginDisabledGroup(_disabled);
            DrawInternal(targets);
            EditorGUI.EndDisabledGroup();
        }

        protected abstract void DrawInternal(IEnumerable<object> targets);
    }
}
#endif