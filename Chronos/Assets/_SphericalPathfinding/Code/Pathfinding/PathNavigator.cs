﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlanetBody))]
public class PathNavigator : MonoBehaviour
{
	public SphericalGrid sphericalGrid;
    GameObject player;
	public Transform target;
    Vector3 targetPosition;
    Vector3 prevTargetPos;

	public float moveSpeed = 2;
	public float lookSpeed = 2;
    public float targetThreshold = 6;

	Vector3[] path;
	int targetIndex;

	bool travelling = false;

	PlanetBody planetBody;

	public bool drawPath;

	#region Unity

	void Awake()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        planetBody = GetComponent<PlanetBody>();

        targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        // only set target if the ruebe is near the player (trigger contains player)
        // don't forget to delete target if it leaves the trigger
        // target = 
        
        StartCoroutine(WaitRandomTime(0));

        sphericalGrid = GameObject.Find("PathFinding").GetComponent<SphericalGrid>();
    }

    public void TargetPlayerOnce()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        
        if (distanceToPlayer > targetThreshold)
        {
            targetPosition = Vector3.zero;
        }
        else
        {
            targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
    }

    void Update()
	{
        if (!DieOnTouch.gameRunning)
        {
            StopAllCoroutines();
            return;
        }

		// if the target position has moved
		if(target != null)
		{
			float targetPosDiff = Vector3.Distance(prevTargetPos, target.position);

			if(targetPosDiff > 0.01f)
			{
				travelling = true;
				PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
			}

			prevTargetPos = target.position;
		}

		if(!travelling) // if the navigator has finished travelling
		{
			Vector3 targetPos = Vector3.zero;
            if (target != null)
            {
                targetPos = target.position;
            }
            else if (targetPosition != Vector3.zero)
            {
                targetPos = targetPosition;
            }
            else targetPos = RandomTargetPos();

			// check the distance to its target position, if it's far away start navigating again
			float dist = sphericalGrid.GetSphericalDistance(transform.position, targetPos);
			if(dist > 0.1f)
			{
				travelling = true;
				PathRequestManager.RequestPath(transform.position, targetPos, OnPathFound);
			}
		}
	}

	#endregion


	// *************************
	//          NAVIGATE
	// *************************
	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
        if (!this || !this.gameObject)
        {
            return;
        }

		if (pathSuccessful && newPath.Length > 0)
		{
			path = newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
		else
		{
			travelling = false;
		}
	}
	
	IEnumerator FollowPath()
	{
		targetIndex = 0;
		Vector3 currentWaypoint = path[targetIndex];
		
		while (true) 
		{
			float dist = sphericalGrid.GetSphericalDistance(transform.position, currentWaypoint);

			if (dist<= 0.09f) 
			{
				targetIndex ++;
				if (targetIndex >= path.Length) 
				{
					travelling = false;
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			MoveTowards(currentWaypoint);
			yield return null;
			
		}
	}

	public void MoveTowards(Vector3 targetPos)
	{
		Quaternion newRot = planetBody.LookAtTarget(targetPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRot, lookSpeed * Time.deltaTime);

		transform.position 	= planetBody.MoveForward(moveSpeed);
	}


	// *************************
	//         UTILITY
	// *************************


	Vector3 RandomTargetPos()
	{
		Vector3 rndDir = new Vector3(transform.forward.x * Random.Range(-1, 1), transform.forward.y * Random.Range(-1, 1), transform.forward.z * Random.Range(-1, 1));
		float distance = Random.Range(1, 20);
		Vector3 point = transform.position + ((rndDir) * distance);
		
		return planetBody.GroundPosition(point);
	}

	// *************************
	//          DEBUG
	// *************************
	
	public void OnDrawGizmos()
	{
		if (path != null && drawPath) 
		{
			for (int i = targetIndex; i < path.Length; i ++) 
			{
				Gizmos.color = Color.green;
				Gizmos.DrawCube(path[i], Vector3.one*0.02f);
				
				if (i == targetIndex) 
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else 
				{
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}

    IEnumerator WaitRandomTime(float time)
    {
        float currentTime = 0.0f;
        
        do
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        // Do something after waiting a specific time
        
        TargetPlayerOnce();

        // spawn something
        StartCoroutine(WaitRandomTime(Random.Range(0.5f, 2f)));
    }
}
