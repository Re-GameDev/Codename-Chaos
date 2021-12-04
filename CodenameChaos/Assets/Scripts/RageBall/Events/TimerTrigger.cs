using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TimerEvent : UnityEvent { }

namespace RageBall
{
    public class TimerTrigger : MonoBehaviour
    {
        [SerializeField] TimerEvent OnTimer;
        [SerializeField] bool playOnStart = true;
        [SerializeField] bool repeat = true;
        [SerializeField] float interval = 1f;
        bool isPlaying = false;
        float i;

        // Start is called before the first frame update
        void Start()
        {
            if( playOnStart )
                isPlaying = true;
            i = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if( isPlaying )
            {
                i += Time.deltaTime;
                i = Mathf.Min( i, interval );
                if( interval <= i )
                {
                    OnTimer?.Invoke();
                    if( !repeat )
                        isPlaying = false;    
                    i = 0f;
                }   
            }
        }

        public void Stop()
        {
            isPlaying = false;
            i = 0f;
        }

        public void Play()
        {
            isPlaying = true;
        }

        public void Reset()
        {
            i = 0f;
        }
    }
}
