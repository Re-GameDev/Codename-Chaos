
// +================================================================+
// |                              Vec2                              |
// +================================================================+
function Vec2(_x, _y) constructor
{
	x = _x;
	y = _y;
	
	static Copy = function()
	{
		return new Vec2(x, y);
	}
	
	// Const Functions
	static Length = function()
	{
		return sqrt((x * x) + (y * y));
	}
	static Dist = function(otherVector)
	{
		var difference = otherVector.Copy();
		difference.Sub(self);
		return difference.Length();
	}
	static Dot = function(otherVector)
	{
		return (x * otherVector.x) + (y * otherVector.y);
	}
	static Equals = function(otherVector)
	{
		return (x == otherVector.x && y == otherVector.y);
	}
	static NotEquals = function(otherVector)
	{
		return (x != otherVector.x || y != otherVector.y);
	}
	static BasicallyEquals = function(otherVector, tolerance)
	{
		return (
			(abs(x - otherVector.x) <= tolerance) &&
			(abs(y - otherVector.y) <= tolerance)
		);
	}
	static BasicallyNotEquals = function(otherVector, tolerance)
	{
		return (
			(abs(x - otherVector.x) > tolerance) ||
			(abs(y - otherVector.y) > tolerance)
		);
	}
	
	// Manipulation Functions
	static Add = function(otherVector)
	{
		x += otherVector.x;
		y += otherVector.y;
	}
	static Sub = function(otherVector)
	{
		x -= otherVector.x;
		y -= otherVector.y;
	}
	static Normalize = function()
	{
		var length = self.Length();
		if (length == 0) { return; }
		x /= length;
		y /= length;
	}
	static Mult = function(otherVector)
	{
		x *= otherVector.x;
		y *= otherVector.y;
	}
	static Div = function(otherVector)
	{
		x /= otherVector.x;
		y /= otherVector.y;
	}
	static Scale = function(scalar)
	{
		x *= scalar;
		y *= scalar;
	}
	static Shrink = function(scalar)
	{
		x /= scalar;
		y /= scalar;
	}
	static Clamp = function(minVector, maxVector)
	{
		x = clamp(x, minVector.x, maxVector.x);
		y = clamp(y, minVector.y, maxVector.y);
	}
	static Lerp = function(otherVector, amount)
	{
		x = lerp(x, otherVector.x, amount);
		y = lerp(y, otherVector.y, amount);
	}
	static AlignTo = function(alignmentScale)
	{
		x = round(x / alignmentScale) * alignmentScale;
		y = round(y / alignmentScale) * alignmentScale;
	}
}

#region Vec2 Functions

function Vec2Copy(vector)
{
	return vector.Copy();
}
function Vec2FromVec3xy(vector)
{
	return new Vec2(vector.x, vector.y);
}
function Vec2FromVec3xz(vector)
{
	return new Vec2(vector.x, vector.z);
}
function Vec2FromVec3yz(vector)
{
	return new Vec2(vector.y, vector.z);
}
function Vec2FromVec3yx(vector)
{
	return new Vec2(vector.y, vector.x);
}
function Vec2FromVec3zx(vector)
{
	return new Vec2(vector.z, vector.x);
}
function Vec2FromVec3zy(vector)
{
	return new Vec2(vector.z, vector.y);
}

// Const Functions
function Vec2Length(vector)
{
	return vector.Length();
}
function Vec2Dist(left, right)
{
	return left.Dist(right);
}
function Vec2Dot(left, right)
{
	return left.Dot(right);
}
function Vec2Equals(left, right)
{
	return left.Equals(right);
}
function Vec2NotEquals(left, right)
{
	return left.NotEquals(right);
}
function Vec2BasicallyEquals(left, right, tolerance)
{
	return left.BasicallyEquals(right, tolerance);
}
function Vec2BasicallyNotEquals(left, right, tolerance)
{
	return left.BasicallyNotEquals(right, tolerance);
}

// Manipulation functions
function Vec2Add(left, right)
{
	return new Vec2(left.x + right.x, left.y + right.y);
}
function Vec2Sub(left, right)
{
	return new Vec2(left.x - right.x, left.y - right.y);
}
function Vec2Normalize(vector)
{
	var result = vector.Copy();
	result.Normalize();
	return result;
}
function Vec2Mult(left, right)
{
	var result = left.Copy();
	result.Mult(right);
	return result;
}
function Vec2Div(left, right)
{
	var result = left.Copy();
	result.Div(right);
	return result;
}
function Vec2Scale(vector, scalar)
{
	var result = vector.Copy();
	result.Scale(scalar);
	return result;
}
function Vec2Shrink(vector, scalar)
{
	var result = vector.Copy();
	result.Shrink(scalar);
	return result;
}
function Vec2Clamp(vector, minVector, maxVector)
{
	var result = vector.Copy();
	result.Clamp(minVector, maxVector);
	return result;
}
function Vec2Lerp(startVector, endVector, amount)
{
	var result = startVector.Copy();
	result.Lerp(endVector, amount);
	return result;
}
function Vec2AlignTo(vector, alignmentScale)
{
	var result = vector.Copy();
	result.AlignTo(alignmentScale);
	return result;
}

#endregion

// +================================================================+
// |                              Vec3                              |
// +================================================================+
function Vec3(_x, _y, _z) constructor
{
	x = _x;
	y = _y;
	z = _z;
	
	static Copy = function()
	{
		return new Vec3(self.x, self.y, self.z);
	}
	
	// Const Functions
	static Length = function()
	{
		return sqrt((x * x) + (y * y) + (z * z));
	}
	static Dist = function(otherVector)
	{
		var difference = otherVector.Copy();
		difference.Sub(self);
		return difference.Length();
	}
	static Dot = function(otherVector)
	{
		return (x * otherVector.x) + (y * otherVector.y) + (z * otherVector.z);
	}
	static Cross = function(otherVector)
	{
		return new Vec3(
			(y * otherVector.z) - (z * otherVector.y),
			(z * otherVector.x) - (x * otherVector.z),
			(x * otherVector.y) - (y * otherVector.x)
		);
	}
	static Equals = function(otherVector)
	{
		return (x == otherVector.x && y == otherVector.y && z == otherVector.z);
	}
	static NotEquals = function(otherVector)
	{
		return (x != otherVector.x || y != otherVector.y || z != otherVector.z);
	}
	static BasicallyEquals = function(otherVector, tolerance)
	{
		return (
			(abs(x - otherVector.x) <= tolerance) &&
			(abs(y - otherVector.y) <= tolerance) &&
			(abs(z - otherVector.z) <= tolerance)
		);
	}
	static BasicallyNotEquals = function(otherVector, tolerance)
	{
		return (
			(abs(x - otherVector.x) > tolerance) ||
			(abs(y - otherVector.y) > tolerance) ||
			(abs(z - otherVector.z) > tolerance)
		);
	}
	
	// Manipulation Functions
	static Add = function(otherVector)
	{
		x += otherVector.x;
		y += otherVector.y;
		z += otherVector.z;
	}
	static Sub = function(otherVector)
	{
		x -= otherVector.x;
		y -= otherVector.y;
		z -= otherVector.z;
	}
	static Normalize = function()
	{
		var length = self.Length();
		if (length == 0) { return; }
		x /= length;
		y /= length;
		z /= length;
	}
	static Mult = function(otherVector)
	{
		x *= otherVector.x;
		y *= otherVector.y;
		z *= otherVector.z;
	}
	static Div = function(otherVector)
	{
		x /= otherVector.x;
		y /= otherVector.y;
		z /= otherVector.z;
	}
	static Scale = function(scalar)
	{
		return new Vec3(x * scalar, y * scalar, z * scalar);
	}
	static Shrink = function(scalar)
	{
		return new Vec3(x / scalar, y / scalar, z / scalar);
	}
	static Clamp = function(minVector, maxVector)
	{
		x = clamp(x, minVector.x, maxVector.x);
		y = clamp(y, minVector.y, maxVector.y);
		z = clamp(z, minVector.z, maxVector.z);
	}
	static Lerp = function(otherVector, amount)
	{
		x = lerp(x, otherVector.x, amount);
		y = lerp(y, otherVector.y, amount);
		z = lerp(z, otherVector.z, amount);
	}
	static AlignTo = function(alignmentScale)
	{
		x = round(x / alignmentScale) * alignmentScale;
		y = round(y / alignmentScale) * alignmentScale;
		z = round(z / alignmentScale) * alignmentScale;
	}
}

#region Vec3 Functions

function Vec3Copy(vector)
{
	return vector.Copy();
}
function Vec3FromVec2(vector2, z)
{
	return new Vec3(vector2.x, vector2.y, z);
}
function Vec3FromVec2xz(vector2, y)
{
	return new Vec3(vector2.x, y, vector2.y);
}

// Const Functions
function Vec3Length(vector)
{
	return vector.Length();
}
function Vec3Dist(left, right)
{
	return left.Dist(right);
}
function Vec3Dot(left, right)
{
	return left.Dot(right);
}
function Vec3Cross(left, right)
{
	return left.Dot(right);
}
function Vec3Equals(left, right)
{
	return left.Equals(right);
}
function Vec3NotEquals(left, right)
{
	return left.NotEquals(right);
}
function Vec3BasicallyEquals(left, right, tolerance)
{
	return left.BasicallyEquals(right, tolerance);
}
function Vec3BasicallyNotEquals(left, right, tolerance)
{
	return left.BasicallyNotEquals(right, tolerance);
}
function Vec3xy(vector)
{
	return new Vec2(vector.x, vector.y);
}
function Vec3xz(vector)
{
	return new Vec2(vector.x, vector.z);
}
function Vec3yz(vector)
{
	return new Vec2(vector.y, vector.z);
}
function Vec3yx(vector)
{
	return new Vec2(vector.y, vector.x);
}
function Vec3zx(vector)
{
	return new Vec2(vector.z, vector.x);
}
function Vec3zy(vector)
{
	return new Vec2(vector.z, vector.y);
}

// Manipulation functions
function Vec3Add(left, right)
{
	return new Vec3(left.x + right.x, left.y + right.y, left.z + right.z);
}
function Vec3Sub(left, right)
{
	return new Vec3(left.x - right.x, left.y - right.y, left.z - right.z);
}
function Vec3Normalize(vector)
{
	var result = vector.Copy();
	result.Normalize();
	return result;
}
function Vec3Mult(left, right)
{
	var result = left.Copy();
	result.Mult(right);
	return result;
}
function Vec3Div(left, right)
{
	var result = left.Copy();
	result.Div(right);
	return result;
}
function Vec3Scale(vector, scalar)
{
	var result = vector.Copy();
	result.Scale(scalar);
	return result;
}
function Vec3Shrink(vector, scalar)
{
	var result = vector.Copy();
	result.Shrink(scalar);
	return result;
}
function Vec3Clamp(vector, minVector, maxVector)
{
	var result = vector.Copy();
	result.Clamp(minVector, maxVector);
	return result;
}
function Vec3Lerp(startVector, endVector, amount)
{
	var result = startVector.Copy();
	result.Lerp(endVector, amount);
	return result;
}
function Vec3AlignTo(vector, alignmentScale)
{
	var result = vector.Copy();
	result.AlignTo(alignmentScale);
	return result;
}

#endregion
