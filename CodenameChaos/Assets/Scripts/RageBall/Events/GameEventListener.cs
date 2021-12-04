using UnityEngine;
using UnityEngine.Events;
using System;

namespace RageBall
{
    // wonder why I couldn't just make this a abstract class? 
    public abstract class GameEventListener : MonoBehaviour 
    {
        [SerializeField] GameEvent gameEvent;

        void Awake() => gameEvent?.Register( this );

        void OnDestroy() => gameEvent?.Deregister( this );

        public abstract void RaiseEvent();
    }
}