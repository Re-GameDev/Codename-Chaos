/// @desc Reset requested

if (self.resetState != "")
{
	show_debug_message("Resetting to state " + string(self.gridSize.x) + " wide state:\n" + self.resetState);
	ApplySerializationToBdGridArea(self.resetState, self.gridPos, self.gridSize);
}

