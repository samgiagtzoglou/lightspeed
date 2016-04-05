using UnityEngine;
using System.Collections;

public class SceneConfig : MonoBehaviour {
	public static int players;
	public static int score;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

}