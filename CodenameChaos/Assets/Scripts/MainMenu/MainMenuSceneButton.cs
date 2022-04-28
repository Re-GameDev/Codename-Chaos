using UnityEngine;

public class MainMenuSceneButton : MonoBehaviour
{
	public SceneField Scene;

	/// <summary> Immediately loads the Unity scene </summary>
	public void OnClick()
	{
		if (string.IsNullOrEmpty(Scene))
		{
			Debug.LogError($"{this.name} has no Scene selected for {nameof(MainMenuSceneButton)} script");
		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
		}
	}
}
