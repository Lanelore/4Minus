using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehaviour : MonoBehaviour {
    Vector3 originalScale;
    Vector3 destinationScale;
    public GameObject explosion;
    public float triggerRange;
    public float explosionRange;
    public LayerMask enemies;

    // Use this for initialization
    void Start () {
        originalScale = new Vector3(0.01f, 0.01f, 0.01f);
        destinationScale = new Vector3(0.1f, 0.1f, 0.1f);

        StartCoroutine(ScaleOverTime(0.5f));
    }

    public void Update()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, triggerRange, enemies);

        if(collider.Length > 0)
        {
            collider = Physics.OverlapSphere(transform.position, explosionRange, enemies);
            Destroy(GameObject.Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject,2);

            foreach (Collider c in collider)
            {
                c.gameObject.GetComponent<RuebeAnimation>().Die();
            }

            Destroy(this.gameObject);
        }
        
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        CheckEnemy(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckEnemy(other);
    }
    
    public void CheckEnemy(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // spawn explosion
            GameObject createdDummy = GameObject.Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(createdDummy, 2);
            Destroy(this.gameObject);
        }
    }*/

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
