using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieOnTouch : MonoBehaviour {

    public GameObject geo;
    public float radius;
    public LayerMask mask;
    public GameObject expolsion;
    public GameObject ui;
    float levelTime = 0;
    bool gameRunning = true;
    double roundedTime = 0;
    public Text scoreText;

    bool alive = true;

	// Use this for initialization
	void Start () {
        levelTime = 0;
        gameRunning = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameRunning)
        {
            levelTime += Time.deltaTime;
        }

        Collider[] c = Physics.OverlapSphere(transform.position, radius, mask);

        if( c.Length > 0 && alive)
        {
            alive = false;
            geo.SetActive(false);
            gameRunning = false;
            RuebeAnimation.gameRunning = false;
            roundedTime = System.Math.Round(levelTime, 1);
            this.GetComponent<FirstPersonController>().enabled = false;
            StartCoroutine(WaitTime(1));           
        }
	}

    IEnumerator WaitTime(float time)
    {
        float currentTime = 0.0f;

        do
        {
            Destroy(GameObject.Instantiate(expolsion, this.transform.position, UnityEngine.Random.rotation) as GameObject, 2);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        // Do something after waiting a specific time 
        
        scoreText.text = "Zeit: " + roundedTime + "s\nKills: " + RuebeAnimation.deadRueben;

        ui.SetActive(true);
    }
}
