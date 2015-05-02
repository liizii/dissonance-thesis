using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelStart : MonoBehaviour {
	public bool shouldExit=false;
	public int counter;
	public GameObject[] showObj;
	public GameObject[] disableObj;
	bool triggered=false;
	public GameObject exitL;
	public GameObject exitR;
	public int enterNextCounter=120;
	public GameObject[] exitObj;

	void Start () {
	
	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Application.LoadLevel(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Application.LoadLevel(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Application.LoadLevel(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			Application.LoadLevel(3);
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			Application.LoadLevel(4);
		}

		if(Input.GetKeyDown(KeyCode.Alpha6)){
			Application.LoadLevel(5);
		}


		if(Input.GetKeyDown(KeyCode.P)){
			int i = Application.loadedLevel;
			Application.LoadLevel(i + 1);
		}



		if(exitL  && exitR)
		if(exitL.GetComponent<exit>().triggered && exitR.GetComponent<exit>().triggered){
			for (int i = 0; i <  exitObj.Length; i++) {
				exitObj[i].GetComponent<myAppearance>().shouldAppear=false;
				if(exitObj[i].name=="Cover"){
					exitObj[i].GetComponent<myAppearance>().shouldAppear=true;
				}
			}
			shouldExit=true;
		}

		if(shouldExit){
			enterNextCounter--;
			if(enterNextCounter<0){
			int i = Application.loadedLevel;
			Application.LoadLevel(i + 1);
			}
		}



		counter--;
		if(counter<0){

				for (int i = 0; i <  disableObj.Length; i++) {
					disableObj[i].GetComponent<myAppearance>().shouldAppear=false;
				}
				
				if(counter<-50)
				if(!triggered){
					for (int i = 0; i <  showObj.Length; i++) {
						showObj[i].GetComponent<myAppearance>().shouldAppear=true;
						triggered=true;
					}
				}
		}
		
	}
}
