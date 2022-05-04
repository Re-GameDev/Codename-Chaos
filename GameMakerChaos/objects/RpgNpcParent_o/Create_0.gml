/// @desc Init vars and create collisionObj

self.facingDir = Dir.Down;
self.isMoving = false;
self.aiType = RpgNpcAi.None;
self.aiUpdatePeriod = 0;
self.movementSpeed = 1;
self.lastDecisionTime = 0;
self.npc = 0;

self.collisionObj = instance_create_layer(x, y, "Collision", RpgSolidParent_o);

