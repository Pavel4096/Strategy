using System;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Strategy.UserControl.Utils.AssetsInjector
{
    public static class AssetInjector
    {
        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);
        public static T InjectAsset<T>(this AssetsContext context, T target)
        {
            Type targetType = target.GetType();

            while(targetType != null)
            {
                FieldInfo[] fields = targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach(FieldInfo field in fields)
                {
                    InjectAssetAttribute injectAttribute = field.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
                    if(injectAttribute == null)
                        continue;
                    
                    Object objectToInject = context.GetObjectOfType(field.FieldType, injectAttribute.AssetName);
                    field.SetValue(target, objectToInject);
                }

                targetType = targetType.BaseType;
            }

            return target;
        }
    }
}
