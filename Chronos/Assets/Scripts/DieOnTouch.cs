using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnTouch : MonoBehaviour {

    public GameObject geo;
    public float radius;
    public LayerMask mask;
    public GameObject expolsion;
    public GameObject ui;

    bool alive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Collider[] c = Physics.OverlapSphere(transform.position, radius, mask);

        if( c.Length > 0 && alive)
        {
            Destroy(GameObject.Instantiate(expolsion, this.transform.position,transform.rotation) as GameObject, 2);


            alive = false;
            geo.SetActive(false);
            this.GetComponent<FirstPersonController>().enabled = false;
            StartCoroutine(WaitTime(1));

            
            
        }

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
        ui.SetActive(true);
    }
}
