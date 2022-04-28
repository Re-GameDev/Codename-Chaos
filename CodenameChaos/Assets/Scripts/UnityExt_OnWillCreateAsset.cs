using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

// This script allows us to use a custom template for new behavior scripts that
// auto fills the author name (PC username) and creation date.
// You can add more auto-filling features here but if you want to change the template
// itself you will need to modify the file in the ScriptTemplates folder
//  - Taylor Robbins

#if UNITY_EDITOR
public class UnityExt_OnWillCreateAsset : UnityEditor.AssetModificationProcessor
{
	static void OnWillCreateAsset(string assetName)
	{
		string metaFilePath = assetName;
		if (!metaFilePath.EndsWith(".meta")) { return; }
		string assetFilePath = metaFilePath.Substring(0, metaFilePath.Length - 5);
		if (!assetFilePath.EndsWith(".cs")) { return; }
		
		string dateString = DateTime.Now.ToString("MMMM dd yyyy");
		string usernameString = Environment.UserName;
		
		string fileContents = File.ReadAllText(assetFilePath);
		
		fileContents = fileContents.Replace("#DATETIME#", dateString);
		fileContents = fileContents.Replace("#USERNAME#", usernameString);
		
		File.WriteAllText(assetFilePath, fileContents);
	}
}
#endif
