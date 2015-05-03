using UnityEngine;
using System.Collections;

public class Rotatable : MonoBehaviour {
	protected enum Axis {
		X = 0, Y = 1, Z = 2
	}

	private Transform _rotator;
	private Transform _oldParent;
	private float _fractionRotated = 0f;
	private bool _isRotating = false;
	public bool IsRotating {
		get { return _isRotating; }
	}
	private float _nextAngle=0;
	private Quaternion _nextRot;
	private float _rotationDuration = 1f;
	[SerializeField]
	Axis _rotationAxis = Axis.Y;
	[SerializeField]
	bool _canBeDrivenByCharacter = true;
	[SerializeField]
	Rotatable[] _drives;

	void Awake () {
		_rotator = new GameObject(gameObject.name + "_Rotator").transform;
		_rotator.parent = transform.parent;
		_rotator.position = transform.position;
	}

	void StartRotation () {
		_oldParent = transform.parent;
		transform.SetParent(_rotator);
	}

	void FinishRotation () {
		transform.SetParent(_oldParent);
	}

	void Update () {
		if (_isRotating) {
			_fractionRotated += Time.deltaTime/_rotationDuration;
			if (_fractionRotated >= 1f) {
				_isRotating = false;
				_fractionRotated = 1f;
				FinishRotation();
			}
			_rotator.rotation = Quaternion.Lerp(_rotator.rotation, _nextRot, _fractionRotated);
		}
	}

	public void CharacterDriveRotation (float direction, Vector3 origin, float duration) {
		if (!_canBeDrivenByCharacter) {
			return;
		} else {
			PerformRotation (direction, origin, duration);
		}
	}

	public void PerformRotation (float direction, Vector3 origin, float duration) {
		if (_isRotating) {
			return;
		}
		_rotationDuration = duration;
		_rotator.position = origin;
		_nextAngle=(_nextAngle+direction*90)%360;
		Vector3 rot = _rotator.eulerAngles;
		rot[(int)_rotationAxis] = _nextAngle;
		_nextRot = Quaternion.Euler(rot);
		_fractionRotated = 0f;
		_isRotating = true;
		StartRotation();

		for (int i = 0; i < _drives.Length; i++) {
			_drives[i].PerformRotation (direction, _drives[i].transform.position, duration);
		}
	}
}