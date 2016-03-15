using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerController : MonoBehaviour { 
	public GameObject baseParent;

	public MotionBlur sc_MotionBlur;

	public AudioReverbZone reverbZone;
	public float roomHF_min = -2000f;
	public float roomHF_max = 0f;
	public float decayTime_min = 1.49f;
	public float decayTime_max = 8.39f;
	public float decayHFratio_min = .5f;
	public float decayHFratio_max = 1.39f;
	public float reflections_min = -2466f;
	public float reflections_max = -115f;
	public float reflectionsDelay_min = .002f;
	public float reflectionsDelay_max = .179f;
	public float reverb_min = -1926f;
	public float reverb_max = 985f;
	public float reverbDelay_min = .03f;
	public float reverbDelay_max = .1f;
	public float diffusion_min = 21f;
	public float diffusion_max = 100f;

	public GameObject cup;
	public GameObject holder;

	public bool drinking = false;
	public bool drunk = false;
	private float timeElapsed = 0f;

	private float xRot;
	private float yRot;
	private float zRot;

	public float drunkLevel = 0f;
	public float drunkMax = 10f;
	public float maxBlur = .92f;

	private float sinceDrunk = 0f;

	// Use this for initialization
	void Start () {
		sc_MotionBlur = GetComponent<MotionBlur> ();
		reverbZone = GetComponent<AudioReverbZone> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && !drinking && !holder.GetComponent<HolderController>().activated && !drunk && drunkLevel < drunkMax) {
			drinking = true;
			drunkLevel += 1f;
			cup.GetComponent<CupController> ().started = true;
			cup.GetComponent<CupController> ().ended = false;
		}
			
		yRot = transform.localEulerAngles.y;

		if (drinking) {
				timeElapsed += Time.deltaTime;
				//gameObject.transform.localEulerAngles = new Vector3 (-Mathf.Pow(-(4f*(timeElapsed - 1.94f)) , 2f) + 60f, yRot, 0f);
				gameObject.transform.localEulerAngles = new Vector3 (60f * Mathf.Sin(timeElapsed), yRot, 0f);
			if (timeElapsed >= (2*Mathf.PI)) {
				drinking = false;
				timeElapsed = 0f;
				gameObject.transform.localEulerAngles = new Vector3 (0f, yRot, 0f);
			} else if (timeElapsed >= (.5f*Mathf.PI)) {
				holder.GetComponent<HolderController> ().activated = true;
			}
		}

		if(!drunk && drunkLevel < drunkMax)
			drunkLevel -= .0005f;

		if (drunkLevel <= 0f)
			drunkLevel = 0f;
		else if (drunkLevel >= drunkMax) {
			drunkLevel = drunkMax;
			if (!drunk && cup.GetComponent<CupController>().started == false && cup.GetComponent<CupController>().ended == true) {
				baseParent.GetComponent<Rigidbody> ().useGravity = true;
				baseParent.GetComponent<Rigidbody> ().AddForce (baseParent.transform.right);
				sinceDrunk += Time.deltaTime;
				if(sinceDrunk > .75f)
					drunk = true;
			}
		}

		UpdateBlur ();
		UpdateSound ();
	}

	void UpdateBlur(){
		sc_MotionBlur.blurAmount = Mathf.Pow(drunkLevel/(drunkMax/Mathf.Sqrt(maxBlur)),2);
	}

	void UpdateSound(){
		reverbZone.roomHF = (int)(((roomHF_max-roomHF_min)/100)*Mathf.Pow(drunkLevel,2f) + roomHF_min);
		reverbZone.decayTime = ((decayTime_max-decayTime_min)/100)*Mathf.Pow(drunkLevel,2f) + decayTime_min;
		reverbZone.decayHFRatio = ((decayHFratio_max-decayHFratio_min)/100)*Mathf.Pow(drunkLevel,2f) + decayHFratio_min;
		reverbZone.reflections = (int)(((reflections_max-reflections_min)/100)*Mathf.Pow(drunkLevel,2f) + reflections_min);
		reverbZone.reflectionsDelay = ((reflectionsDelay_min-reflectionsDelay_max)/100)*Mathf.Pow(drunkLevel,2f) + reflectionsDelay_max;
		reverbZone.reverb = (int)(((reverb_max-reverb_min)/100)*Mathf.Pow(drunkLevel,2f) + reverb_min);
		reverbZone.reverbDelay = ((reverbDelay_min-reverbDelay_max)/100)*Mathf.Pow(drunkLevel,2f) + reverbDelay_max;
		reverbZone.diffusion = ((diffusion_max-diffusion_min)/100)*Mathf.Pow(drunkLevel,2f) + diffusion_min;
	}
}
