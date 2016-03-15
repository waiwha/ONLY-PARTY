using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour {
	public GameObject myJoint;

	// Use this for initialization
	void Start () {
		switch(gameObject.name){
		case "rope":
			transform.position = new Vector3 (-.13f, 8.86f, -11.21f);
				break;
			case "rope (1)":
			transform.position = new Vector3 (1.18f,8.86f,-10.75f);
				break;
			case "rope (2)":
			transform.position = new Vector3 (2.9f,8.86f,-11.87f);
				break;
			case "rope (3)":
			transform.position = new Vector3 (4.95f,8.86f,-11.86f);
				break;
			case "rope (4)":
			transform.position = new Vector3 (5.01f,8.86f,-6.98f);
				break;
			case "rope (5)":
			transform.position = new Vector3 (-1.61f,8.86f,-8.88f);
				break;
			case "rope (6)":
			transform.position = new Vector3 (-6.13f,8.86f,-11.64f);
				break;
			case "rope (7)":
			transform.position = new Vector3 (-5.35f,8.86f,-9.95f);
				break;
			case "rope (8)":
			transform.position = new Vector3 (-3.31f,8.86f,-7.36f);
				break;
			default:
			//do nothing
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
