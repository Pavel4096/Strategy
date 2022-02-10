using Strategy.Abstractions;
using Strategy.CommonTypes;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public ISelectionCapabilities GetCapabilities(ItemTypes typeId)
        {
            SelectionCapabilities capabilities = new SelectionCapabilities();

            if(_itemConfigs.ContainsKey(typeId))
            {
                ItemData itemData = _itemConfigs[typeId];
                capabilities.SetCapabilities(itemData);
            }

            return capabilities;
        }

        public Sprite GetItemImage(ItemTypes itemId)
        {
            ItemData item = _itemConfigs[itemId];
            Sprite image = Resources.Load<Sprite>(item.Icon);

            return image;
        }

        public string GetPrefab(ItemTypes itemId)
        {
            ItemData item = _itemConfigs[itemId];

            return item.Prefab;
        }

        public float GetProductionTime(ItemTypes itemId)
        {
            ItemData item = _itemConfigs[itemId];

            return item.ProductionTime;
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
