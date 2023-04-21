#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Utility.Buttons.Editor
{
    internal class ButtonWithoutParams : Button
    {
        public ButtonWithoutParams(MethodInfo method, ButtonAttribute buttonAttribute)
            : base(method, buttonAttribute)
        {
        }

        protected override void DrawInternal(IEnumerable<object> targets, int spacing)
        {
            if (spacing > 0) {
                GUILayout.Space(spacing);
            }
            if (GUILayout.Button(displayName)) {
                foreach (object obj in targets) {
                    method.Invoke(obj, null);
                }
            }
        }
    }
}
#endif