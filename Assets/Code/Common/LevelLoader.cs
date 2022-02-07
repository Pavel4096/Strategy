using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy.Common
{
    public sealed class LevelLoader : ILevelLoader
    {
        private List<IEnvironmentItemLoader> _environmentLoaders = new List<IEnvironmentItemLoader>();
        private List<IItemLoader> _itemLoaders = new List<IItemLoader>();

        public void LoadLevel(int index)
        {
            LevelObject levelObject = Object.FindObjectOfType<LevelObject>();
            if(levelObject != null)
                Object.Destroy(levelObject.gameObject);

            string fileName = Path.Combine(Application.dataPath, $"Levels/Level{index}.json");

            if(!File.Exists(fileName))
            {
                Debug.Log($"Cann't fild level file '{fileName}'");
                return;
            }

            string levelData = File.ReadAllText(fileName);
            Level currentLevel = JsonUtility.FromJson<Level>(levelData);
            levelObject = new GameObject("Level").AddComponent<LevelObject>();
            levelObject.Name = currentLevel.Name;
            
            GameObject ground = new GameObject("Ground");
            ground.transform.SetParent(levelObject.gameObject.transform);
            GameObject decorations = new GameObject("Decorations");
            decorations.transform.SetParent(levelObject.gameObject.transform);
            GameObject buildings = new GameObject("Buildings");
            buildings.transform.SetParent(levelObject.gameObject.transform);
            GameObject units = new GameObject("Units");
            units.transform.SetParent(levelObject.gameObject.transform);

            foreach(IItemLoader loader in _itemLoaders)
                loader.SetUnitsParent(units.transform);

            foreach(EnvironmentItem currentItem in currentLevel.Ground)
            {
                var i = 0;
                for(i = 0; i < _environmentLoaders.Count; i++)
                {
                    if(_environmentLoaders[i].TryLoadEnvironmentItem(currentItem, ground.transform, true))
                        break;
                }

                if(i == _environmentLoaders.Count)
                    Debug.Log($"Cann't load item '{currentItem.Prefab}'");
            }

            foreach(EnvironmentItem currentItem in currentLevel.Decorations)
            {
                var i = 0;
                for(i = 0; i < _environmentLoaders.Count; i++)
                {
                    if(_environmentLoaders[i].TryLoadEnvironmentItem(currentItem, decorations.transform, false))
                        break;
                }

                if(i == _environmentLoaders.Count)
                    Debug.Log($"Cann't load item '{currentItem.Prefab}'");
            }

            foreach(SavedLevelItem currentItem in currentLevel.Buildings)
            {
                var i = 0;
                for(i = 0; i < _itemLoaders.Count; i++)
                {
                    if(_itemLoaders[i].TryLoadItem(currentItem, buildings.transform, true))
                        break;
                }

                if(i == _itemLoaders.Count)
                    Debug.Log($"Cann't load item '{currentItem.Prefab}'");
            }

            foreach(SavedLevelItem currentItem in currentLevel.Units)
            {
                var i = 0;
                for(i = 0; i < _itemLoaders.Count; i++)
                {
                    if(_itemLoaders[i].TryLoadItem(currentItem, units.transform, false))
                        break;
                }

                if(i == _itemLoaders.Count)
                    Debug.Log($"Cann't load item '{currentItem.Prefab}'");
            }
        }

        public void AddEnvironmentItemLoader(IEnvironmentItemLoader loader)
        {
            if(!_environmentLoaders.Contains(loader))
                _environmentLoaders.Add(loader);
        }

        public void AddItemLoader(IItemLoader loader)
        {
            if(!_itemLoaders.Contains(loader))
                _itemLoaders.Add(loader);
        }
    }
}
