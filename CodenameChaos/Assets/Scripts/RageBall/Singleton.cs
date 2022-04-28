// Creation Date: January 15 2022
// Author(s): Jordan Bejar

using UnityEngine;

[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	internal static T _instance;
	static object _lock = new object();
	static bool applicationIsQuitting = false;

	public static T instance
	{
		get 
		{
			if( applicationIsQuitting )
			{
				Debug.LogWarning( $"[Singleton] Instance {typeof(T)} already destroyed on application quit. Won't create again - returning null.");
				return null;
			}
			lock(_lock)
			{
				if( _instance == null )
				{
					_instance = (T)FindObjectOfType(typeof(T));

					if( FindObjectsOfType(typeof(T)).Length > 1 )
					{
						return _instance;
					}

					if( _instance == null )
					{
						_instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
					}
				}
			}

			return _instance;
		}
	}
	
	public static bool Exist() => _instance != null;

	private static bool IsDontDestroyOnLoad()
	{
		if(_instance == null )
			return false;
		return _instance.gameObject.hideFlags.HasFlag( HideFlags.DontSave );
	}

	void OnDestroy()
	{
		if( IsDontDestroyOnLoad())
			applicationIsQuitting = true;
	}
}
