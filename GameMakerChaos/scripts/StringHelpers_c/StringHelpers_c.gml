
function FindSubstring(str, substr)
{
	var strLength = string_length(str);
	var substrLength = string_length(substr);
	for (var cIndex = 0; cIndex + substrLength <= strLength; cIndex++)
	{
		var allMatched = true;
		for (var cIndex2 = 0; cIndex2 < substrLength; cIndex2++)
		{
			if (string_char_at(str, cIndex) != string_char_at(substr, cIndex2))
			{
				allMatched = false;
				break;
			}
		}
		if (allMatched)
		{
			return cIndex;
		}
	}
	return -1;
}

//Simply a wrapper because string_char_at first index is 1 but we usually work with 0 based indices
function StringCharAt(str, charIndex)
{
	return string_char_at(str, charIndex+1);
}

function StringPart(str, startIndex, length)
{
	var result = "";
	var strLength = string_length(str);
	if (startIndex >= strLength) { return ""; }
	if (startIndex + length > strLength) { length = strLength - startIndex; }
	for (var cIndex = 0; cIndex < length; cIndex++)
	{
		result += StringCharAt(str, startIndex + cIndex);
	}
	return result;
}

