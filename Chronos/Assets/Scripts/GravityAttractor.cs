using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {
	
	public float gravity = -9.8f;

	public void Attract(Transform body) {
		Vector3 gravityUp = (body.position - transform.position).normalized; //target direction / gravity
		Vector3 localUp = body.up; //object gravity
		
		// Apply downwards gravity to body
		body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
		// Allign bodies up axis with the centre of planet
		//body.rotation = Quaternion.FromToRotation(localUp,gravityUp) * body.rotation;

        Quaternion targetQuat = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
        Vector3 v = transform.rotation.eulerAngles;
        body.rotation = Quaternion.Euler(targetQuat.eulerAngles.x, targetQuat.eulerAngles.y, targetQuat.eulerAngles.z);
    }

    public void AIAttract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized; //target direction / gravity
        Vector3 localUp = body.up; //object gravity

        // Apply downwards gravity to body
        //body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        // Allign bodies up axis with the centre of planet
        body.rotation = Quaternion.FromToRotation(localUp,gravityUp) * body.rotation;

        Quaternion targetQuat = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
        Vector3 v = transform.rotation.eulerAngles;
        //body.rotation = Quaternion.Euler(v.x, v.y, v.z);
    }

    /*
                    //transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, angularSpeed);

              //  Quaternion originalRot = transform.rotation;
               // transform.rotation = originalRot * Quaternion.AngleAxis(degrees, Vector3.Up);
               //transform.rotation = rotation.eulerAngles.y
                Quaternion targetQuat = Quaternion.RotateTowards (transform.rotation, targetRotation, angularSpeed);

                Vector3 v = transform.rotation.eulerAngles;
              //  transform.rotation = Quaternion.Euler(v.x, targetQuat.eulerAngles.y, targetQuat.eulerAngles.z);

                //          float angle = 0.0F;
                //  Vector3 axis = this.transform.up;

                // transform.rotation.ToAngleAxis(out angle, out axis);

                //print("Player: " + transform.rotation + ", target: " + targetRotation);
                //transform.Rotate(0, yAmount, 0, Space.Self);
    */
}