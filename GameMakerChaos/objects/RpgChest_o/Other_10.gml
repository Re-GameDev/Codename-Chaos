/// @desc Chest Opened
event_inherited();

//TODO: Once we have a dialogue system, we should use that instead of show_message_async
if (self.heldItem != noone && self.heldCount > 0)
{
	if (self.heldCount == 1)
	{
		show_message_async("Collected " + self.heldItem.displayIndefArticle + " " + self.heldItem.displayName + "!");
	}
	else
	{
		show_message_async("Collected " + string(self.heldCount) + " " + self.heldItem.displayNamePlural + "!");
	}
	instance_destroy();
}
else
{
	show_message_async("The chest was empty...");
}

