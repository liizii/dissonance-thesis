using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class blockMoveToPos : MonoBehaviour {
	public GameObject targetObj;
	List<Transform> moveObjects= new List<Transform>();
	List<Vector3> oPos= new List<Vector3>();
	List<Quaternion> oRot= new List<Quaternion>();
	List<Transform> targetPos= new List<Transform>();
	public GameObject controller;
	int counter=0;
	float amount=0;
	List<float> t=new List<float>();
	public Transform camTarget;
	public Transform cam;
	public List<GameObject> fadeOutObjs= new List<GameObject>();
	bool arrived=false;
	Vector3 camNextPos;
	Vector3 myNextPos;
	Vector3 controllerNextPos;
	bool startMoving=false;


	
	void Start () {
		camNextPos = cam.position - new Vector3(0,5,0);
		myNextPos = transform.position - new Vector3(3,0,3);
		controllerNextPos = controllerNextPos - new Vector3(3,0,3);
		counter=0;
		amount=0;
		foreach (Transform child in transform)
		{
			moveObjects.Add(child);
			oPos.Add(child.position);
			oRot.Add(child.rotation);
			t.Add(Random.Range(4, 10));
			fadeOutObjs.Add(child.gameObject);
		}

		foreach (Transform child in targetObj.transform)
		{
			targetPos.Add(child);
		}


	}

	void Update () {
		// Figure these out at beginning
//		Vector3 startPos;
//		Vector3 endPos;
//		int nTotalRotations;
//
//		// Now, for each frame..
//		int nCurrentRotations; // increment whenever player rotates
//		float rotationProgress = (float)nCurrentRotations / nTotalRotations; // independent of object, changes every time I rotate
//		rotationProgress = Mathf.Clamp01(rotationProgress); // independent of object
//
//		Vector3 targetPos = Vector3.Lerp(start, end, rotationProgress); // find my CURRENT target, this changes every time I rotate
//
//		// Now, change current position by easing
//		object.transform.position = Vector3.Lerp(object.transform.position, targetPos, easeFactor);



		cam.LookAt(camTarget);
		if(controller.GetComponent<toParent>().rotOnce!=0 && !startMoving){
			counter++;
			//GetComponent<Light>().GetComponent<Light>().intensity+=controller.GetComponent<toParent>().rotOnce/100;
			if(controller.GetComponent<toParent>().rotOnce==1){
				amount++;
			for (int i = 0; i <  moveObjects.Count; i++) {
				if(Vector3.Distance(moveObjects[i].transform.position,targetPos[i].position)>0.2f){
				moveObjects[i].transform.position=Vector3.Lerp(moveObjects[i].transform.position,targetPos[i].position,Time.deltaTime*10/t[i]);
				moveObjects[i].transform.localScale=Vector3.Lerp(moveObjects[i].transform.localScale,targetPos[i].localScale,Time.deltaTime*10/t[i]);
				moveObjects[i].transform.rotation=Quaternion.Lerp(moveObjects[i].transform.rotation,targetPos[i].rotation,Time.deltaTime*10/t[i]);
					arrived=false;
				}else{
					arrived=true;
				}
			}
			}
			if(controller.GetComponent<toParent>().rotOnce==-1){
				if(amount>0)
				amount--;
			for (int i = 0; i <  moveObjects.Count; i++) {
				if(Vector3.Distance(moveObjects[i].transform.position,oPos[i])>0.3f){
				moveObjects[i].transform.position=Vector3.Lerp(moveObjects[i].transform.position,oPos[i],Time.deltaTime*10/t[i]);
				moveObjects[i].transform.localScale=Vector3.Lerp(moveObjects[i].transform.localScale,new Vector3(0.05f,0.05f,0.05f),Time.deltaTime*10/t[i]);
				moveObjects[i].transform.rotation=Quaternion.Lerp(moveObjects[i].transform.rotation,oRot[i],Time.deltaTime*10/t[i]);
				}
			}
			}
			if(counter%20==0)
				controller.GetComponent<toParent>().rotOnce=0;
			
		}
	}

	void FixedUpdate(){

		/////////when this is certain degree and other platforms arrive at target positions, enter next level. with exit animation. with sound
		if(amount>58 && arrived && controller.transform.parent.eulerAngles.y%360<10){
			startMoving=true;
			for (int i = 0; i < fadeOutObjs.Count; i++) {
				for (int j = 0; j < fadeOutObjs[i].GetComponent<Renderer>().materials.Length; j++) {
					Color cl=fadeOutObjs[i].GetComponent<Renderer>().materials[j].color;
					cl.a=0;
					fadeOutObjs[i].GetComponent<Renderer>().materials[j].color=Color.Lerp(fadeOutObjs[i].GetComponent<Renderer>().materials[j].color,cl, Time.deltaTime);
				}
			}
			//transform.position =  Vector3.Lerp(transform.position,myNextPos,Time.deltaTime);
			//controller.transform.position = Vector3.Lerp(controller.transform.position,controllerNextPos,Time.deltaTime); 
			if(cam.position.y>20)
				cam.Translate(new Vector3(0,-10,0) * Time.deltaTime);

			
			
		}
	}
}
