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
	
	public enum PlantPart //Tree, Rice, Cactus, Mushroom, Kelp, Palmtree, Bush, Sunflower, Count
	{
		TreeTrunk, TreeBranch, TreeLeaf, TreeFruit, ThornsTrunk, Count
	}
	
	public enum PlantType
	{
		Tree, Thorns, Count
	}
	
	enum ColLayer
	{
		Default, TransparentFX, IgnoreRaycast, blank, Water, UI, Planet, Player, Projectile, Plant, Fruit, Gun, Vender, NPC, Count
	}
	
	Branch rootTrunk;
	List<Branch> branches = new List<Branch>();
	
	public PlantPart Variant = PlantPart.TreeTrunk;
	public PlantType WhichPlant = PlantType.Tree;
	public Sprite[] BranchSprites; //for each PlantPart
	public LayerMask PlanetLayer;
	public LayerMask PlantLayer;
    public float growRadius = 8;
	public GameObject fruitType;
	public GameObject SeedBullet;
	int numberOfBranches = 0;
	bool luckySuperPlant = false;
	bool fullyGrown = false;
	bool timeOfDying = false;
    float PlantLifeSpan = 120;
    float timerTicker = 0;
	float MaxLife;
	int CurrentlyWatered = 0;
	float rootAngle = 0;
	Vector3 gravityPos = new Vector3(0, 0, 0);
	
	private void Awake()
	{
		MaxLife = PlantLifeSpan;
		
		rotateTowardGravity();
	
		shouldIBeAbleToGrow();
		
		if (WhichPlant == PlantType.Tree)
		{
			growTree();
		}
		else if (WhichPlant == PlantType.Thorns)
		{
			growThorns();
		}
		
	}

	private void shouldIBeAbleToGrow()
	{
		if (WhichPlant != PlantType.Thorns)
		{
			List<Collider2D> PlantsInRange = Physics2D.OverlapCircleAll(transform.position, growRadius, PlantLayer).ToList();
			
			if (PlantsInRange.Count > 0)
			{
				//print($"found {PlantsInRange.Count} plants nearby so I cannot grow!");
				Destroy(gameObject);
				return;
			}
		}
		else
		{
			List<Collider2D> PlantsInRange = Physics2D.OverlapCircleAll(transform.position, growRadius/3, PlantLayer).ToList();
			
			if (PlantsInRange.Count > 0)
			{
				//print($"found {PlantsInRange.Count} plants nearby so I cannot grow!");
				Destroy(gameObject);
				return;
			}
		}
	}
	
	private void rotateTowardGravity()
	{
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
        
		if (closestPlanet != null)
		{
			gravityPos = closestPlanet.gameObject.transform.position;
            rootAngle = Mathf.Atan2(transform.position.y - closestPlanet.gameObject.transform.position.y, transform.position.x - closestPlanet.gameObject.transform.position.x) * Mathf.Rad2Deg;
            //Vector2 distanceVector = (Vector2)closestPlanet.gameObject.transform.position - (Vector2)transform.position;
            //rootAngle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(rootAngle - 90, Vector3.forward);

            //print($"Planet is at ({closestPlanet.transform.position.x}, {closestPlanet.transform.position.y}) we are at ({transform.position.x}, {transform.position.y}) angle is {rootAngle}");
        }
	}
	
	void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, growRadius);
    }

	private void growTree()
	{
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
			rootTrunk = AddBranch(null, rootAngle, Random.Range(5.0f, 7.0f), PlantPart.TreeTrunk, 0, ColLayer.Plant, true);
			
		}
		else
		{
			rootTrunk = AddBranch(null, rootAngle, Random.Range(4.0f, 5.0f), PlantPart.TreeTrunk, 0, ColLayer.Plant, true);
		}

		//print("adding branches");
		for (int i = 0; i < numBranches; i++)
		{
			AddRandomBranch(PlantPart.TreeBranch, -1);
		}
		
		//print("Leafing it up");
		int currentNumberOfBranches = numberOfBranches;
		for(int i = 0; i < currentNumberOfBranches; i++)
		{
			if (branches[i].children.Count == 0)
			{
				//print($"the branch value is {branch.whereInList}");
				AddBranch(branches[i], Random.Range(-20.0f,20.0f), Random.Range(branches[i].length * 0.6f, branches[i].length * 0.8f), PlantPart.TreeLeaf, 10, ColLayer.Plant, false);
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
				AddBranch(branches[i], HangingDown, Random.Range(branches[i].length * 0.4f, 1.0f), PlantPart.TreeFruit, 10, ColLayer.Fruit, true);
			}
		}
	}

	private void growThorns()
	{
		//print("starting a new tree");
        int numBranches = Random.Range(3, 10);
		if (Random.Range(0.0f,100.0f) > 95.0f)
		{
			numBranches = Random.Range(10, 30);
			luckySuperPlant = true;
			print($"super lucky number {luckySuperPlant}!");
		}
		
		//Creating the Base for the plant
		if (luckySuperPlant)
		{
			rootTrunk = AddBranch(null, rootAngle, Random.Range(2.0f, 4.0f), PlantPart.ThornsTrunk, 0, ColLayer.Plant, true);
			
		}
		else
		{
			rootTrunk = AddBranch(null, rootAngle, Random.Range(1.0f, 2.0f), PlantPart.ThornsTrunk, 0, ColLayer.Plant, true);
		}

		//print("adding branches");
		for (int i = 0; i < numBranches; i++)
		{
			AddRandomBranch(PlantPart.ThornsTrunk, 0);
		}
	}
	
	private void AddRandomBranch(PlantPart spriteChoice, int shrinkAmount)
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
		float smallestSize = (branches[branchIndex].length + shrinkAmount);
		if (smallestSize < 1) {smallestSize = 1;}
        AddBranch(branches[branchIndex], newAngle, Random.Range(smallestSize, branches[branchIndex].length), spriteChoice, 0, ColLayer.Plant, false);
	}
	
	private Branch AddBranch(Branch parent, float rotation, float length, PlantPart spriteChoice, int PlantLayerMod, ColLayer CollisionLayer, bool hasCollider)
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
		newBranch.whatPartAmI = (int)Variant + (int)spriteChoice;  //difference
		Assert.IsTrue(newBranch.whatPartAmI < BranchSprites.Length);
		spriteRenderer.sprite = BranchSprites[newBranch.whatPartAmI];
		
		if (hasCollider)
		{
			BoxCollider2D boxCollider = newBranch.obj.AddComponent<BoxCollider2D>();
			boxCollider.offset = new Vector2(0, spriteRenderer.sprite.bounds.size.y/2);
			boxCollider.size = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
			boxCollider.isTrigger = true;
		}

        newBranch.obj.transform.parent = this.transform;
        newBranch.obj.transform.position = parent?.obj.transform.position ?? this.transform.position;
		float scale = newBranch.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * newBranch.growth;
		newBranch.obj.transform.localScale = new Vector3(scale, scale, scale);
		newBranch.obj.transform.rotation = Quaternion.AngleAxis(newBranch.angle - 90, Vector3.forward);
		
		if (parent != null) {parent.children.Add(newBranch);}
		newBranch.howFarIveCome = (parent?.howFarIveCome ?? 0) + 1;
		spriteRenderer.sortingOrder = newBranch.howFarIveCome + PlantLayerMod; //difference
		newBranch.obj.layer = (int)CollisionLayer; //difference
		
		branches.Add(newBranch); 
		
		numberOfBranches++;
		
		return newBranch;
	}
	
	void Update()
	{
		int doneGrowing = 0;
		int doneShrinking = 0;
		foreach(Branch branch in branches)
		{
			if (!timeOfDying)
			{
				float growthSpeed = 0.001f;
				if (WhichPlant == PlantType.Thorns)
				{
					growthSpeed = 0.005f;
				}
				else if (CurrentlyWatered > 0)
				{
					growthSpeed = 0.02f;
				}
				
				if (WhichPlant == PlantType.Thorns && CurrentlyWatered > 0)
				{
					if (branch.growth > 0.10f)
					{
						branch.growth -= (growthSpeed / branch.length);
					}
					else if (branch.growth < 0.10f)
					{
						doneShrinking++;
					}
				}
				else if (branch.growth < 1 && (branch.parent == null || branch.parent.growth > 0.95f))
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
				
				if (branch.parent == null)
				{
					float swayAmount = Mathf.Sin(Time.time * Mathf.PI * 2 / (2 + 1 * branch.randSeed) + branch.randSeed * Mathf.PI * 2) * 2;
					branch.currentAngle = branch.angle + swayAmount;
					branch.obj.transform.rotation = Quaternion.AngleAxis(branch.currentAngle - 90, Vector3.forward);
				}
				foreach(Branch child in branch.children)
				{
					if(child.whatPartAmI == (int)PlantPart.TreeFruit) {child.obj.transform.position = branch.obj.transform.position;}
					else {child.obj.transform.position = branch.obj.transform.position + (branch.obj.transform.up * branch.length * branch.growth);}
					float swayAmount = Mathf.Sin(Time.time * Mathf.PI * 2 / (5 + 4 * child.randSeed) + child.randSeed * Mathf.PI * 2) * 4;
					child.currentAngle = branch.currentAngle + child.angle + swayAmount;
					child.obj.transform.rotation = Quaternion.AngleAxis(child.currentAngle - 90, Vector3.forward);
				}
			}
			
			SpriteRenderer spriteRenderer = branch.obj.GetComponent<SpriteRenderer>();
			float scale = branch.length/(spriteRenderer.sprite.bounds.size.y - 0.3f) * branch.growth;
			branch.obj.transform.localScale = new Vector3(scale, scale, scale);
			
			float median = MaxLife/2;
			float increment = (median)/6;
			if (PlantLifeSpan < median && PlantLifeSpan > 0)
			{
				if (PlantLifeSpan < (median - increment * 4))
				{
					deathTriggered();
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
		
		if (branches.Count == doneShrinking && WhichPlant == PlantType.Thorns && CurrentlyWatered > 0 && !timeOfDying)
		{
			deathTriggered();
			if (PlantLifeSpan > (MaxLife / 6)) { PlantLifeSpan = (MaxLife / 6); }
		}
	}
	
	public void deathTriggered()
	{
		timeOfDying = true;
	}
	
	void FixedUpdate()
    {
		if (CurrentlyWatered > 0)
		{
			CurrentlyWatered--;
		}
		
		if (timeOfDying)
		{
			if (PlantLifeSpan > (MaxLife / 6)) { PlantLifeSpan = (MaxLife / 6); }
			PlantLifeSpan -= Time.fixedDeltaTime * 5;
		}
		else
		{
			if (fullyGrown && WhichPlant != PlantType.Thorns)
			{
				if (CurrentlyWatered > 0 && PlantLifeSpan < MaxLife)
				{
					PlantLifeSpan += Time.fixedDeltaTime * 100;
				}
				else
				{
					PlantLifeSpan -= Time.fixedDeltaTime;
				}
			}
		
			timerTicker += Time.fixedDeltaTime;
			if (WhichPlant == PlantType.Thorns && timerTicker > 10.0f)
			{
				KillAndGrow();
				timerTicker = 0;
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
		
		if (targetFruit != null && targetFruit.growth > 0.95f)
		{
			//clear out from parent
			targetFruit.parent.children.Remove(targetFruit);
			
			//shorten branch list
			numberOfBranches--;
			branches.Remove(targetFruit);
			
			GameObject newFruit = Instantiate(fruitType, targetFruit.obj.transform.position, targetFruit.obj.transform.rotation);
			//SpriteRenderer fruitSpriteRenderer = newFruit.AddComponent<SpriteRenderer>();
			
			//clear out self
			Destroy(targetFruit.obj);
		}
	}
	
	private void KillAndGrow()
	{
		List<Collider2D> PlantsInRange = Physics2D.OverlapCircleAll(transform.position, growRadius, PlantLayer).ToList();
		
		foreach(Collider2D plantFound in PlantsInRange)
		{
			if (plantFound != null)
			{
				PlantType otherPlant = plantFound.gameObject.GetComponentInParent<TreeScript>().WhichPlant;
				if (otherPlant != PlantType.Thorns)
				{
					plantFound.gameObject.GetComponentInParent<TreeScript>().deathTriggered();
				}
			}
		}		
		
		Vector3 upDir = transform.position - gravityPos;
		//upDir = upDir.normalized;
		Vector3 rightDir = new Vector3(upDir.y, -upDir.x, 0);
		Vector3 leftDir = (upDir - rightDir);
		rightDir = (upDir + rightDir);
		float ShootAngleR = Mathf.Atan2(rightDir.y, rightDir.x) * Mathf.Rad2Deg;
		float ShootAngleL = Mathf.Atan2(leftDir.y, leftDir.x) * Mathf.Rad2Deg;
		GameObject bulletShotR = Instantiate(SeedBullet, transform.position + (upDir.normalized * 1.0f), transform.rotation);
		GameObject bulletShotL = Instantiate(SeedBullet, transform.position + (upDir.normalized * 1.0f), transform.rotation);
		Rigidbody2D bulletRigidBodyR = bulletShotR.GetComponent<Rigidbody2D>();
		Rigidbody2D bulletRigidBodyL = bulletShotL.GetComponent<Rigidbody2D>();
		bulletRigidBodyR.velocity = new Vector2(Mathf.Cos(ShootAngleR * Mathf.Deg2Rad),Mathf.Sin(ShootAngleR * Mathf.Deg2Rad)) * Random.Range(5.0f, 10.0f);
		bulletRigidBodyL.velocity = new Vector2(Mathf.Cos(ShootAngleL * Mathf.Deg2Rad),Mathf.Sin(ShootAngleL * Mathf.Deg2Rad)) * Random.Range(5.0f, 10.0f);
	}
}
