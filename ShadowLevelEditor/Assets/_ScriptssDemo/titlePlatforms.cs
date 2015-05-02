using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class titlePlatforms : MonoBehaviour {
	public GameObject pXY;
	public GameObject pZY;
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
	bool startMoving=false;
	int nTotalRotations=7;
	int nCurrentRotations=0; // increment whenever player rotates
	float rotationProgress=0;
	
	
	void Start () {
		camNextPos = cam.position - new Vector3(0,5,0);
		myNextPos = transform.position + new Vector3(-4.5f,0,-4.5f);
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
			rotationProgress = (float)nCurrentRotations / nTotalRotations; // independent of object, changes every time I rotate
			rotationProgress = Mathf.Clamp01(rotationProgress); // independent of object

		
		
		
		cam.LookAt(camTarget);
		if(controller.GetComponent<toParent>().rotOnce!=0 && !startMoving){
			if(controller.GetComponent<toParent>().rotOnce==1){
				nCurrentRotations++;
				controller.GetComponent<toParent>().rotOnce=0;
			}
			//	controller.GetComponent<toParent>().rotOnce=0;	
		}
		if(!startMoving)
		for (int i = 0; i <  moveObjects.Count; i++) {
			Vector3 targetPosi = Vector3.Lerp(oPos[i], targetPos[i].position, rotationProgress); // find my CURRENT target, this changes every time I rotate
			Quaternion targetRot = Quaternion.Lerp(oRot[i],targetPos[i].rotation,rotationProgress);
			Vector3 targetScale = Vector3.Lerp(new Vector3(0.1f,0.1f,0.1f),targetPos[i].localScale,rotationProgress);
			if(Vector3.Distance(moveObjects[i].transform.position,targetPos[i].position)>0.1f){
				moveObjects[i].transform.position=Vector3.Lerp(moveObjects[i].transform.position,targetPosi,Time.deltaTime*10/t[i]);
				moveObjects[i].transform.localScale=Vector3.Lerp(moveObjects[i].transform.localScale,targetScale,Time.deltaTime*10/t[i]);
				moveObjects[i].transform.rotation=Quaternion.Lerp(moveObjects[i].transform.rotation,targetRot,Time.deltaTime*10/t[i]);
				arrived=false;
			}else{
				arrived=true;
			}
		}
		
	}
	
	void FixedUpdate(){
		
		/////////when this is certain degree and other platforms arrive at target positions, enter next level. with exit animation. with sound
		if(arrived && controller.transform.parent.eulerAngles.y%360<10 && rotationProgress==1)
			startMoving=true;
		
		if(arrived && startMoving && rotationProgress==1){
			controller.GetComponent<toParent>().rotatable=false;
			pXY.SetActive(false);
			pZY.SetActive(false);
			controller.transform.parent = transform;
			for (int i = 0; i < fadeOutObjs.Count; i++) {
				for (int j = 0; j < fadeOutObjs[i].GetComponent<Renderer>().materials.Length; j++) {
					Color cl=fadeOutObjs[i].GetComponent<Renderer>().materials[j].color;
					cl.a=0;
					fadeOutObjs[i].GetComponent<Renderer>().materials[j].color=Color.Lerp(fadeOutObjs[i].GetComponent<Renderer>().materials[j].color,cl, Time.deltaTime);
				}
			}
			transform.position =  Vector3.Lerp(transform.position,myNextPos,Time.deltaTime);
			if(cam.position.y>20)
				cam.Translate(new Vector3(0,-10,0) * Time.deltaTime);
			
			
			
		}
	}
}
