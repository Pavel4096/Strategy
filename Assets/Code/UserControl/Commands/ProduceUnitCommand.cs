using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using UnityEngine;

namespace Strategy.UserControl.Commands
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        public GameObject UnitPrefab => _unitPrefab;
        public Sprite Icon => _icon;
        public string Name => _name;
        public float ProductionTime => _productionTime;

        [InjectAsset(CommandConstants.Unit0)] private GameObject _unitPrefab;
        [Inject(Id = "Chomper")] private Sprite _icon;
        [Inject(Id = "Chomper")] private string _name;
        [Inject(Id = "Chomper")] private float _productionTime;

        public ProduceUnitCommand()
        {}

        public ProduceUnitCommand(GameObject prefab, Sprite icon, float productionTime)
        {
            _unitPrefab = prefab;
            _icon = icon;
            _productionTime = productionTime;
        }
    }
}
