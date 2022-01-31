using Strategy.Abstractions;
using System;
using TMPro;
using UniRx;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.View
{
    public sealed class UICenterView : MonoBehaviour
    {
        public IObservable<int> ButtonClicks => _buttonClicks;

        [SerializeField] private GameObject _progressSliderObject;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _name;


        [SerializeField] private GameObject[] _buttonObjects;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private Image[] _icons;

        private Subject<int> _buttonClicks = new Subject<int>();
        private IDisposable _updateDisposable;

        [Inject]
        private void Init()
        {
            for(var i = 0; i < _buttons.Length; i++)
            {
                var index = i;
                _buttons[i].onClick.AddListener(() => _buttonClicks.OnNext(index));
            }
        }

        public void Clear()
        {
            _progressSliderObject.SetActive(false);
            _name.enabled = false;

            for(var i = 0; i < _buttonObjects.Length; i++)
            {
                _buttonObjects[i].SetActive(false);
            }

            _updateDisposable?.Dispose();
        }

        public void SetItem(IUnitProductionTask task, int index)
        {
            if(task == null)
            {
                _buttonObjects[index].SetActive(false);

                if(index == 0)
                {
                    _progressSliderObject.SetActive(false);
                    _name.enabled = false;
                    _updateDisposable?.Dispose();
                }
            }
            else
            {
                _buttonObjects[index].SetActive(true);
                _icons[index].sprite = task.Icon;

                if(index == 0)
                {
                    _progressSliderObject.SetActive(true);
                    _name.text = task.Name;
                    _name.enabled = true;
                    _updateDisposable = Observable.EveryUpdate().Subscribe((_) =>
                        _progressSlider.value = task.TimeLeft / task.ProductionTime
                    );
                }
            }
        }
    }
}
