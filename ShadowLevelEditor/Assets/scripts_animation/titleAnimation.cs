using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class titleAnimation : MonoBehaviour {
	public Transform endPosTarget;
	public Transform endObj;
	List<Transform> currentChildren= new List<Transform>();
	List<float> speed=new List<float>();
	List<Transform> endChildren= new List<Transform>();
	public List<Transform> centers= new List<Transform>();
	public List<Transform> playersVisual2D= new List<Transform>();
	public Transform cam;
	public Transform camLookat;
	Vector3 camNextPos;
	public List<Transform> others= new List<Transform>();

	void Start () {
		foreach(Transform child in transform){
			currentChildren.Add(child);
			speed.Add(Random.Range(4, 10));
		}
		foreach(Transform child in endObj){
			endChildren.Add(child);
		}
		camNextPos = cam.position - new Vector3(0,60,0);
	}

	void FixedUpdate () {
		cam.LookAt(camLookat);
		if(Mathf.Abs(centers[0].eulerAngles.y - 270) < 0.5f){
			enableStaticCenter();
			moveCloser();
		}
		else
		for(int i = 0; i < currentChildren.Count; i++){
			currentChildren[i].position = Vector3.Lerp(currentChildren[i].position,endChildren[i].position,Time.fixedDeltaTime * 10 / speed[i]);
			currentChildren[i].localScale = Vector3.Lerp(currentChildren[i].localScale,endChildren[i].localScale,Time.fixedDeltaTime * 10 / speed[i]);
			currentChildren[i].rotation = Quaternion.Lerp(currentChildren[i].rotation,endChildren[i].rotation,Time.fixedDeltaTime * 10 / speed[i]);
		}
	}

	void enableStaticCenter(){
		centers[0].gameObject.SetActive(false);
		centers[1].gameObject.SetActive(true);
		centers[1].parent = this.transform;
		foreach(Transform p in playersVisual2D){
			p.parent = transform;
		}
	}

	void moveCloser(){
		transform.position =  Vector3.Lerp(transform.position, endPosTarget.position, Time.fixedDeltaTime);
		foreach(Transform obj in currentChildren){
			changeColor(obj, new Color(1,1,1,0));
		}
		foreach(Transform obj in centers){
		changeColor(obj, new Color(1,1,1,0));
		}
		foreach(Transform obj in others){
			changeColor(obj, new Color(1,1,1,0));
		}
		cam.position = Vector3.Lerp(cam.position,camNextPos,Time.fixedDeltaTime/5);
	}

	void changeColor(Transform obj, Color nextC){
		obj.GetComponent<Renderer>().material.color = Color.Lerp(obj.GetComponent<Renderer>().material.color,nextC, Time.fixedDeltaTime * 2);
	}

//	rotationProgress = (float)nCurrentRotations / nTotalRotations; // independent of object, changes every time I rotate
//	rotationProgress = Mathf.Clamp01(rotationProgress); // independent of object
//	
//	
//	
//	
//	cam.LookAt(camTarget);
//	if(controller.GetComponent<toParent>().rotOnce!=0 && !startMoving){
//		if(controller.GetComponent<toParent>().rotOnce==1){
//			nCurrentRotations++;
//			controller.GetComponent<toParent>().rotOnce=0;
//		}
//		//	controller.GetComponent<toParent>().rotOnce=0;	
//	}
//	if(!startMoving)
//	for (int i = 0; i <  moveObjects.Count; i++) {
//		Vector3 targetPosi = Vector3.Lerp(oPos[i], targetPos[i].position, rotationProgress); // find my CURRENT target, this changes every time I rotate
//		Quaternion targetRot = Quaternion.Lerp(oRot[i],targetPos[i].rotation,rotationProgress);
//		Vector3 targetScale = Vector3.Lerp(new Vector3(0.1f,0.1f,0.1f),targetPos[i].localScale,rotationProgress);
//		if(Vector3.Distance(moveObjects[i].transform.position,targetPos[i].position)>0.1f){
//			moveObjects[i].transform.position=Vector3.Lerp(moveObjects[i].transform.position,targetPosi,Time.deltaTime*10/t[i]);
//			moveObjects[i].transform.localScale=Vector3.Lerp(moveObjects[i].transform.localScale,targetScale,Time.deltaTime*10/t[i]);
//			moveObjects[i].transform.rotation=Quaternion.Lerp(moveObjects[i].transform.rotation,targetRot,Time.deltaTime*10/t[i]);
//			arrived=false;
//		}else{
//			arrived=true;
//		}
//	}
}
