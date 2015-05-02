using UnityEngine;
using System.Collections;

public class dudecloth : MonoBehaviour {
	public Animator Anim;
	public bool playanim=false;
	public int me=2;

	void Start () {
		Anim = this.transform.GetComponent<Animator>();
	}

	void Update () {
		if(Character3D.touch3dObj && Character3D.touch3dObj.GetComponent<toParent>() && Character3D.touch3dObj.GetComponent<toParent>().rotatable ){
			Anim.SetBool("shouldStayUp",true);
		}else if(Input.GetKey(KeyCode.UpArrow)){
			if(me==PlayerInputController.playerStatus){
			Anim.SetBool("shouldStayUp",true);
			}else{
				Anim.SetBool("shouldStayUp",false);
			}
		}else{
			Anim.SetBool("shouldStayUp",false);
		}


	}
}
