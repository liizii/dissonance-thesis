using UnityEngine;
using System.Collections;

public class blockLoopMoveChild : MonoBehaviour {
	public float dirX;
	public float dirY;
	public float dirZ;
	public GameObject controller;
	public Transform[] growChildren;
	public float[] growSpeed;
	public GameObject rotParent;
	public float limitUp;
	public float limitDown;
	bool movable=true;
	float moveAmount=0;
	public float offset=0.05f;
	float dir=1;
	
	
	void Start () {
		moveAmount=0;
		movable=true;
	}
	
	void Update () {
		if(controller.GetComponent<BlockInformation>().rotOnce!=0){
			for (int i = 0; i <  growChildren.Length; i++) {
					growChildren[i].position+= dir*new Vector3(growSpeed[i]*dirX, growSpeed[i]*dirY,growSpeed[i]*dirZ);
					moveAmount+=dir*growSpeed[i];	
			}

			if(dir>0 && moveAmount>limitUp){
				dir=-1;
				controller.GetComponent<BlockInformation>().rotOnce=0;
			}
			if(dir<0 && moveAmount<limitDown){
				dir=1;
				controller.GetComponent<BlockInformation>().rotOnce=0;
			}
			
		}
		
	}
}