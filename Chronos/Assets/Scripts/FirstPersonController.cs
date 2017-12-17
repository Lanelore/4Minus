using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonController : MonoBehaviour {
    public float triggerRange;
    public LayerMask enemies;

    // public vars
    public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;
	public float walkSpeed = 6; //movement/walking speed
	
	// General Audio
	private AudioSource walk;

	// System vars
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;

	// find out if player is inactive for 30 seconds or more (to get back to the HUB)
	private float inactiveSeconds = 0.0f;
	private float maxInactiveSeconds = 50.0f;
	private float lastMouseX = 0.0f;
	private float lastMouseY = 0.0f;
	
	void Start() { //Awake
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		cameraTransform = Camera.main.transform;
        var audioSources = GetComponents<AudioSource>();
        this.walk = audioSources[0];
        StartCoroutine(WaitTime(0.5f));
    }

    void CheckNavigatorDistance()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, triggerRange, enemies);

        if (collider.Length > 0)
        {
            print("Fast Updates for this pathfinder");
            collider = Physics.OverlapSphere(transform.position, triggerRange, enemies);
            
            foreach (Collider c in collider)
            {
                c.gameObject.GetComponent<PathNavigator>().TargetPlayerOnce();
            }            
        }
    }

	void Update() {
		// play the walking sound if player walked enough
		if (Input.GetAxisRaw("Vertical")!= 0 || Input.GetAxisRaw("Horizontal")!= 0) {
			if (walk.isPlaying == false) {
				this.walk.Play ();
			}
		} else {
			this.walk.Stop ();
		}

        // set dampig dependend if grounded or not
        float damping = 1;
		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
		
		Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f * damping); //ref allows to modify a global variable
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime; //transform to local space (instead of world space - move on the surface of the sphere)
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
	}

    public void ChangeMouseSensitivity(float sensitivity)
    {
        mouseSensitivityX = sensitivity;
        mouseSensitivityY = sensitivity;
        string sensitivityString = sensitivity.ToString();
    }

    IEnumerator WaitTime(float time)
    {
        float currentTime = 0.0f;
        
        do
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        // Do something after waiting a specific time 
        CheckNavigatorDistance();

        // spawn something
        StartCoroutine(WaitTime(0.5f));
    }
}