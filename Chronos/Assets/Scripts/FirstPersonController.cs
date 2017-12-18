using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
    public float triggerRange;
    public LayerMask enemies;

    // public vars
    public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;
	public float walkSpeed = 6; //movement/walking speed
    bool escapeGame = false;
	
	// General Audio
	private AudioSource walk;

	// System vars
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	
	void Start() { //Awake
        escapeGame = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
        var audioSources = GetComponents<AudioSource>();
        this.walk = audioSources[0];
       // StartCoroutine(WaitTime(0.5f));
    }

    void CheckNavigatorDistance()
    {
        Collider[] collider = Physics.OverlapSphere(this.transform.position, triggerRange, enemies);

        if (collider.Length > 0)
        {
            collider = Physics.OverlapSphere(transform.position, triggerRange, enemies);
            
            foreach (Collider c in collider)
            {
                c.gameObject.GetComponent<PathNavigator>().TargetPlayerOnce();
            }            
        }
    }

	void Update() {
        // set dampig dependend if grounded or not
        float damping = 1;
        // Look rotation:
        float rotationInput = Input.GetAxis("Controller X");
        if (rotationInput != 0.0f)
        {
            if (rotationInput < 0.2 && rotationInput > -0.2)
            {
                rotationInput = 0;
            }
        } else
        {
            rotationInput = Input.GetAxis("Mouse X");
        }
        
        transform.Rotate(Vector3.up * rotationInput * mouseSensitivityX * Time.deltaTime);
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");

        if (inputX > 0.5)
        {
            inputX = 1;
        }
        else if (inputX < -0.5)
        {
            inputX = -1;
        }
        else
        {
            inputX = 0;
        }

        if (inputY > 0.5)
        {
            inputY = 1;
        }
        else if (inputY < -0.5)
        {
            inputY = -1;
        }
        else
        {
            inputY = 0;
        }
        		
		Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f * damping); //ref allows to modify a global variable
	}
	
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime; //transform to local space (instead of world space - move on the surface of the sphere)
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
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
        StartCoroutine(WaitTime(1));
    }
}