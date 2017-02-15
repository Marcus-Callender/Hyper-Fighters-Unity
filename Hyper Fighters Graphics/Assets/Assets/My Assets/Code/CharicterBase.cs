using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterBase : MonoBehaviour
{
	const int m_c_numInputs = 6;
	KeyCode[] m_inputs = new KeyCode[m_c_numInputs];
	string[] m_names = new string[m_c_numInputs];

	

	// Use this for initialization
	void Start()
	{
		m_names[0] = "Light Attack";
		m_names[1] = "Heavy Attack";
		m_names[2] = "Throw";
		m_names[3] = "Block";
		m_names[4] = "Dodge";
		m_names[5] = "Hyper Move";

		m_inputs[0] = KeyCode.None;
		m_inputs[1] = KeyCode.None;
		m_inputs[2] = KeyCode.None;
		m_inputs[3] = KeyCode.None;
		m_inputs[4] = KeyCode.None;
		m_inputs[5] = KeyCode.None;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public bool StartKeyAssign()
	{
		for (int z = 0; z < m_c_numInputs; z++)
		{
			if (m_inputs[z] == KeyCode.None)
			{
				Debug.Log("Please enter a key for " + m_names[z] + ".");
				// get input
				foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
				{
					if (Input.GetKeyDown(key))
					{
						m_inputs[z] = key;
					}
				}

				return false;
			}
		}

		return true;
	}

	public void DetectKeyPress()
	{
		for (int z = 0; z < m_c_numInputs; z++)
		{
			if (Input.GetKeyDown(m_inputs[z]))
			{
				Debug.Log("Key pressed: " + m_names[z]);
			}
		}
	}
}
