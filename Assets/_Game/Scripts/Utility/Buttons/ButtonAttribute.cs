namespace EasyButtons
{
    using System;

    public enum ButtonMode
    {
        AlwaysEnabled,
        EnabledInPlayMode,
        DisabledInPlayMode
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ButtonAttribute : Attribute
    {
        public readonly string name;

        public ButtonAttribute()
        {
        }

        public ButtonAttribute(string name) => this.name = name;

        public ButtonMode Mode { get; set; } = ButtonMode.AlwaysEnabled;
    }
}