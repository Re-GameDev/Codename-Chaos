using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float gravity = -10;
    public float effectionRadius = 10;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
	public AnimationCurve GravityStrength;
    [HideInInspector] public Transform planetTransform;

    void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SetAttractedObjects();
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

    void SetAttractedObjects()
    {
        AttractedObjects = Physics2D.OverlapCircleAll(planetTransform.position, effectionRadius, AttractionLayer).ToList();
        for (int i = 0; i < AttractedObjects.Count; i++)
        {
			if (AttractedObjects[i].gameObject.activeInHierarchy)
			{
				AttractedObjects[i].GetComponent<Magnetized>()?.RotateToCenter(this);
			}
        }
    }

    void AttractObjects()
    {
        for (int i = 0; i < AttractedObjects.Count; i++)
        {
			if (AttractedObjects[i].gameObject.activeInHierarchy)
			{
				AttractedObjects[i].GetComponent<Magnetized>()?.Attract(this);
			}
        }
    }
}
