/// @desc Consume the item
event_inherited();

if (self.item != noone)
{
	if (self.dialogueMessage != "")
	{
		var dialogue = ShowDialogue(self.dialogueMessage);
		dialogue.leftSprite = self.item.sprite_index;
	}
	with (self.item)
	{
		event_perform(ev_other, ev_user0); //Consume the item
	}
	instance_destroy(self.item);
}

instance_destroy();

