using UnityEngine;
using System.Collections;

public class BlockInformation : MonoBehaviour
{
	[SerializeField]
	private Rotatable _rotatable;
	public Rotatable MyRotatable {
		get {return _rotatable;}
	}

    // public Transform myParent;
    [HideInInspector]
    public int beTouched = 2;
    Color oColor;
    // [SerializeField]
    // bool _canRotate = false;
    public bool CanRotate
    {
        get { return _rotatable != null; }
    }

    [HideInInspector]
    public Vector3 myVel;
    [HideInInspector]
    public float rotOnce = 0;

    void Start()
    {
        myVel = new Vector3(0, 0, 0);
        if (this.GetComponent<Renderer>().materials.Length > 1)
            oColor = this.GetComponent<Renderer>().materials[1].color;
    }

    void FixedUpdate()
    {
        if (this.GetComponent<Renderer>().materials.Length > 1)
        if (beTouched == 2)
        {
            this.GetComponent<Renderer>().materials[1].color = oColor;
        }
        else
        {
            if (beTouched == PlayerInputController.playerStatus)
            {
                this.GetComponent<Renderer>().materials[1].color = oColor / 2;
            }
            else
            {
                this.GetComponent<Renderer>().materials[1].color = oColor / 2;
            }
        }
    }
}
