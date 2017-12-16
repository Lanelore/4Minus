using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {
    public float walkSpeed = 6; //movement/walking speed
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    GameObject player;
    GameObject planet;
    public GameObject dummy;
    Vector3 currentTarget;
    Vector3 currentTargetDirection;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        if (planets.Length > 0)
        {
            planet = planets[0];
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate()
    {
        float damping = 1;

        Vector3 aiPos = this.transform.position;
        Vector3 aiPlayerDir = (player.transform.position - this.transform.position).normalized;
        Vector3 pointTowardsPlayer = aiPos + aiPlayerDir * 4;
   //     Debug.DrawLine(aiPos, pointTowardsPlayer, Color.red, 1);

        //Raycast experiment
        RaycastHit hit;
        Vector3 planetPos = planet.transform.position;
        // dir = (end - start).normalized
        Vector3 planetDir = (pointTowardsPlayer - planetPos).normalized;
        Vector3 outsidePosition = planetPos + planetDir * 50;
        Vector3 outsideDirection = (planetPos - outsidePosition).normalized;
        Ray ray = new Ray(outsidePosition, outsideDirection);

        Vector3 pointDirection = (pointTowardsPlayer - aiPos).normalized;


       // Debug.DrawLine(outsidePosition, planetPos, Color.blue, Mathf.Infinity);
      

        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("Ground");
                // find target
                currentTarget = hit.point;
                // end - start
                currentTargetDirection = (currentTarget - aiPos).normalized;
                Debug.Log("Ground");
                // find target
                GameObject createdDummy = GameObject.Instantiate(dummy, hit.point, Quaternion.identity) as GameObject;
                Destroy(createdDummy, 3);

            }
        }

        //     float mouseSensitivityX = 250;
        //       transform.Rotate(Vector3.up * pointDirection.y * mouseSensitivityX * Time.deltaTime);

        //  transform.localRotation = Quaternion.Euler(0, currentTargetDirection.y, 0);

        Debug.DrawLine(aiPos, currentTarget, Color.blue, Mathf.Infinity);


        Vector3 moveDir = new Vector3(currentTargetDirection.x, currentTargetDirection.y, currentTargetDirection.z).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f * damping); //ref allows to modify a global variable


        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime; //transform to local space (instead of world space - move on the surface of the sphere)
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);

        //GetComponent<Rigidbody>().MovePosition(currentTarget);
    }
}