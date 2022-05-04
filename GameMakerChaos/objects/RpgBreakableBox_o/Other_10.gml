/// @desc Collision callback

if (GetActiveBuffSum("move_speed") >= 2)
{
	if (self.collisionObj != noone)
	{
		instance_destroy(self.collisionObj);
		self.collisionObj = noone;
	}
	
	InstanceCollected(self.id);
	self.isDestroyed = true;
	image_index = 1;
	
	//TODO: Play a sound effect
}

