function InitGlobalVars()
{
	global.TargetFramerate = 60;
	global.TargetFrametime = (1000 / global.TargetFramerate);
	global.ProgramTime = 0;
	global.ElapsedMs = 0;
	global.TimeScale = 0;
}