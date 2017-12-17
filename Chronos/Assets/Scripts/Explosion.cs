using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Explode(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Explode(other);
    }

    void Explode(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<RuebeAnimation>().Die();
        }
    }
}
