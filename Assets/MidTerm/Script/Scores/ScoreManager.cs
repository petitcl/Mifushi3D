using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ScoreManager : MonoBehaviour {
	IDictionary<float, List<string>> m_scores = new SortedDictionary<float, List<string>>();
	// Use this for initialization
	string m_filename = "score.txt";
	void Awake () {
		if (!File.Exists (Application.dataPath + "/" + m_filename))
			return;
		StreamReader l_reader = new StreamReader(Application.dataPath + "/" + m_filename);
		while (!l_reader.EndOfStream) {
			string l_line = l_reader.ReadLine();
			string[] l_array = l_line.Split(" "[0]);
			float l_score = float.Parse(l_array[1]);
			string l_username = l_array[0];
			m_scores[l_score].Add(l_username);
		}
		// load scores;
	}

	void Start () {
		Runity.Messenger.AddListener("Game.Start", this.ClockStart);
		Runity.Messenger.AddListener("Game.Pause", this.ClockPause);
		Runity.Messenger.AddListener("Game.Resume", this.ClockResume);
		Runity.Messenger.AddListener("Game.End", this.EndGame);
		Runity.Messenger<string>.AddListener("Game.SetUsername", this.AddScoreTo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Save() {
		// save scores;
		int num_max = 10;
		Debug.Log(Application.dataPath + "/" + m_filename);
		StreamWriter l_writer;
		if (!File.Exists(Application.dataPath + "/" + m_filename)) {
			l_writer = File.CreateText(Application.dataPath + "/" + m_filename);
		}
		else {
			l_writer = new StreamWriter(Application.dataPath + "/" + m_filename);
		}
		foreach (KeyValuePair<float, List<string>> l_list in m_scores) {
			float l_score = l_list.Key;
			foreach (string name in l_list.Value) {
				l_writer.WriteLine("{0} {1}", name, l_score);
				num_max--;
				if (num_max <= 0) {
					l_writer.Close();
					return ;
				}
			}
		}
		l_writer.Close();
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
		m_scores [l_score].Add(_username);
		Save ();
	}
}
