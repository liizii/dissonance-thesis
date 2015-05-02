using UnityEngine;
using System.Collections;

public class blockWeightSensor : MonoBehaviour {
	public Vector3 deltaPos;
	public float speed;
	Vector3 endPos;
	Vector3 endPos2;
	Vector3 oPos;
	Vector3 nextPos;
	public int dir=0;
	Vector3 objVel;
	Vector3 lastPos;
	public GameObject bt;
	Vector3 btoPos;
	Transform btoParent;
	bool withPlayer=false;
	
	void Start () {
		oPos=transform.parent.position;
		endPos=oPos+deltaPos;
		endPos2=oPos+deltaPos+new Vector3(0,-1.1f,0);
		nextPos=oPos;
		dir=0;
		btoPos=bt.transform.position;
		btoParent=bt.transform.parent;

	}
	
	void Update () {
		if(GetComponent<toParent>().beTouched!=2){
			withPlayer=true;
			PlayerInputController.jumpOK=2;
		if(dir!=0){
			if(GetComponent<toParent>().beTouched==PlayerInputController.playerStatus || GetComponent<toParent>().beTouched==10){
					PlayerInputController.jumpOK=GetComponent<toParent>().beTouched;
			}
		}
		}else{
			if(withPlayer){
				PlayerInputController.jumpOK=2;
				withPlayer=false;
			}
		}

		if(Vector3.Distance(nextPos,transform.parent.position)<0.1f){
			dir=0;
		}
		objVel=deltaPos*dir*speed;
		transform.parent.position=transform.parent.position+objVel;

		if(GetComponent<toParent>().beTouched==2){
			bt.transform.parent=btoParent;
			bt.transform.position=btoPos;
			if(nextPos!=oPos){
				lastPos=nextPos;
				dir=-1;
				nextPos=oPos;
			}
		}else{
			//GetComponent<toParent>().myVel=objVel;
			if(GetComponent<toParent>().beTouched==0 || GetComponent<toParent>().beTouched==10){
				PlayerInputController.platformVelXY=objVel;
			}
			if(GetComponent<toParent>().beTouched==1 || GetComponent<toParent>().beTouched==10){
				PlayerInputController.platformVelZY=objVel;
			}
			if(GetComponent<toParent>().beTouched==10){
				if(transform.position.y-endPos.y<0)
				bt.transform.parent=this.transform;
				if(nextPos!=endPos2){
					lastPos=nextPos;
					dir=1;
					nextPos=endPos2;
				}
			}else{
				if(nextPos!=endPos){
					lastPos=nextPos;
					if(lastPos==oPos){
						dir=1;
					}else{
						dir=-1;
					}
					nextPos=endPos;
				}
			}
		}
		
	}

}
