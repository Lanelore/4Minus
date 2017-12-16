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
        CheckEnemy(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckEnemy(other);
    }
    
    public void CheckEnemy(Collider other)
    {
        print("Detect Collider " + other.tag);

        if (other.tag == "Enemy")
        {
            print("Detect Enemy");
            Destroy(gameObject);
            // spawn explosion
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
    }
}
