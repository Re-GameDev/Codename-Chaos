
using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{
    [RequireComponent(typeof(Camera)), DisallowMultipleComponent]
    public class CameraFocusAll : MonoBehaviour
    {
        HashSet<Transform> objectInFocus = new HashSet<Transform>();

        public void AddTargetToFocus( Transform target )
        {
            objectInFocus.Add( target );
        }

        public void RemoveTargetInFocus( Transform target )
        {
            if( objectInFocus.Contains( target ))
                objectInFocus.Remove(target);
        }

        void LateUpdate()
        {
            if( objectInFocus.Count == 0 )
                return;

            Vector3 target = Vector3.zero;
            foreach( var t in objectInFocus )
                target += t.position;
            target /= objectInFocus.Count;
            Camera.main.transform.LookAt( target );
        }
    }
}
