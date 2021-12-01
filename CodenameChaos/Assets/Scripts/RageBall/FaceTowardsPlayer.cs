
using UnityEngine;

namespace RageBall
{
    public class FaceTowardsPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        Transform _cam;

        void Start()
        {
            _cam = Camera.main?.transform ?? FindObjectOfType<Camera>().transform;
        }

        void LateUpdate()
        {
            if( _cam == null )
                return;
            Vector3 inverseDir = 2 * transform.position - _cam.position;
            transform.LookAt( inverseDir );
        }
    }
}
