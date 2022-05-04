/// @desc Find all NPCs

global.npcs = FindAllInstancesOf(RpgNpcParent_o);
for (var nIndex = 0; nIndex < array_length(global.npcs); nIndex++)
{
	global.npcs[nIndex].npc = new RpgNpc(global.npcs[nIndex]);
}

