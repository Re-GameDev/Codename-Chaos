/// @desc Init vars and collisionObj

self.collisionObj = noone;
self.isDestroyed = false;

if (HasInstanceBeenCollected(self.id))
{
	self.isDestroyed = true;
	image_index = 1;
}
else
{
	self.collisionObj = instance_create_layer(x, y, "Collision", RpgSolidParent_o);
	if (self.collisionObj != noone)
	{
		self.collisionObj.collisionCallbackInstance = self.id;
	}
}

