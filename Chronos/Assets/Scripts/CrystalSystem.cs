using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalSystem : MonoBehaviour {
    int crystalCount = 10;
    public GameObject crystal;
    GameObject player;
    public Vector3 planetCenter;
    float spawnCD = 0;
    public GameObject killText;

    public UnityEngine.UI.Text counter;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        counter.text = crystalCount.ToString();
    }

    public void CollectCrystal()
    {
        crystalCount += 1;
        counter.text = crystalCount.ToString();
    }

    void UseCrystal()
    {
        if (crystalCount > 0)
        {
            spawnCD = 0.3f;
            crystalCount -= 1;
            Vector3 dir = (planetCenter - player.transform.position).normalized;
            Vector3 spawnPos = player.transform.position + this.transform.up * (player.transform.localScale.x * -0.3f);
            spawnPos = spawnPos + this.transform.forward * (player.transform.localScale.x * -1);
            GameObject createdDummy = GameObject.Instantiate(crystal, spawnPos, player.transform.rotation) as GameObject;
            counter.text = crystalCount.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Action") && DieOnTouch.gameRunning)
        {
            UseCrystal();
        }

        if (spawnCD > 0)
        {
            spawnCD -= Time.deltaTime;
        }
    }
}
