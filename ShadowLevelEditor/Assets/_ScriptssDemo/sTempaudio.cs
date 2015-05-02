using UnityEngine;
using System.Collections;

public class sTempaudio : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}