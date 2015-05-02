using UnityEngine;
using System.Collections;

public class legAnime : MonoBehaviour {
	public Animator Anim;
	void Start () {
		Anim = this.transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
