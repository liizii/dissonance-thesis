using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ObjType {
	Blank = 0,
	Gear=1
}

public class objAnimation : MonoBehaviour {
	public ObjType objectType;
	Transform planePos;
	public GameObject animatedObj;
	public List<GameObject> objIControl=new List<GameObject>();
	public List<GameObject> gearParts=new List<GameObject>();
	List<GameObject> controlledAnimatedObj=new List<GameObject>();
	float speed=0;
	int counterForRot=20;

	
	void Start () {
		planePos=GameObject.Find("PlaneXY").transform;
		for(int i=0;i<objIControl.Count;i++){
			GameObject newObj = Instantiate(animatedObj) as GameObject;
			newObj.transform.eulerAngles=new Vector3(0,0,180);
			controlledAnimatedObj.Add(newObj);
		}
		if((int)objectType==0){
			speed=8;
		}
//		else if((int)objectType==1){
//			speed=4;
//		}
	}
	
	// TODO: simplify the code
	void FixedUpdate () {
		Vector3 sc=animatedObj.transform.localScale;
		Vector3 pos=animatedObj.transform.position;
		Vector3 nextSc=sc;
		Vector3 nextPos=pos;
//		if((int)objectType==1){
//		
//			if(Character3D.touch3dObj && Character3D.touch3dObj==this.gameObject.transform){
//				for(int i=0;i<controlledAnimatedObj.Count;i++){
//					controlledAnimatedObj[i].transform.position=new Vector3(objIControl[i].transform.position.x,planePos.position.y-0.2f,objIControl[i].transform.position.z);
//					Vector3 inextsc=controlledAnimatedObj[i].transform.localScale;
//					inextsc.y=(objIControl[i].transform.position.y-planePos.position.y)/2;
//					controlledAnimatedObj[i].transform.localScale=Vector3.Lerp(controlledAnimatedObj[i].transform.localScale,inextsc,Time.deltaTime*speed);
//				}
//
//			}else{
//				for(int i=0;i<controlledAnimatedObj.Count;i++){
//					controlledAnimatedObj[i].transform.position=new Vector3(objIControl[i].transform.position.x,planePos.position.y-0.2f,objIControl[i].transform.position.z);
//					Vector3 inextsc=controlledAnimatedObj[i].transform.localScale;
//					inextsc.y=0;
//					controlledAnimatedObj[i].transform.localScale=Vector3.Lerp(controlledAnimatedObj[i].transform.localScale,inextsc,Time.deltaTime*speed);
//				}
//				for(int i=0;i<gearParts.Count;i++){
//					Vector3 gpsc=new Vector3(0,0,0);
//					//gpsc.z=0;
//					gearParts[i].transform.localScale=Vector3.Lerp(gearParts[i].transform.localScale,gpsc,Time.deltaTime*speed);
//				}
//
//			}
//		}


		if(animatedObj)
		if(GetComponent<BlockInformation>().beTouched!=2){
			counterForRot--;
			if(counterForRot<0)
			for(int i=0;i<gearParts.Count;i++){
				Vector3 gpsc=new Vector3(0.15f,0.15f,2f);
				//gpsc.z=2.1f;
				gearParts[i].transform.localScale=Vector3.Lerp(gearParts[i].transform.localScale,gpsc,Time.deltaTime*speed);
			}
			if((int)objectType==0){
				nextPos = transform.position-new Vector3(0,0.7f,0);
				nextSc=new Vector3(0.45f,0.9f,0.45f);
			}
//			else if((int)objectType==1){
//				nextSc= new Vector3(sc.x,1.2f,sc.z);//this.transform.position.y-planePos.position.y,sc.z);
//
//
//
//			}
		}else{
			counterForRot=20;

			if((int)objectType==0){
				nextSc =new Vector3(0,0,0);
				nextPos=this.transform.position;
			}
//			else if((int)objectType==1){
//				nextSc= new Vector3(sc.x,0,sc.z);
//
//			}
		}
		animatedObj.transform.position=Vector3.Lerp(pos,nextPos,Time.deltaTime*speed);
		animatedObj.transform.localScale=Vector3.Lerp(sc,nextSc,Time.deltaTime*speed);
	
	}
}
