// Creation Date: January 12 2022
// Author(s): Jordan Bejar

using UnityEngine;
using System;
using UnityEngine.Events;

namespace RageBall
{
    public class TimerState : State
    {
        float _t = 0f;

        // public 
        public float interval = 0f;
        public UnityEvent onInterval;

        /// <summary>
        /// Append value to the current timer (Can accept negative value)
        /// </summary>
        /// <param name="time"></param>
        public void AddTimer( float time ) => this._t += time;

        /// <summary>
        /// Retrieve current timer value
        /// </summary>
        /// <returns></returns>
        public float GetValue() => this._t;

#region interface implementation

        public override void Execute()
        {
            _t += Time.deltaTime;
            if( _t > interval ) 
            {
                onInterval?.Invoke();
                OnNextState();
            }
        }

#endregion
    }
}
