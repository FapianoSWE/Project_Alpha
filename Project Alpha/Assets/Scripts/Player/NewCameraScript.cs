using UnityEngine;
using System.Collections;

public class NewCameraScript : MonoBehaviour
{

    public Transform target;                            // Target to follow
    Rigidbody rb;
    public float targetHeight = 1.7f;                       // Vertical offset adjustment
    public float distance = 12.0f;                          // Default Distance
    public float offsetFromWall = 0.1f;                     // Bring camera away from any colliding objects
    public float maxDistance = 20;                      // Maximum zoom Distance
    public float minDistance = 0f;                        // Minimum zoom Distance
    public float xSpeed = 200.0f;                           // Orbit speed (Left/Right)
    public float ySpeed = 200.0f;                           // Orbit speed (Up/Down)
    public float yMinLimit = -80;                           // Looking up limit
    public float yMaxLimit = 80;                            // Looking down limit
    public float zoomRate = 40;                             // Zoom Speed
    public float rotationDampening = 3.0f;              // Auto Rotation speed (higher = faster)
    public float zoomDampening = 5.0f;                  // Auto Zoom speed (Higher = faster)
    LayerMask collisionLayers = -1;     // What the camera will collide with

    public bool lockToRearOfTarget;
    public bool allowMouseInputX = true;
    public bool allowMouseInputY = true;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    public float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind;
    

    public GameObject userModel;
    public bool inFirstPerson;

    public bool isLockedOn;
    bool hasDisabledLockOn = false;
    public GameObject lockOnObject;

    public MenuOpenScript menuOpenScript;

    public Vector2 pitchMinMax = new Vector2(-10, 60);

    Quaternion lockOnRotation;  

    [Range(0,1)]
    float lockOnTime;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
        // Make the rigid body not change rotation 
        if (rb)
            rb.freezeRotation = true;

        if (lockToRearOfTarget)
            rotateBehind = true;
        Cursor.visible = false;

        menuOpenScript = GameObject.Find("Main Canvas").GetComponent<MenuOpenScript>();
    }

    void Update()
    {
        if(menuOpenScript.MenuOpen|| GameObject.Find("Player").GetComponent<PlayerController>().itemCanvasOpen)
        {
            Cursor.visible = true;
        }
        else if(!menuOpenScript.MenuOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {

                if (inFirstPerson == true)
                {

                    minDistance = 0;
                    desiredDistance = 0;
                    userModel.SetActive(true);
                    inFirstPerson = false;
                }
            }

            if (desiredDistance == 10)
            {

                minDistance = 0;
                desiredDistance = 0;
                userModel.SetActive(false);
                inFirstPerson = true;
            }
        }
    }


    //Only Move camera after everything else has been updated
    void LateUpdate()
    {
        if (!menuOpenScript.MenuOpen && !GameObject.Find("Player").GetComponent<PlayerController>().itemCanvasOpen)
        {

            if (Input.GetMouseButton(1))
            {
                if(lockOnObject == null && isLockedOn)
                {
                    isLockedOn = false;
                    hasDisabledLockOn = true;
                    lockOnTime = 0;
                }

                RaycastHit hit;
                Ray ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(transform.position, transform.forward);
                if (Physics.SphereCast(ray,10,out hit,100) && hasDisabledLockOn)
                {
                    if (hit.transform.gameObject.GetComponent<EnemyAIScript>()&&!hit.collider.isTrigger)
                    {
                        lockOnObject = hit.transform.gameObject;
                        hasDisabledLockOn = false;
                        transform.LookAt(lockOnObject.transform);
                        lockOnRotation = transform.rotation;
                        isLockedOn = true;
                    }
                }
                else if (lockOnObject != null)
                {
                    isLockedOn = true;
                }
                else
                    lockOnObject = null;

                

                if (isLockedOn)
                {
                    lockOnTime += Time.deltaTime;
                    transform.LookAt(lockOnObject.transform);
                    lockOnRotation = transform.rotation;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(transform.position, transform.forward);
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    print("har träffat " + hit.transform.gameObject.name);
                    if (hit.transform.gameObject.tag == "QuestNPC")
                    {
                        hit.transform.gameObject.GetComponent<QuestGiverScript>().OnPress(target.gameObject);
                    }
                }

            }
            else
            {
                isLockedOn = false;
                hasDisabledLockOn = true;
                lockOnObject = null;
                lockOnTime = 0;
            }
            // Don't do anything if target is not defined 
            if (!target)
                return;

            Vector3 vTargetOffset3;

            // If either mouse buttons are down, let the mouse govern camera position 
            //Check to see if mouse input is allowed on the axis
            if (allowMouseInputX && !isLockedOn)
                xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            if (allowMouseInputY)
                yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            if(isLockedOn)
            {
                xDeg = Quaternion.Lerp(transform.rotation, lockOnRotation, lockOnTime).eulerAngles.y;
            }
            ClampAngle(yDeg);

            // Set camera rotation 
            Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);

            // Calculate the desired distance 
            desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
            desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
            correctedDistance = desiredDistance;

            // Calculate desired camera position
            Vector3 vTargetOffset = new Vector3(0, -targetHeight, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);

            // Check for collision using the true target's desired registration point as set by user using height 
            RaycastHit collisionHit;
            Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

            // If there was a collision, correct the camera position and calculate the corrected distance 
            bool isCorrected = false;
            if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers))
            {
                if(collisionHit.transform.tag == "Terrain")
                {
                    correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
                    isCorrected = true;
                }
            }

            // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance 
            currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;

            // Keep within limits
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

            // Recalculate position based on the new currentDistance 
            position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

            //Finally Set rotation and position of camera
            transform.rotation = rotation;
            transform.position = position;

            /*if (transform.rotation.eulerAngles.x < pitchMinMax.x || transform.rotation.eulerAngles.x > pitchMinMax.y)
            {
                print("Turned off lock-on due to too large rotation");
                isLockedOn = false;
                hasDisabledLockOn = true;
                lockOnObject = null;
            }*/ 
            if (lockOnObject)
                if (Vector3.Distance(transform.position, lockOnObject.transform.position) > 50)
                {
                    print("Turned off lock-on due to too large distance");
                    isLockedOn = false;
                    hasDisabledLockOn = true;
                    lockOnObject = null;
                }
        }
    }

    void ClampAngle(float angle)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        yDeg = Mathf.Clamp(angle, -10, 45);
    }

}