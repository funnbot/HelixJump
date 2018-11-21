using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform Player;
	public float CameraOffset;

	void Start() {
		var playerY = Player.position.y;
		var pos = transform.position;
		pos.y = playerY + CameraOffset;
		transform.position = pos;
	}

	void Update() {
		var playerY = Player.position.y;
		if (playerY + CameraOffset < transform.position.y) {
			var pos = transform.position;
			pos.y = playerY + CameraOffset;
			transform.position = pos;
		}
	}
}