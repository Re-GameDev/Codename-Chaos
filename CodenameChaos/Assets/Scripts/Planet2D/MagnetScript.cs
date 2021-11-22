using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public LayerMask BulletLayer;
    public float gravity = -10;
    public float effectionRadius = 10;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    public List<Collider2D> AttractedBullets = new List<Collider2D>();
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
        AttractedBullets = Physics2D.OverlapCircleAll(planetTransform.position, effectionRadius, BulletLayer).ToList();
    }

    void AttractObjects()
    {
        for (int i = 0; i < AttractedObjects.Count; i++)
        {
            AttractedObjects[i].GetComponent<Magnetized>().Attract(this);
        }
        for (int i = 0; i < AttractedBullets.Count; i++)
        {
            AttractedBullets[i].GetComponent<BulletControllerScript>().Attract(this);
        }
    }
}
