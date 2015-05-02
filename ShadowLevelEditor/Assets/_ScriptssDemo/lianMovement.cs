using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lianMovement : MonoBehaviour {
	public List<Animator> anim= new List<Animator>();
	//public List<HashIDs> hash= new List<HashIDs>();
	public float speedDampTime = 0.1f;  // The damping for the speed parameter
		
		
		void Awake ()
		{
			
		}
		
		
		void Update ()
		{
			
			float h = Input.GetAxis("Horizontal");
			bool jump=PlayerInputController.Jumping;
			if(PlayerInputController.Jumping){
				jump=PlayerInputController.Jumping;
				PlayerInputController.Jumping=false;
			}



			MovementManagement(h, jump);
		}

		
		void MovementManagement (float horizontal,  bool jumping)
		{
		for(int i=0; i<anim.Count;i++){
			anim[i].SetBool("Jump", jumping);
			if(horizontal != 0f || Input.GetKey(KeyCode.LeftArrow) ||  Input.GetKey(KeyCode.RightArrow))
			{
				anim[i].SetFloat("Speed", 5.5f, speedDampTime, Time.deltaTime);
			}
			else{
				anim[i].SetFloat("Speed", 0);
			}
			if(Character3D.touch3dObj && Character3D.touch3dObj.GetComponent<toParent>() && Character3D.touch3dObj.GetComponent<toParent>().rotatable ){
				if(Character3D.pRotDirection!=0){
					anim[i].SetBool("rotating",true);
				}else{
					anim[i].SetBool("rotating",false);
				}
				anim[i].SetBool("idleFat",true);
			}else{
				anim[i].SetBool("idleFat",false);
			}
		}
		}
	}