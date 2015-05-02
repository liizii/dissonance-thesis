using UnityEngine;
using System.Collections;

public class objBalls : MonoBehaviour {
	public Transform controller;
	public Transform center;
	public Vector3 scaleB;
	Vector3 scaleA;
	Vector3 nextS;
	Color colorA;
	public GameObject myMesh;
	public bool  iamdirected=false;
	int countA=20;
	int countB=20;
	//public GameObject[] linedots;


	void Start(){
		scaleA=this.transform.localScale;
		nextS=scaleB;
		colorA=myMesh.GetComponent<Renderer>().material.color;
	}

	void Update(){

		if(Character3D._touch3dObj && Character3D._touch3dObj==controller){
			changeColor(new Color(1,1,1,0.4f),4);
			if(Character3D._pRotDirection==0){
				nextS=scaleB+new Vector3(0.15f,0.15f,0.15f);
			}else{
				nextS=scaleB+new Vector3(0.5f,0.5f,0.5f);
			}
			if(iamdirected){
				changeScale(this.gameObject,nextS,7);
			}

		}else{
				nextS = scaleA;

//			if(nextS!=scaleA && nextS!=scaleB){
//				nextS=scaleA;
//			}
//			changeColor(colorA,7);
//			if(nextS==scaleA){
//				countA=20;
//				countB--;
//				if(countB<0)
//					nextS=scaleB;
//			}
//			if(nextS==scaleB){
//				countB=20;
//				countA--;
//				if(countA<0)
//					nextS=scaleA;
//			}
			changeScale(this.gameObject,nextS,2.5f);

		}

		if(center)
		if(controller.GetComponent<toParent>().beTouched==2){
			changeScale(center.gameObject,new Vector3(0.3f,0.3f,0.3f),7);
		}else{

			changeScale(center.gameObject,new Vector3(0.55f,0.55f,0.55f),7);
		}

	}

	void changeScale(GameObject go, Vector3 endScale, float t){
		go.transform.localScale=Vector3.Lerp(go.transform.localScale,endScale,Time.deltaTime*t);
	}

	void changeColor(Color endColor,float t){
		myMesh.GetComponent<Renderer>().material.color=Color.Lerp(myMesh.GetComponent<Renderer>().material.color,endColor,Time.deltaTime*t);
	}

}
