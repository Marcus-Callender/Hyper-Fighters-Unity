using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AI_Controler : BaseControler
{
    int m_opponantPrevMove = -1;

    int [ , , ] m_pastOutcomes = new int[m_c_numInputs, m_c_numInputs, (int) E_SimpleResult.TOTAL];

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

    public override void GetResult(E_RESULT myRes, E_RESULT othRes, int opponantsMove)
    {
        // checks opponants last move is valid
        if (m_opponantPrevMove != -1)
        {
            // increments the outcome count for using a specified move after a specified opponant move
            m_pastOutcomes[m_opponantPrevMove, m_currentMove, (int)findSimpleResult(myRes, othRes)]++;
        }

        m_opponantPrevMove = opponantsMove;
    }

    private E_SimpleResult findSimpleResult(E_RESULT myRes, E_RESULT othRes)
    {
        if ((int) myRes > (int) othRes)
        {
            return E_SimpleResult.WIN;
        }
        else if (myRes == othRes)
        {
            return E_SimpleResult.DRAW;
        }

        return E_SimpleResult.LOSE;
    }
}
