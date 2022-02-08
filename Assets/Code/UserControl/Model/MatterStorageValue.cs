using Strategy.Abstractions;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    [CreateAssetMenu(menuName = "Game/MatterStorage Value", fileName = "MatterStorageValue")]
    public sealed class MatterStorageValue : ValueBase<IMatterStorage>
    {}
}
