/// @desc Init vars
event_inherited();

self.targetRoom = noone;
self.displayStrColor = c_white;

// Dont change this. This is used to handle changes to our self variables made in the instance
// creation code by running some code on the first step that we run.
self.firstStep = true;

