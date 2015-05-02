using UnityEngine;
using System.Collections;

public class tutorialExit : MonoBehaviour {
	public bool triggered=false;
	public GameObject myMesh;
	
	void Start () {
	
	}

	void Update () {
		Color cl=myMesh.GetComponent<Renderer>().material.color;
		if(triggered){
			cl.a=0;
		}else{
			cl.a=1;
		}
		myMesh.GetComponent<Renderer>().material.color=Color.Lerp(myMesh.GetComponent<Renderer>().material.color,cl,Time.deltaTime*6);
	
	}
	void OnTriggerStay2D(Collider2D other) {
		if(other.name=="triggerXY" && this.name=="triggerL"){
			triggered=true;
		}
		if(other.name=="triggerZY" && this.name=="triggerR"){
			triggered=true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.name=="triggerXY" && this.name=="triggerL")
			triggered=false;
		if(other.name=="triggerZY" && this.name=="triggerL")
			triggered=false;
	}
}
