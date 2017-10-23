using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

	public float Speed = 2;
	bool running = false;

    public bool canMove;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
    public float jumpHeight;
    float currentHeight;
    bool jumping,
         canJump = true;
    float baseSpeed;

    public GameObject AttackZone;


    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    Animator animator;
	Transform cameraT;

	void Start () 
	{
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
        baseSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () 
	{

        if (Input.GetMouseButtonDown(0) && canMove)
        {
            AttackZone.SetActive(true);

        }

        if (Input.GetKey(KeyCode.LeftShift) && canMove)
        {
            speed = 100;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && canMove)
        {
            speed = baseSpeed;
        }
        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        //Vector2 inputDir = input.normalized;

        //if (inputDir != Vector2.zero) 
        //{
        //	float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        //	transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y,targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        //}
        //force controller down slope. Disable jumping
      
    //      float targetSpeed = Speed * inputDir.magnitude;
    //currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

    //transform.Translate (transform.forward * targetSpeed * Time.deltaTime, Space.World);

}
}
