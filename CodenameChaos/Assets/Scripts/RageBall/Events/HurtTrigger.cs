using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RageBall
{

    [RequireComponent(typeof(Collider))]
    public class HurtTrigger : MonoBehaviour
    {
        [SerializeField] int damage = 5;
        [SerializeField] float perSecond = 0f;
        // [SerializeField] LayerMask mask; 
        List<Health> inTrigger = new List<Health>();
        float i = 0f;

        void Start()
        {
            if( this.TryGetComponent<Collider>(out Collider collider) )
                collider.isTrigger = true;
            else
            {
                Debug.LogWarning($"Gameobject {gameObject.name} does not have any collider!", gameObject);
                this.enabled = false;
            }
        }
        
        void OnTriggerEnter(Collider other )
        {
            if( TryGetComponent<Health>(out Health health ))
            {
                if( inTrigger.Contains( health ) )
                    return;

                // for now we'll just accept anything in.
                // if( !other.gameObject.layer.HasFlags( mask ) )
                // {
                    inTrigger.Add( health );
                // }
            }
        }

        void OnTriggerExit( Collider other )
        {
            if( TryGetComponent<Health>(out Health health ))
            {
                if( inTrigger.Contains( health ))
                    inTrigger.Remove( health );

                if( inTrigger.Count == 0 )
                    i = 0;
            }
        }
    
        void Update()
        {
            // skip if the array is empty
            if( inTrigger.Count == 0 )
                return;
            
            // increment time 
            i += Time.deltaTime;
            // clamp time 
            i = Mathf.Min( i, perSecond );
            // if we reach our per second threshold, damage anyone who's in the trigger
            if( i >= perSecond )
            {
                for( int i = inTrigger.Count - 1; i > 0; --i )
                {
                    // if the game object is still active in scene, continue to hurt.
                    if( inTrigger[i] != null && inTrigger[i].gameObject.activeInHierarchy )
                        inTrigger[i].TakeDamage( damage );
                    else
                    // otherwise remove from the list if it has been destroy or removed from scene
                        inTrigger.RemoveAt(i);
                }

                i = 0f;
            }
        }
    }
}
