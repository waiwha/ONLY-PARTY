using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	public float rotSpeed;

	private Vector3 mySpot;
	private Vector3[] posSpots;
	private Quaternion lookRotation;
	private Vector3 dir;
	private bool spotHit = false;
	private bool spotFound = false;


	// Use this for initialization
	void Start () {
		posSpots = new Vector3[8];
		posSpots [0] = new Vector3 (0f,1f,4f);
		posSpots [1] = new Vector3 (4f,1f,4f);
		posSpots [2] = new Vector3 (-4f,1f,4f);
		posSpots [3] = new Vector3 (-4f,1f,0f);
		posSpots [4] = new Vector3 (4f,1f,0f);
		posSpots [5] = new Vector3 (0f,1f,-4f);
		posSpots [6] = new Vector3 (4f,1f,-4f);
		posSpots [7] = new Vector3 (-4f,1f,-4f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!spotFound) {
			mySpot = posSpots [Random.Range (0, posSpots.Length)];
			dir = (mySpot - transform.position).normalized;
			lookRotation = Quaternion.LookRotation (dir);
			spotFound = true;
		} else {
			if (!spotHit) {
				if (Quaternion.Angle(transform.rotation,lookRotation) <= 10f) {
					spotHit = true;
				}
				transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
			} else {
				spotFound = false;
				spotHit = false;
			}
		}
	}
		
}
