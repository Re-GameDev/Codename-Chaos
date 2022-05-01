function StopMusic()
{
	if (global.currentMusic != noone)
	{
		audio_stop_sound(global.currentMusic);
		global.currentMusic = noone;
		global.currentMusicId = noone;
	}
}

function PlayMusic(soundId, forceRestart = false)
{
	if (forceRestart || !(IsMusicPlaying() && global.currentMusicId == soundId))
	{
		StopMusic();
		global.currentMusicId = soundId;
		audio_sound_gain(soundId, global.masterVolume * global.musicVolume, 0);
		global.currentMusic = audio_play_sound(soundId, 0, true);
	}
}

function IsMusicPlaying()
{
	return (global.currentMusic != noone);
}

function UpdateMusicVolume()
{
	if (IsMusicPlaying())
	{
		audio_sound_gain(global.currentMusicId, global.masterVolume * global.musicVolume, 0);
	}
}

function PlaySoundEffect(soundId)
{
	audio_sound_gain(soundId, global.masterVolume * global.soundEffectVolume, 0);
	audio_play_sound(soundId, 0, false);
}

