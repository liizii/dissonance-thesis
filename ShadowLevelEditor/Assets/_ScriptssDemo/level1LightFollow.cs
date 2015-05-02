using UnityEngine;
using System.Collections;

public class level1LightFollow : MonoBehaviour {
	public Transform target;
	public Vector3 deltaPos;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position=deltaPos+target.position;
	
	}
}
