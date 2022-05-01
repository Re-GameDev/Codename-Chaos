function InitGlobalVars()
{
	global.TargetFramerate = 60;
	global.TargetFrametime = (1000 / global.TargetFramerate);
	global.ProgramTime = 0;
	global.ElapsedMs = 0;
	global.TimeScale = 0;
	global.mouseClickHandled = false;
	global.cursorSet = false;
	global.masterVolume = 0.8;
	global.musicVolume = 1.0;
	global.soundEffectVolume = 1.0;
	global.currentMusic = noone;
	global.currentMusicId = noone;
}