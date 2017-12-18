using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour {

    public string level;
    bool loadGame = false;
    bool escapeGame = false;

	// Use this for initialization
	void Start () {
        loadGame = false;
        escapeGame = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Action"))
        {
            loadGame = true;
        }

        if (Input.GetButtonDown("Escape") && SceneManager.GetActiveScene().buildIndex == 0)
        {
            escapeGame = true;
        }
    }

    private void FixedUpdate()
    {
        if (loadGame)
        {
            loadGame = false;
            SceneManager.LoadScene(1);
        }

        if (escapeGame)
        {
            Application.Quit();
        }
    }
}
