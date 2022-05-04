/// @desc Update NPC Logic

if (self.npc != 0 && !global.showingDialogue && !global.enteringCheatCode)
{
	if (self.npc.IsPathing())
	{
		self.lastDecisionTime = global.ProgramTime;
	}
	else if (global.ProgramTime >= self.lastDecisionTime + self.aiUpdatePeriod)
	{
		self.npc.MakeDecision();
		self.lastDecisionTime = global.ProgramTime;
	}
	
	self.npc.Update();
}

