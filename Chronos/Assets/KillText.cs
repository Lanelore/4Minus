using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowKill(int kill)
    {
        if (DieOnTouch.gameRunning == false)
        {
            return;
        }

        string killAmount = "";
        if (kill == 1)
        {
            killAmount = "One Kill!";
        }
            if (kill == 2)
        {
            killAmount = "Double Kill!";
        } else if (kill == 3)
        {
            killAmount = "Triple Kill!";
        } else if (kill == 4)
        {
            killAmount = "Quadruple Kill!";
        } else if (kill >= 5)
        {
            killAmount = "Massaker!";
        }

        this.GetComponent<Text>().text = killAmount;

        StopCoroutine(WaitTime(1));
        StartCoroutine(WaitTime(1));
    }

    IEnumerator WaitTime(float time)
    {
        float currentTime = 0.0f;

        do
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        // Do something after waiting a specific time
        this.GetComponent<Text>().text = "";
    }
}
