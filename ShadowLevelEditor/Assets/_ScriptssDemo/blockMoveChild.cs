using UnityEngine;
using System.Collections;

public class blockMoveChild : MonoBehaviour {
	public float dirX;
	public float dirY;
	public float dirZ;
	public GameObject controller;
	public Transform[] growChildren;
	public float[] growSpeed;
	public float limitUp;
	public float limitDown;
	bool movable=true;
	float moveAmount=0;
	public float offset=0.05f;

	
	void Start () {
		moveAmount=0;
		movable=true;
	}

	void Update () {
		if(controller.GetComponent<blockWeightSensor>()){
			if(controller.GetComponent<blockWeightSensor>().dir>0 && moveAmount>limitUp){
				movable=false;
			}else if(controller.GetComponent<blockWeightSensor>().dir<0 && moveAmount<limitDown){
				movable=false;
			}else{
				movable=true;
			}

			if(movable){
				moveAmount+=controller.GetComponent<blockWeightSensor>().dir*growSpeed[0];
			for (int i = 0; i <  growChildren.Length; i++) {
					Vector3 vel=new Vector3(controller.GetComponent<blockWeightSensor>().dir*growSpeed[i]*dirX, controller.GetComponent<blockWeightSensor>().dir*growSpeed[i]*dirY,controller.GetComponent<blockWeightSensor>().dir*growSpeed[i]*dirZ);
//					if(growChildren[i].gameObject.GetComponent<toParent>().beTouched==0){
//						if(vel.y>0.02f){
//					PlayerInputController.platformVelXY=new Vector3(vel.x,vel.y*2.5f,vel.z);
//						}else{
//							PlayerInputController.platformVelXY=new Vector3(vel.x,vel.y*1.01f,vel.z);
//						}
//				}
//				if(growChildren[i].GetComponent<toParent>().beTouched==1){
//						if(vel.y>0.02f){
//					PlayerInputController.platformVelZY=new Vector3(vel.x,vel.y*2.5f,vel.z);
//						}else{
//							PlayerInputController.platformVelZY=new Vector3(vel.x,vel.y*1.01f,vel.z);
//						}
//				}
					if(growChildren[i].GetComponent<BlockInformation>()){
						growChildren[i].GetComponent<BlockInformation>().myVel=vel;
					}
				growChildren[i].position+=vel;
			}
			}

		
		}
	}
}
