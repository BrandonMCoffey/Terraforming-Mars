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

        protected override void DrawInternal(IEnumerable<object> targets)
        {
            if (GUILayout.Button(displayName)) {
                foreach (object obj in targets) {
                    method.Invoke(obj, null);
                }
            }
        }
    }
}
#endif