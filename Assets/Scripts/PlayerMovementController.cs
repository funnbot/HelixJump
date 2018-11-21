using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public AnimationCurve BounceCurve = new AnimationCurve(
		new Keyframe(0f, 0f, 0f, 20f),
		new Keyframe(0.125f, 1f),
		new Keyframe(0.25f, -0.1f, -20f, 0f));

	public float BounceHeight = 2f;
	public float BallSpeed = 0.25f;

	private bool bouncing;
	private float position;
	private float time;

	void Update() {
		time += Time.deltaTime;
		if (bouncing) {
			var bounce = BounceCurve.Evaluate(time * BallSpeed) * BounceHeight;
			transform.position = (position + bounce) * Vector3.up;
			if (bounce < 0f) bouncing = false;
		} else {
			transform.position = position * Vector3.up;
			position -= BallSpeed;
		}
	}

	void OnCollisionEnter(Collision other) {
		position = other.contacts[0].point.y +
			transform.localScale.y / 2;
		time = 0f;
		bouncing = true;
	}
}