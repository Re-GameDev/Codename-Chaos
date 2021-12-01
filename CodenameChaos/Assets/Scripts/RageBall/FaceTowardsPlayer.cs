
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

        void Update()
        {
            if( _cam == null )
                return;
            transform.LookAt( _cam.position );
        }
    }
}
