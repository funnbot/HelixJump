using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
	public Image PauseImage;
	public Text AddScoreText;
	public Text ScoreText;
	public Text AnyKeyText;
	public int CurrentScore;
	
	public float AddScoreDecay;

	private float addScoreTimer;
	private bool ended;

	void Start() {
		ScoreText.text = "Score: 0";
	}

	void Update() {
		if (addScoreTimer > 0) {
			addScoreTimer -= Time.deltaTime;
			if (addScoreTimer <= 0) {
				AddScoreText.gameObject.SetActive(false);
			}
		}

		if (ended) {
			if (Input.anyKeyDown) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}

	public void AddScore(int am) {
		addScoreTimer = AddScoreDecay;
		AddScoreText.text = "+" + am;
		AddScoreText.gameObject.SetActive(true);
		CurrentScore += am;
		ScoreText.text = "Score: " + CurrentScore;
	}

	public void ShowEnd() {
		AnyKeyText.gameObject.SetActive(true);
		PauseImage.gameObject.SetActive(true);
		ended = true;
	}

	public static Score Inst;
	void Awake() {
		if (Inst != null) Destroy(gameObject);
		Inst = this;
	}
}
