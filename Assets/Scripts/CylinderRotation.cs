using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderRotation : MonoBehaviour {
	public float RotationSpeed;
	public bool InputLock;

	void Update () {
		if (!InputLock) {
			transform.Rotate(Vector3.up * -Input.GetAxis("Mouse X") * RotationSpeed);
		}
	}
}
