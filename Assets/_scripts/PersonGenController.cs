using UnityEngine;
using System.Collections;

public class PersonGenController : MonoBehaviour {
	public GameObject base_player;
	public GameObject pr_person;

	public static int currPeople = 0;
	public static int maxPeople = 100;
	public bool gameStarted = false;

	// Use this for initialization
	void Start () {
		while (currPeople < maxPeople)
			CreatePerson ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currPeople < maxPeople)
			CreatePerson ();
		else if (currPeople == maxPeople)
			gameStarted = true;
	}

	void CreatePerson(){
		var minDistance = 4.5f;
		var maxDistance = 9f;
		var posAngle = 0f;
		var refAngle = 0f;
		var posDist = 0f;
		var personPos = Vector3.zero;

		if (gameStarted) {
			refAngle = Quaternion.Inverse (base_player.transform.rotation).eulerAngles.y - 90f;
			posAngle = Random.Range (refAngle - 120f, refAngle + 120f);
		} else {
			posAngle = Random.Range (0f, 360f);
		}

		posDist = Random.Range (minDistance, maxDistance);
		personPos = new Vector3 (posDist*Mathf.Cos(Mathf.Deg2Rad*posAngle),0f, posDist*Mathf.Sin(Mathf.Deg2Rad*posAngle));
		Instantiate (pr_person, personPos, pr_person.transform.rotation);
		++currPeople;
	}
}
