using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
			SceneManager.LoadScene ("PARTY_SCENE");

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}
}
