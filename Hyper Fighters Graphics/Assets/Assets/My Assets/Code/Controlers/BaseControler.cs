using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BaseControler : ScriptableObject /*MonoBehaviour*/
{
    const int m_c_numInputs = 6;
    FighterData m_me;
    InputTracker m_inputTracker;

    int m_currentMove;

    KeyCode[] m_inputs = new KeyCode[m_c_numInputs];
    string[] m_moveNames = new string[m_c_numInputs];

    public void Initialize(FighterData me, InputTracker inputTracker)
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
    }

    public bool Setup()
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

    public void WriteMoveUI(Text moveUI)
    {
        string temp = "";

        for (int z = 0; z < m_c_numInputs; z++)
        {
            temp += m_moveNames[z] + " : " + m_inputs[z] + "\n";
        }

        moveUI.text = temp;
    }

    public void RevealMovesUI(Text moveUI)
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

    public int SelectMove()
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

    public KeyCode[] GetInputKeys()
    {
        return m_inputs;
    }
}
