/// @desc Destroy back

if (self.backInstance != noone)
{
	instance_destroy(self.backInstance);
	self.backInstance = noone;
}

