
function SetupRpgGlobals()
{
	global.noclipEnabled = false;
	global.numCoins = 0;
	global.targetDoorNum = -1;
	global.showingDialogue = false;
	global.currentDialogue = noone;
	global.collectedChestIds = [];
}

function ShowDialogue(text)
{
	if (global.currentDialogue != noone && global.currentDialogue.data != 0)
	{
		global.currentDialogue.data.Close();
	}
	global.currentDialogue = instance_create_layer(0, 0, "UI", RpgDialogue_o);
	global.currentDialogue.data = new Dialogue(DialogueFont_f, c_white, text, RpgDialogueBack2_s, 3);
	global.currentDialogue.data.Open();
	global.showingDialogue = true;
	return global.currentDialogue.data;
}

function ChestCollected(chestId)
{
	array_push(global.collectedChestIds, chestId);
}
function HasChestBeenCollected(chestId)
{
	for (var cIndex = 0; cIndex < array_length(global.collectedChestIds); cIndex++)
	{
		if (global.collectedChestIds[cIndex] == chestId) { return true; }
	}
	return false;
}

