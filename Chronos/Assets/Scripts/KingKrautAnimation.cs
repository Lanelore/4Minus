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
        if (animator.gameObject.activeSelf)
        {
            float h = Input.GetAxis("Horizontal");
            float f = Input.GetAxis("Vertical");

            float speed = new Vector3(h, 0, f).magnitude;
            animator.SetFloat("speed", speed);
        }
	}
}
