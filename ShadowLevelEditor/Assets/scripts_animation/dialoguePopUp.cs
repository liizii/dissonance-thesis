using UnityEngine;
using System.Collections;

public class dialoguePopUp : MonoBehaviour {
	float animDuration = .9f;
	float animRamp = 2.5f;
	float timer = 0;
	int counter = 50;
	bool enableDialogue = false;
	public float interp;

	void Start () {

	
	}
	

	void FixedUpdate () {
		counter --;
		if(counter < 0){
			enableDialogue = true;
		}

		if(enableDialogue){
		interp = Mathf.Clamp01(timer / animDuration);
		
		if (interp < .5f)
		{
			interp *= 2;
			interp = Mathf.Pow(interp, animRamp);
			interp *= .5f;
		}
		else
		{
			interp -= .5f;
			interp *= 2;
			interp = 1- interp;
			
			interp = Mathf.Pow(interp, animRamp);
			
			interp = 1 - interp;
			interp *= .5f;
			interp += .5f;
		}
		transform.localScale = Vector3.Lerp(new Vector3(0,0,0), new Vector3(1,1,1), interp);
		timer += Time.deltaTime;
		}
	
	}
}
