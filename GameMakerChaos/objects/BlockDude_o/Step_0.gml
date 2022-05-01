/// @desc Handle Input

if (self.gridSpace != 0)
{
	// #==============#
	// |     Down     |
	// #==============#
	if (keyboard_check_pressed(vk_down))
	{
		var gridSpaceInFront = GetBdGridSpace(new Vec2(self.gridPos.x + (self.isFacingLeft ? -1 : 1), self.gridPos.y));
		var gridSpaceInFrontUp = GetBdGridSpace(new Vec2(self.gridPos.x + (self.isFacingLeft ? -1 : 1), self.gridPos.y - 1));
		if (gridSpaceInFront != 0)
		{
			if (self.heldBlock != noone)
			{
				if (gridSpaceInFrontUp != 0 && !gridSpaceInFrontUp.IsSolid())
				{
					self.heldBlock.isPickedUp = false;
					gridSpaceInFrontUp.SetInstance(self.heldBlock);
					with (self.heldBlock)
					{
						event_perform(ev_other, ev_user1);
					}
					self.heldBlock = noone;
				}
			}
			else
			{
				if (gridSpaceInFront.IsBlock())
				{
					var gridSpaceUp = GetBdGridSpace(new Vec2(self.gridPos.x, self.gridPos.y - 1));
					if (gridSpaceUp != 0 && !gridSpaceUp.IsSolid())
					{
						self.heldBlock = gridSpaceInFront.PopOut();
						self.heldBlock.isPickedUp = true;
					}
				}
			}
		}
	}
	
	// #==============#
	// |     Left     |
	// #==============#
	else if (keyboard_check_pressed(vk_left))
	{
		self.isFacingLeft = true;
		var canMove = true;
		var gridSpaceLeft = GetBdGridSpace(new Vec2(self.gridPos.x - 1, self.gridPos.y));
		if (gridSpaceLeft == 0 || gridSpaceLeft.IsSolid()) { canMove = false; }
		if (self.heldBlock != noone)
		{
			var gridSpaceUpLeft = GetBdGridSpace(new Vec2(self.gridPos.x - 1, self.gridPos.y - 1));
			if (gridSpaceUpLeft == 0 || gridSpaceUpLeft.IsSolid()) { canMove = false; }
		}
		if (canMove)
		{
			show_debug_message("Moving left to x " + string(self.gridPos.x - 1));
			self.gridSpace.MoveInstanceTo(gridSpaceLeft);
		}
		else
		{
			show_debug_message("Can't move left to x " + string(self.gridPos.x - 1));
		}
	}
	
	// #==============#
	// |     Right    |
	// #==============#
	else if (keyboard_check_pressed(vk_right))
	{
		self.isFacingLeft = false;
		var canMove = true;
		var gridSpaceRight = GetBdGridSpace(new Vec2(self.gridPos.x + 1, self.gridPos.y));
		if (gridSpaceRight == 0 || gridSpaceRight.IsSolid()) { canMove = false; }
		if (self.heldBlock != noone)
		{
			var gridSpaceUpRight = GetBdGridSpace(new Vec2(self.gridPos.x + 1, self.gridPos.y - 1));
			if (gridSpaceUpRight == 0 || gridSpaceUpRight.IsSolid()) { canMove = false; }
		}
		if (canMove)
		{
			show_debug_message("Moving right to x " + string(self.gridPos.x + 1));
			self.gridSpace.MoveInstanceTo(gridSpaceRight);
		}
		else
		{
			show_debug_message("Can't move right to x " + string(self.gridPos.x + 1));
		}
	}
	
	// #==============#
	// |      Up      |
	// #==============#
	else if (keyboard_check_pressed(vk_up))
	{
		var canMove = true;
		var gridSpaceUp = GetBdGridSpace(new Vec2(self.gridPos.x + (self.isFacingLeft ? -1 : 1), self.gridPos.y - 1));
		if (gridSpaceUp == 0 || gridSpaceUp.IsSolid()) { canMove = false; }
		if (self.heldBlock != noone)
		{
			var gridSpaceUpUp = GetBdGridSpace(new Vec2(self.gridPos.x + (self.isFacingLeft ? -1 : 1), self.gridPos.y - 2));
			if (gridSpaceUpUp == 0 || gridSpaceUpUp.IsSolid()) { canMove = false; }
		}
		if (canMove)
		{
			show_debug_message("Moving up to y " + string(self.gridPos.y + 1));
			self.gridSpace.MoveInstanceTo(gridSpaceUp);
		}
		else
		{
			show_debug_message("Can't move up to y " + string(self.gridPos.y + 1));
		}
	}
	// #==============#
	// |     Reset    |
	// #==============#
	else if (keyboard_check_pressed(ord("R")) && !global.enteringCheatCode)
	{
		var roomArea = FindRoomAreaAtGridPos(self.gridPos);
		if (roomArea != noone)
		{
			with (roomArea)
			{
				event_perform(ev_other, ev_user0); //request area reset
			}
		}
	}
	
	// #==============#
	// |     Fall     |
	// #==============#
	var gridSpaceDown = GetBdGridSpace(new Vec2(self.gridPos.x, self.gridPos.y + 1));
	while (gridSpaceDown != 0 && !gridSpaceDown.IsSolid())
	{
		show_debug_message("falling to y " + string(self.gridPos.y + 1));
		self.gridSpace.MoveInstanceTo(gridSpaceDown);
		gridSpaceDown = GetBdGridSpace(new Vec2(self.gridPos.x, self.gridPos.y + 1));
	}
	
	// #==============#
	// |     Door     |
	// #==============#
	var doorBelowMe = instance_position(x+1, y+1, ExitDoor_o);
	if (doorBelowMe != noone)
	{
		show_debug_message("Found exit door!");
		with(doorBelowMe)
		{
			event_perform(ev_other, ev_user1);
		}
	}
	
	// Debug Teleport
	if (global.debugModeEnabled && self.gridSpace != 0 && keyboard_check_pressed(vk_home))
	{
		var roomArea = FindRoomAreaAtGridPos(self.gridPos);
		if (roomArea != noone && roomArea.exitDoor != noone)
		{
			self.gridSpace.MoveInstanceTo(GetBdGridSpace(roomArea.exitDoor.gridPos));
		}
	}
	
	if (self.heldBlock != noone)
	{
		self.heldBlock.x = self.x;
		self.heldBlock.y = self.y - global.bdTileSize.y;
		self.heldBlock.gridPos = new Vec2(self.gridPos.x, self.gridPos.y - 1);
	}
}
else if (!self.hasComplainedAboutNonPlacement)
{
	self.hasComplainedAboutNonPlacement = true;
	show_debug_message(
		"Dude is outside playable area!! " +
		"Or he didn't get picked up and placed in a grid space by the BlockDudeManager_o"
	);
}

