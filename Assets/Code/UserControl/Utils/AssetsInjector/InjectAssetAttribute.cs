using System;

namespace Strategy.UserControl.Utils.AssetsInjector
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InjectAssetAttribute : Attribute
    {
        public readonly string AssetName;

        public InjectAssetAttribute(string assetName)
        {
            AssetName = assetName;
        }
    }
}
