using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorial3control : MonoBehaviour {
	public GameObject[] exits;
	public Text[] narrative;



	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(exits[0].transform.FindChild("triggerL").GetComponent<tutorialExit>().triggered &&
		   exits[1].transform.FindChild("triggerR").GetComponent<tutorialExit>().triggered){
			narrative[0].enabled=false;
			narrative[1].enabled=true;

		}

		if(Character3D._touch3dObj && Character3D._touch3dObj.GetComponent<toParent>().rotatable){
			narrative[0].enabled=true;
		}else{
				narrative[0].enabled=false;
		}
	
	}
}
