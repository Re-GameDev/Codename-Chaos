/// @desc Chest Opened
event_inherited();

if (self.heldItem != noone && self.heldCount > 0)
{
	var dialogueStr = "";
	if (self.heldCount == 1)
	{
		dialogueStr = "Collected " + self.heldItem.displayIndefArticle + " " + self.heldItem.displayName + "!";
		
	}
	else
	{
		dialogueStr = "Collected " + string(self.heldCount) + " " + self.heldItem.displayNamePlural + "!";
	}
	var dialogue = ShowDialogue(dialogueStr);
	dialogue.leftSprite = self.heldItem.sprite_index;
	dialogue.leftSpriteScale = sprite_get_height(self.heldItem.sprite_index) / floor(dialogue.GetSize().y - dialogue.padding*dialogue.backScale);
	
	if (self.heldItem.object_index == RpgCoin_o)
	{
		global.numCoins += self.heldCount;
	}
	else if (self.heldItem.object_index == RpgLamp_o)
	{
		global.hasLantern = true;
	}
	
	InstanceCollected(self.id);
	
	instance_destroy(self.heldItem);
	instance_destroy();
}
else
{
	if (self.heldItem != noone) { instance_destroy(self.heldItem); }
	ShowDialogue("The chest was empty...");
}

