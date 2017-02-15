using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterBase : MonoBehaviour
{
	const int m_c_numInputs = 6;
	KeyCode[] m_inputs = new KeyCode[m_c_numInputs];
	string[] m_moveNames = new string[m_c_numInputs];
	int m_currentMove = -1;
	
	void Start()
	{
		m_moveNames[0] = "Light Attack";
		m_moveNames[1] = "Heavy Attack";
		m_moveNames[2] = "Throw";
		m_moveNames[3] = "Block";
		m_moveNames[4] = "Dodge";
		m_moveNames[5] = "Hyper Move";

		m_inputs[0] = KeyCode.None;
		m_inputs[1] = KeyCode.None;
		m_inputs[2] = KeyCode.None;
		m_inputs[3] = KeyCode.None;
		m_inputs[4] = KeyCode.None;
		m_inputs[5] = KeyCode.None;
	}
	
	void Update()
	{

	}

	public bool StartKeyAssign()
	{
		for (int z = 0; z < m_c_numInputs; z++)
		{
			if (m_inputs[z] == KeyCode.None)
			{
				Debug.Log("Please enter a key for " + m_moveNames[z] + ".");

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

	public void CameraUpdateExample()
	{
		Vector3 pos = this.transform.position;

		for (int z = 0; z < m_c_numInputs; z++)
		{
			if (Input.GetKeyDown(m_inputs[z]))
			{
				Debug.Log("Key pressed: " + m_moveNames[z]);
			}
		}

		if (Input.GetKey(m_inputs[0]))
		{
			pos.x += 2.0f * Time.deltaTime;
		}

		if (Input.GetKey(m_inputs[1]))
		{
			pos.z += 2.0f * Time.deltaTime;
		}

		this.transform.position = pos;
	}

	public bool SelectMove()
	{
		if (m_currentMove != -1)
		{
			for (int z = 0; z < m_c_numInputs; z++)
			{
				if (Input.GetKeyDown(m_inputs[z]))
				{
					m_currentMove = z;
				}
			}

			return false;
		}

		return true;
	}

	public void Rest()
	{
		m_currentMove = -1;
	}
}
