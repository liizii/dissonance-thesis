using UnityEngine;
using System.Collections;

public class dudeAnime : MonoBehaviour {
	public Animator Anim;



	void Start () {
		Anim = this.transform.GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)){
		Anim.SetBool("walk",true);
		}else{
			Anim.SetBool("walk",false);
		}


	
	}
}
