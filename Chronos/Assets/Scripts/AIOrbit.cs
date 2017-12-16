using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOrbit : MonoBehaviour
{
    GameObject[] planets;
    GameObject targetPlanet;
    bool planetGravity = false;
    GameObject player;
    Quaternion tmpRotation;
    Quaternion from;
    Quaternion to;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        planets = GameObject.FindGameObjectsWithTag("Planet");
        if (planets.Length > 0)
        {
            planetGravity = true;
            targetPlanet = planets[0];
        }
    }

    // Update is called once per frame
    void Update()
    {



        Vector3 originOfAngle = targetPlanet.transform.position; //this will be where you are casing your angle from.
        Vector3 angle = targetPlanet.transform.localEulerAngles; //this will be your target angle.
        float distanceToTestFor = 3000; //this will be your testing radius
                                        //set the above values however you wish

        Ray rayToTest = new Ray(originOfAngle, angle);
        Vector3 targetPoint = rayToTest.GetPoint(distanceToTestFor);


        transform.LookAt(targetPlanet.transform.position);
        float targetAngle = Vector3.Angle(targetPlanet.transform.position, player.transform.position);

        //print(targetAngle);

        //Quaternion currentRotation = Quaternion.RotateTowards(transform.rotation, lookAt1, 6 * Time.deltaTime);

        tmpRotation = Quaternion.Slerp(to, from, Time.time * 20);

        //    Vector3 p2 = tmpRotation * (Vector3.forward * 20);
        //    this.transform.position = p2;
        //The above line should return your desired Vector3

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateTarget();
        }
    }

    void CalculateTarget(){
        print("Target ");
        to = Quaternion.LookRotation(player.transform.position - targetPlanet.transform.position);
        from = Quaternion.LookRotation(this.transform.position - targetPlanet.transform.position);
    }

    private void FixedUpdate()
    {
        
        tmpRotation = Quaternion.Slerp(to, from, Time.time * 20);

        Vector3 p2 = tmpRotation * (Vector3.forward * 20);
        this.transform.position = p2;


    }
}
