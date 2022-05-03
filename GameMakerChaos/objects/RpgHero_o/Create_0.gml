/// @desc Init vars and create RpgController_o

self.controller = instance_find(RpgController_o, 0);
if (self.controller == noone)
{
	show_debug_message("Creating the RPG Controller");
	self.controller = instance_create_layer(0, 0, "Controllers", RpgController_o);
}

self.facingDir = Dir.Down;
self.onPedestals = false;

