using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AI_Controler : BaseControler
{

    public override bool Setup()
    {
        return true;
    }

    public override void WriteMoveUI(Text moveUI)
    {
        string temp = "";

        for (int z = 0; z < m_c_numInputs; z++)
        {
            temp += m_moveNames[z] + "\n";
        }

        moveUI.text = temp;
    }


    public override void RevealMovesUI(Text moveUI)
    {
        string temp = "";

        for (int z = 0; z < m_c_numInputs; z++)
        {
            if (z == m_currentMove)
            {
                temp += ">>" + m_moveNames[z] + "<<\n";
            }
            else
            {
                temp += m_moveNames[z] + "\n";
            }
        }

        moveUI.text = temp;
    }

    public override int SelectMove()
    {
        m_currentMove = Random.Range(0, m_c_numInputs);

        if (m_currentMove == 5)
        {
            if (!m_me.CanUseHyper())
            {
                m_currentMove = -1;
                return -1;
            }
        }

        return m_currentMove;
    }
}
