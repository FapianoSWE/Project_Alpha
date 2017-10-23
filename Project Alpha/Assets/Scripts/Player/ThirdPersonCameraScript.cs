using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraScript : MonoBehaviour
{

	public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform target;
	public float distanceFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2 (-10, 60);

	public float rotationSmoothTime = 0.07f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;
    public bool isLockedOn;
    bool hasDisabledLockOn = false;
    GameObject lockOnObject;

    public MenuOpenScript menuOpenScript;

    void Start()
	{
		if (lockCursor) 
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

        menuOpenScript = GameObject.Find("Main Canvas").GetComponent<MenuOpenScript>();
    }

	void LateUpdate ()
	{
        if (!menuOpenScript.MenuOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(transform.position, transform.forward);
                if (Physics.Raycast(ray, out hit, 10) && hasDisabledLockOn)
                {
                    if (hit.transform.gameObject.GetComponent<EnemyAIScript>())
                    {
                        lockOnObject = hit.transform.gameObject;
                        hasDisabledLockOn = false;
                        transform.LookAt(lockOnObject.transform);
                        isLockedOn = true;
                    }
                }
                else if (lockOnObject != null)
                {
                    transform.LookAt(lockOnObject.transform);
                    isLockedOn = true;
                }
                else
                    lockOnObject = null;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(transform.position, transform.forward);
                if (Physics.Raycast(ray, out hit, 10))
                {
                    if (hit.transform.gameObject.tag == "QuestNPC")
                    {
                      //  hit.transform.gameObject.GetComponent<QuestGiverScript>().OnPress(target.gameObject);
                    }
                }

            }
            else
            {
                isLockedOn = false;
                hasDisabledLockOn = true;
                lockOnObject = null;
            }

        if(transform.rotation.eulerAngles.x < pitchMinMax.x|| transform.rotation.eulerAngles.x >pitchMinMax.y)
        {
            print("Turned off lock-on due to too large rotation");
            isLockedOn = false;
            hasDisabledLockOn = true;
            lockOnObject = null;
        }

            if (!isLockedOn)
            {
                yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
                pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

                Vector3 targetRotation = new Vector3(pitch, yaw);
                transform.eulerAngles = currentRotation;
            }
            transform.position = target.position - (transform.forward * distanceFromTarget) + new Vector3(0, 1, 0);
        }
        else if (menuOpenScript.MenuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
	}
}
