using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
	public Transform Player;
	public GameObject PlatformFab;
	public float SpaceBetween;

	private float playerY {
		get { return Player.position.y; }
	}
	private Platform[] platforms;
	private float spawnedY;

	void Start() {
		spawnedY = playerY;
	}

	void Update() {
		Debug.Log(playerY + " : " + spawnedY + " : " + (playerY - spawnedY));
		if (spawnedY - playerY > SpaceBetween) {
			spawnedY = playerY;
			SpawnPlatform();
		}
	}

	void SpawnPlatform() {
		Transform t = Instantiate(PlatformFab).transform;
		Platform p = t.GetComponent<Platform>();
		t.SetParent(transform);
		t.position = (spawnedY - 15)* Vector3.up;
		p.SetSignature("PPPPPPDEEDPPPPPP");
	}
}