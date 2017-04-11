﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AI_Controler : BaseControler
{
    int m_opponantPrevMove = -1;

    // this tracks what moves are sucsessful after a given move is used by the opponant
    int [ , , ] m_pastOutcomes = new int[m_c_numInputs, m_c_numInputs, (int) E_SimpleResult.TOTAL];
    
    // This tracks what move the opponant uses after a specified move gets a specified result
    int [ , , ] m_opponantMovePattern = new int [m_c_numInputs, (int) E_SimpleResult.TOTAL, m_c_numInputs];
    
    // this tracks the ammount of times the opponant has repeted the same move, starts from 0
    int m_repetedMovesCount = 0;

    E_SimpleResult m_opponantPrevResult = E_SimpleResult.NONE;

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

        if (opponantsMove == m_opponantPrevMove)
        {
            m_repetedMovesCount++;
        }
        else
        {
            m_repetedMovesCount = 0;
        }

        if (m_opponantPrevResult != E_SimpleResult.NONE)
        {
            m_opponantMovePattern[m_opponantPrevMove, (int) m_opponantPrevResult, opponantsMove]++;
        }

        m_opponantPrevResult = findOpponantSimpleResult(myRes, othRes);

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

    private E_SimpleResult findOpponantSimpleResult(E_RESULT myRes, E_RESULT othRes)
    {
        if ((int)myRes > (int)othRes)
        {
            return E_SimpleResult.LOSE;
        }
        else if (myRes == othRes)
        {
            return E_SimpleResult.DRAW;
        }

        return E_SimpleResult.WIN;
    }
}
