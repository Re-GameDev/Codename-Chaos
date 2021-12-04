using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{
    [CreateAssetMenu(menuName= "Game Event", fileName="GameEvent")]
    public class GameEvent : ScriptableObject
    {
        HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

        public void Invoke()
        {
            foreach( var globalEventListener in listeners )
                globalEventListener.RaiseEvent();
        }

        public void Register( GameEventListener gameEventListener ) => listeners.Add( gameEventListener );

        public void Deregister ( GameEventListener gameEventListener ) => listeners.Remove( gameEventListener );
    }
}
