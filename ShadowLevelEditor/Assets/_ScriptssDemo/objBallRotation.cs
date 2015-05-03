using UnityEngine;
using System.Collections;

public class objBallRotation : MonoBehaviour {
	public Vector3 scaleB;
	public Transform controller;
	public Transform center;
	Vector3 scaleA;
	Vector3 nextS;
	Color colorA;


	void Start () {
		scaleA=transform.localScale;
		colorA=GetComponent<Renderer>().material.color;
	
	}
	

	void Update () {
		if(Character3D._touch3dObj && Character3D._touch3dObj==controller){
			changeColor(new Color(1,1,1,0.35f),7);
			if(Character3D._pRotDirection==0){
				nextS=scaleB;
			}else{
				nextS=scaleB+new Vector3(0.6f,0.6f,0.6f);
			}
		}else{
			changeColor(colorA,7);
			nextS=scaleA;
			if(center)
			if(controller.GetComponent<BlockInformation>().beTouched==2){
				changeScale(center.gameObject,new Vector3(0.35f,0.35f,0.35f),7);
			}else{
				changeScale(center.gameObject,new Vector3(0.65f,0.65f,0.65f),7);
			}
		}
		changeScale(this.gameObject,nextS,7);
	
	}

	void changeScale(GameObject go, Vector3 endScale, float t){
		go.transform.localScale=Vector3.Lerp(go.transform.localScale,endScale,Time.deltaTime*t);
	}

	void changeColor(Color endColor,float t){
		GetComponent<Renderer>().material.color=Color.Lerp(GetComponent<Renderer>().material.color,endColor,Time.deltaTime*t);
	}
}
