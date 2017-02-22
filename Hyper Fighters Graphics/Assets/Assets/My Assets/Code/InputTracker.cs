using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTracker : MonoBehaviour
{
	const int m_c_keyArrayMaxLength = 15;
	int m_assignedKeys = 0;
	KeyCode[] m_inUseKeys = new KeyCode[m_c_keyArrayMaxLength];

	void Start()
	{
		for (int z = 0; z < m_c_keyArrayMaxLength; z++)
		{
			m_inUseKeys[z] = KeyCode.None;
		}
	}

	public void AddKey(KeyCode key)
	{
		if (m_assignedKeys < m_c_keyArrayMaxLength)
		{
			m_inUseKeys[m_assignedKeys] = key;
			m_assignedKeys++;
		}
	}

	public bool IsKeyInUse(KeyCode key)
	{
		for (int z = 0; z < m_assignedKeys; z++)
		{
			if (m_inUseKeys[z] == key)
			{
				return false;
			}
		}

		AddKey(key);
		return true;
	}
}
