using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy.Common
{
    public sealed class ConfigData
    {
        private Dictionary<ItemTypes, ItemData> _itemConfigs = new Dictionary<ItemTypes, ItemData>();
        private bool _isLoaded;

        public void Load(string configPath)
        {
            if(_isLoaded)
                return;
            
            ProcessDirectory(configPath);
        }

        private void ProcessDirectory(string path)
        {
            foreach(string currentPath in Directory.EnumerateFiles(path))
                ProcessFile(currentPath);

            foreach(string currentPath in Directory.EnumerateDirectories(path))
                ProcessDirectory(currentPath);
        }

        private void ProcessFile(string path)
        {
            if(!path.EndsWith(".json"))
                return;

            string itemConfigData = File.ReadAllText(path);
            ItemData item = JsonUtility.FromJson<ItemData>(itemConfigData);

            if(!_itemConfigs.ContainsKey(item.Id))
                _itemConfigs.Add(item.Id, item);
        }
    }
}
