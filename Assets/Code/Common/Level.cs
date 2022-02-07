using System;
using System.Collections.Generic;

namespace Strategy.Common
{
    [Serializable]
    public sealed class Level
    {
        public string Name;
        public List<EnvironmentItem> Ground = new List<EnvironmentItem>();
        public List<EnvironmentItem> Decorations = new List<EnvironmentItem>();
        public List<SavedLevelItem> Buildings = new List<SavedLevelItem>();
        public List<SavedLevelItem> Units = new List<SavedLevelItem>();
    }
}
