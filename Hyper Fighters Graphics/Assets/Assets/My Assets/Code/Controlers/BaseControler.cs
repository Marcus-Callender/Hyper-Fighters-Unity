using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public enum E_SimpleResult
{
    WIN,
    DRAW,
    LOSE,

    TOTAL,
    NONE
}

public class BaseControler : ScriptableObject
{
    protected const int m_c_numInputs = 6;
    protected FighterData m_me;
    protected InputTracker m_inputTracker;

    protected int m_currentMove;

    protected KeyCode[] m_inputs = new KeyCode[m_c_numInputs];
    protected string[] m_moveNames = new string[m_c_numInputs];
    protected BaseMove[] m_moves = new BaseMove[m_c_numInputs];

    public void Initialize(FighterData me, InputTracker inputTracker, BaseMove[] moves)
    {
        m_me = me;

        m_inputTracker = inputTracker;

        for (int z = 0; z < m_c_numInputs; z++)
        {
            m_inputs[0] = KeyCode.None;
            m_inputs[1] = KeyCode.None;
            m_inputs[2] = KeyCode.None;
            m_inputs[3] = KeyCode.None;
            m_inputs[4] = KeyCode.None;
            m_inputs[5] = KeyCode.None;
        }

        m_moveNames[0] = "Light";
        m_moveNames[1] = "Heavy";
        m_moveNames[2] = "Throw";
        m_moveNames[3] = "Block";
        m_moveNames[4] = "Dodge";
        m_moveNames[5] = "Hyper";

        m_moves = moves;
    }

    public virtual bool Setup()
    {
        for (int z = 0; z < m_c_numInputs; z++)
        {
            if (m_inputs[z] == KeyCode.None)
            {
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

    public virtual void WriteMoveUI(Text moveUI)
    {
        string temp = "";

        for (int z = 0; z < m_c_numInputs; z++)
        {
            temp += m_moveNames[z] + " : " + m_inputs[z] + "\n";
        }

        moveUI.text = temp;
    }

    public virtual void RevealMovesUI(Text moveUI)
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

        moveUI.text = temp;
    }

    public virtual int SelectMove()
    {
        for (int z = 0; z < m_c_numInputs; z++)
        {
            if (Input.GetKeyDown(m_inputs[z]))
            {

                // if the move is the charicters hyper move
                if (z == 5)
                {
                    if (!m_me.CanUseHyper())
                    {
                        m_currentMove = -1;
                        return -1;
                    }
                }

                m_currentMove = z;
                return z;
            }
        }

        return -1;
    }

    public virtual void GetResult(E_RESULT myRes, E_RESULT othRes, int opponantsMove)
    {

    }

    public virtual void SetMoveCounters(BaseMove[] opponantsMoves)
    {

    }

    public virtual void OnGameEnd()
    {

    }
}
