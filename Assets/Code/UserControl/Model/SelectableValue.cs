using Strategy.Abstractions;
using System;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    [CreateAssetMenu(menuName = "Game/Selectable value", fileName = "SelectableValue")]
    public sealed class SelectableValue : ValueBase<ISelectable>
    {}
}
