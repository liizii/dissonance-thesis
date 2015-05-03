using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class objRotAnimation : MonoBehaviour {
	public GameObject bottom;
	public Vector3 bottomDist;
	Vector3 bottomOPos;
	public List<GameObject> objIControl=new List<GameObject>();
	public List<GameObject> objPoles=new List<GameObject>();
	//public List<GameObject> gearParts=new List<GameObject>();
	List<GameObject> controlledAnimatedObj=new List<GameObject>();
	Transform planePos;
	public Transform animatedObj;
	public float speed;


	void Start () {
		bottomOPos=bottom.transform.position;
		planePos=GameObject.Find("PlaneXY").transform;
		for(int i=0;i<objIControl.Count;i++){
			GameObject newObj = animatedObj.gameObject;
			newObj.transform.eulerAngles=new Vector3(0,0,180);
			controlledAnimatedObj.Add(newObj);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Character3D._touch3dObj && Character3D._touch3dObj==this.gameObject.transform){
			for(int i=0;i<controlledAnimatedObj.Count;i++){
				controlledAnimatedObj[i].transform.position=new Vector3(objIControl[i].transform.position.x,planePos.position.y-0.2f,objIControl[i].transform.position.z);
				Vector3 inextsc=controlledAnimatedObj[i].transform.localScale;
				inextsc.y=(objIControl[i].transform.position.y-planePos.position.y)/2;
				controlledAnimatedObj[i].transform.localScale=Vector3.Lerp(controlledAnimatedObj[i].transform.localScale,inextsc,Time.deltaTime*speed);
			}
//			for(int i=0;i<gearParts.Count;i++){
//				Vector3 gpsc=gearParts[i].transform.localScale;
//				gpsc.z=2.1f;
//				gearParts[i].transform.localScale=Vector3.Lerp(gearParts[i].transform.localScale,gpsc,Time.deltaTime*speed);
//			}
		}else{
//			for(int i=0;i<gearParts.Count;i++){
//				Vector3 gpsc=gearParts[i].transform.localScale;
//				gpsc.z=0;
//				gearParts[i].transform.localScale=Vector3.Lerp(gearParts[i].transform.localScale,gpsc,Time.deltaTime*speed);
//			}
			for(int i=0;i<controlledAnimatedObj.Count;i++){
				controlledAnimatedObj[i].transform.position=new Vector3(objIControl[i].transform.position.x,planePos.position.y-0.2f,objIControl[i].transform.position.z);
				Vector3 inextsc=controlledAnimatedObj[i].transform.localScale;
				inextsc.y=0;
				controlledAnimatedObj[i].transform.localScale=Vector3.Lerp(controlledAnimatedObj[i].transform.localScale,inextsc,Time.deltaTime*speed);
			}
		}

		if(GetComponent<BlockInformation>().beTouched!=2){
			bottom.transform.position=Vector3.Lerp(bottom.transform.position,bottomOPos+bottomDist,Time.deltaTime*speed);
			for(int i=0;i<objPoles.Count;i++){
				objPoles[i].transform.localScale=Vector3.Lerp(objPoles[i].transform.localScale,new Vector3(0.2f,0.1f,0.2f),Time.deltaTime*speed);

			}
		}else{
			bottom.transform.position=Vector3.Lerp(bottom.transform.position,bottomOPos,Time.deltaTime*speed);
			for(int i=0;i<objPoles.Count;i++){
				objPoles[i].transform.localScale=Vector3.Lerp(objPoles[i].transform.localScale,new Vector3(0.2f,0,0.2f),Time.deltaTime*speed);
				
			}
		}
	
	}
}
