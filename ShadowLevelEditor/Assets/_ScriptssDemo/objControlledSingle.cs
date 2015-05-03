using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objControlledSingle : MonoBehaviour {
	public GameObject controller;
	public List<Transform> growChildren= new List<Transform>();
	public List<Vector3> childMoveDistance= new List<Vector3>();
	List<Vector3> childrenOPos= new List<Vector3>();
	float moveChild=0;

	void Start () {
		for(int i = 0; i < growChildren.Count; i++){
			childrenOPos.Add(growChildren[i].position);
		}
	}
	

	void Update () {
		if(Character3D._touch3dObj && Character3D._touch3dObj.gameObject==controller){
			if(controller.GetComponent<BlockInformation>().rotOnce!=0 && moveChild>=0 && moveChild <=growChildren.Count){
				moveChild=moveChild+controller.GetComponent<BlockInformation>().rotOnce;
				if(moveChild<0)
					moveChild=0;
				if(moveChild > growChildren.Count)
					moveChild =  growChildren.Count;
				controller.GetComponent<BlockInformation>().rotOnce=0;
			}
		}
		if(moveChild>=0)
			for(int i = 0; i < growChildren.Count; i++){
			Vector3 childNextPos=growChildren[i].position;
				if(i < moveChild){
				childNextPos = childrenOPos[i]+childMoveDistance[i];
				}else{
				childNextPos = childrenOPos[i];
			}

			growChildren[i].position = Vector3.Lerp(growChildren[i].position,childNextPos,Time.deltaTime*2);
			}

	
	}
}
