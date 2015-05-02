using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerDummy : MonoBehaviour {
	public List<GameObject> meshes = new List<GameObject>();
	public int myStatus=2;//0:xy; 1:zy
	public int displayMeshes=0;//0:cut off; -1: gradually cut; 1: gradually display
	public Transform pXY;
	public Transform pZY;
	public Transform pxyxDir;
	float nextAlpha =0;

	void Start () {
	
	}
	

	void Update () {
		transform.eulerAngles=new Vector3(0,pxyxDir.eulerAngles.y-90,0);
		for(int i=0; i<meshes.Count;i++){
			Color icolor=meshes[i].GetComponent<Renderer>().material.color;
			icolor.a=nextAlpha;
			meshes[i].GetComponent<Renderer>().material.color=Color.Lerp(meshes[i].GetComponent<Renderer>().material.color,icolor,Time.deltaTime*9);
		}
		if(Character3D.touch3dObj && !pxyxDir.parent.GetComponent<Character3D>().DoesExist){
			nextAlpha=0.5f;
			if(myStatus==0){
				transform.position=new Vector3(pXY.transform.position.x,pXY.transform.position.y,Character3D.touch3dObj.transform.position.z);
			}else if(myStatus==1){
				transform.position=new Vector3(Character3D.touch3dObj.transform.position.x,pZY.transform.position.y,-pZY.transform.position.x);
			}
		}else{
			nextAlpha=0;
		}

		for(int i=0; i<meshes.Count;i++){
			if(pxyxDir.parent.GetComponent<Character3D>().DoesExist){
				//meshes[i].SetActive(false);
			}else{
				//meshes[i].SetActive(true);
			}
		}
	
	}
}
