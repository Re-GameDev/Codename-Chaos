/// @desc Handle being clicked on

//NOTE: We delay the event firing for 1 frame so we can render the clicked sprite before things change
//      For example if the button takes us to another room, it's more satisfying if we see the button we
//      clicked go down for at least 1 frame before loading/transitioning to that room
if (self.doEventNextFrame)
{
	event_perform(ev_other, ev_user0);
	self.doEventNextFrame = false;
}

if (mouse_check_button_pressed(mb_left) && self.isHovered)
{
	self.isPressed = true;
	image_index = 2;
	self.doEventNextFrame = true;
}

if (self.isPressed && !(mouse_check_button(mb_left) && self.isHovered))
{
	self.isPressed = false;
	image_index = 0;
}

