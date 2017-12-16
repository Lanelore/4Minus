using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuebeAnimation : MonoBehaviour {

	Animator animator;

	void Awake(){
		animator = this.GetComponentInChildren<Animator> ();
		animator.speed = UnityEngine.Random.Range (1.5f, 2f);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
