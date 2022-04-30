/// @desc First step handling
event_inherited();

// Handle changes made to our self vars by instance creation code
if (self.firstStep)
{
	// Automatically set displayStr to room name if it wasn't set to something by instance creation code
	if (self.displayStr == "" && self.targetRoom != noone)
	{
		self.displayStr = room_get_name(self.targetRoom);
	}
	
	self.firstStep = false;
}

