using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehaviour : MonoBehaviour {
    Vector3 originalScale;
    Vector3 destinationScale;

    // Use this for initialization
    void Start () {
        originalScale = new Vector3(0.01f, 0.01f, 0.01f);
        destinationScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(ScaleOverTime(1));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

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
        } while (currentTime <= time);

     //   Destroy(gameObject);
    }
}
