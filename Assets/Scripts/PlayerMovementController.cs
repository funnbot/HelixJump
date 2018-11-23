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
	private PlayerLevel Level;
	private Animator anim;
	public Rigidbody rb;

	void Start() {
		position = transform.position.y;
		Level = GetComponent<PlayerLevel>();
		anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
		rb.sleepThreshold = 0;
	}

	void FixedUpdate() {
		time += Time.deltaTime;
		var location = transform.position;
		if (bouncing) {
			var bounce = BounceCurve.Evaluate(time * BallSpeed) * BounceHeight;
			if (bounce < 0f) bouncing = false;
			else location.y = position + bounce;
		} else {
			if (collisions == 0) {
				location.y = position;
				position -= BallSpeed - 0.1f;
			}
		}
		//rb.MovePosition(location);
	}

	int collisions;

	void OnCollisionEnter(Collision other) {
		collisions++;
		Transform parent = other.transform.parent;
		if (parent == null) return;
		Platform p = parent.GetComponent<Platform>();
		if (p == null) return;

		if (other.transform.CompareTag("Platform")) {
			Level.CollidePlatform(p);
			position = other.contacts[0].point.y +
				transform.localScale.y / 2;
			time = 0f;
			bouncing = true;
			anim.SetTrigger("Bounce");
		} else if (other.transform.CompareTag("Obstacle")) {
			Level.CollideObstacle(p);
		}
	}

	void OnCollisionExit(Collision other) {
		collisions--;
	}

	void OnTriggerEnter(Collider other) {
		Platform p = other.transform.GetComponent<Platform>();
		if (p == null) return;
		Level.PassPlatform(p);
	}
}