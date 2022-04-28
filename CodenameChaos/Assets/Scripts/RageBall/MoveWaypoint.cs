using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{
    [DisallowMultipleComponent]
    public class MoveWaypoint : MonoBehaviour
    {
        [SerializeField] private float smoothDamp = 1f;
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private bool showDebug = false;

        private Transform _target;
        private Vector3 _velocity = Vector3.zero;
        private int _position;

        private void Start()
        {
            _position = 0;
            if (waypoints.Count == 0)
                this.enabled = false;
        }

        private void OnDrawGizmos()
        {
            if (!showDebug || waypoints.Count == 0)
                return;

            Gizmos.DrawWireCube(waypoints[0].position, Vector3.one);
            for (int i = 1, c = waypoints.Count; i < c; ++i)
            {
                Gizmos.DrawWireCube(waypoints[i].position, Vector3.one);
                Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
            }
        }

        public void MoveNextWaypoint()
        {
            _position = Mathf.Min(++_position, waypoints.Count - 1);
            _target = waypoints[_position];
        }

        public void MovePreviousWaypoint()
        {
            _position = Mathf.Max(--_position, 0);
            _target = waypoints[_position];
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;
            this.transform.position =
                Vector3.SmoothDamp(transform.position, _target.position, ref _velocity, smoothDamp);
        }
    }
}