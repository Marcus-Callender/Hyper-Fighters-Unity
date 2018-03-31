using System.Collections;
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

    // tracks what moves of the AIs will beet the players moves
    E_SimpleResult[ , ] m_moveMatchups = new E_SimpleResult[m_c_numInputs, m_c_numInputs];

    E_SimpleResult m_opponantPrevResult = E_SimpleResult.NONE;

    public override bool Setup()
    {
        m_currentMove = 0;

        m_pastOutcomes = ExpandArray(SavedDataManager.m_instance.LoadIntArray("Outcomes.sav"), 6, 6, 3);
        m_opponantMovePattern = ExpandArray(SavedDataManager.m_instance.LoadIntArray("MovePatterns.sav"), 6, 3, 6);

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


        //if (m_currentMove == -1)
        {
            m_currentMove = Random.Range(0, m_c_numInputs);
        }

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
            Debug.Log("Array: " + m_opponantPrevMove + ", " + m_currentMove + ", " + (int)findSimpleResult(myRes, othRes));
            
            // increments the outcome count for using a specified move after a specified opponant move
            m_pastOutcomes[m_opponantPrevMove, m_currentMove, (int)findSimpleResult(myRes, othRes)]++;

            if (m_opponantPrevResult != E_SimpleResult.NONE)
            {
                m_opponantMovePattern[m_opponantPrevMove, (int) m_opponantPrevResult, opponantsMove]++;
            }
        }

        if (opponantsMove == m_opponantPrevMove)
        {
            m_repetedMovesCount++;
        }
        else
        {
            m_repetedMovesCount = 0;
        }

        // reversing the paramiters gives the opponants result
        m_opponantPrevResult = findSimpleResult(othRes, myRes);

        m_opponantPrevMove = opponantsMove;
    }

    public override void SetMoveCounters(BaseMove[] opponantsMoves)
    {
        for (int z = 0; z < m_c_numInputs; z++)
        {
            for (int x = 0; x < m_c_numInputs; x++)
            {
                E_RESULT myRes = m_moves[z].Use(opponantsMoves[x]);
                E_RESULT oppRes = opponantsMoves[z].Use(m_moves[x]);

                m_moveMatchups[z, x] = findSimpleResult(myRes, oppRes);
            }
        }
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

    private int findWiningMove(int moveToBeat, E_SimpleResult resultToFind)
    {
        int[] suitableMoves = new int [m_c_numInputs];
        int numSuitableMoves = 0;

        for (int z = 0; z < m_c_numInputs; z++)
        {
            if (m_moveMatchups[moveToBeat, z] == resultToFind)
            {
                suitableMoves[z] = -1;
                suitableMoves[numSuitableMoves] = z;
            }
        }

        int randomMove = Random.Range(0, numSuitableMoves);
        
        return -1;
    }

    public override void OnGameEnd()
    {
        SavedDataManager.m_instance.SaveData(FlattenArray(m_pastOutcomes, 6, 6, 3), "Outcomes.sav");
        SavedDataManager.m_instance.SaveData(FlattenArray(m_opponantMovePattern, 6, 3, 6), "MovePatterns.sav");
    }

    private int[] FlattenArray(int[ , , ] array, int one, int two, int three)
    {
        int[] newArray = new int[one * two * three];

        int total = 0;

        for (int z = 0; z < one; z++)
        {
            for (int x = 0; x < two; x++)
            {
                for (int c = 0; c < three; c++)
                {
                    newArray[total] = array[z, x, c];
                    total++;
                }
            }
        }

        return newArray;
    }

    private int[,,] ExpandArray(int[] array, int one, int two, int three)
    {
        int[,,] newArray = new int[one, two, three];

        int total = 0;

        for (int z = 0; z < one; z++)
        {
            for (int x = 0; x < two; x++)
            {
                for (int c = 0; c < three; c++)
                {
                    newArray[z, x, c] = array[total];
                    total++;
                }
            }
        }

        return newArray;
    }

    /*private int[ , , ] ExpandArrayOutcome(int[] array)
    {
        int[ , , ] newArray = new int[6,6,3];

        int total = 0;

        for (int z = 0; z < 3; z++)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int c = 0; c < 3; c++)
                {
                    newArray[z, x, c] = array[total];
                    total++;
                }
            }
        }

        return newArray;
    }

    private int[,,] ExpandArrayPattern(int[] array)
    {
        int[,,] newArray = new int[6, 3, 6];

        int total = 0;

        for (int z = 0; z < 3; z++)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int c = 0; c < 3; c++)
                {
                    newArray[z, x, c] = array[total];
                    total++;
                }
            }
        }

        return newArray;
    }*/
}
