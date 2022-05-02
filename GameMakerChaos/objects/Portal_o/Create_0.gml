/// @desc Init vars

self.gridPos = new Vec2(0, 0);
self.inDir = Dir.Down;
self.gridSpace = 0;
self.otherInstance = noone;

//NOTE: In order to draw half of our portal below objects in the world and the other half over
//      we have to make another object instance that resides on another layer at a differnt depth
//      that's sole purpose is to replicate our state and draw half our portal
self.backInstance = instance_create_layer(x, y, "PortalBacks", PortalBack_o);

