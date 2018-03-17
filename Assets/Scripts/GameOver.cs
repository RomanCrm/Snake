using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		Invoke ("GameEnd", 1.0f);
	}
	
	void GameEnd()
	{
		SceneManager.LoadScene (0);
	}
}
