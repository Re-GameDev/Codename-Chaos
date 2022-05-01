/// @desc Update ProgramTime and others

global.ProgramTime = current_time;
global.ElapsedMs = delta_time / 1000;
global.TimeScale = global.ElapsedMs / global.TargetFrametime;

global.cursorSet = false;

global.mouseClickHandled = false;

