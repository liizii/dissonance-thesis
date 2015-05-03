using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objRotBlock : MonoBehaviour {
	public List<GameObject> objBottom=new List<GameObject>();
	List<Vector3> objBottomOPos=new List<Vector3>();
	List<Vector3> objBottomSplitPos=new List<Vector3>();
	public List<GameObject> objXY=new List<GameObject>();
	List<Vector3> objXYOPos=new List<Vector3>();
	public List<GameObject> objZY=new List<GameObject>();
	List<Vector3> objZYOPos=new List<Vector3>();

	public GameObject animatedObj;
	public List<GameObject> objIControl=new List<GameObject>();
	List<GameObject> controlledAnimatedObj=new List<GameObject>();
	public Transform planePos;

	public float speed;
	public Vector3 objBtDist;
	float splitDist=0.5f;

	void Start () {
		for(int i=0;i<objBottom.Count;i++){
			objBottomOPos.Add(objBottom[i].transform.position);
			objBottomSplitPos.Add(objBottom[i].transform.localPosition);
		}
		for(int i=0;i<objXY.Count;i++){
			objXYOPos.Add(objXY[i].transform.position);
		}
		for(int i=0;i<objZY.Count;i++){
			objZYOPos.Add(objZY[i].transform.position);
		}

		for(int i=0;i<objBottomSplitPos.Count;i++){
			Vector3 cDist=splitDist* new Vector3(Mathf.Cos(Mathf.PI/4*i+Mathf.PI/8),-1,Mathf.Sin(Mathf.PI/4*i+Mathf.PI/8));
			objBottomSplitPos[i]=objBottomSplitPos[i]+cDist;
		}


		for(int i=0;i<objIControl.Count;i++){
			GameObject newObj = Instantiate(animatedObj) as GameObject;
			newObj.transform.eulerAngles=new Vector3(0,0,180);
			controlledAnimatedObj.Add(newObj);
		}
	}

	void Update () {
		List<GameObject> moveObj=objBottom;
		List<Vector3> nextPos=objBottomOPos;
		Vector3 nextDist=new Vector3(0,0,0);

		if(Character3D._touch3dObj && Character3D._touch3dObj==this.gameObject.transform){
//******************when it is rotating

//******************when it becomes 3d character
				//objMove(objBottom,objBottomOPos,objBtDist);
				moveObj=objBottom;
				nextPos=objBottomOPos;
				nextDist=objBtDist;
				poleScale(1);
			myPole(new Vector3(0.2f,0.5f,0.2f));
			//}
		}else{
//****************** when it returns to normal

//			if(GetComponent<toParent>().beTouched!=2){
//			//objMove(objBottom,objBottomOPos,new Vector3(0,0,0));
//				moveObj=objBottom;
//				nextPos=objBottomOPos;
//				nextDist=new Vector3(0,0,0);
//			}
			if(GetComponent<BlockInformation>().beTouched==0){
				//objMove(objXY,objXYOPos,objBtDist);
				moveObj=objXY;
				nextPos=objXYOPos;
				nextDist=objBtDist;

			}else if(GetComponent<BlockInformation>().beTouched==1){
				//objMove(objZY,objZYOPos,objBtDist);
				moveObj=objZY;
				nextPos=objZYOPos;
				nextDist=objBtDist;

			}
			poleScale(0);
			myPole(new Vector3(0.2f,0f,0.2f));
		}



		if(Character3D._hit3dObj && Character3D._hit3dObj==this.gameObject.transform && Character3D._pRotDirection!=0){
			objMoveL(objBottom,objBottomSplitPos,new Vector3(0,0,0));
			moveObj=objZY;
			nextPos=objZYOPos;
			nextDist=objBtDist;
		}else{
			objMove(moveObj,nextPos,nextDist);
		}

	}

	void objMove(List<GameObject> listGO,List<Vector3> listPos,Vector3 dist){
		for(int i=0;i<listGO.Count;i++){
			listGO[i].transform.position=Vector3.Lerp(listGO[i].transform.position,listPos[i]+dist,Time.deltaTime*speed);
		}
	}

	void objMoveL(List<GameObject> listGO,List<Vector3> listPos,Vector3 dist){
		for(int i=0;i<listGO.Count;i++){
			listGO[i].transform.localPosition=listPos[i];
		}
	}

	void poleScale(float scY){
		for(int i=0;i<controlledAnimatedObj.Count;i++){
			controlledAnimatedObj[i].transform.position=new Vector3(objIControl[i].transform.position.x,planePos.position.y,objIControl[i].transform.position.z);
			Vector3 inextsc=controlledAnimatedObj[i].transform.localScale;
			inextsc.y=scY*(objIControl[i].transform.position.y-planePos.position.y)/2;
			controlledAnimatedObj[i].transform.localScale=Vector3.Lerp(controlledAnimatedObj[i].transform.localScale,inextsc,Time.deltaTime*speed);
		}
	}

	void myPole(Vector3 sc){
		animatedObj.transform.localScale=Vector3.Lerp( animatedObj.transform.localScale,sc,Time.deltaTime*speed);
	}




}
