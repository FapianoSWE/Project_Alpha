using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public Animator anim; 
    public PlayerController playerController;
    public CharacterController characterController;
	// Use this for initialization
	void Start ()
    {


        anim = GetComponent<Animator>();
        playerController = transform.parent.gameObject.GetComponent<PlayerController>();
        characterController = transform.parent.gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerController.isWalking)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (playerController.castStates == CastStates.casting)
        {
            anim.SetBool("isCasting", true);

            if (playerController.isProjectile)
            {
                anim.SetBool("isProjectile", true);
            }
            
        }
        else
        {
            anim.SetBool("isCasting", false);
            anim.SetBool("isProjectile", false);
        }
	}
}
