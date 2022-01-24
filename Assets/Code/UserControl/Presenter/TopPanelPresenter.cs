using Strategy.Abstractions;
using Zenject;
using UniRx;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.Presenter
{
    public sealed class TopPanelPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _time;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _menuGameObject;

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe( seconds => {
                var time = TimeSpan.FromSeconds(seconds);
                _time.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
            });

            _menuButton.OnClickAsObservable().Subscribe((_) => _menuGameObject.SetActive(true));
        }
    }
}
