using UnityEngine;
using System.Collections;

public class HolderController : MonoBehaviour {
	public GameObject cup;

	public bool activated = false;
	private float timeElapsed = Mathf.PI / 2f;

	private float xRot;
	private float yRot;
	private float zRot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		yRot = transform.localEulerAngles.y;
		if (activated) {
			timeElapsed += Time.deltaTime;
			gameObject.transform.localEulerAngles = new Vector3 (60f * Mathf.Sin(timeElapsed), yRot, 0f);

			if (timeElapsed >= (5 * Mathf.PI) / 2f) {
				timeElapsed = Mathf.PI / 2f;
				gameObject.transform.localEulerAngles = new Vector3 (60f, yRot, 0f);
				cup.GetComponent<CupController> ().moving = false;
				cup.GetComponent<CupController> ().started = false;
				cup.GetComponent<CupController> ().ended = true;
				activated = false;
			} else {
				cup.GetComponent<CupController> ().moving = true;
			}
		}
	}
}
