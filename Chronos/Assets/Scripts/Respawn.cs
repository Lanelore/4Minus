using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {
    bool escapeGame = false;

    // Use this for initialization
    void Start () {
        escapeGame = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            print("escape now");
            escapeGame = true;
        }
    }

    void FixedUpdate()
    {
        if (escapeGame)
        {
            escapeGame = false;
            print("load start scene");
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerExit(Collider other)
    {        
        SpawnPosition spawnPos = other.gameObject.GetComponent<SpawnPosition>();
        if (spawnPos)
        {
            other.gameObject.transform.position = spawnPos.spawnPosition;
            other.gameObject.transform.rotation = Quaternion.Euler(spawnPos.spawnRotation);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
