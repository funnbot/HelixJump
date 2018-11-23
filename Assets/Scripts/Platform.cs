using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Transform[] Slices;
	public Material PlatformMat,
	ObstacleMat;
	public string Signature;

	public void SetSignature(string signature) {
		Signature = signature;
		for (int i = 0; i < signature.Length; i++) {
			char c = signature[i];
			// Empty
			if (c == 'E') Slices[i].gameObject.SetActive(false);
			// Normal Platform
			else if (c == 'P') {
				Slices[i].GetComponent<MeshRenderer>().sharedMaterial = PlatformMat;
				Slices[i].tag = "Platform";
			}
			// Death 
			else if (c == 'D') {
				Slices[i].GetComponent<MeshRenderer>().sharedMaterial = ObstacleMat;
				Slices[i].tag = "Obstacle";
				Slices[i].localScale = new Vector3(1.6f, 4.5f, 1.6f);
			}
		}
	}

	public void Destroy() {
		StartCoroutine(DestroyRoutine());
	}

	IEnumerator DestroyRoutine() {
		float lerp = 0f;
		while (lerp < 1f) {
			lerp += Time.deltaTime * 2;
			for (int i = 0; i < Slices.Length; i++) {
				var slice = Slices[i];
				slice.position += slice.forward * 0.1f;
				slice.localEulerAngles += Vector3.left;
			}
			yield return null;
		}
		Destroy(gameObject);
	}
}