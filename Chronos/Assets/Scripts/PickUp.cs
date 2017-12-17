using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<CrystalSystem>().CollectCrystal();

            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Renderer>().enabled = false;

            print("pickup and disable");
            StartCoroutine(WaitTime(Random.Range(1, 5)));
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
        
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<Renderer>().enabled = true;
    }
}