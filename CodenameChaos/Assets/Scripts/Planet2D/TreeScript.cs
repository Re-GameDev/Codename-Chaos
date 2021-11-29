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
		public int whereInList;
		public int whatPartAmI;
		public int howFarIveCome;
	}
	
	public enum PlantVariant //Tree, Rice, Cactus, Mushroom, Kelp, Palmtree, Bush, Sunflower, Count
	{
		TreeTrunk, TreeBranch, TreeLeaf, TreeFruit, Count
	}		
	
	Branch rootTrunk;
	List<Branch> branches = new List<Branch>();
	
	public PlantVariant Variant = PlantVariant.TreeTrunk;
	public Sprite[] BranchSprites; //for each PlantVariant
	public LayerMask PlanetLayer;
	public LayerMask PlantLayer;
    public float growRadius = 10;
	int numberOfBranches = 0;
	bool luckySuperPlant = false;
	bool fullyGrown = false;
    float PlantLifeSpan = 1200;
	float MaxLife;
	int CurrentlyWatered = 0;
	
	private void Awake()
	{
		MaxLife = PlantLifeSpan;
		List<Collider2D> closePlanets = Physics2D.OverlapCircleAll(transform.position, 100f, PlanetLayer).ToList();
        //print($"Found {closePlanets.Count} planet(s) nearby!");
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
            //print($"Planet is at ({closestPlanet.transform.position.x}, {closestPlanet.transform.position.x}) we are at ({transform.position.x}, {transform.position.y}) angle is {rootAngle}");
        }
		
	
		List<Collider2D> PlantsInRange = Physics2D.OverlapCircleAll(transform.position, growRadius, PlantLayer).ToList();

		if (PlantsInRange.Count > 0)
		{
			//print($"found {PlantsInRange.Count} plants nearby so I cannot grow!");
			Destroy(gameObject);
			return;
		}
		
		//print("starting a new tree");
        int numBranches = Random.Range(5, 15);
		if (Random.Range(0.0f,100.0f) > 95.0f)
		{
			numBranches = Random.Range(30, 120);
			luckySuperPlant = true;
			print($"super lucky number {luckySuperPlant}!");
		}
		
		//Creating the Base for the plant
		if (luckySuperPlant)
		{
			rootTrunk = AddTrunk(null, rootAngle, Random.Range(5.0f, 7.0f));
			
		}
		else
		{
			rootTrunk = AddTrunk(null, rootAngle, Random.Range(4.0f, 5.0f));
		}

		//print("adding branches");
		for (int i = 0; i < numBranches; i++)
		{
			AddRandomBranch();
		}
		
		//print("Leafing it up");
		int currentNumberOfBranches = numberOfBranches;
		for(int i = 0; i < currentNumberOfBranches; i++)
		{
			if (branches[i].children.Count == 0)
			{
				//print($"the branch value is {branch.whereInList}");
				AddNewLeaf(branches[i], Random.Range(-20.0f,20.0f), Random.Range(branches[i].length * 0.6f, branches[i].length * 0.8f));
			}
		}
		
		//print("Fruit!");
		currentNumberOfBranches = numberOfBranches;
		for(int i = 0; i < currentNumberOfBranches; i++)
		{
			if (branches[i].children.Count == 0)
			{
				//print($"the branch value is {branch.whereInList}");
				float HangingDown = rootTrunk.currentAngle - branches[i].accumAngle;
				AddNewFruit(branches[i], HangingDown, Random.Range(branches[i].length * 0.4f, branches[i].length * 0.6f));
			}
		}
		
	}

	void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, growRadius);
    }

	private Branch AddNewFruit(Branch parent, float rotation, float length)
	{
        if (parent != null) { Assert.IsNotNull(parent.obj); }
		Branch newFruit = new Branch();
		newFruit.whereInList = numberOfBranches;
		newFruit.parent = parent;
		newFruit.seed = null;
		newFruit.length = length;
		newFruit.randSeed = Random.Range(0.0f, 1.0f);
		newFruit.angle = rotation;
        newFruit.accumAngle = (parent?.accumAngle ?? 0) + rotation;
		newFruit.currentAngle = newFruit.angle;
		newFruit.growth = 0;
		newFruit.children = new List<Branch>();
		
		newFruit.obj = new GameObject($"{((parent != null) ? (parent.obj.name + "_") : "")}Branch{parent?.children.Count ?? 0}");
		SpriteRenderer spriteRenderer = newFruit.obj.AddComponent<SpriteRenderer>();
		newFruit.whatPartAmI = (int)Variant + 3;
		Assert.IsTrue(newFruit.whatPartAmI < BranchSprites.Length);
		spriteRenderer.sprite = BranchSprites[newFruit.whatPartAmI];
		spriteRenderer.sortingOrder = 2;
		newFruit.obj.layer = 10;

		CircleCollider2D circleCollider = newFruit.obj.AddComponent<CircleCollider2D>();
		circleCollider.offset = new Vector2(0, spriteRenderer.sprite.bounds.size.y/2);
		circleCollider.radius = spriteRenderer.sprite.bounds.size.x/2;
		circleCollider.isTrigger = true;

        newFruit.obj.transform.parent = this.transform;
        newFruit.obj.transform.position = parent.obj.transform.position;
		float scale = newFruit.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * newFruit.growth;
		newFruit.obj.transform.localScale = new Vector3(scale, scale, scale);
		newFruit.obj.transform.rotation = Quaternion.AngleAxis(newFruit.angle - 90, Vector3.forward);
		
		parent.children.Add(newFruit);
		newFruit.howFarIveCome = parent.howFarIveCome + 1; 
		spriteRenderer.sortingOrder = newFruit.howFarIveCome + 10;
		
		branches.Add(newFruit); 
		
		numberOfBranches++;
		
		return newFruit;
	}

	private Branch AddNewLeaf(Branch parent, float rotation, float length)
	{
        if (parent != null) { Assert.IsNotNull(parent.obj); }
		Branch newLeaf = new Branch();
		newLeaf.whereInList = numberOfBranches;
		newLeaf.parent = parent;
		newLeaf.seed = null;
		newLeaf.length = length;
		newLeaf.randSeed = Random.Range(0.0f, 1.0f);
		newLeaf.angle = rotation;
        newLeaf.accumAngle = (parent?.accumAngle ?? 0) + rotation;
		newLeaf.currentAngle = newLeaf.angle;
		newLeaf.growth = 0;
		newLeaf.children = new List<Branch>();
		
		newLeaf.obj = new GameObject($"{((parent != null) ? (parent.obj.name + "_") : "")}Branch{parent?.children.Count ?? 0}");
		SpriteRenderer spriteRenderer = newLeaf.obj.AddComponent<SpriteRenderer>();
		newLeaf.whatPartAmI = (int)Variant + 2;
		Assert.IsTrue(newLeaf.whatPartAmI < BranchSprites.Length);
		spriteRenderer.sprite = BranchSprites[newLeaf.whatPartAmI];

        newLeaf.obj.transform.parent = this.transform;
        newLeaf.obj.transform.position = parent.obj.transform.position;
		float scale = newLeaf.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * newLeaf.growth;
		newLeaf.obj.transform.localScale = new Vector3(scale, scale, scale);
		newLeaf.obj.transform.rotation = Quaternion.AngleAxis(newLeaf.angle - 90, Vector3.forward);
		
		parent.children.Add(newLeaf); 
		newLeaf.howFarIveCome = parent.howFarIveCome + 1;
		spriteRenderer.sortingOrder = newLeaf.howFarIveCome + 10;
		
		branches.Add(newLeaf); 
		
		numberOfBranches++;
		
		return newLeaf;
	}
	
	private void AddRandomBranch()
	{
		int branchIndex = Random.Range(0, branches.Count);
        float parentAngle = branches[branchIndex].accumAngle;
        float angleRange = 92.0f;
		float lowerBound = rootTrunk.angle - angleRange;
		float upperBound = rootTrunk.angle + angleRange;
		float lowerRand = parentAngle - angleRange;
		float upperRand = parentAngle + angleRange;
		//print($"bound of {lowerBound} to {upperBound} with a rand of {lowerRand} to {upperRand}");
        if (lowerRand < lowerBound) { lowerRand = Mathf.Abs(lowerRand - lowerBound) - angleRange; }
		else {lowerRand = -angleRange;}
        if (upperRand > upperBound) { upperRand = angleRange - Mathf.Abs(upperRand - upperBound); }
		else {upperRand = angleRange;}
		float newAngle = Random.Range(lowerRand, upperRand);
		//print($"submitting {lowerRand} to {upperRand} result {newAngle}");
		if (newAngle > 180.0f) { newAngle = newAngle - 360.0f;}
		if (newAngle < -180.0f) { newAngle = 360.0f - newAngle;}
		float smallestSize = (branches[branchIndex].length - 1);
		if (smallestSize < 1) {smallestSize = 1;}
        AddBranch(branches[branchIndex], newAngle, Random.Range(smallestSize, branches[branchIndex].length));
	}
	
	private Branch AddBranch(Branch parent, float rotation, float length)
	{
		if (parent != null) { Assert.IsNotNull(parent.obj); }
		Branch newBranch = new Branch();
		newBranch.whereInList = numberOfBranches;
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
		newBranch.whatPartAmI = (int)Variant + 1;
		Assert.IsTrue(newBranch.whatPartAmI < BranchSprites.Length);
		spriteRenderer.sprite = BranchSprites[newBranch.whatPartAmI];

        newBranch.obj.transform.parent = this.transform;
        newBranch.obj.transform.position = parent?.obj.transform.position ?? this.transform.position;
		float scale = newBranch.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * newBranch.growth;
		newBranch.obj.transform.localScale = new Vector3(scale, scale, scale);
		newBranch.obj.transform.rotation = Quaternion.AngleAxis(newBranch.angle - 90, Vector3.forward);
		
		parent.children.Add(newBranch);
		newBranch.howFarIveCome = parent.howFarIveCome + 1;
		spriteRenderer.sortingOrder = newBranch.howFarIveCome;
		
		branches.Add(newBranch); 
		
		numberOfBranches++;
		
		return newBranch;
	}
	
	private Branch AddTrunk(Branch parent, float rotation, float length)
	{
		Branch newTrunk = new Branch();
		newTrunk.whereInList = numberOfBranches;
		newTrunk.parent = parent;
		newTrunk.seed = null;
		newTrunk.length = length;
		newTrunk.randSeed = Random.Range(0.0f, 1.0f);
		newTrunk.angle = rotation;
        newTrunk.accumAngle = 0 + rotation;
		newTrunk.currentAngle = newTrunk.angle;
		newTrunk.growth = 0;
		newTrunk.children = new List<Branch>();
		
		newTrunk.obj = new GameObject($"{((parent != null) ? (parent.obj.name + "_") : "")}Branch{parent?.children.Count ?? 0}");
		SpriteRenderer spriteRenderer = newTrunk.obj.AddComponent<SpriteRenderer>();
		newTrunk.whatPartAmI = (int)Variant;
		Assert.IsTrue(newTrunk.whatPartAmI < BranchSprites.Length);
		spriteRenderer.sprite = BranchSprites[newTrunk.whatPartAmI];
		spriteRenderer.sortingOrder = 1;
		newTrunk.obj.layer = 9;

		BoxCollider2D boxCollider = newTrunk.obj.AddComponent<BoxCollider2D>();
		boxCollider.offset = new Vector2(0, spriteRenderer.sprite.bounds.size.y/2);
		boxCollider.size = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
		boxCollider.isTrigger = true;
		
        newTrunk.obj.transform.parent = this.transform;
        newTrunk.obj.transform.position = this.transform.position;
		float scale = newTrunk.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * newTrunk.growth;
		newTrunk.obj.transform.localScale = new Vector3(scale, scale, scale);
		newTrunk.obj.transform.rotation = Quaternion.AngleAxis(newTrunk.angle - 90, Vector3.forward);
		 
		newTrunk.howFarIveCome = 0; 
		
		branches.Add(newTrunk);
		
		numberOfBranches++;
		
		return newTrunk;
	}
	
	void Update()
	{
		int doneGrowing = 0;
		foreach(Branch branch in branches)
		{
			float growthSpeed = 0.002f;
			if (CurrentlyWatered > 0)
			{
				growthSpeed = 0.02f;
			}
			if (branch.growth < 1 && (branch.parent == null || branch.parent.growth > 0.95f))
			{
				branch.growth += (growthSpeed / branch.length);
			}
			else if (branch.growth > 1)
			{
				branch.growth = 1;
			}
			else if (branch.growth == 1)
			{
				doneGrowing++;
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
				if(child.whatPartAmI == (int)PlantVariant.TreeFruit) {child.obj.transform.position = branch.obj.transform.position;}
				else {child.obj.transform.position = branch.obj.transform.position + (branch.obj.transform.up * branch.length * branch.growth);}
				float swayAmount = Mathf.Sin(Time.time * Mathf.PI * 2 / (5 + 4 * child.randSeed) + child.randSeed * Mathf.PI * 2) * 4;
				child.currentAngle = branch.currentAngle + child.angle + swayAmount;
				child.obj.transform.rotation = Quaternion.AngleAxis(child.currentAngle - 90, Vector3.forward);
			}
			float median = MaxLife/2;
			float increment = (median)/6;
			if (PlantLifeSpan < median && PlantLifeSpan > 0)
			{
				if (PlantLifeSpan < (median - increment * 4))
				{
					spriteRenderer.color = new Color(0.5f,0.5f,0.5f,PlantLifeSpan/increment);
				}
				else if (PlantLifeSpan < (median - increment * 3))
				{
					spriteRenderer.color = new Color(0.6f,0.6f,0.6f,1);
				}
				else if (PlantLifeSpan < (median - increment * 2))
				{
					spriteRenderer.color = new Color(0.7f,0.7f,0.7f,1);
				}
				else if (PlantLifeSpan < (median - increment))
				{
					spriteRenderer.color = new Color(0.8f,0.8f,0.8f,1);
				}
				else
				{
					spriteRenderer.color = new Color(0.9f,0.9f,0.9f,1);
				}
			}
		}
		if (branches.Count == doneGrowing)
		{
			fullyGrown = true;
		}
	}
	
	void FixedUpdate()
    {
		if (CurrentlyWatered > 0)
		{
			CurrentlyWatered--;
		}
		if (fullyGrown)
		{
			PlantLifeSpan--;
			if (CurrentlyWatered > 0 && PlantLifeSpan < MaxLife)
			{
				PlantLifeSpan = PlantLifeSpan + 100;
			}
		}
		if (PlantLifeSpan <= 0)
		{
			//print("Rot!");
			Destroy(gameObject);
		}
    }
	
	public void GetWatered()
	{
		CurrentlyWatered = 5;
	}
	
	public void GatherFruit(GameObject FruitObj)
	{
		Branch targetFruit = null;
		
		foreach(Branch branch in branches)
		{
			if (branch.obj == FruitObj)
			{
				targetFruit = branch;
			}
		}
		
		if (targetFruit != null)
		{
			//clear out from parent
			targetFruit.parent.children.Remove(targetFruit);
			
			//shorten branch list
			numberOfBranches--;
			branches.Remove(targetFruit);
			
			//clear out self
			Destroy(targetFruit.obj);
		}
	}
}
