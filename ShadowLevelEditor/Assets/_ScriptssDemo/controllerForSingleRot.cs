using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controllerForSingleRot : MonoBehaviour {
	public List<Transform> forObjs= new List<Transform>();
	public List<Vector3> objRotA= new List<Vector3>();
	public List<Vector3> objRotB= new List<Vector3>();
	float moveChild=0;
	BlockInformation _toparent;
//	List<Vector3> nextAngles = new List<Vector3>();
//	public int rotAxis = 0;//0:x, 1:y, 2:z
	[SerializeField]
	Transform handle;

	void Start () {
		if(GetComponent<BlockInformation>())
		_toparent = GetComponent<BlockInformation>();
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

		if(_toparent.beTouched == 10){
			if(GetComponent<BlockInformation>().rotOnce != 0 && moveChild >= 0 && moveChild <= forObjs.Count){
				moveChild = moveChild + GetComponent<BlockInformation>().rotOnce;
				if(moveChild<0)
					moveChild=0;
				if(moveChild > forObjs.Count)
					moveChild =  forObjs.Count;
				GetComponent<BlockInformation>().rotOnce=0;
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
		
