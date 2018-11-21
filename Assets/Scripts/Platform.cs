using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Transform[] Slices;

	public void SetSignature(string signature) {
		for (int i = 0; i < signature.Length; i++) {
			char c = signature[i];
			// Empty
			if (c == 'E') Slices[i].gameObject.SetActive(false);
			// Normal Platform
			else if (c == 'P') { }
			// Death 
			else if (c == 'D') Slices[i].GetComponent<MeshRenderer>().material.color = Color.red;
		}
	}
}