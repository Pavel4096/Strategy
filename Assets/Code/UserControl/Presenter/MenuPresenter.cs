using Strategy.Abstractions;
using Zenject;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.Presenter
{
    public sealed class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        [Inject]
        private void Init()
        {
            _backButton.OnClickAsObservable().Subscribe((_) => gameObject.SetActive(false));
            _exitButton.OnClickAsObservable().Subscribe((_) => Application.Quit());
        }
    }
}
