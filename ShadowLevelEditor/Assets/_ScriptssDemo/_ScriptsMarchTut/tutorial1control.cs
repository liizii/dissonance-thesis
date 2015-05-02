using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorial1control : MonoBehaviour {
	public GameObject[] middleObjects;
	public GameObject[] afterDisplayObjects;
	public Transform[] afterDisplayPos;
	public bool displayMiddle=false;
	public bool displayRight=false;
	public GameObject cam;
	public Transform camTarget;
	public Transform target;
	public GameObject[] exits;
	int afterCount=60;
	int camCount=60;
	public Text[] narrative;

	void Start () {

	
	}
	

	void Update () {

		if(displayRight){

			if(camCount<0){
				narrative[0].enabled=false;
				narrative[1].enabled=true;
			if(cam.GetComponent<Camera>().orthographicSize<13){
				cam.GetComponent<Camera>().orthographicSize+=0.05f;
			}
			cam.transform.position=Vector3.Lerp(cam.transform.position,camTarget.position,Time.deltaTime*3);
			cam.transform.LookAt(target);
			}else{
				camCount--;
			}
		}
		displayObj(middleObjects,displayMiddle);
		if(displayMiddle){

			if(afterCount<0){
				narrative[1].enabled=false;
				narrative[2].enabled=true;
				for(int i=0; i<afterDisplayObjects.Length; i++){
					afterDisplayObjects[i].transform.position=Vector3.Lerp(afterDisplayObjects[i].transform.position,afterDisplayPos[i].position,Time.deltaTime);
				}
			}else{
				afterCount--;
			}
		}

		exitTriggers();
	
	}

	void exitTriggers(){
		displayRight=exits[0].transform.FindChild("triggerL").GetComponent<tutorialExit>().triggered;
		displayMiddle=exits[1].transform.FindChild("triggerR").GetComponent<tutorialExit>().triggered;
		if(displayMiddle){
			exits[0].SetActive(false);
			exits[1].SetActive(false);
			exits[2].SetActive(true);
			exits[3].SetActive(true);
		}
		if(exits[2].transform.FindChild("triggerL").GetComponent<tutorialExit>().triggered &&
		   exits[3].transform.FindChild("triggerR").GetComponent<tutorialExit>().triggered){
			narrative[2].enabled=false;
			narrative[3].enabled=true;
			if(Input.GetKeyDown(KeyCode.P)){
				int i = Application.loadedLevel;
				Application.LoadLevel(i + 1);
			}
		}
		
	}

	void displayObj(GameObject[] objects,bool dspl){
		foreach(GameObject obj in objects){
			obj.GetComponent<Renderer>().enabled=dspl;
			Color oc = obj.GetComponent<Renderer>().material.color;
			if(dspl)
				oc.a=1;
			
			obj.GetComponent<Renderer>().material.color = Color.Lerp(obj.GetComponent<Renderer>().material.color,oc,Time.deltaTime*3);
		}
	}
}
