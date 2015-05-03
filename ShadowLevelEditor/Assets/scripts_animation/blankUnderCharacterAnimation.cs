using UnityEngine;
using System.Collections;

public class blankUnderCharacterAnimation : MonoBehaviour {
	BlockInformation _toparent;
	toShadows _toshadows;
	float speed = 6;
	Color startColor;

	void Start () {
		if(GetComponent<BlockInformation>())
		_toparent = GetComponent<BlockInformation>();
		startColor = GetComponent<Renderer>().material.color;
	
	}
	

	void FixedUpdate () {
		if(!_toshadows && GetComponent<toShadows>())
			_toshadows = GetComponent<toShadows>();

		if(_toparent){
			if(_toparent.beTouched == 2)
				changeColor(this.gameObject,startColor,Color.black);
			if(_toparent.beTouched == 0)
				changeColor(this.gameObject,startColor/2,startColor/2);
			if(_toparent.beTouched == 1)
				changeColor(this.gameObject,startColor/2,startColor/2);
			if(_toparent.beTouched == 10)
				changeColor(this.gameObject,startColor/4,startColor/4);

		}
	
	}

	void changeColor(GameObject g, Color nextC, Color shadowC){
		foreach(Material m in g.GetComponent<Renderer>().materials)
			m.color = Color.Lerp(m.color,nextC,Time.fixedTime * speed);
		if(_toshadows)
			foreach(Transform k in _toshadows.myShadows)
			k.GetComponent<Renderer>().material.color = Color.Lerp(k.GetComponent<Renderer>().material.color,shadowC,Time.fixedDeltaTime*speed);
	}
}
