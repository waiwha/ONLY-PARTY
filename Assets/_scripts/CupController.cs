using UnityEngine;
using System.Collections;

public class CupController : MonoBehaviour {
	public Camera player;
	public GameObject holder;

	public bool moving = false;
	public string dir = "up";
	private float origYPos;
	public bool started = false;
	public bool ended = false;

	// Use this for initialization
	void Start () {
		origYPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
