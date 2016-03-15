using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour {
	public Camera player;

	public float rotSpeed;
	// Use this for initialization
	void Start () {
		rotSpeed = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!player.GetComponent<PlayerController>().drunk)
			transform.Rotate (transform.up * Time.deltaTime * rotSpeed);
	}
}
