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
	InputTracker m_inputTracker;
	
	void Start()
	{
		m_Data = gameObject.GetComponent<FighterData>();
		m_Data.Initialize();

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

		m_moves[0] = gameObject.AddComponent<LightAttack>();
		m_moves[1] = gameObject.AddComponent<HeavyAttack>();
		m_moves[2] = gameObject.AddComponent<Throw>();
		m_moves[3] = gameObject.AddComponent<Block>();
		m_moves[4] = gameObject.AddComponent<DODGE>();
		m_moves[5] = gameObject.AddComponent<HeavyAttack>();

		m_moves[0].SetAnimation(m_Data.GetAnimationControler(), m_Data.GetSprites());
		m_moves[1].SetAnimation(m_Data.GetAnimationControler(), m_Data.GetSprites());
		m_moves[2].SetAnimation(m_Data.GetAnimationControler(), m_Data.GetSprites());
		m_moves[3].SetAnimation(m_Data.GetAnimationControler(), m_Data.GetSprites());
		m_moves[4].SetAnimation(m_Data.GetAnimationControler(), m_Data.GetSprites());
		m_moves[5].SetAnimation(m_Data.GetAnimationControler(), m_Data.GetSprites());

		m_moves[0].SetValues(15, 8);
		m_moves[1].SetValues(23, 13);
		m_moves[2].SetValues(25, 18);
		m_moves[3].SetValues(0, 0);
		m_moves[4].SetValues(17, 5);
		m_moves[5].SetValues(45, 12);
	}
	
	void Update()
	{
		RenderNameUI();
	}

	public void Initialize(int playerNum, Canvas UICanvas, InputTracker refInputTracker)
	{
		m_playerNum = playerNum;

		Text[] texts = UICanvas.GetComponentsInChildren<Text>();

		m_inputTracker = refInputTracker;

		for (int z = 0; z < texts.Length; z++)
		{
			if (texts[z].name == ("Player " + m_playerNum + " Health"))
			{
				m_healthUI = texts[z];
			}
			else if (texts[z].name == ("Player " + m_playerNum + " Hyper"))
			{
				m_hyperUI = texts[z];
				m_hyperUI.text = ("Player " + m_playerNum + " hyper!");
			}
			else if (texts[z].name == ("Player " + m_playerNum + " Moves"))
			{
				m_moveUI = texts[z];
			}
		}
	}

	public bool StartKeyAssign()
	{
		for (int z = 0; z < m_c_numInputs; z++)
		{
			if (m_inputs[z] == KeyCode.None)
			{
				//Debug.Log("Please enter a key for " + m_moveNames[z] + ".");

				foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
				{
					if (Input.GetKeyDown(key))
					{
						if (m_inputTracker.IsKeyInUse(key))
						{
							m_inputs[z] = key;
						}
					}
				}

				return false;
			}
		}

		return true;
	}

	public void WriteMoveUI()
	{
		string temp = "";

		for (int z = 0; z < m_c_numInputs; z++)
		{
			temp += m_moveNames[z] + " : " + m_inputs[z] + "\n";
		}

		m_moveUI.text = temp;
	}

	public void RevealMovesUI()
	{
		string temp = "";

		for (int z = 0; z < m_c_numInputs; z++)
		{
			if (z == m_currentMove)
			{
				temp += ">>" + m_moveNames[z] + " : " + m_inputs[z] + "<<\n";
			}
			else
			{
				temp += m_moveNames[z] + " : " + m_inputs[z] + "\n";
			}
		}

		m_moveUI.text = temp;
	}

	public void RenderNameUI()
	{
		m_healthUI.text = m_Data.GetHpUIString();
		m_hyperUI.text = m_Data.GetFocusUIString();
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

	public bool MoveUpdate1(FighterData enemy)
	{
		return m_moves[m_currentMove].Update1(enemy);
	}

	public bool MoveUpdate2(E_RESULT myResult, E_RESULT otherResult, FighterData enemy)
	{
		return m_moves[m_currentMove].Update2(myResult, otherResult, enemy);
	}

	public void SetAnimation(E_ANIMATIONS z)
	{
		m_Data.SetAnimaton(z);
	}

	public void StartSeccondAnimation()
	{

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

	public KeyCode[] GetKeys()
	{
		return m_inputs;
	}
}
