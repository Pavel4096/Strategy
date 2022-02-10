using Strategy.Abstractions.Commands;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.View
{
    public sealed class CommandButtonsView : MonoBehaviour
    {
        public event Action<ICommandExecutor> CommandSelected;

        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _produceUnitButton;
        [SerializeField] private GameObject _stopButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _bringMatterButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;

        private ICommandExecutor _defaultCommandExecutor;

        private void Awake()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>();
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IBringMatterCommand>), _bringMatterButton);
        }

        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors)
        {
            foreach(ICommandExecutor currentExecutor in commandExecutors)
            {
                GameObject currentButtonObject = null;
                foreach(var currentButton in _buttonsByExecutorType)
                {
                    if(currentButton.Key.IsInstanceOfType(currentExecutor))
                    {
                        currentButtonObject = currentButton.Value;
                        break;
                    }
                }
                if(currentExecutor is CommandExecutorBase<IMoveCommand>)
                {
                    _defaultCommandExecutor = currentExecutor;
                    EnableDefaultCommand();
                }
                else
                {
                    currentButtonObject.SetActive(true);
                    Button button = currentButtonObject.GetComponent<Button>();
                    button.onClick.AddListener(() => CommandSelected?.Invoke(currentExecutor));
                }
            }
        }

        public void BlockInteraction(ICommandExecutor executor)
        {
            UnblockAllInteractions();
            GetButtonObjectByType(executor.GetType()).GetComponent<Selectable>().interactable = false;
        }

        public void UnblockAllInteractions()
        {
            _attackButton.GetComponent<Selectable>().interactable = true;
            _produceUnitButton.GetComponent<Selectable>().interactable = true;
            _stopButton.GetComponent<Selectable>().interactable = true;
            _patrolButton.GetComponent<Selectable>().interactable = true;
            _moveButton.GetComponent<Selectable>().interactable = true;
            _bringMatterButton.GetComponent<Selectable>().interactable = true;
        }

        public void EnableDefaultCommand()
        {
            //if(_defaultCommandExecutor != null)
            //    CommandSelected?.Invoke(_defaultCommandExecutor);
        }

        public void Clear()
        {
            foreach(var currentButton in _buttonsByExecutorType)
            {
                currentButton.Value.SetActive(false);
                Button button = currentButton.Value.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
            }
            _defaultCommandExecutor = null;
        }

        private GameObject GetButtonObjectByType(Type executorType)
        {
            foreach(KeyValuePair<Type, GameObject> currentButton in _buttonsByExecutorType)
            {
                if(currentButton.Key.IsAssignableFrom(executorType))
                    return currentButton.Value;
            }

            return null;
        }
    }
}
