using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour {
	public GameObject pr_head;
	public GameObject pr_nose;
	public GameObject pr_torso;
	public GameObject pr_armL;
	public GameObject pr_armR;
	public GameObject pr_legs;


	private Sprite[] headSprites;
	private Sprite[] noseSprites;
	private Sprite[] torsoSprites;
	private Sprite[] armSprites;
	private Sprite[] legSprites;

	private float height;
	public char armOrient_L;
	public char armOrient_R;
	public char nosePos;
	public char torsoPos;

	private Sprite myHead;
	private Sprite myNose;
	private Sprite myTorso;
	private Sprite myArm_L;
	private Sprite myArm_R;
	private Sprite myLegs;

	private GameObject _head;
	private GameObject _nose;
	private GameObject _torso;
	private GameObject _armL;
	private GameObject _armR;
	private GameObject _legs;

	public Vector3 lookPos;
	public Vector3 lookDir;
	public Quaternion lookRot;
	public float rotDist;

	public Vector3 startPos;
	public Vector3 instancePos;

	private bool beenOnScreen;
	private Plane[] planes;


	// Use this for initialization
	void Start () {
		headSprites = Resources.LoadAll <Sprite>("_sprites/heads");
		noseSprites = Resources.LoadAll <Sprite>("_sprites/noses");
		torsoSprites = Resources.LoadAll <Sprite>("_sprites/torsos");
		armSprites = Resources.LoadAll <Sprite>("_sprites/arms");
		legSprites = Resources.LoadAll <Sprite>("_sprites/legs");

		DecideSprites ();
		FindHeight ();

		transform.position += new Vector3 (0f, height/2f, 0f);
		startPos = transform.position + new Vector3(0f,height/2f,0f);

		SetNosePosition ();
		SetTorsoPosition ();
		SetArmMovements ();
		CreateBodyPrefabs ();
		SetBodySprites ();

//		lookPos = new Vector3 (0f, height/2f, 0f);
//		lookDir = (lookPos - transform.position).normalized;
//		lookRot = Quaternion.LookRotation (lookDir);
//		rotDist = Quaternion.Angle (lookRot,transform.localRotation);
//
//		transform.Rotate (-rotDist * transform.up, Space.Self);
		transform.LookAt(Camera.main.transform);
	}
	
	// Update is called once per frame
	void Update () {
		planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);

		if (GeometryUtility.TestPlanesAABB(planes, GetComponent<MeshCollider>().bounds)) {
			beenOnScreen = true;
		}
		else {
			//Debug.Log ("not on screen");
			if (beenOnScreen) {
				PersonGenController.currPeople -= 1;
				Destroy (gameObject);
			}
		}

	}

	void DecideSprites(){
		myHead = headSprites [Random.Range (0, headSprites.Length)];
		myNose = noseSprites [Random.Range (0, noseSprites.Length)];
		myTorso = torsoSprites [Random.Range (0, torsoSprites.Length)];
		myArm_L = armSprites [Random.Range (0, armSprites.Length)];
		myArm_R = armSprites [Random.Range (0, armSprites.Length)];
		myLegs = legSprites [Random.Range (0, legSprites.Length)];
	}

	void FindHeight(){
		height += myHead.bounds.size.y*.75f;
		height += myTorso.bounds.size.y;
		height += myLegs.bounds.size.y*2;
		height -= .12f;
	}

	void CreateBodyPrefabs(){
		var headHeight = myHead.bounds.size.y;
		var headWidth = myHead.bounds.size.x;
		var torsoHeight = myTorso.bounds.size.y;
		var torsoWidth = myTorso.bounds.size.x;
		var legHeight = myLegs.bounds.size.y;
		var noseWidth = myNose.bounds.size.x;
		var armLWidth = myArm_L.bounds.size.x;
		var armLHeight = myArm_L.bounds.size.y;
		var armRWidth = myArm_R.bounds.size.x;
		var armRHeight = myArm_R.bounds.size.y;

		instancePos = startPos - new Vector3 (0f,headHeight/2f,0f);
		_head = Instantiate (pr_head, instancePos, pr_head.transform.rotation) as GameObject;

		if (nosePos == 'l') {
			_nose = Instantiate (pr_nose, instancePos - new Vector3 (headWidth / 2f + noseWidth*3f / 10f, 0f, 0f), pr_nose.transform.rotation) as GameObject;
			_nose.transform.localScale += new Vector3 (-2f, 0f, 0f);
		}
		else
			_nose = Instantiate (pr_nose, instancePos + new Vector3(headWidth / 2f + noseWidth*3f / 10f,0f,0f), pr_nose.transform.rotation) as GameObject;

		instancePos -= new Vector3 (0f,headHeight/4f+torsoHeight/2,0f);
		if (torsoPos == 'l') {
			_torso = Instantiate (pr_torso, instancePos, pr_torso.transform.rotation) as GameObject;
			_torso.transform.localScale += new Vector3 (-2f, 0f, 0f);
		}
		else
			_torso = Instantiate (pr_torso, instancePos, pr_torso.transform.rotation) as GameObject;

		if (armOrient_L == 'u')
			_armL = Instantiate (pr_armL, instancePos - new Vector3 (torsoWidth / 2f + armLWidth / 2f - .1f, -armLHeight / 2f, 0f), pr_armL.transform.rotation) as GameObject;
		else {
			_armL = Instantiate (pr_armL, instancePos - new Vector3 (torsoWidth / 2f + armLWidth / 2f - .1f - .2f, armLHeight / 2f - .5f, 0f), pr_armL.transform.rotation) as GameObject;
			_armL.transform.localScale += new Vector3 (0f, -2f, 0f);
		}
		
		if (armOrient_R == 'u')
			_armR = Instantiate (pr_armR, instancePos + new Vector3 (torsoWidth / 2f + armRWidth / 2f - .1f, armRHeight / 2f, 0f), pr_armR.transform.rotation) as GameObject;
		else {
			_armR = Instantiate (pr_armR, instancePos + new Vector3 (torsoWidth / 2f + armRWidth / 2f - .1f - .2f, -armRHeight / 2f + .5f, 0f), pr_armR.transform.rotation) as GameObject;
			_armR.transform.localScale += new Vector3 (0f, -2f, 0f);
		}

		instancePos -= new Vector3(0f, torsoHeight/2f+legHeight/2f, 0f);
		if (myLegs.name == "legs_down")
			_legs = Instantiate (pr_legs, instancePos + new Vector3 (0f, .12f, 0f), pr_legs.transform.rotation) as GameObject;
		else
			_legs = Instantiate (pr_legs, instancePos+new Vector3(.18f,.12f,0f), pr_legs.transform.rotation) as GameObject;
	}

	void SetBodySprites(){
		_head.GetComponent<SpriteRenderer> ().sprite = myHead;
		_head.transform.parent = transform;
		_nose.GetComponent<SpriteRenderer> ().sprite = myNose;
		_nose.transform.parent = transform;
		_torso.GetComponent<SpriteRenderer> ().sprite = myTorso;
		_torso.transform.parent = transform;
		_armL.GetComponent<SpriteRenderer> ().sprite = myArm_L;
		_armL.transform.parent = transform;
		_armR.GetComponent<SpriteRenderer> ().sprite = myArm_R;
		_armR.transform.parent = transform;
		_legs.GetComponent<SpriteRenderer> ().sprite = myLegs;
		_legs.transform.parent = transform;
	}

	void SetArmMovements(){
		var j = Random.Range (0, 2);

		//set left
		if (j == 0)
			armOrient_L = 'd';
		else
			armOrient_L = 'u';
			
		j = Random.Range (0, 2);

		//set right
		if (j == 0)
			armOrient_R = 'd';
		else
			armOrient_R = 'u';
	}

	void SetNosePosition(){
		var j = Random.Range (0, 2);

		//set left
		if (j == 0) {
			nosePos = 'l';
		} else
			nosePos = 'r';
	}

	void SetTorsoPosition(){
		var j = Random.Range (0, 2);

		//set left
		if (j == 0) {
			torsoPos = 'l';
		} else
			torsoPos = 'r';
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
