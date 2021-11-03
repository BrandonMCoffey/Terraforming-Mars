namespace GridTool.DataScripts
{
    public struct LevelObjectData
    {
        public string DisplayName => string.IsNullOrEmpty(Name) ? "-" : Name;
        public string Name { get; set; }

        public LevelObjectData(string name)
        {
            Name = name == "-" ? "" : name;
        }
    }
}