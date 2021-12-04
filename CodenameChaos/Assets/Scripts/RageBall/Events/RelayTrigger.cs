
using UnityEngine;
using UnityEngine.Events;

namespace RageBall
{
    public class RelayTrigger : MonoBehaviour
    {
        [SerializeField] UnityEvent OnFired;
        [SerializeField] UnityEvent OnDelayFired;
        [SerializeField] float delay = 0f;

        public void Fire()
        {
            OnFired?.Invoke();
            Invoke( nameof( DelayFire ), delay );
        }

        void DelayFire() => OnDelayFired?.Invoke();
    }
}
