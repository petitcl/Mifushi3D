using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ScoreManager : MonoBehaviour {
	public TextMesh[] scores = new TextMesh[10];
	IDictionary<float, List<string>> m_scores = new SortedDictionary<float, List<string>>();
	// Use this for initialization
	void Awake () {
		for (int l_index = 1; l_index <= 10; l_index++) {
			float l_score = PlayerPrefs.GetFloat("Score" + l_index.ToString() + "Value");
			string l_username = PlayerPrefs.GetString("Score" + l_index.ToString() + "Name");
			if (scores[l_index - 1]) {
				scores[l_index - 1].text = "";
				if (l_score > 0) {
					if (!m_scores.ContainsKey(l_score)) {
						m_scores.Add(new KeyValuePair<float, List<string>>(l_score, new List<string>()));
					}
					m_scores[l_score].Add(l_username);
					scores[l_index - 1].text = l_username + " " + l_score.ToString();
				}
			}
		}
	}

	void Start () {
		if (!this.gameObject.GetComponent<Clock> ()) {
			return ;
		}
		Runity.Messenger.AddListener("Game.Start", this.ClockStart);
		Runity.Messenger.AddListener("Game.Pause", this.ClockPause);
		Runity.Messenger.AddListener("Game.Resume", this.ClockResume);
		Runity.Messenger.AddListener("Game.End", this.EndGame);
		Runity.Messenger<string>.AddListener("Game.SetUsername", this.AddScoreTo);
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.gameObject.GetComponent<Clock> ()) {
			return ;
		}
		GameObject obj = GameObject.Find("score_value");
		Debug.Log("test1");
		if (obj == null) {
			return ;
		}
		Debug.Log("test2");
		obj.GetComponent<TextMesh>().text = this.gameObject.GetComponent<Clock> ().timer.ToString();
		
	}
	void Save() {
		// save scores;
		int l_index = 1;
		foreach (KeyValuePair<float, List<string>> l_list in m_scores) {
			float l_score = l_list.Key;
			foreach (string l_name in l_list.Value) {
				Debug.Log("Score" + l_index.ToString() + "Value");
				PlayerPrefs.SetFloat("Score" + l_index.ToString() + "Value", l_score);
				PlayerPrefs.SetString("Score" + l_index.ToString() + "Name", l_name);
				l_index++;
				if (l_index > 10) {
					PlayerPrefs.Save();
					return ;
				}
			}
		}
		PlayerPrefs.Save();
	}

	void ClockStart()
	{
		this.gameObject.GetComponent<Clock> ().Restart ();
	}
	void ClockPause()
	{
		this.gameObject.GetComponent<Clock> ().Pause ();
	}
	void ClockResume()
	{
		this.gameObject.GetComponent<Clock> ().Continue ();
	}
	void EndGame()
	{
		this.gameObject.GetComponent<Clock> ().Pause ();
	}
	void AddScoreTo(string _username)
	{
		float l_score = this.gameObject.GetComponent<Clock> ().timer;
		if (!m_scores.ContainsKey(l_score)) {
			m_scores.Add(new KeyValuePair<float, List<string>>(l_score, new List<string>()));
		}
		m_scores [l_score].Add(_username);
		Save ();
	}
}
