using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {
	public LevelData[] Levels;
	public MeshRenderer Cylinder;

	private int currentLevel;
	private int platformsPassed;
	private int streak;
	private PlayerMovementController movement;

	void Start() {
		movement = GetComponent<PlayerMovementController>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Score.Inst.ShowEnd();
			movement.enabled = false;
			movement.rb.isKinematic = true;
			GameObject.FindObjectOfType<CylinderRotation>().InputLock = true;
			currentLevel = 0;
		}
	}

	public void PassPlatform(Platform p) {
		p.Destroy();
		platformsPassed++;
		streak++;
		Score.Inst.AddScore(currentLevel + 1);
		if (platformsPassed > GetLevel().PlatformCount) {
			currentLevel++;
			platformsPassed = 0;
		}
		if (platformsPassed == 5) Cylinder.sharedMaterial = GetLevel().PlatformMat;
	}

	public void CollidePlatform(Platform p) {
		if (streak >= GetLevel().StreakToDestroy) {
			Score.Inst.AddScore((streak + 1) * (currentLevel + 1));
			p.Destroy();
		}
		streak = 0;
	}

	public void CollideObstacle(Platform p) {
		if (streak >= GetLevel().StreakToDestroy) {
			Score.Inst.AddScore((streak + 1) * (currentLevel + 1));
			p.Destroy();
			streak = 0;
			return;
		}
		Score.Inst.ShowEnd();
		movement.enabled = false;
		movement.rb.isKinematic = true;
		GameObject.FindObjectOfType<CylinderRotation>().InputLock = true;
		currentLevel = 0;
	}

	public LevelData GetLevel() {
		var data = Levels[Mathf.Min(currentLevel, Levels.Length - 1)];
		data.Validate();
		return data;
	}
}

[System.Serializable]
public struct LevelData {
	public int PlatformCount;
	public int StreakToDestroy;
	[Range(0, 1)]
	public float ChanceOfEmpty;
	[Range(0, 1)]
	public float ChanceOfDeath;

	public Material PlatformMat,
	ObstacleMat;

	public void Validate() {
		if (ChanceOfDeath + ChanceOfEmpty > 1f) {
			ChanceOfEmpty = 1 - ChanceOfDeath;
		}
	}

	public char RandomType() {
		float val = Random.value;
		// First range 0 to 0.2f
		if (val < ChanceOfEmpty) return 'E';
		// Second change 0 to 0.6f but ignores < 0.2f
		else if (val < ChanceOfEmpty + ChanceOfDeath) return 'D';
		// Third if greater than 0.6f
		else return 'P';
	}
}