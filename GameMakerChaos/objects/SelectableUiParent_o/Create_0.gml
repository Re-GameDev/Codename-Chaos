/// @desc isSelected and SelectMe function

self.isSelected = false;

function SelectMe()
{
	var allSelectables = FindAllInstancesOf(SelectableUiParent_o);
	self.isSelected = true;
	for (var sIndex = 0; sIndex < array_length(allSelectables); sIndex++)
	{
		if (allSelectables[sIndex] != self.id)
		{
			allSelectables[sIndex].isSelected = false;
		}
	}
}

