/// @desc Handle Movement and Collisions

if (!global.showingDialogue)
{
	var bboxOffset = new Vec2(bbox_left - x, bbox_top - y);
	var ourPos = new Vec2(bbox_left, bbox_top);
	var ourSize = new Vec2(bbox_right - bbox_left, bbox_bottom - bbox_top);
	
	// Handle Player Input
	var movementDir = new Vec2(0, 0);
	var movementSpeed = (keyboard_check(vk_shift) ? 1.60 : 1.0);
	movementSpeed *= 1 + GetActiveBuffSum("move_speed");
	if (keyboard_check(vk_right)) { movementDir.x += 1; facingDir = Dir.Right; }
	if (keyboard_check(vk_left))  { movementDir.x -= 1; facingDir = Dir.Left;  }
	if (keyboard_check(vk_down))  { movementDir.y += 1; facingDir = Dir.Down;  }
	if (keyboard_check(vk_up))    { movementDir.y -= 1; facingDir = Dir.Up;    }
	movementDir.Normalize();

	// Move the player
	ourPos.Add(Vec2Scale(movementDir, movementSpeed));

	//Handle Collsions
	if (!global.noclipEnabled)
	{
		var allSolids = FindAllInstancesOf(RpgSolidParent_o);
		var numSolids = array_length(allSolids);
		for (var sIndex = 0; sIndex < numSolids; sIndex++)
		{
			var solidPos = new Vec2(allSolids[sIndex].bbox_left, allSolids[sIndex].bbox_top);
			var solidSize = new Vec2(allSolids[sIndex].bbox_right - allSolids[sIndex].bbox_left, allSolids[sIndex].bbox_bottom - allSolids[sIndex].bbox_top);
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
	}

	// Check for pedestals
	self.onPedestals = false;
	var allPedestals = FindAllInstancesOf(RpgPedestal_o);
	var ourFootPos = new Vec2(ourPos.x + ourSize.x/2, ourPos.y + ourSize.y);
	for (var pIndex = 0; pIndex < array_length(allPedestals); pIndex++)
	{
		var pedestalPos = new Vec2(allPedestals[pIndex].bbox_left, allPedestals[pIndex].bbox_top);
		var pedestalSize = new Vec2(allPedestals[pIndex].bbox_right - allPedestals[pIndex].bbox_left, allPedestals[pIndex].bbox_bottom - allPedestals[pIndex].bbox_top);
		if (ourFootPos.x > pedestalPos.x &&
			ourFootPos.y > pedestalPos.y &&
			ourFootPos.x < pedestalPos.x + pedestalSize.x &&
			ourFootPos.y < pedestalPos.y + pedestalSize.y)
		{
			self.onPedestals = true;
			break;
		}
	}

	// Check for doors
	var allDoors = FindAllInstancesOf(RpgDoor_o);
	for (var dIndex = 0; dIndex < array_length(allDoors); dIndex++)
	{
		var door = allDoors[dIndex];
		var doorPos = new Vec2(door.bbox_left, door.bbox_top);
		var doorSize = new Vec2(door.bbox_right - door.bbox_left, door.bbox_bottom - door.bbox_top);
		if (ourPos.x + ourSize.x > doorPos.x &&
			ourPos.y + ourSize.y > doorPos.y &&
			ourPos.x < doorPos.x + doorSize.x &&
			ourPos.y < doorPos.y + doorSize.y)
		{
			with (door) { event_perform(ev_other, ev_user0); } // trigger the door
		}
		else
		{
			door.heroJustCameThrough = false;
		}
	}

	//Handle Interact Key
	if (keyboard_check_pressed(vk_enter) || keyboard_check_pressed(vk_space))
	{
		var targetDirVec = Vec2Mult(Vec2FromDir(self.facingDir), new Vec2(sprite_width, sprite_height));
		var targetPos = new Vec2(x + sprite_width/2 + targetDirVec.x, y + sprite_height/2 + targetDirVec.y);
		var targetInstance = instance_position(targetPos.x, targetPos.y, RpgInteractableParent_o);
		if (targetInstance != noone)
		{
			with (targetInstance)
			{
				event_perform(ev_other, ev_user0); //trigger the interactable
			}
		}
	}

	//Update walking animation
	var walkingImageIndexOffset = (movementDir.Length() > 0) ? ((global.ProgramTime % 500) >= 250 ? 1 : 0) : 0;
	switch (self.facingDir)
	{
		case Dir.Down:  image_index = 0 + walkingImageIndexOffset; break;
		case Dir.Right: image_index = 2 + walkingImageIndexOffset; break;
		case Dir.Left:  image_index = 4 + walkingImageIndexOffset; break;
		case Dir.Up:    image_index = 6 + walkingImageIndexOffset; break;
	}

	// Apply ourPos changes
	x = ourPos.x - bboxOffset.x;
	y = ourPos.y - bboxOffset.y;
}

