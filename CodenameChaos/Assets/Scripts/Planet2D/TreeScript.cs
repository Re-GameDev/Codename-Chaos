using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class TreeScript : MonoBehaviour
{
	public class Branch
	{
		public Branch parent;
		public float length;
		public float randSeed;
		public float angle;
        public float accumAngle;
		public float currentAngle;
		public GameObject obj;
		public GameObject seed;
		public float growth;
		public List<Branch> children;
	}
	
	public enum PlantVariant
	{
		Tree, Rice, Cactus, Mushroom, Kelp, Palmtree, Bush, Sunflower, Count
	}		
	
	Branch rootBranch;
	List<Branch> branches = new List<Branch>();
	
	public PlantVariant Variant = PlantVariant.Tree;
	public Sprite[] BranchSprites; //for each PlantVariant
	public LayerMask PlanetLayer;
	
	private void Awake()
	{
		List<Collider2D> closePlanets = Physics2D.OverlapCircleAll(transform.position, 100f, PlanetLayer).ToList();
        print($"Found {closePlanets.Count} planet(s) nearby!");
		float closestPlanetDist = 0;
		Collider2D closestPlanet = null;
		foreach (Collider2D planet in closePlanets)
		{
			float planetDist = (planet.gameObject.transform.position - transform.position).magnitude;
			if (closestPlanet == null || (planetDist < closestPlanetDist))
			{
				closestPlanet = planet;
				closestPlanetDist = planetDist;
			}
		}
		
        float rootAngle = 0;
		if (closestPlanet != null)
		{
            rootAngle = Mathf.Atan2(transform.position.y - closestPlanet.gameObject.transform.position.y, transform.position.x - closestPlanet.gameObject.transform.position.x) * Mathf.Rad2Deg;
            print($"Planet is at ({closestPlanet.transform.position.x}, {closestPlanet.transform.position.x}) we are at ({transform.position.x}, {transform.position.y}) angle is {rootAngle}");
        }

        rootBranch = AddBranch(null, rootAngle, Random.Range(4.0f, 6.0f));

        int numBranches = Random.Range(40, 120);
		for (int i = 0; i < numBranches; i++)
		{
			AddRandomBranch();
		}
		foreach(Branch branch in branches)
		{
			if (branch.children.Count == 0)
			{
				SpriteRenderer spriteRenderer = branch.obj.GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = BranchSprites[(int)Variant + 1];
				spriteRenderer.sortingOrder = 2;
				//AddLeaf(null, rootAngle, Random.Range(4.0f, 6.0f));
			}
		}
	}
	
	/*private void AddLeaf(Branch Parent, float rotation, float length)
	{		
		GameObject Leaf = new GameObject($"{((parent != null) ? (parent.obj.name + "_") : "")}Branch{parent?.children.Count ?? 0}");
		SpriteRenderer spriteRenderer = newBranch.obj.AddComponent<SpriteRenderer>();
		Assert.IsTrue((int)Variant < BranchSprites.Length);
		spriteRenderer.sprite = BranchSprites[(int)Variant];
		spriteRenderer.sortingOrder = 1;

        newBranch.obj.transform.parent = this.transform;
        newBranch.obj.transform.position = parent?.obj.transform.position ?? this.transform.position;
		float scale = newBranch.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * newBranch.growth;
		newBranch.obj.transform.localScale = new Vector3(scale, scale, scale);
		newBranch.obj.transform.rotation = Quaternion.AngleAxis(newBranch.angle - 90, Vector3.forward);
		
		branches.Add(newBranch); 
		if (parent != null) { parent.children.Add(newBranch); }
		
		return newBranch;
	}*/
	
	private void AddRandomBranch()
	{
		int branchIndex = Random.Range(0, branches.Count);
        float parentAngle = branches[branchIndex].accumAngle;
        float angleRange = 92.0f;
		float lowerBound = rootBranch.angle - angleRange;
		float upperBound = rootBranch.angle + angleRange;
		float lowerRand = parentAngle - angleRange;
		float upperRand = parentAngle + angleRange;
		print($"bound of {lowerBound} to {upperBound} with a rand of {lowerRand} to {upperRand}");
        if (lowerRand < lowerBound) { lowerRand = Mathf.Abs(lowerRand - lowerBound) - angleRange; }
		else {lowerRand = -angleRange;}
        if (upperRand > upperBound) { upperRand = angleRange - Mathf.Abs(upperRand - upperBound); }
		else {upperRand = angleRange;}
		float newAngle = Random.Range(lowerRand, upperRand);
		print($"submitting {lowerRand} to {upperRand} result {newAngle}");
		if (newAngle > 180.0f) { newAngle = newAngle - 360.0f;}
		if (newAngle < -180.0f) { newAngle = 360.0f - newAngle;}
        AddBranch(branches[branchIndex], newAngle, Random.Range(1.0f,4.0f));
	}
	
	private Branch AddBranch(Branch parent, float rotation, float length)
	{
        if (parent != null) { Assert.IsNotNull(parent.obj); }
		Branch newBranch = new Branch();
		newBranch.parent = parent;
		newBranch.seed = null;
		newBranch.length = length;
		newBranch.randSeed = Random.Range(0.0f, 1.0f);
		newBranch.angle = rotation;
        newBranch.accumAngle = (parent?.accumAngle ?? 0) + rotation;
		newBranch.currentAngle = newBranch.angle;
		newBranch.growth = 0;
		newBranch.children = new List<Branch>();
		
		newBranch.obj = new GameObject($"{((parent != null) ? (parent.obj.name + "_") : "")}Branch{parent?.children.Count ?? 0}");
		SpriteRenderer spriteRenderer = newBranch.obj.AddComponent<SpriteRenderer>();
		Assert.IsTrue((int)Variant < BranchSprites.Length);
		spriteRenderer.sprite = BranchSprites[(int)Variant];
		spriteRenderer.sortingOrder = 1;

        newBranch.obj.transform.parent = this.transform;
        newBranch.obj.transform.position = parent?.obj.transform.position ?? this.transform.position;
		float scale = newBranch.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * newBranch.growth;
		newBranch.obj.transform.localScale = new Vector3(scale, scale, scale);
		newBranch.obj.transform.rotation = Quaternion.AngleAxis(newBranch.angle - 90, Vector3.forward);
		
		branches.Add(newBranch); 
		if (parent != null) { parent.children.Add(newBranch); }
		
		return newBranch;
	}
	
	void Update()
	{
		foreach(Branch branch in branches)
		{
			if (branch.growth < 1 && (branch.parent == null || branch.parent.growth > 0.95f))
			{
				branch.growth += (0.002f / branch.length);
			}
			SpriteRenderer spriteRenderer = branch.obj.GetComponent<SpriteRenderer>();
			float scale = branch.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * branch.growth;
			branch.obj.transform.localScale = new Vector3(scale, scale, scale);
			if (branch.parent == null)
			{
				float swayAmount = Mathf.Sin(Time.time * Mathf.PI * 2 / (2 + 1 * branch.randSeed) + branch.randSeed * Mathf.PI * 2) * 2;
				branch.currentAngle = branch.angle + swayAmount;
				branch.obj.transform.rotation = Quaternion.AngleAxis(branch.currentAngle - 90, Vector3.forward);
			}
			foreach(Branch child in branch.children)
			{
				child.obj.transform.position = branch.obj.transform.position + (branch.obj.transform.up * branch.length * branch.growth);
				float swayAmount = Mathf.Sin(Time.time * Mathf.PI * 2 / (5 + 4 * child.randSeed) + child.randSeed * Mathf.PI * 2) * 4;
				child.currentAngle = branch.currentAngle + child.angle + swayAmount;
				child.obj.transform.rotation = Quaternion.AngleAxis(child.currentAngle - 90, Vector3.forward);
			}
		}
	}
}
