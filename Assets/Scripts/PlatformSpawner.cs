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

	private float spawnedY;
	private PlayerLevel Level;

	void Start() {
		spawnedY = playerY;
		Level = Player.GetComponent<PlayerLevel>();
	}

	void Update() {
		if (spawnedY - playerY > SpaceBetween) {
			spawnedY = playerY;
			SpawnPlatform();
		}
	}

	void SpawnPlatform() {
		var level = Level.GetLevel();
		string sig = GenerateSignature(level);

		Transform t = Instantiate(PlatformFab).transform;
		t.SetParent(transform);
		t.position = (spawnedY - 15) * Vector3.up;
		t.localRotation = Quaternion.identity;
		//t.localEulerAngles = Random.value > 0.5f ? new Vector3(0f, 0f, 0.001f) : new Vector3(0f, 0f, -0.001f);

		Platform p = t.GetComponent<Platform>();
		p.PlatformMat = level.PlatformMat;
		p.ObstacleMat = level.ObstacleMat;
		p.SetSignature(sig);
	}

	const int SliceCount = 16;

	string GenerateSignature(LevelData data) {
		char[] sig = new char[SliceCount];
		for (int i = 0; i < SliceCount; i++) {
			sig[i] = data.RandomType();
		}
		if (!System.Linq.Enumerable.Any(sig, t => t == 'E')) sig[0] = 'E';
		return new string(sig);
	}
}