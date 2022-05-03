/// @desc Door triggered

if (!self.heroJustCameThrough && self.targetRoom != noone)
{
	global.targetDoorNum = self.targetDoorNum;
	room_goto(self.targetRoom);
}

