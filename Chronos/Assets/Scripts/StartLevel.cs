using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour {

    public string level;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Action"))
            Application.LoadLevel(1);

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    public void LoadLevel()
    {
        
    }
}
