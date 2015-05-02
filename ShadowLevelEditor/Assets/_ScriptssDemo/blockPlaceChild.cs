using UnityEngine;
using System.Collections;

public class blockPlaceChild : MonoBehaviour {
	public float dirX;
	public float dirY;
	public float dirZ;
	public GameObject[] controller;
	public Transform[] growChildren;
	public float[] growSpeed;
	public GameObject rotParent;
	public float limitUp;
	public float limitDown;
	bool movable=true;
	float moveAmount=0;
	public float offset=0.05f;
	public GameObject[] linedots;

	
	void Start () {
		moveAmount=0;
		movable=true;
	}

	void FixedUpdate () {
		foreach(GameObject obj in linedots)
		foreach(Transform child in obj.transform){
			child.gameObject.SetActive(false);
			for( int i=0; i < child.GetComponent<toShadows>().myShadows.Count; i++)
				child.GetComponent<toShadows>().myShadows[i].gameObject.SetActive(false);
		}

		if(Character3D._touch3dObj){
			foreach(GameObject k in controller){
				if(Character3D._touch3dObj.gameObject==k){
					
			foreach(GameObject obj in linedots)
			foreach(Transform child in obj.transform){
				child.gameObject.SetActive(true);
				for( int i=0; i < child.GetComponent<toShadows>().myShadows.Count; i++)
					child.GetComponent<toShadows>().myShadows[i].gameObject.SetActive(true);
			}
			}
			}

		}

		if(Character3D._hit3dObj){
			foreach(GameObject k in controller){

				if(Character3D._hit3dObj.gameObject==k && Character3D._pRotDirection!=0)
			if(Character3D._pRotDirection>0 && moveAmount>=limitUp){
				movable=false;
			}else if(Character3D._pRotDirection<0 && moveAmount<=limitDown){
				movable=false;
			}else{
				movable=true;
			}

			if(movable){
					moveAmount+=Character3D._pRotDirection*growSpeed[0];
			for (int i = 0; i <  growChildren.Length; i++) {
				growChildren[i].position+= new Vector3(Character3D._pRotDirection*growSpeed[i]*dirX, Character3D._pRotDirection*growSpeed[i]*dirY,Character3D._pRotDirection*growSpeed[i]*dirZ)/2;
			}
					}

			}
		}
	
	}
}
