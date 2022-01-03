using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using UnityEngine;

namespace Strategy.UserControl.Commands
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        public GameObject UnitPrefab => _unitPrefab;

        [InjectAsset(CommandConstants.Unit0)] private GameObject _unitPrefab;
    }
}
