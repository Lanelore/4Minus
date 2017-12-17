using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRueben : MonoBehaviour {
    public GameObject ruebe;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitTime(0.1f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WaitTime(float time)
    {
        float currentTime = 0.0f;


        print("time " + currentTime);

        do
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        // Do something after waiting a specific time
        print("spawn ruebe");
        GameObject createdDummy = GameObject.Instantiate(ruebe, this.transform.position, this.transform.rotation) as GameObject;
        
        // spawn something
        StartCoroutine(WaitTime(3));
    }
}
