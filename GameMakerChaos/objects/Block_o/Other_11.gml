/// @desc Block placed

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
else
{
	show_debug_message("Block couldn't fall in user event 1 because it was still picked up or not in a gridSpace");
}
