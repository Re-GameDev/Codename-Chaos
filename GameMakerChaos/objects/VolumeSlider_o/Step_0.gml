/// @desc Handle mouse interactions

//TODO: We could handle clicks on the bar portion and jump the handle straight to that volume level
//      Right now we only handle grabbing the handle where it's currently at and dragging

if (self.firstUpdate)
{
	if (self.isMasterSlider) { self.currVolume = global.masterVolume; }
	if (self.isMusicSlider) { self.currVolume = global.musicVolume; }
	if (self.isSoundEffectSlider) { self.currVolume = global.soundEffectVolume; }
	self.firstUpdate = false
}

var previousVolume = self.currVolume;

if (mouse_x >= x && mouse_y >= y &&
	mouse_x <= x + sprite_width && mouse_y <= y + sprite_height)
{
	if (mouse_check_button_pressed(mb_left))
	{
		global.mouseClickHandled = true;
		if (!self.isSelected) { SelectMe(); }
	}
}

if (self.isSelected && keyboard_check_pressed(vk_left))
{
	self.currVolume = clamp(self.currVolume - 0.05, 0, 1);
}
if (self.isSelected && keyboard_check_pressed(vk_right))
{
	self.currVolume = clamp(self.currVolume + 0.05, 0, 1);
}

UpdateRecs();

if (mouse_x >= self.handlePos.x && mouse_x < self.handlePos.x + self.handleSize.x &&
	mouse_y >= self.handlePos.y && mouse_y < self.handlePos.y + self.handleSize.y)
{
	window_set_cursor(cr_handpoint);
	global.cursorSet = true;
	if (!self.isHandleGrabbed && mouse_check_button_pressed(mb_left))
	{
		self.isHandleGrabbed = true;
		self.handleGrabOffset = new Vec2(mouse_x - self.handlePos.x, mouse_y - self.handlePos.y);
		show_debug_message("Grab offset: (" + string(self.handleGrabOffset.x) + ", " + string(self.handleGrabOffset.y) + ")");
	}
}

if (self.isHandleGrabbed)
{
	window_set_cursor(cr_handpoint);
	global.cursorSet = true;
	if (mouse_check_button(mb_left))
	{
		var targetHandleX = mouse_x - self.handleGrabOffset.x;
		var handleMinX = x + (self.barOffsetX * image_xscale) - self.handleSize.x/2;
		var handleMaxX = x + (self.barOffsetX * image_xscale) + (self.barTotalWidth * image_xscale) - self.handleSize.x/2;
		var newHandleX = clamp(targetHandleX, handleMinX, handleMaxX);
		self.currVolume = (newHandleX - handleMinX) / (handleMaxX - handleMinX);
		self.currVolume = clamp(round(self.currVolume / 0.05) * 0.05, 0, 1);
	}
	else
	{
		self.isHandleGrabbed = false;
	}
}

if (self.currVolume != previousVolume)
{
	if (self.isMasterSlider) { global.masterVolume = self.currVolume; }
	if (self.isMusicSlider) { global.musicVolume = self.currVolume; }
	if (self.isSoundEffectSlider) { global.soundEffectVolume = self.currVolume; }
	
	if (self.isMasterSlider || self.isMusicSlider)
	{
		UpdateMusicVolume();
	}
	
	if (self.currVolume == 0.0 || self.currVolume == 1.0)
	{
		PlaySoundEffect(UiClick2_a);
	}
	else
	{
		PlaySoundEffect(UiClick1_a);
	}
}

