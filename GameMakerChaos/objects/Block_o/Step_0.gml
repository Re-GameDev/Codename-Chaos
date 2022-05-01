/// @desc Fall

if (!self.isPickedUp && self.gridSpace != 0)
{
	var gridSpaceDown = GetBdGridSpace(new Vec2(self.gridPos.x, self.gridPos.y + 1));
	while (gridSpaceDown != 0 && !gridSpaceDown.IsSolid())
	{
		//show_debug_message("falling to y " + string(self.gridPos.y + 1));
		self.gridSpace.MoveInstanceTo(gridSpaceDown);
		gridSpaceDown = GetBdGridSpace(new Vec2(self.gridPos.x, self.gridPos.y + 1));
	}
}
else if (!self.isPickedUp)
{
	show_debug_message(
		"Block is outside playable area!! " +
		"Or it didn't get picked up and placed in a grid space by the BlockDudeManager_o"
	);
}

