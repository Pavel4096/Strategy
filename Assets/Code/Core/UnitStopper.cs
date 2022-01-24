using Strategy.Abstractions;
using Strategy.UserControl.Utils;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy.Core
{
    public sealed class UnitStopper : MonoBehaviour, ICompletableValue<AwaiterExtensions.Void>, IAwaitable<AwaiterExtensions.Void>
    {
        public IAwaiter<AwaiterExtensions.Void> GetAwaiter()
        {
            return new Awaiter<AwaiterExtensions.Void>(this);
        }

        public event Action<AwaiterExtensions.Void> Completed;

        private NavMeshAgent meshAgent;

        private void Start()
        {
            meshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(meshAgent.pathPending || !meshAgent.enabled)
                return;
            
            if(meshAgent.remainingDistance < meshAgent.stoppingDistance)
                Completed?.Invoke(new AwaiterExtensions.Void());
        }
    }
}
