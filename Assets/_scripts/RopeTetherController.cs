using UnityEngine;
using System.Collections;

public class RopeTetherController : MonoBehaviour {
	private int swingDir;
	private int swingMag;
	private float swingSpeed;

	// Use this for initialization
	void Start () {
		switch (gameObject.name) {
		case "rope_tether_0":
			transform.position += new Vector3 (0f,0f,-.88f);
			break;
		case "rope_tether_1":
			transform.position += new Vector3 (0f,0f,2.93f);
			break;
		case "rope_tether_2":
			transform.position += new Vector3 (0f,0f,2.8f);
			break;
		case "rope_tether_3":
			transform.position += new Vector3 (0f,0f,.71f);
			break;
		case "rope_tether_4":
			transform.position += new Vector3 (0f,0f,2.41f);
			break;
		case "rope_tether_5":
			transform.position += new Vector3 (0f,0f,3.81f);
			break;
		case "rope_tether_6":
			transform.position += new Vector3 (0f,0f,2.01f);
			break;
		case "rope_tether_7":
			transform.position += new Vector3 (0f,0f,3.54f);
			break;
		case "rope_tether_8":
			transform.position += new Vector3 (0f,0f,-.47f);
			break;
		default:
			break;
		}

		swingDir = Random.Range (0, 2);

		if (swingDir == 0)
			swingMag = 1;
		else
			swingMag = -1;

		swingSpeed = Random.Range (1f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (swingMag*.5f*Mathf.Sin(Time.time*swingSpeed),transform.position.y,transform.position.z);
	}
}
