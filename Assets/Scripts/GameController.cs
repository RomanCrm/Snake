using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public Button exitButton;

	public Text scoreText;
	public int score;

	public int xBound, yBound;

	public int maxSize, currentSize;

	public GameObject foodPrefab;
	public GameObject currentFood;

	public GameObject snakePrefab;

	public Snake head, tail;

	public int direction;
	public Vector2 nextPos;

	public float deltaTimer;

	void OnEnable()
	{
		Snake.hit += hit;
	}

	void OnDisable()
	{
		Snake.hit -= hit;
	}

	void hit(string whatWasSent)
	{
		if(whatWasSent == "Food")
		{
			if (deltaTimer >= .1f)
			{
				deltaTimer -= .05f;
				CancelInvoke ("TimerInvoke");
				InvokeRepeating ("TimerInvoke", 0, deltaTimer);
			}
			FoodFunction ();
			maxSize++;
			score++;
			scoreText.text = score.ToString ();
			int tmp = PlayerPrefs.GetInt ("HighScore");
			if(score > tmp)
			{
				PlayerPrefs.SetInt ("HighScore", score);
			}
		}
		if(whatWasSent == "Snake")
		{
			CancelInvoke ("TimerInvoke");
			Exit ();
		}
	}

	public void Exit()
	{
		PlayerPrefs.SetInt ("LastScore", score);
		SceneManager.LoadScene (2);
	}

	// Use this for initialization
	void Start () 
	{
		exitButton.onClick.AddListener (Exit);

		InvokeRepeating ("TimerInvoke", 0, deltaTimer);
		FoodFunction ();
	}

	// Update is called once per frame
	void Update () 
	{
		ComputerChangeDirection ();
	}

	void TimerInvoke()
	{
		Movement ();
		StartCoroutine (CheckVisible());
		if (currentSize >= maxSize)
		{
			TailFunction ();
		}
		else
		{
			currentSize++;
		}
	}

	void Movement() 
	{
		GameObject tmp;
		nextPos = head.transform.position;

		switch(direction)
		{
		// Up
		case 0:
			nextPos = new Vector2(nextPos.x, nextPos.y + 1.0f);
			break;
			// Right
		case 1:
			nextPos = new Vector2(nextPos.x + 1.0f, nextPos.y);
			break;
			// Left
		case 2:
			nextPos = new Vector2(nextPos.x - 1.0f, nextPos.y);
			break;
			// Down
		case 3:
			nextPos = new Vector2(nextPos.x, nextPos.y - 1.0f);
			break;
		}
		tmp = (GameObject)Instantiate(snakePrefab, nextPos, transform.rotation);
		head.SetNext(tmp.GetComponent<Snake>());
		head = tmp.GetComponent<Snake> ();
	}

	void ComputerChangeDirection()
	{
		if (direction != 3 && Input.GetKey(KeyCode.UpArrow))
		{
			direction = 0;
		}
		if (direction != 0 && Input.GetKey(KeyCode.DownArrow))
		{
			direction = 3;
		}
		if (direction != 1 && Input.GetKey(KeyCode.LeftArrow))
		{
			direction = 2;
		}
		if (direction != 2 && Input.GetKey(KeyCode.RightArrow))
		{
			direction = 1;
		}
	}

	void TailFunction()
	{
		Snake tmpSnake = tail;
		tail = tail.GetNext ();
		tmpSnake.RemoveTail ();
	}

	void FoodFunction()
	{
		int xPos = Random.Range (-xBound, xBound);
		int yPos = Random.Range (-yBound, yBound);

		currentFood = (GameObject)Instantiate (foodPrefab, new Vector2 (xPos, yPos), transform.rotation);
		StartCoroutine (CheckRender (currentFood));
	}

	IEnumerator CheckRender(GameObject IN)
	{
		yield return new WaitForEndOfFrame();
		if(!IN.GetComponent<Renderer>().isVisible)
		{
			if(IN.tag == "Food")
			{
				Destroy (IN);
				FoodFunction ();
			}
		}
	}

	void Wrap()
	{
		// Up
		if (direction == 0)
		{
			head.transform.position = new Vector2 (head.transform.position.x, -(head.transform.position.y - 1));
		}
		// Right
		else if (direction == 1)
		{
			head.transform.position = new Vector2 (-(head.transform.position.x - 1), head.transform.position.y);
		}
		// Left
		else if (direction == 2)
		{
			head.transform.position = new Vector2 (-(head.transform.position.x + 1), head.transform.position.y);
		}
		// Down
		else if (direction == 3)
		{
			head.transform.position = new Vector2 (head.transform.position.x, -(head.transform.position.y + 1));
		}
	}

	IEnumerator CheckVisible()
	{
		yield return new WaitForEndOfFrame ();

		if (!head.GetComponent<Renderer> ().isVisible)
		{
			Wrap ();
		}
	}

}
