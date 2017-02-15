using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum E_GameStates
{
	SETUP,
	MOVE_SELECT,
	USE_ACTIONS
}

public class GameManager : MonoBehaviour
{
	public GameObject m_charicterPrefab;

	private GameObject[] m_charicters = new GameObject[2];
	private CharicterBase[] m_charScripts = new CharicterBase[2];
	private CameraManager m_camera;

	private E_GameStates m_state = E_GameStates.SETUP;

	// Use this for initialization
	void Start()
	{
		m_camera = FindObjectOfType<CameraManager>();

		m_charicters[0] = Instantiate(m_charicterPrefab);
		m_charicters[1] = Instantiate(m_charicterPrefab);

		m_charScripts[0] = m_charicters[0].GetComponent<CharicterBase>();
		m_charScripts[1] = m_charicters[1].GetComponent<CharicterBase>();

		m_charicters[0].transform.position = new Vector3(10.0f, 0.0f, 10.0f);
		m_charicters[1].transform.position = new Vector3(12.0f, 0.0f, 16.0f);

		//m_charicters[0].transform.rotation.
		//m_charicters[0].transform.rotation.

		m_camera.Initialize(m_charicters[0], m_charicters[1]);

		GetComponent<PositionManager>().Initialize(m_charicters[0], m_charicters[1], m_camera.gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		if (m_state == E_GameStates.SETUP)
		{
			if (m_charScripts[0].StartKeyAssign())
			{
				if (m_charScripts[1].StartKeyAssign())
				{
					m_state = E_GameStates.MOVE_SELECT;
				}
			}
		}
		else if (m_state == E_GameStates.MOVE_SELECT)
		{
			if (m_charScripts[0].SelectMove() && m_charScripts[1].SelectMove())
			{
				m_state = E_GameStates.USE_ACTIONS;
			}
		}
	}
}
