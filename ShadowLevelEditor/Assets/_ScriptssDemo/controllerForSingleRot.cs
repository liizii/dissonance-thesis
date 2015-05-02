using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controllerForSingleRot : MonoBehaviour {
	public List<Transform> forObjs= new List<Transform>();
	public List<Vector3> objRotA= new List<Vector3>();
	public List<Vector3> objRotB= new List<Vector3>();
	float moveChild=0;
	toParent _toparent;
//	List<Vector3> nextAngles = new List<Vector3>();
//	public int rotAxis = 0;//0:x, 1:y, 2:z
	[SerializeField]
	Transform handle;

	void Start () {
		if(GetComponent<toParent>())
		_toparent = GetComponent<toParent>();
//		foreach(Transform r in forObjs){
//			nextAngles.Add(r.transform.eulerAngles);
//		}


	}
	
	
	void Update () {
//		if(_toparent){
//			if(_toparent.beTouched != 2)
//				if(GetComponent<toParent>().rotOnce == 1){
//					if(rotAxis == 0)
//					nextAngles = 
//
//			    }
//				else if(GetComponent<toParent>().rotOnce == -1){
//					
//				}
//		}

		if(Character3D._touch3dObj && Character3D._touch3dObj.gameObject == this.gameObject){
			if(GetComponent<toParent>().rotOnce != 0 && moveChild >= 0 && moveChild <= forObjs.Count){
				moveChild = moveChild + GetComponent<toParent>().rotOnce;
				if(moveChild<0)
					moveChild=0;
				if(moveChild > forObjs.Count)
					moveChild =  forObjs.Count;
				GetComponent<toParent>().rotOnce=0;
			}
			if(moveChild>=0)
			for(int i = 0; i < forObjs.Count; i++){
				Vector3 childNextRot = forObjs[i].eulerAngles;
				if(i < moveChild){
					childNextRot = objRotB[i];
				}else{
					childNextRot = objRotA[i];

				}
				
				forObjs[i].eulerAngles = Vector3.Lerp(forObjs[i].eulerAngles,childNextRot,Time.deltaTime*5);
			}

		}

	}
}
		
