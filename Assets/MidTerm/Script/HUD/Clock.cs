using UnityEngine;
using System.Collections;

public class Clock
   : MonoBehaviour
{
	public float timer;
	bool m_pause = true;
	public GameObject textMesh;
	public void Pause()
	{
		m_pause = true;
	}
	public void Continue()
	{
		m_pause = false;
	}
	public void ResetClock()
	{
		timer = 0;
	}
	public void Restart()
	{
		timer = 0;
		m_pause = false;
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (m_pause) {
			return ;
		}
		timer += Time.deltaTime;
		string l_text = "";
		if (timer < 10.0f) {
			l_text = "0";
		}
		float l_truncated = (int)(timer * 100);
		l_truncated /= 100;
		l_text += l_truncated.ToString();
		l_text = l_text.Replace ('.', ':');
		if (textMesh == null) {
			textMesh = this.gameObject;
		}
		TextMesh l_component = textMesh.GetComponent<TextMesh> ();
		if (l_component) {
			l_component.text = l_text;
		}
	}
}
