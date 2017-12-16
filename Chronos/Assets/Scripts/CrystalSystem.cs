using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSystem : MonoBehaviour {
    int crystalCount = 10;
    public GameObject crystal;
    GameObject player;
    public Vector3 planetCenter;

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
            crystalCount -= 1;

            //
            Vector3 aiPos = this.transform.position;
            Vector3 aiPlayerDir = (player.transform.position - this.transform.position).normalized;
            Vector3 pointTowardsPlayer = aiPos + aiPlayerDir * 4;
            //     Debug.DrawLine(aiPos, pointTowardsPlayer, Color.red, 1);

            //Raycast experiment
            RaycastHit hit;
            //Vector3 planetPos = planet.transform.position;
            // dir = (end - start).normalized
            //  Vector3 dir = (planetCenter - player.transform.position).normalized;
            //   Vector3 outsidePosition = planetCenter + dir * 50;
            //   Vector3 outsideDirection = (planetCenter - outsidePosition).normalized;
            //   Ray ray = new Ray(outsidePosition, outsideDirection);

            //   Vector3 pointDirection = (pointTowardsPlayer - aiPos).normalized;


            // Debug.DrawLine(outsidePosition, planetPos, Color.blue, Mathf.Infinity);

            /*
            //RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Vector3 dir = (planetCenter - player.transform.position).normalized;
                    Vector3 spawnPos = player.transform.position + dir * (player.transform.localScale.x * 0.5f);


                    GameObject createdDummy = GameObject.Instantiate(crystal, spawnPos, player.transform.rotation) as GameObject;
            
                }
            }
            //
            */

            //GameObject createdDummy = GameObject.Instantiate(crystal, player.transform.position, player.transform.rotation) as GameObject;
            //Destroy(createdDummy, 3);
            Vector3 dir = (planetCenter - player.transform.position).normalized;
            Vector3 spawnPos = player.transform.position + dir * (player.transform.localScale.x * 0.5f);

            GameObject createdDummy = GameObject.Instantiate(crystal, spawnPos, player.transform.rotation) as GameObject;

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Action"))
        {
            UseCrystal();
        }
    }
}
