enum Dir
{
	None  = 0x00,
	Right = 0x01,
	Down  = 0x02,
	Left  = 0x04,
	Up    = 0x08,
	All   = 0x0F,
	
	UpRight   = 0x10,
	DownRight = 0x20,
	DownLeft  = 0x40,
	UpLeft    = 0x80,
	Diagonals = 0xF0,
	All8      = 0xFF,
};

function PackedDir() constructor
{
	value = Dir.None;
	
	static GetRight = function()
	{
		return IsFlagSet(self.value, Dir.Right);
	}
	static GetDown = function()
	{
		return IsFlagSet(self.value, Dir.Down);
	}
	static GetLeft = function()
	{
		return IsFlagSet(self.value, Dir.Left);
	}
	static GetUp = function()
	{
		return IsFlagSet(self.value, Dir.Up);
	}
	static GetUpRight = function()
	{
		return IsFlagSet(self.value, Dir.UpRight);
	}
	static GetDownRight = function()
	{
		return IsFlagSet(self.value, Dir.DownRight);
	}
	static GetDownLeft = function()
	{
		return IsFlagSet(self.value, Dir.DownLeft);
	}
	static GetUpLeft = function()
	{
		return IsFlagSet(self.value, Dir.UpLeft);
	}
	static HasDiagonals = function()
	{
		return IsFlagSet(self.value, Dir.Diagonals);
	}
	
	static SetRight = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.Right, enabled);
	}
	static SetDown = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.Down, enabled);
	}
	static SetLeft = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.Left, enabled);
	}
	static SetUp = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.Up, enabled);
	}
	static SetUpRight = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.UpRight, enabled);
	}
	static SetDownRight = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.DownRight, enabled);
	}
	static SetDownLeft = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.DownLeft, enabled);
	}
	static SetUpLeft = function(enabled)
	{
		self.value = FlagSetTo(self.value, Dir.UpLeft, enabled);
	}
	static ClearDiagonals = function()
	{
		self.value = (self.value & Dir.All);
	}
	static GetAngle = function()
	{
		return GetAngleFromDir(self.value);
	}
}

// Since game maker imports our 2D sprite sheets as single dimensional arrays of frames,
// we gotta convert from where we remember the image being in the 2D sheet to where it is in this 1D strip
function SheetIndex(sheetX, sheetY, sheetWidth)
{
	return (sheetY * sheetWidth) + sheetX;
}

function GetConnectedSpriteIndex(packedDirValue)
{
	switch (packedDirValue & Dir.All)
	{
		//Sides
		case (Dir.Down | Dir.Left | Dir.Up):             return SheetIndex(2, 1, 4); //right side
		case (Dir.Right | Dir.Left | Dir.Up):            return SheetIndex(1, 2, 4); //bottom side
		case (Dir.Right | Dir.Down | Dir.Up):            return SheetIndex(0, 1, 4); //left side
		case (Dir.Right | Dir.Down | Dir.Left):          return SheetIndex(1, 0, 4); //top side
		//Straights
		case (Dir.Right | Dir.Left):                     return SheetIndex(1, 3, 4); //horizontal
		case (Dir.Down | Dir.Up):                        return SheetIndex(3, 1, 4); //vertical
		//Corners
		case (Dir.Right | Dir.Down):                     return SheetIndex(0, 0, 4); //top left corner
		case (Dir.Down | Dir.Left):                      return SheetIndex(2, 0, 4); //top right corner
		case (Dir.Left | Dir.Up):                        return SheetIndex(2, 2, 4); //bottom right corner
		case (Dir.Right | Dir.Up):                       return SheetIndex(0, 2, 4); //bottom left corner
		//DeadEnds
		case (Dir.Right):                                return SheetIndex(0, 3, 4); //right
		case (Dir.Down):                                 return SheetIndex(3, 0, 4); //down
		case (Dir.Left):                                 return SheetIndex(2, 3, 4); //left
		case (Dir.Up):                                   return SheetIndex(3, 2, 4); //up
		//Center
		case (Dir.All):                                  return SheetIndex(1, 1, 4);
		//Island
		case (Dir.None):                                 return SheetIndex(3, 3, 4);
		
		default: return SheetIndex(3, 3, 4);
	}
}

function GetConnectedSpriteIndices(packedDirValue)
{
	var result = [];
	array_push(result, GetConnectedSpriteIndex(packedDirValue));
	if (IsFlagSet(packedDirValue, Dir.Up) && IsFlagSet(packedDirValue, Dir.Right) && !IsFlagSet(packedDirValue, Dir.UpRight))
	{
		array_push(result, SheetIndex(0, 4, 4));
	}
	if (IsFlagSet(packedDirValue, Dir.Down) && IsFlagSet(packedDirValue, Dir.Right) && !IsFlagSet(packedDirValue, Dir.DownRight))
	{
		array_push(result, SheetIndex(2, 4, 4));
	}
	if (IsFlagSet(packedDirValue, Dir.Down) && IsFlagSet(packedDirValue, Dir.Left) && !IsFlagSet(packedDirValue, Dir.DownLeft))
	{
		array_push(result, SheetIndex(3, 4, 4));
	}
	if (IsFlagSet(packedDirValue, Dir.Up) && IsFlagSet(packedDirValue, Dir.Left) && !IsFlagSet(packedDirValue, Dir.UpLeft))
	{
		array_push(result, SheetIndex(1, 4, 4));
	}
	return result;
}

function Vec2FromDir(dir)
{
	var result = new Vec2(0, 0);
	if (IsFlagSet(dir, Dir.Left))
	{
		result.x -= 1;
	}
	if (IsFlagSet(dir, Dir.Right))
	{
		result.x += 1;
	}
	if (IsFlagSet(dir, Dir.Up))
	{
		result.y -= 1;
	}
	if (IsFlagSet(dir, Dir.Down))
	{
		result.y += 1;
	}
	return result;
}

function Vec2FromPackedDir(packedDir)
{
	var result = new Vec2(0, 0);
	if (packedDir.GetLeft())
	{
		result.x -= 1;
	}
	if (packedDir.GetRight())
	{
		result.x += 1;
	}
	if (packedDir.GetUp())
	{
		result.y -= 1;
	}
	if (packedDir.GetDown())
	{
		result.y += 1;
	}
	return result;
}

//Down is 0 degrees (or radians), angles go clockwise
function GetAngleFromDir(dir, inRadians = false)
{
	switch (dir)
	{
		case Dir.Down:  return (inRadians ?        0 :   0);
		case Dir.Left:  return (inRadians ?   (pi/2) :  90);
		case Dir.Up:    return (inRadians ?     (pi) : 180);
		case Dir.Right: return (inRadians ? (3/2*pi) : 270);
		default: return 0;
	}
}

function DirOpposite(dir)
{
	switch (dir)
	{
		case Dir.Right: return Dir.Left;
		case Dir.Down:  return Dir.Up;
		case Dir.Left:  return Dir.Right;
		case Dir.Up:    return Dir.Down;
		default: return Dir.None;
	}
}

function DirFromIndex(ind)
{
	switch (floor(ind % 4))
	{
		case 0: return Dir.Right;
		case 1: return Dir.Down;
		case 2: return Dir.Left;
		case 3: return Dir.Up;
		default: return Dir.Right;
	}
}

function DirFromVec2(vec)
{
	if (vec.x == 0 && vec.y == 0) { return Dir.Down; }
	if (abs(vec.x) > abs(vec.y))
	{
		return (vec.x > 0) ? Dir.Right : Dir.Left;
	}
	else
	{
		return (vec.y > 0) ? Dir.Down : Dir.Up;
	}
}

function GetDirStr(dir)
{
	switch (dir)
	{
		case Dir.Right: return "Right";
		case Dir.Down:  return "Down";
		case Dir.Left:  return "Left";
		case Dir.Up:    return "Up";
		default: return "Unknown";
	}
}

