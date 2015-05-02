using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rotationAnchorAnimation : MonoBehaviour { 
		toParent _toparent;
		public List<Transform> anchorObj = new List<Transform>();
		List<Vector3> anchorObjOPos = new List<Vector3>();
		List<Vector3> anchorObjOScale = new List<Vector3>();
		public List<Transform> anchorObjTarget = new List<Transform>();
		public List<Transform> anchorObjRotTarget = new List<Transform>();
		float speed = 6;
		
		void Start(){
		_toparent = GetComponent<toParent>();
			for (int i = 0; i < anchorObj.Count; i++){
				anchorObjOPos.Add(anchorObj[i].position);
				anchorObjOScale.Add(anchorObj[i].localScale);
			}
		}
		
		void LateUpdate(){
			for (int i = 0; i < anchorObj.Count; i++){
				if(_toparent.beTouched == 2)
					lerpPosScale (anchorObj[i],anchorObjOPos[i],anchorObjOScale[i]);
			else if(_toparent.beTouched == 0 || _toparent.beTouched == 1)
				lerpPosScale (anchorObj[i],anchorObjTarget[i].position,anchorObjTarget[i].localScale);
			else if(_toparent.beTouched == 10)
				lerpPosScale (anchorObj[i],anchorObjRotTarget[i].position,anchorObjRotTarget[i].localScale);
				
			}
			
		}
		
		void lerpPosScale(Transform trans, Vector3 pos, Vector3 scale){
			trans.position = Vector3.Lerp(trans.position,pos,Time.deltaTime*speed);
			trans.localScale = Vector3.Lerp(trans.localScale,scale,Time.deltaTime*speed);
			
		}
		
	}