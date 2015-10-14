﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	/*
	 * Original = http://trisearch.16mb.com/server/
	 * Test = http://trisearch.avellar.c9.io/server/
	 */

	public Text namesList;
	public Text scoreList;
	public string name;
	public int score;
	public string db_url = "http://trisearch.16mb.com/server/";
	public bool isUpdating;
	public bool isUpdatingScore;
	private string[] scoresText;


	void Awake() {
		isUpdating = true;
		isUpdatingScore = true;
		db_url = "http://trisearch.16mb.com/server/";
	}

	void Update() {
		if (isUpdating) {
			StartCoroutine (LoadScores());
			StopCoroutine(SaveScores());
		} else {
			StopCoroutine(LoadScores());
		}

		if (isUpdatingScore && !isUpdating) {
            namesList.text = "<color=#B8860B>" + scoresText[0] + "</color> \n";
            scoreList.text = "<color=#B8860B>" + scoresText[1] + "</color> \n";

			for (int i = 2; i < scoresText.Length; i += 2) {
				namesList.text += scoresText [i] + "\n";
			}
			for (int i = 3; i < scoresText.Length; i += 2) {
				scoreList.text += scoresText [i] + "\n";
			}
			isUpdatingScore = false;
		}
	}

	public IEnumerator SaveScores() {
		WWWForm form = new WWWForm();
		form.AddField ("newName", name);
        form.AddField("newScore", PlayerPrefs.GetInt("actualPoints"));
		WWW webRequest = new WWW(db_url + "saveScore.php", form);

		yield return webRequest;
	}

	IEnumerator LoadScores() {
		WWW webRequest = new WWW(db_url + "loadScore.php");
		yield return webRequest;
		scoresText = webRequest.text.Split('|');
		isUpdating = false;
		isUpdatingScore = true;
	}
	
}