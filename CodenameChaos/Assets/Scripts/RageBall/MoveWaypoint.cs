using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{
    [DisallowMultipleComponent]
    public class MoveWaypoint : MonoBehaviour
    {
        [SerializeField] float smoothDamp = 1f;
        [SerializeField] List<Transform> waypoints = new List<Transform>();
        [SerializeField] bool showDebug = false;
        Transform _target = null;
        Vector3 velocity = Vector3.zero;
        int position;
        
        void Start()
        {
            position = 0;
            if( waypoints.Count == 0 )
                this.enabled = false;    
        }

        void OnDrawGizmos()
        {
            if( !showDebug || waypoints.Count == 0 )  
                return;

            Gizmos.DrawWireCube( waypoints[0].position, Vector3.one );
            for( int i = 1, c = waypoints.Count; i<c; ++i )
            {
                Gizmos.DrawWireCube( waypoints[i].position, Vector3.one );
                Gizmos.DrawLine( waypoints[i-1].position, waypoints[i].position );
            }
        }

        public void MoveNextWaypoint()
        {
            position = Mathf.Min( position, waypoints.Count - 1 );
            _target = waypoints[position];
        }

        public void MovePreviousWaypoint()
        {
            position = Mathf.Max( position, 0 );
            _target = waypoints[position];
        }

        void FixedUpdate()
        {
            if( _target == null )
                return;
            this.transform.position = Vector3.SmoothDamp( transform.position, _target.position, ref velocity, smoothDamp );
        }
    }
}
