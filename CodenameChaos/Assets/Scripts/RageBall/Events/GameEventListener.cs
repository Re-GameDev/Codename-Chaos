using UnityEngine;
using UnityEngine.Events;
using System;

namespace RageBall
{
    public class GameEventListener : MonoBehaviour 
    {
        [SerializeField] GameEvent gameEvent;
        [SerializeField] UnityEvent unityEvent;

        void Awake() => gameEvent.Register( this );

        void OnDestroy() => gameEvent.Deregister( this );

        public virtual void RaiseEvent() => unityEvent.Invoke();
    }
}