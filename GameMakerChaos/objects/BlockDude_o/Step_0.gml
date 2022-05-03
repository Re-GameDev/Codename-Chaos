/// @desc Handle Input

function FirePortal(dir, isOrange)
{
	var hitPos = self.gridPos.Copy();
	var dirVec = Vec2FromDir(dir);
	var nextGridSpace = GetBdGridSpace(Vec2Add(hitPos, dirVec));
	while (nextGridSpace != 0 && !nextGridSpace.IsSolid())
	{
		hitPos.Add(dirVec);
		nextGridSpace = GetBdGridSpace(Vec2Add(hitPos, dirVec));
	}
		
	var newGridSpace = GetBdGridSpace(hitPos);
	if (newGridSpace != 0 && newGridSpace.GetPortalOnSide(dir) == 0)
	{
		var newPortalInstance = instance_create_layer(0, 0, "Portals", Portal_o);
		var newBdPortal = newGridSpace.SetPortalOnSide(newPortalInstance, (isOrange ? self.bluePortal : self.orangePortal), dir);
		if (isOrange)
		{
			if (self.orangePortal != noone)
			{
				self.orangePortal.gridSpace.ClearPortalOnSide(self.orangePortal.inDir);
			}
			self.orangePortal = newPortalInstance;
			self.orangePortal.sprite_index = PortalOrange_s;
			if (self.bluePortal != noone)
			{
				self.bluePortal.gridSpace.ConnectPortalOnSide(self.bluePortal.inDir, newBdPortal);
			}
		}
		else
		{
			if (self.bluePortal != noone)
			{
				self.bluePortal.gridSpace.ClearPortalOnSide(self.bluePortal.inDir);
			}
			self.bluePortal = newPortalInstance;
			self.bluePortal.sprite_index = PortalBlue_s;
			if (self.orangePortal != noone)
			{
				self.orangePortal.gridSpace.ConnectPortalOnSide(self.orangePortal.inDir, newBdPortal);
			}
		}
	}
}

if (self.gridSpace != 0)
{
	// #==============#
	// |     Down     |
	// #==============#
	if (keyboard_check_pressed(vk_down))
	{
		var gridSpaceUp = self.gridSpace.GetGridSpaceInDir(Dir.Up);
		var gridSpaceForward = self.gridSpace.GetGridSpaceInDir(self.isFacingLeft ? Dir.Left : Dir.Right);
		if (gridSpaceUp != 0)
		{
			var gridSpaceUpForward = gridSpaceUp.GetGridSpaceInDir(self.isFacingLeft ? Dir.Left : Dir.Right);
			if (self.heldBlock != noone)
			{
				//Try drop block
				if (gridSpaceUpForward != 0 && !gridSpaceUpForward.IsSolid() && !gridSpaceUpForward.IsDude())
				{
					self.heldBlock.isPickedUp = false;
					gridSpaceUpForward.SetInstance(self.heldBlock);
					with (self.heldBlock)
					{
						event_perform(ev_other, ev_user1);
					}
					self.heldBlock = noone;
				}
				else if (!gridSpaceForward.IsSolid() && !gridSpaceForward.IsDude())
				{
					self.heldBlock.isPickedUp = false;
					gridSpaceForward.SetInstance(self.heldBlock);
					with (self.heldBlock)
					{
						event_perform(ev_other, ev_user1);
					}
					self.heldBlock = noone;
				}
			}
			else
			{
				//Try pickup block
				if (gridSpaceForward.IsBlock() && gridSpaceUp != 0 && !gridSpaceUp.IsSolid() && !gridSpaceUp.IsDude())
				{
					self.heldBlock = gridSpaceForward.PopOut();
					self.heldBlock.isPickedUp = true;
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
		var gridSpaceLeft = self.gridSpace.GetGridSpaceInDir(Dir.Left);
		var gridSpaceLeftDir = self.gridSpace.GetDirAfterMovingInDir(Dir.Left);
		if (gridSpaceLeft == 0 || gridSpaceLeft.IsSolid()) { canMove = false; }
		if (self.heldBlock != noone && canMove)
		{
			var gridSpaceUpLeft = gridSpaceLeft.GetGridSpaceInDir(Dir.Up);
			if (gridSpaceUpLeft == 0 || gridSpaceUpLeft.IsSolid()) { canMove = false; }
		}
		if (canMove)
		{
			show_debug_message("Moving left to x " + string(self.gridPos.x - 1));
			if (!gridSpaceLeft.gridPos.Equals(self.gridPos))
			{
				self.gridSpace.MoveInstanceTo(gridSpaceLeft);
			}
			self.isFacingLeft = (gridSpaceLeftDir == Dir.Left);
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
		var gridSpaceRight = self.gridSpace.GetGridSpaceInDir(Dir.Right);
		var gridSpaceRightDir = self.gridSpace.GetDirAfterMovingInDir(Dir.Right);
		if (gridSpaceRight == 0 || gridSpaceRight.IsSolid()) { canMove = false; }
		if (self.heldBlock != noone)
		{
			var gridSpaceUpRight = gridSpaceRight.GetGridSpaceInDir(Dir.Up);
			if (gridSpaceUpRight == 0 || gridSpaceUpRight.IsSolid()) { canMove = false; }
		}
		if (canMove)
		{
			show_debug_message("Moving right to x " + string(self.gridPos.x + 1));
			if (!gridSpaceRight.gridPos.Equals(self.gridPos))
			{
				self.gridSpace.MoveInstanceTo(gridSpaceRight);
			}
			self.isFacingLeft = (gridSpaceRightDir == Dir.Left);
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
		var headingDir = (isFacingLeft ? Dir.Left : Dir.Right);
		var gridSpaceUp = self.gridSpace.GetGridSpaceInDir(Dir.Up);
		var gridSpaceUpForward = (gridSpaceUp != 0) ? gridSpaceUp.GetGridSpaceInDir(headingDir) : 0;
		var gridSpaceUpForwardDir = (gridSpaceUp != 0) ? gridSpaceUp.GetDirAfterMovingInDir(headingDir) : headingDir;
		if (gridSpaceUpForward == 0 || gridSpaceUpForward.IsSolid()) { canMove = false; }
		if (self.heldBlock != noone)
		{
			var gridSpaceUpForwardUp = gridSpaceUpForward.GetGridSpaceInDir(Dir.Up);
			if (gridSpaceUpForwardUp == 0 || gridSpaceUpForwardUp.IsSolid()) { canMove = false; }
		}
		if (canMove)
		{
			show_debug_message("Moving up to y " + string(self.gridPos.y + 1));
			if (!gridSpaceUpForward.gridPos.Equals(self.gridPos))
			{
				self.gridSpace.MoveInstanceTo(gridSpaceUpForward);
			}
			self.isFacingLeft = (gridSpaceUpForwardDir == Dir.Left);
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
	
	// #====================#
	// |    Portal Right    |
	// #====================#
	else if (keyboard_check_pressed(ord("Z")) && !global.enteringCheatCode && global.hasPortalGun)
	{
		FirePortal((self.isFacingLeft ? Dir.Left : Dir.Right), false);
	}
	
	// #====================#
	// |    Portal Left     |
	// #====================#
	else if (keyboard_check_pressed(ord("X")) && !global.enteringCheatCode && global.hasPortalGun)
	{
		FirePortal((self.isFacingLeft ? Dir.Left : Dir.Right), true);
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

