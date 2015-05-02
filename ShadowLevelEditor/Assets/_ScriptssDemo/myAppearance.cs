using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class myAppearance : MonoBehaviour {
	public bool shouldAppear=false;
	public Color nextColor;

	void Start () {
	
	}
	

	void Update () {
		//text
		if(this.tag=="text"){
			if(shouldAppear){
				if(GetComponent<Text>().color.a<1f)
					GetComponent<Text>().color+=new Color(0,0,0,0.005f);
			}else{
				if(GetComponent<Text>().color.a>0)
					GetComponent<Text>().color-=new Color(0,0,0,0.01f);
			}
		}
		//light
		else if(this.tag=="light"){
			if(shouldAppear){
				if(GetComponent<Light>().intensity<1)
					GetComponent<Light>().intensity+=0.005f;
			}else{
				if(GetComponent<Light>().intensity>-0.1f)
					GetComponent<Light>().intensity-=0.005f;
			}
		}
		//object
		else{

			//this.renderer.material.color=Color.Lerp(renderer.material.color,nextColor,Time.deltaTime*4);
			if(shouldAppear){
				//nextColor=Color.white;
				GetComponent<Renderer>().enabled=true;
				this.GetComponent<Renderer>().material.color=Color.Lerp(GetComponent<Renderer>().material.color,nextColor,Time.deltaTime*2);
			}else{
				//nextColor=Color.black;
				if(this.name=="Cover"){
				this.GetComponent<Renderer>().material.color=Color.Lerp(GetComponent<Renderer>().material.color,new Color(0,0,0,0),Time.deltaTime*2);
				}else{
					GetComponent<Renderer>().enabled=false;
				}
			}
		}

	
	}
}
