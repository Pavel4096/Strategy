using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Core
{
    internal sealed class UnitProducer : IUnitProducer
    {
        private Queue<GameObject> _unitsToProduce = new Queue<GameObject>();
        private Transform _parent;
        private bool _isProducing;
        private int _delay;

        public UnitProducer(Transform parent, int delay)
        {
            _parent = parent;
            _delay = delay;
        }

        public async void ProduceUnit(GameObject prefab)
        {
            _unitsToProduce.Enqueue(prefab);
            if(!_isProducing)
            {
                _isProducing = true;
                while(_unitsToProduce.Count > 0)
                {
                    await Task.Delay(_delay);
                    var currentPrefab = _unitsToProduce.Dequeue();
                    Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
                    GameObject.Instantiate(currentPrefab, position, Quaternion.identity, _parent);
                }

                _isProducing = false;
            }
        }
    }
}
