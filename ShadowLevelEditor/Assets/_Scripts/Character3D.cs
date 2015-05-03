using UnityEngine;
using System.Collections;

public class Character3D : MonoBehaviour {
	public static Transform _hit3dObj;
	public static Transform _touch3dObj;
	public static float _pRotDirection=0;
	[SerializeField]
	private Transform _rotParent;
	[SerializeField]
	private lianDummy _dummyXY;
	[SerializeField]
	private lianDummy _dummyZY;
	public Vector3 _targetPos; // XXX
	[SerializeField]
	private Light _lightExist;
	[SerializeField]
	private float _rotationSpeed;

	private Transform _oParent;
	private float _fractionRotated = 0f;
	private bool _isRotating = false;
	private Renderer[] _myMesh;
	float _nextAngle=0;
	Quaternion _nextRot;
	bool _iHitObject=false;

	[SerializeField]
	GameObject _playerxy;
	[SerializeField]
	GameObject _playerzy;
	Renderer _renderer;
	[SerializeField]
	float _visibilityThreshold = 1f;
	[SerializeField]
	float _raycastEpsilon = 0.1f;
	[SerializeField]
	float _pivotGridPrecision = 0f;

	Transform _playerxyTransform;
	Transform _playerzyTransform;
	BoxCollider2D _playerxyCollider;
	BoxCollider2D _playerzyCollider;

	Transform _transform;

	void Awake () {
		_playerxyTransform = _playerxy.transform;
		_playerzyTransform = _playerzy.transform;
		_playerxyCollider = _playerxy.GetComponent<BoxCollider2D>();
		_playerzyCollider = _playerzy.GetComponent<BoxCollider2D>();
		_transform = transform;
		_renderer = GetComponent<Renderer>();
		_myMesh = GetComponentsInChildren<Renderer>();
	}

	private float YPosition {
		get { return (_playerxyTransform.position.y + _playerzyTransform.position.y)/2f; }
	}

	public bool DoesExist {
		get { return Mathf.Abs(_playerxyTransform.position.y - _playerzyTransform.position.y) < _visibilityThreshold && _iHitObject; }//use this for transparent
	}

	//zi: detect if i hit an object
	void detectHitObject() {
		RaycastHit[] hitInfo = new RaycastHit[4];
		bool[] didHit = new bool[4];
		float maxDistance = 1.5f;
		Collider hitCollider = null;
		Vector3 pivot = SnappingMath.SnapToRoundedOffset (_transform.position, _pivotGridPrecision);
		didHit[0] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.right * _playerxyCollider.size.x/2f, -Vector3.up, out hitInfo[0]);
		didHit[1] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.left * _playerxyCollider.size.x/2f, -Vector3.up, out hitInfo[1]);
		didHit[2] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.forward * _playerzyCollider.size.x/2f, -Vector3.up, out hitInfo[2]);
		didHit[3] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.back * _playerzyCollider.size.x/2f, -Vector3.up, out hitInfo[3]);
		for (int i = 0; i<4; i++) {
			float currDistance = hitInfo[i].distance;
			if (didHit[i] && currDistance < maxDistance) {
				maxDistance = currDistance;
				hitCollider = hitInfo[i].collider;
			}
		}
		if(_touch3dObj && _touch3dObj.GetComponent<BlockInformation>() && _touch3dObj.GetComponent<BlockInformation>().beTouched!=0 && _touch3dObj.GetComponent<BlockInformation>().beTouched!=1)
			_touch3dObj.GetComponent<BlockInformation>().beTouched = 2;
		if (hitCollider == null) {
			_iHitObject=false;

			_touch3dObj=null;
		} else {
			if(hitCollider.transform.parent.name!="Planes")
				_iHitObject=true;
			_touch3dObj=hitCollider.transform;
			if(_touch3dObj.GetComponent<BlockInformation>())
			_touch3dObj.GetComponent<BlockInformation>().beTouched = 10;
		}
	}

	public void Rotate(float direction) {
		if (_isRotating) {
			return;
		}

		RaycastHit[] hitInfo = new RaycastHit[4];
		bool[] didHit = new bool[4];
		float maxDistance = Mathf.Infinity;
		Collider hitCollider = null;

		Vector3 pivot = SnappingMath.SnapToRoundedOffset (_transform.position, _pivotGridPrecision);
		// Debug.DrawLine(pivot - new Vector3(0,10,0), pivot + new Vector3(0,10,0), Color.red, 10f);

		// XXX (JULIAN): THIS DOES NOT DO WHAT IT SHOULD (PIVOTS NOW WORK DIFFERENTLY)!!!
		didHit[0] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.right * _playerxyCollider.size.x/2f, -Vector3.up, out hitInfo[0]);
		didHit[1] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.left * _playerxyCollider.size.x/2f, -Vector3.up, out hitInfo[1]);
		didHit[2] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.forward * _playerzyCollider.size.x/2f, -Vector3.up, out hitInfo[2]);
		didHit[3] = Physics.Raycast(pivot + Vector3.up * _raycastEpsilon + Vector3.back * _playerzyCollider.size.x/2f, -Vector3.up, out hitInfo[3]);
		for (int i = 0; i<4; i++) {
			float currDistance = hitInfo[i].distance;
			if (didHit[i] && currDistance < maxDistance) {
				maxDistance = currDistance;
				hitCollider = hitInfo[i].collider;
			}
		}

		if (hitCollider == null) {
			Debug.LogError("HIT NOTHING!");
		} else {
			BlockInformation blockInfo = hitCollider.GetComponent<BlockInformation>();
			if(hitCollider.transform.parent.name!="Planes" && blockInfo != null && blockInfo.CanRotate){
				_pRotDirection=direction;
				if(blockInfo.myParent.GetComponent<EditorBlock>()){
					_oParent=blockInfo.myParent.GetComponent<EditorBlock>().oParentObj.transform;
				}else{
					_oParent=null;
				}
				blockInfo.rotOnce=direction;
				blockInfo.myParent.parent=_rotParent;//!!!!!!how to find real parent?
				_hit3dObj=hitCollider.transform;
				_nextAngle=(_nextAngle+direction*90)%360;
				_nextRot = Quaternion.Euler(_rotParent.eulerAngles.x, _nextAngle, _rotParent.eulerAngles.z);

				_fractionRotated = 0f;
				_isRotating = true;
			}
		}
	}

	void DoRotation () {
		if (_isRotating) {
			_fractionRotated += Time.deltaTime * _rotationSpeed;
			if (_fractionRotated >= 1f) {
				_isRotating = false;
				_fractionRotated = 1f;

				// XXX (JULIAN): WAT???
				if(_hit3dObj!=null){
					if(_oParent){
						_hit3dObj.GetComponent<BlockInformation>().myParent.parent=_oParent;
					}else{
						_hit3dObj.GetComponent<BlockInformation>().myParent.parent=null;
					}
				}//!!!!!!how to find real parent?
				_pRotDirection=0;
			}
			_rotParent.rotation = Quaternion.Lerp(_rotParent.rotation,_nextRot,_fractionRotated);
		} else if (_touch3dObj) {
			_rotParent.position = new Vector3(_touch3dObj.transform.position.x,0,_touch3dObj.transform.position.z);
		}
	}

	void Update(){
		detectHitObject();
		DoRotation();
	}

	void LateUpdate () {
		_targetPos=this.transform.position;

		if(_pRotDirection==0){
			//_transform.parent=GameObject.Find("Players").transform;
			_transform.position = new Vector3(_playerxyTransform.position.x,
		                                  YPosition,
		                                  -_playerzyTransform.position.x);
		}else{
			//_transform.parent=rotParent;
			_playerxyTransform.position=new Vector3(_transform.position.x,_transform.position.y,_playerxyTransform.position.z);
			_playerzyTransform.position=new Vector3(-_transform.position.z,_transform.position.y,_playerzyTransform.position.z);

		}
//////////Show 3D character & Animation
		if (DoesExist) {
			changeStatus(Color.black);
		} else {
			changeStatus(Color.black);
		}
	}

	void changeStatus(Color shadowColor){
		if(DoesExist && !_dummyXY.doesExist && !_dummyZY.doesExist){
			BlockInformation blockInfo = null;
			if (_touch3dObj != null) { blockInfo = _touch3dObj.GetComponent<BlockInformation>(); }
			if(_lightExist && blockInfo != null && blockInfo.CanRotate) {
			_lightExist.transform.position = this.transform.position + new Vector3(0.5f,0.5f,0.5f);
			_lightExist.enabled=true;
			_lightExist.range = Mathf.Lerp(_lightExist.range,10,Time.deltaTime*7);
			_lightExist.intensity = Mathf.Lerp (_lightExist.intensity, 0, Time.deltaTime*7);
			}
			foreach (var themesh in _myMesh){
				themesh.enabled=true;
			}
		}else{
			if(_lightExist){
			_lightExist.range=0;
			_lightExist.intensity = 1;
			_lightExist.enabled=false;
			}
			foreach (var themesh in _myMesh){
				themesh.enabled=false;
			}
		}
		if(_playerxy && _playerxy.GetComponentInChildren<SpriteRenderer>())
		_playerxy.GetComponentInChildren<SpriteRenderer>().color=shadowColor;
		if(_playerzy && _playerzy.GetComponentInChildren<SpriteRenderer>())
		_playerzy.GetComponentInChildren<SpriteRenderer>().color=shadowColor;
	}


}
