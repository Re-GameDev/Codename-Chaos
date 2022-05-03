/// @desc Init vars, FillWith func, and collisionObj creation
event_inherited();

self.heldItem = noone;
self.heldCount = 1;
self.hintText = "Open Chest";

function FillWith(itemObject, count = 1)
{
	self.heldItem = instance_create_layer(-1000, -1000, "Items", itemObject);
	self.heldCount = count;
}

self.collisionObj = instance_create_layer(x, y, "Collision", RpgSolidParent_o);

if (HasChestBeenCollected(self.id))
{
	instance_destroy();
}

