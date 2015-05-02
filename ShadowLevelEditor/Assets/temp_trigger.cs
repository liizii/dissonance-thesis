using UnityEngine;
using System.Collections;

public class temp_trigger : MonoBehaviour {
	public Transform[] enableObjs;
	bool triggered = false;

	void Start () {
	
	}
	

	void Update () {
		if(triggered)
		foreach(Transform obj in enableObjs)
			obj.gameObject.SetActive(true);

	
	}
	void OnTriggerStay2D(Collider2D other) {
		if(other.name=="triggerXY"){
			triggered=true;
		}
	}
		
}
