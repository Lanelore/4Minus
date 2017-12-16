using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingKrautAnimation : MonoBehaviour {

    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float f = Input.GetAxis("Vertical");

        float speed = new Vector3(h, 0, f).magnitude;

        Debug.Log(speed);

        animator.SetFloat("speed",speed);

	}
}
