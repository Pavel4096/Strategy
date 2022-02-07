using Strategy.Common;
using Zenject;
using System.IO;
using UnityEngine;

namespace Strategy
{
    public sealed class Game : MonoBehaviour
    {
        [Inject] private ConfigData _configData;
        [Inject] private DiContainer _diContainer;
        private void Awake()
        {
            LoadUI();

            string configPath = Path.Combine(Application.dataPath, "ItemConfigs");
            _configData.Load(configPath);

            IEnvironmentItemLoader environmentItemLoader = new EnvironmentItemLoader();
            IItemLoader itemLoader = new ItemLoader(_diContainer);
            ILevelLoader levelLoader = new LevelLoader();

            levelLoader.AddEnvironmentItemLoader(environmentItemLoader);
            levelLoader.AddItemLoader(itemLoader);

            levelLoader.LoadLevel(0);



            string fileName = System.IO.Path.Combine(Application.dataPath, "ItemConfigs/Unit0.json");
            string itemConfigData = System.IO.File.ReadAllText(fileName);
            ItemData item = JsonUtility.FromJson<ItemData>(itemConfigData);
        }

        private void LoadUI()
        {
            GameObject uiPrefab = Resources.Load<GameObject>("UI");
            GameObject ui = Object.Instantiate(uiPrefab);
            _diContainer.InjectGameObject(ui);
        }
    }
}
