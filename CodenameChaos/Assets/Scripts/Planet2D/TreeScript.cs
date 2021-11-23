using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
	public class Branch
	{
		public Branch parent;
		public float length;
		public float randSeed;
		public float angle;
		public float currentAngle;
		public GameObject obj;
		public GameObject seed;
		public float growth;
		public List<Branch> children;
	}
    
	List<Branch> branches = new List<Branch>();
	
	public enum typeOfPlant
	{
		tree, rice, cactus, mushroom, kelp, palmtree, bush, sunflower, count
	}		
	
	public typeOfPlant plantType = typeOfPlant.tree;
	public Sprite[] branchSprites;
	
	private void Awake()
	{
		Branch rootBranch = AddBranch(null, 0, 5);
		for (int i = 0; i < 100; i++)
		{
			AddRandomBranch();
		}
	}
	
	private void AddRandomBranch()
	{
		int branchIndex = Random.Range(0, branches.Count);
		AddBranch(branches[branchIndex], Random.Range(-60,60), Random.Range(2.0f,7.0f));
	}
	
	private Branch AddBranch(Branch parent, float rotation, float length)
	{
		Branch NewBranch = new Branch();
		NewBranch.parent = parent;
		NewBranch.seed = null;
		NewBranch.length = length;
		NewBranch.randSeed = Random.Range(0.0f,1.0f);
		NewBranch.angle = rotation;
		NewBranch.currentAngle = NewBranch.angle;
		NewBranch.growth = 0;
		NewBranch.children = new List<Branch>();
		NewBranch.obj = new GameObject("branch");
		SpriteRenderer branchSpr = NewBranch.obj.AddComponent<SpriteRenderer>();
		branchSpr.sprite = branchSprites[(int)plantType];
		branchSpr.sortingOrder = 1;
		NewBranch.obj.transform.position = parent?.obj.transform.position??this.transform.position;
		//NewBranch.obj.transform.parent = this.transform;
		float tempFlt = NewBranch.length/(branchSpr.sprite.bounds.size.y - 0.3f) * NewBranch.growth;
		//print(branchSpr.sprite.bounds.size.y);
		NewBranch.obj.transform.localScale = new Vector3(tempFlt, tempFlt, tempFlt);
		NewBranch.obj.transform.rotation = Quaternion.AngleAxis(NewBranch.angle, Vector3.forward);
		
		branches.Add(NewBranch); 
		if (parent != null)
		{
			parent.children.Add(NewBranch);
		}
		
		return NewBranch;
	}
	
	void Update()
	{
		foreach(Branch branch in branches)
		{
			if (branch.growth < 1 && (branch.parent == null || branch.parent.growth > 0.95f))
			{
				branch.growth += 0.0001f;
			}
			SpriteRenderer branchSpr = branch.obj.GetComponent<SpriteRenderer>();
			float tempFlt = branch.length/(branchSpr.sprite.bounds.size.y - 0.3f) * branch.growth;
			branch.obj.transform.localScale = new Vector3(tempFlt, tempFlt, tempFlt);
			if (branch.parent == null)
			{
				branch.currentAngle = branch.angle + Mathf.Sin(Time.time * Mathf.PI * 2 / (2 + 1 * branch.randSeed) + branch.randSeed * Mathf.PI * 2) * 2;
				branch.obj.transform.rotation = Quaternion.AngleAxis(branch.currentAngle, Vector3.forward);
			}
			foreach(Branch child in branch.children)
			{
				child.obj.transform.position = branch.obj.transform.position + (branch.obj.transform.up * branch.length * branch.growth);
				child.currentAngle = branch.currentAngle + child.angle + Mathf.Sin(Time.time * Mathf.PI * 2 / (5 + 4 * child.randSeed) + child.randSeed * Mathf.PI * 2) * 4;
				child.obj.transform.rotation = Quaternion.AngleAxis(child.currentAngle, Vector3.forward);
			}
		}
	}
}
