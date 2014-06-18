using UnityEngine;
using System.Collections;

public class TextInput : MonoBehaviour {

	bool m_focus = false;
	string m_libelle = "";
	public GameObject textMesh;
	public delegate void OnValidateEvent(string _libelle);
	public OnValidateEvent OnValidate = null;
	public int maxSize = 3;

	public void Focus () {
		m_focus = true;
	}
	public void LoseFocus () {
		m_focus = false;
	}

	public string getLibelle()
	{
		return m_libelle;
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		if (textMesh == null) {
			textMesh = this.gameObject;
		}
		TextMesh l_component = textMesh.GetComponent<TextMesh> ();
		foreach (char c in Input.inputString) {
			// Backspace - Remove the last character
			if (c == "\b"[0]) {
				if (m_libelle.Length != 0)
					m_libelle = l_component.text.Substring(0, l_component.text.Length - 1);
			}
			// End of entry
			else if (c == "\n"[0] || c == "\r"[0]) {// "\n" for Mac, "\r" for windows.
				if (OnValidate != null)
				OnValidate(m_libelle);
			}
			// Normal text input - just append to the end
			else if (m_libelle.Length < this.maxSize) {
				m_libelle += c;
			}
		}
		if (l_component) {
			l_component.text = m_libelle;
		}
	}
}
