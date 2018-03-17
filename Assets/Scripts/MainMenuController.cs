using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public Text hS;
	public Text lS;
	public Button playButton;

	public Button exitButton;

	// Use this for initialization
	void Start ()
	{
		Button btnPlay = playButton.GetComponent<Button> ();
		btnPlay.onClick.AddListener(Play);

		Button btnExit = exitButton.GetComponent<Button> ();
		btnExit.onClick.AddListener (ExitGame);

		HSFunction ();
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void Play()
	{
		SceneManager.LoadScene (1);
	}

	public void HSFunction()
	{
		hS.text = PlayerPrefs.GetInt ("HighScore").ToString ();
		lS.text = PlayerPrefs.GetInt ("LastScore").ToString ();
	}
}
