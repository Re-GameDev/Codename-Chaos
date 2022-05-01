/// @desc Go to gameplay room

if (self.targetRoom != noone)
{
	room_goto(self.targetRoom);
}
else
{
	show_debug_message(
		"This ScreenshotButton has not been assigned a room to go to! " +
		"Please add instance creation code and set self.targetRoom equal to something!"
	);
}

