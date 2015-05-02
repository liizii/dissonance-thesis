using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {
	public bool touched=false;
	public GameObject[] enableObj;
	public GameObject[] disableObj;
	public bool triggered=false;
	Color nextColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.FindChild("Quad"))
			transform.FindChild("Quad").GetComponent<Renderer>().material.color=Color.Lerp(transform.FindChild("Quad").GetComponent<Renderer>().material.color,nextColor,Time.deltaTime*4);

		if(touched){
			nextColor=new Color(0.1f,0.1f,0.1f);
			if(!triggered){
				for (int i = 0; i <  enableObj.Length; i++) {
					enableObj[i].GetComponent<myAppearance>().shouldAppear=true;
				}
				for (int i = 0; i <  disableObj.Length; i++) {
					disableObj[i].GetComponent<myAppearance>().shouldAppear=false;
				}
				triggered=true;
			}
		}else{
			nextColor=Color.grey;
		}

	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.name=="triggerXY" && this.name=="exitL")
			touched=true;

		if(other.name=="triggerZY"&& this.name=="exitR")
			touched=true;
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.name=="triggerXY" && this.name=="exitL")
			touched=false;
		if(other.name=="triggerZY" && this.name=="exitR")
			touched=true;
	}
}
