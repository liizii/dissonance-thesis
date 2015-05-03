using UnityEngine;
using System.Collections;

public class blockGrowChild : MonoBehaviour {
	public Transform[] growChildren;
	public float[] growSpeed;
	public GameObject rotParent;
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
		if(Character3D._hit3dObj && Character3D._hit3dObj.GetComponent<BlockInformation>().myParent.gameObject==this.gameObject && this.transform.parent==rotParent.transform){
			if(Character3D._pRotDirection>0 && moveAmount>limitUp){
				movable=false;
			}else if(Character3D._pRotDirection<0 && moveAmount<limitDown){
				movable=false;
			}else{
				movable=true;
			}

			if(movable)
			for (int i = 0; i <  growChildren.Length; i++) {
				growChildren[i].localScale+= new Vector3(0, Character3D._pRotDirection*growSpeed[i],0);
				if(Character3D._hit3dObj.gameObject==growChildren[i].GetComponentInChildren<BlockInformation>().transform.gameObject){
					PlayerInputController.platformVelXY=new Vector3(0, Character3D._pRotDirection*(growSpeed[i]+Character3D._pRotDirection*offset),0);
					PlayerInputController.platformVelZY=new Vector3(0, Character3D._pRotDirection*(growSpeed[i]+Character3D._pRotDirection*offset),0);
					moveAmount+=Character3D._pRotDirection*growSpeed[i];
				}
			}
		}
	
	}
}
