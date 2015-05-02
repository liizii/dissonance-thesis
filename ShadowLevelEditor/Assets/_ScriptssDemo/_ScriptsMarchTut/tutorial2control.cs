using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorial2control : MonoBehaviour {
	public Transform[] moveObj;
	public Transform[] moveTo;
	public bool nextMove=false;
	public GameObject[] exits;
	int nextCount=20;
	public Text[] narrative;


	void Start () {
	
	}
	

	void Update () {
		if(exits[0].transform.FindChild("triggerL").GetComponent<tutorialExit>().triggered){
			nextMove=true;
		}

		if(exits[1].transform.FindChild("triggerR").GetComponent<tutorialExit>().triggered){
			narrative[0].enabled=false;
			narrative[1].enabled=true;
			if(Input.GetKeyDown(KeyCode.P)){
				int i = Application.loadedLevel;
				Application.LoadLevel(i + 1);
			}
		}

		if(nextMove)
			if(nextCount<0){
			for(int i = 0; i < moveObj.Length; i++){
				moveObj[i].position=Vector3.Lerp(moveObj[i].position,moveTo[i].position,Time.deltaTime*2);
			}
		}else{
			nextCount--;
		}


	}
}
