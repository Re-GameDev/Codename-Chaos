function FindAllInstancesOf(objectId)
{
	var instanceCount = 0;
	var result = [];
	var nextInstance = instance_find(objectId, instanceCount);
	while (nextInstance != noone)
	{
		array_push(result, nextInstance);
		instanceCount += 1;
		nextInstance = instance_find(objectId, instanceCount);
	}
	return result;
}
