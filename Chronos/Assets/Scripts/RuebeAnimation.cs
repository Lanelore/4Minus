using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuebeAnimation : MonoBehaviour {
	Animator animator;
    Vector3 originalScale;
    Vector3 destinationScale;
    public static bool gameRunning = true;

    public static int deadRueben = 0;

    void Awake(){
		animator = this.GetComponentInChildren<Animator> ();
		animator.speed = UnityEngine.Random.Range (1.5f, 2f);
	}

    // Use this for initialization
    void Start()
    {
        originalScale = this.transform.localScale;
        destinationScale = this.transform.localScale * 0.01f;//new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void Die()
    {
        if (this.GetComponent<Collider>().enabled && this.enabled)
        {
            this.GetComponent<Collider>().enabled = false;
            this.enabled = false;
            if (gameRunning)
            {
                deadRueben += 1;
            }
            print("deadRueben " + RuebeAnimation.deadRueben);
            StartCoroutine(ScaleOverTime(0.1f));
        }
    }

    IEnumerator ScaleOverTime(float time)
    {
        float currentTime = 0.0f;

        do
        {
            this.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        print("Destroy Ruebe");
        Destroy(this.gameObject);
    }
}
