
function SetupRpgGlobals()
{
	global.tileSize = 16;
	global.noclipEnabled = false;
	global.hasLantern = false;
	global.numCoins = 0;
	global.targetDoorNum = -1;
	global.showingDialogue = false;
	global.currentDialogue = noone;
	global.collectedInstances = [];
	global.npcs = [];
	global.activeBuffs = [];
}

function RpgHandleEnteredCheat(cheatStr)
{
	var result = true;
	if (cheatStr == "NOCLIP")
	{
		show_debug_message((global.noclipEnabled ? "Disabling" : "Enabling") + " noclip!");
		global.noclipEnabled = !global.noclipEnabled;
	}
	else if (cheatStr == "CRAZYNPC")
	{
		var allNpcs = FindAllInstancesOf(RpgNpcParent_o);
		for (var nIndex = 0; nIndex < array_length(allNpcs); nIndex++)
		{
			allNpcs[nIndex].aiUpdatePeriod = 0;
			allNpcs[nIndex].movementSpeed = 2;
		}
	}
	else
	{
		result = false;
	}
	return result;
}

function ShowDialogue(text)
{
	if (global.currentDialogue != noone && global.currentDialogue.data != 0)
	{
		global.currentDialogue.data.Close();
	}
	global.currentDialogue = instance_create_layer(0, 0, "UI", RpgDialogue_o);
	global.currentDialogue.data = new Dialogue(DialogueFont_f, c_white, text, RpgDialogueBack2_s, 3);
	global.currentDialogue.data.Open();
	global.showingDialogue = true;
	return global.currentDialogue.data;
}

// +==============================================================================+
// |                              Permanent Changes                               |
// +==============================================================================+
function RoomInstance(_roomId, _instanceId) constructor
{
	roomId = _roomId;
	instanceId = _instanceId;
	
	static Copy = function()
	{
		return new RoomInstance(self.roomId, self.instanceId);
	}
}

function InstanceCollected(instanceId)
{
	array_push(global.collectedInstances, new RoomInstance(room, instanceId));
}
function HasInstanceBeenCollected(instanceId)
{
	for (var iIndex = 0; iIndex < array_length(global.collectedInstances); iIndex++)
	{
		if (global.collectedInstances[iIndex].roomId == room && global.collectedInstances[iIndex].instanceId == instanceId) { return true; }
	}
	return false;
}

// +==============================================================================+
// |                                     NPCs                                     |
// +==============================================================================+
enum RpgNpcAi
{
	None,
	RandomLook,
	LookAtHero,
	RandomMove,
};

function RpgNpc(_instance) constructor
{
	instance = _instance;
	pathingGrid = noone;
	pathFollowIndex = 0;
	path = [];
	
	static Copy = function()
	{
		var result = new RpgNpc(self.instance);
		result.pathingGrid = self.pathingGrid;
		result.pathFollowIndex = self.pathFollowIndex;
		result.path = array_create(array_length(self.path));
		for (var pIndex = 0; pIndex < array_length(self.path); pIndex++)
		{
			result.path[pIndex] = self.path[pIndex].Copy();
		}
		return result;
	}
	
	static IsPathing = function()
	{
		return (array_length(self.path) > 0);
	}
	
	static Update = function()
	{
		if (self.instance == noone) { return; }
		if (self.IsPathing())
		{
			var currentPos = new Vec2(self.instance.x + self.instance.sprite_width/2, self.instance.y + self.instance.sprite_height/2);
			var nextPath = self.path[array_length(self.path)-1];
			var nextPathDelta = Vec2Sub(nextPath, currentPos);
			if (nextPathDelta.Length() >= self.instance.movementSpeed)
			{
				var movementVec = Vec2Scale(Vec2Normalize(nextPathDelta), self.instance.movementSpeed);
				self.instance.x += movementVec.x;
				self.instance.y += movementVec.y;
				self.instance.facingDir = DirFromVec2(nextPathDelta);
			}
			else
			{
				self.instance.x += nextPathDelta.x;
				self.instance.y += nextPathDelta.y;
				array_pop(self.path);
			}
		}
		
		self.instance.isMoving = self.IsPathing();
	}
	static MakeDecision = function()
	{
		if (self.IsPathing()) { return; }
		if (self.instance == noone) { return; }
		
		switch (self.instance.aiType)
		{
			case RpgNpcAi.RandomLook: self.instance.facingDir = DirFromIndex(random(4)); break;
			case RpgNpcAi.LookAtHero:
			{
				var hero = instance_find(RpgHero_o, 0);
				if (hero != noone)
				{
					var heroDirVec = new Vec2(hero.x - self.instance.x, hero.y - self.instance.y);
					self.instance.facingDir = DirFromVec2(heroDirVec);
				}
			} break;
			case RpgNpcAi.RandomMove:
			{
				var currentPos = new Vec2(self.instance.x + self.instance.sprite_width/2, self.instance.y + self.instance.sprite_height/2);
				var gridPos = new Vec2(floor(currentPos.x / global.tileSize), floor(currentPos.y / global.tileSize));
				var gridCenter = new Vec2((gridPos.x + 0.5) * global.tileSize, (gridPos.y + 0.5) * global.tileSize);
				var dirOptions = [];
				if (instance_position(gridCenter.x + global.tileSize, gridCenter.y, RpgSolidParent_o) == noone) //is there something right
				{
					array_push(dirOptions, Dir.Right);
				}
				if (instance_position(gridCenter.x - global.tileSize, gridCenter.y, RpgSolidParent_o) == noone) //is there something left
				{
					array_push(dirOptions, Dir.Left);
				}
				if (instance_position(gridCenter.x, gridCenter.y + global.tileSize, RpgSolidParent_o) == noone) //is there something below
				{
					array_push(dirOptions, Dir.Down);
				}
				if (instance_position(gridCenter.x, gridCenter.y - global.tileSize, RpgSolidParent_o) == noone) //is there something above
				{
					array_push(dirOptions, Dir.Up);
				}
				if (array_length(dirOptions) > 0)
				{
					var dirPick = random(array_length(dirOptions));
					var targetPos = Vec2Add(gridCenter, Vec2Scale(Vec2FromDir(dirOptions[dirPick]), global.tileSize));
					array_push(path, targetPos);
				}
			} break;
		}
	}
}

// +==============================================================================+
// |                                    Buffs                                     |
// +==============================================================================+
function RpgBuff(_startTime, _duration, _effectStr, _amount) constructor
{
	startTime = _startTime;
	duration = _duration;
	effectStr = _effectStr;
	amount = _amount;
	
	static Copy = function()
	{
		return new RpgBuff(self.startTime, self.duration, self.effectStr, self.amount);
	}
}

function UpdateActiveBuffs()
{
	for (var eIndex = 0; eIndex < array_length(global.activeBuffs); )
	{
		if (global.ProgramTime >= global.activeBuffs[eIndex].startTime + global.activeBuffs[eIndex].duration)
		{
			array_delete(global.activeBuffs, eIndex, 1);
			//Don't increment eIndex
		}
		else { eIndex++; }
	}
}

function GetActiveBuff(effectStr)
{
	for (var eIndex = 0; eIndex < array_length(global.activeBuffs); eIndex++)
	{
		if (global.activeBuffs[eIndex].effectStr == effectStr)
		{
			return global.activeBuffs[eIndex];
		}
	}
	return 0;
}
function GetActiveBuffSum(effectStr)
{
	var result = 0;
	for (var eIndex = 0; eIndex < array_length(global.activeBuffs); eIndex++)
	{
		if (global.activeBuffs[eIndex].effectStr == effectStr)
		{
			result += global.activeBuffs[eIndex].amount;
		}
	}
	return result;
}

