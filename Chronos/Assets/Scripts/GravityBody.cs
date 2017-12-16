using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	public float gravity = -11f;
	public Vector3 gravityUp = new Vector3(0,1,0);

	GravityAttractor targetGravity;
	bool planetGravity = false;
	public GameObject targetPlanet;
	
	void Awake () {
		gravityUp = transform.up;
		
		if (targetPlanet) {
			planetGravity = true;

			// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		} else {
			//GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
	
	//FixedUpdate gets called at a regular interval independent from the framerate
	void Update () {
		if (planetGravity) {
			targetGravity = targetPlanet.GetComponent<GravityAttractor> ();

            if (this.tag == "Player")
            {
                targetGravity.Attract(transform);
            } else
            {
                targetGravity.AIAttract(transform);
            }
		}	
	}
}