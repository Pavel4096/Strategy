using Strategy.Abstractions;
using Strategy.UserControl.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.Presenter
{
    public sealed class CheckFor : ICheckFor
    {
        private Camera _camera;
        private int _groundLayer;
        private GameObject _prefab;
        private Sprite _icon;
        private float _productionTime;

        public CheckFor(Camera camera, int groundLayer)
        {
            _camera = camera;
            _groundLayer = groundLayer;
        }

        public bool Ground(out Vector3 position)
        {
            position = new Vector3(0.0f, 0.0f, 0.0f);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 <<_groundLayer))
            {
                position = hit.point;
                return true;
            }

            return false;
        }

        public bool Attackable(out IAttackable attackable)
        {
            attackable = null;
            if(GetHit(out RaycastHit hit))
            {
                attackable = hit.transform.GetComponentInParent<IAttackable>();
                if(attackable != null)
                    return true;
            }
            
            return false;
        }

        public bool MatterStorage(out Vector3 position)
        {
            position = default;
            if(GetHit(out RaycastHit hit))
            {
                IMatterStorage matterStorage = hit.transform.GetComponentInParent<IMatterStorage>();
                if(matterStorage != null)
                {
                    position = matterStorage.GetPosition();
                    return true;
                }
            }

            return false;
        }

        public bool GetItem(out GameObject prefab, out Sprite icon, out float productionTime)
        {
            prefab = _prefab;
            icon = _icon;
            productionTime = _productionTime;

            if(_prefab == null)
                return false;
            else
            {
                _prefab = null;
                _icon = null;
                _productionTime = 0;
                return true;
            }
        }

        public void SetItem(string prefab, Sprite icon, float productionTime)
        {
            _prefab = Resources.Load<GameObject>(prefab);
            _icon = icon;
            _productionTime = productionTime;
        }

        public bool GetHit(out RaycastHit hit)
        {
            hit = default;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
                return true;
            else
                return false;
        }
    }
}
