/// @desc Handle Movement and Collisions

var ourPos = new Vec2(bbox_left, bbox_top);
var bboxOffset = new Vec2(bbox_left - x, bbox_top - y);
var ourSize = new Vec2(bbox_right - bbox_left, bbox_bottom - bbox_top);


var movementDir = new Vec2(0, 0);
var movementSpeed = (keyboard_check(vk_shift) ? 1.60 : 1.0);
if (keyboard_check(vk_right)) { movementDir.x += 1; facingDir = Dir.Right; }
if (keyboard_check(vk_left))  { movementDir.x -= 1; facingDir = Dir.Left;  }
if (keyboard_check(vk_down))  { movementDir.y += 1; facingDir = Dir.Down;  }
if (keyboard_check(vk_up))    { movementDir.y -= 1; facingDir = Dir.Up;    }
movementDir.Normalize();

ourPos.Add(Vec2Scale(movementDir, movementSpeed));

//Handle Collsions
var allSolids = FindAllInstancesOf(RpgSolidParent_o);
var numSolids = array_length(allSolids);
for (var sIndex = 0; sIndex < numSolids; sIndex++)
{
	var solidPos = new Vec2(allSolids[sIndex].x, allSolids[sIndex].y);
	var solidSize = new Vec2(allSolids[sIndex].sprite_width, allSolids[sIndex].sprite_height);
	if (ourPos.x + ourSize.x > solidPos.x &&
		ourPos.y + ourSize.y > solidPos.y &&
		ourPos.x < solidPos.x + solidSize.x &&
		ourPos.y < solidPos.y + solidSize.y)
	{
		var overlapPosX = solidPos.x + solidSize.x - ourPos.x;
		var overlapNegX = ourPos.x + ourSize.x - solidPos.x;
		var overlapPosY = solidPos.y + solidSize.y - ourPos.y;
		var overlapNegY = ourPos.y + ourSize.y - solidPos.y;
		if (min(overlapPosX, overlapNegX) <= min(overlapPosY, overlapNegY))
		{
			if (overlapPosX <= overlapNegX)
			{
				ourPos.x += overlapPosX;
			}
			else
			{
				ourPos.x -= overlapNegX;
			}
		}
		else
		{
			if (overlapPosY <= overlapNegY)
			{
				ourPos.y += overlapPosY;
			}
			else
			{
				ourPos.y -= overlapNegY;
			}
		}
	}
}

var walkingImageIndexOffset = (movementDir.Length() > 0) ? ((global.ProgramTime % 500) >= 250 ? 1 : 0) : 0;
switch (self.facingDir)
{
	case Dir.Down:  image_index = 0 + walkingImageIndexOffset; break;
	case Dir.Right: image_index = 2 + walkingImageIndexOffset; break;
	case Dir.Left:  image_index = 4 + walkingImageIndexOffset; break;
	case Dir.Up:    image_index = 6 + walkingImageIndexOffset; break;
}

x = ourPos.x - bboxOffset.x;
y = ourPos.y - bboxOffset.y;

