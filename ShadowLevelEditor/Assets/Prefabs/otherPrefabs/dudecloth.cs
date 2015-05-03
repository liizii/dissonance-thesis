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
		if(Character3D._touch3dObj && Character3D._touch3dObj.GetComponent<BlockInformation>() && Character3D._touch3dObj.GetComponent<BlockInformation>().CanRotate ){
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
