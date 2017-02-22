using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum E_GameStates
{
	SETUP,
	MOVE_SELECT,
	USE_ACTIONS,
	ACTION_RESOLUSTION,
	CHARICTER_POSITIONING
}

public class GameManager : MonoBehaviour
{
	public GameObject m_charicterPrefab;

	private GameObject[] m_charicters = new GameObject[2];
	private FighterBase[] m_charicterScripts = new FighterBase[2];
	private CameraManager m_camera;
	private Canvas m_canvas;
	private PositionManager m_posManager;
	private InputTracker m_inputTracker;

	private E_GameStates m_state = E_GameStates.SETUP;
	
	void Start()
	{
		m_camera = FindObjectOfType<CameraManager>();
		m_canvas = FindObjectOfType<Canvas>();
		m_inputTracker = gameObject.AddComponent<InputTracker>();

		m_charicters[0] = Instantiate(m_charicterPrefab);
		m_charicters[1] = Instantiate(m_charicterPrefab);

		m_charicterScripts[0] = m_charicters[0].GetComponent<FighterBase>();
		m_charicterScripts[1] = m_charicters[1].GetComponent<FighterBase>();

		m_charicterScripts[0].Initialize(1, m_canvas, m_inputTracker);
		m_charicterScripts[1].Initialize(2, m_canvas, m_inputTracker);

		m_charicters[0].transform.position = new Vector3(5.0f, 0.0f, 0.0f);
		m_charicters[1].transform.position = new Vector3(-5.0f, 0.0f, 0.0f);

		m_camera.Initialize(m_charicters[0], m_charicters[1]);

		m_posManager = GetComponent<PositionManager>();
		m_posManager.Initialize(m_charicters[0], m_charicters[1], m_camera.gameObject);
	}
	
	void Update()
	{
		if (m_state == E_GameStates.SETUP)
		{
			if (m_charicterScripts[0].StartKeyAssign())
			{
				if (m_charicterScripts[1].StartKeyAssign())
				{
					m_state = E_GameStates.MOVE_SELECT;
				}
			}

			m_charicterScripts[0].WriteMoveUI();
			m_charicterScripts[1].WriteMoveUI();

			m_charicterScripts[0].SetAnimation(E_ANIMATIONS.IDLE);
			m_charicterScripts[1].SetAnimation(E_ANIMATIONS.IDLE);
		}
		else if (m_state == E_GameStates.MOVE_SELECT)
		{
			if (m_charicterScripts[0].SelectMove() && m_charicterScripts[1].SelectMove())
			{
				m_state = E_GameStates.USE_ACTIONS;
			}

			m_charicterScripts[0].WriteMoveUI();
			m_charicterScripts[1].WriteMoveUI();

			m_charicterScripts[0].SetAnimation(E_ANIMATIONS.IDLE);
			m_charicterScripts[1].SetAnimation(E_ANIMATIONS.IDLE);
		}
		else if (m_state == E_GameStates.USE_ACTIONS)
		{
			E_RESULT res1 = m_charicterScripts[0].UseMove(m_charicterScripts[1].GetCurrentMove(), m_charicterScripts[1].GetData());
			E_RESULT res2 = m_charicterScripts[1].UseMove(m_charicterScripts[0].GetCurrentMove(), m_charicterScripts[0].GetData());

			if (res1 == res2)
			{
				if (res1 == E_RESULT.WIN || res1 == E_RESULT.SP_WIN)
				{
					m_charicterScripts[0].Win(m_charicterScripts[1].GetCurrentMove(), m_charicterScripts[1].GetData());
					m_charicterScripts[1].Win(m_charicterScripts[0].GetCurrentMove(), m_charicterScripts[0].GetData());
				}
				else
				{
					m_charicterScripts[0].Lose(m_charicterScripts[1].GetCurrentMove(), m_charicterScripts[1].GetData());
					m_charicterScripts[1].Lose(m_charicterScripts[0].GetCurrentMove(), m_charicterScripts[0].GetData());
				}
			}
			else if (res1 > res2)
			{
				m_charicterScripts[1].Lose(m_charicterScripts[0].GetCurrentMove(), m_charicterScripts[0].GetData());
				m_charicterScripts[0].Win(m_charicterScripts[1].GetCurrentMove(), m_charicterScripts[1].GetData());
			}
			else if (res1 < res2)
			{
				m_charicterScripts[0].Lose(m_charicterScripts[1].GetCurrentMove(), m_charicterScripts[1].GetData());
				m_charicterScripts[1].Win(m_charicterScripts[0].GetCurrentMove(), m_charicterScripts[0].GetData());
			}

			m_charicterScripts[0].RevealMovesUI();
			m_charicterScripts[1].RevealMovesUI();

			m_state = E_GameStates.ACTION_RESOLUSTION;
		}
		else if (m_state == E_GameStates.ACTION_RESOLUSTION)
		{
			if (!m_charicterScripts[0].GetData().isMoving() && !m_charicterScripts[1].GetData().isMoving())
			{
				m_charicterScripts[0].Rest();
				m_charicterScripts[1].Rest();

				m_state = E_GameStates.CHARICTER_POSITIONING;
			}

			m_charicterScripts[0].RevealMovesUI();
			m_charicterScripts[1].RevealMovesUI();
		}
		else if (m_state == E_GameStates.CHARICTER_POSITIONING)
		{
			if (m_posManager.ResetCharicterDistance())
			{
				m_state = E_GameStates.MOVE_SELECT;
			}

			m_charicterScripts[0].RevealMovesUI();
			m_charicterScripts[1].RevealMovesUI();
		}
	}
}
