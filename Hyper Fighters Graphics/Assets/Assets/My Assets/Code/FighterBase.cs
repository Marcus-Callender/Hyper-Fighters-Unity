using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterBase : MonoBehaviour
{
	const int m_c_numInputs = 6;
	KeyCode[] m_inputs = new KeyCode[m_c_numInputs];
	string[] m_moveNames = new string[m_c_numInputs];
	int m_currentMove = -1;
	BaseMove[] m_moves = new BaseMove[m_c_numInputs];
	FighterData m_Data;
	int m_playerNum;
	Text m_healthUI;
	Text m_moveUI;
	Text m_hyperUI;
	
	void Start()
	{
		m_Data = gameObject.GetComponent<FighterData>();

		m_moveNames[0] = "Light";
		m_moveNames[1] = "Heavy";
		m_moveNames[2] = "Throw";
		m_moveNames[3] = "Block";
		m_moveNames[4] = "Dodge";
		m_moveNames[5] = "Hyper";

		m_inputs[0] = KeyCode.None;
		m_inputs[1] = KeyCode.None;
		m_inputs[2] = KeyCode.None;
		m_inputs[3] = KeyCode.None;
		m_inputs[4] = KeyCode.None;
		m_inputs[5] = KeyCode.None;

		m_moves[0] = gameObject.AddComponent<HeavyAttack>();
		m_moves[1] = gameObject.AddComponent<LightAttack>();
		m_moves[2] = gameObject.AddComponent<HeavyAttack>();
		m_moves[3] = gameObject.AddComponent<Throw>();
		m_moves[4] = gameObject.AddComponent<Block>();
		m_moves[5] = gameObject.AddComponent<DODGE>();
	}
	
	void Update()
	{

	}

	public void Initialize(int playerNum, Canvas UICanvas)
	{
		m_playerNum = playerNum;

		Text[] texts = UICanvas.GetComponentsInChildren<Text>();

		for (int z = 0; z < texts.Length; z++)
		{
			if (texts[z].name == ("Player " + m_playerNum + " Health"))
			{
				m_healthUI = texts[z];
				m_healthUI.text = ("Player " + m_playerNum + " health!");
			}
			else if (texts[z].name == ("Player " + m_playerNum + " Hyper"))
			{
				m_hyperUI = texts[z];
				m_hyperUI.text = ("Player " + m_playerNum + " hyper!");
			}
			else if (texts[z].name == ("Player " + m_playerNum + " Moves"))
			{
				m_moveUI = texts[z];
				m_moveUI.text = ("Player " + m_playerNum + " moves!");
			}
		}
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

	public void WriteMoveUI()
	{

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
		if (m_currentMove == -1)
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

	public E_RESULT UseMove(BaseMove enemyMove, FighterData enemy)
	{
		return m_moves[m_currentMove].Use(enemyMove, enemy);
	}

	public void Win(BaseMove enemyMove, FighterData enemy)
	{
		m_moves[m_currentMove].Win(enemyMove, enemy);
	}

	public void Lose(BaseMove enemyMove, FighterData enemy)
	{
		m_moves[m_currentMove].Lose(enemyMove, enemy);
	}

	public void Rest()
	{
		m_currentMove = -1;
	}

	public BaseMove GetCurrentMove()
	{
		return m_moves[m_currentMove];
	}

	public FighterData GetData()
	{
		return m_Data;
	}
}
