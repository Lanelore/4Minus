using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRueben : MonoBehaviour {

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

        do
        {
            yield return null;
        }
        while (currentTime <= time);

        // Do something after waiting a specific time

        // spawn something
        StartCoroutine(WaitTime(3));
    }
}
