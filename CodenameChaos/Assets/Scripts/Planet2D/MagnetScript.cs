using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float gravity = -10;
    public float effectionRadius = 10;
	public AnimationCurve GravityStrength;
    [HideInInspector] public Transform planetTransform;

    void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        AttractObjects();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }
    
    void AttractObjects()
    {
        List<Collider2D> AttractedObjects = Physics2D.OverlapCircleAll(planetTransform.position, effectionRadius, AttractionLayer).ToList();
        for (int i = 0; i < AttractedObjects.Count; i++)
        {
			if (AttractedObjects[i].gameObject.activeInHierarchy)
            {
                Magnetized magnetComponent = AttractedObjects[i].GetComponent<Magnetized>();
                if (magnetComponent != null)
                {
                    magnetComponent.Attract(this);
                    magnetComponent.RotateToCenter(this);
                }
			}
        }
    }
}
