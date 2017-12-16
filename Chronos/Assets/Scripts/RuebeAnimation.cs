using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuebeAnimation : MonoBehaviour {

	Animator animator;

	void Awake(){
		animator = this.GetComponent<Animator> ();
		animator.speed = UnityEngine.Random.Range (0.9f, 1.1f);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
