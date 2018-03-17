using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameBlinding : MonoBehaviour
{
	private Text nameTxt;
	private Outline outline;

	// Use this for initialization
	void Start ()
	{
		nameTxt = GetComponent<Text> ();
		outline = GetComponent<Outline> ();
	}

	// Update is called once per frame
	void Update ()
	{
		nameTxt.color = new Color (nameTxt.color.r, nameTxt.color.g, nameTxt.color.b, Mathf.PingPong (Time.time / 2.5f, 1.0f));
		outline.effectColor = new Color (outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, nameTxt.color.a - 0.3f);
	}
}
