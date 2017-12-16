using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSystem : MonoBehaviour {
    int crystalCount = 10;
    public GameObject crystal;
    GameObject player;
    public Vector3 planetCenter;
    float spawnCD = 0;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CollectCrystal()
    {
        crystalCount += 1;
    }

    void UseCrystal()
    {
        if (crystalCount > 0)
        {
            spawnCD = 0.3f;
            crystalCount -= 1;
            Vector3 dir = (planetCenter - player.transform.position).normalized;
            Vector3 spawnPos = player.transform.position + dir * (player.transform.localScale.x * 0.3f);
            GameObject createdDummy = GameObject.Instantiate(crystal, spawnPos, player.transform.rotation) as GameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Action"))
        {
            UseCrystal();
        }

        if (spawnCD > 0)
        {
            spawnCD -= Time.deltaTime;
        }
    }
}
