function Oscillate(valueMin, valueMax, periodMs, offset)
{
	return lerp(valueMin, valueMax, sin((((global.ProgramTime + offset) % periodMs) / periodMs) * 2*pi)/2 + 0.5);
}

function IsFlagSet(packedFlags, flag)
{
	return ((packedFlags & flag) != 0);
}
function IsFlagUnset(packedFlags, flag)
{
	return ((packedFlags & flag) == 0);
}

function FlagSet(packedFlags, flag)
{
	return (packedFlags | flag);
}
function FlagUnset(packedFlags, flag)
{
	return (packedFlags & (~flag));
}
function FlagToggle(packedFlags, flag)
{
	return (IsFlagSet(packedFlags, flag) ? FlagUnset(packedFlags, flag) : FlagSet(packedFlags, flag));
}
function FlagSetTo(packedFlags, flag, enabled)
{
	return (enabled ? FlagSet(packedFlags, flag) : FlagUnset(packedFlags, flag));
}
