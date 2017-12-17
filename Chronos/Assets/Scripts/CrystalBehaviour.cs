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
    GameObject player;
    public static int totalKilledrueben = 0;

    // Use this for initialization
    void Start () {
        originalScale = new Vector3(0.01f, 0.01f, 0.01f);
        destinationScale = new Vector3(0.4f, 0.4f, 0.4f);

        StartCoroutine(ScaleOverTime(0.5f));
        player = GameObject.FindGameObjectWithTag("Player");
        totalKilledrueben = 0;
    }

    public void Update()
    {
        if (DieOnTouch.gameRunning == false)
        {
            return;
        }

        Collider[] collider = Physics.OverlapSphere(transform.position, triggerRange, enemies);
        int killedRueben = 0;
        if (collider.Length > 0)
        {
            collider = Physics.OverlapSphere(transform.position, explosionRange, enemies);
            Destroy(GameObject.Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject,2);

            foreach (Collider c in collider)
            {
                c.gameObject.GetComponent<RuebeAnimation>().Die();
                if (c.gameObject.GetComponent<RuebeAnimation>() && c.gameObject.GetComponent<RuebeAnimation>().counted == false)
                {
                    c.gameObject.GetComponent<RuebeAnimation>().counted = true;
                    killedRueben += 1;
                }
            }

            if (killedRueben > 0)
            {
                totalKilledrueben += killedRueben;
                player.GetComponent<CrystalSystem>().killText.GetComponent<KillText>().ShowKill(killedRueben);
            }

            Destroy(this.gameObject);
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
