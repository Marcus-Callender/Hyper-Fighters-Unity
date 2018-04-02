using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TEST_AI_Controler : BaseControler
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

    int[ , ] m_moveMatchupsChart = new int[m_c_numInputs, m_c_numInputs];

    E_SimpleResult m_opponantPrevResult = E_SimpleResult.NONE;

    public float m_repeateMoveProbabilityFactor = 0.8f;

    bool m_hasSetup = false;

    public override bool Setup(BaseMove[] opponentsMoves)
    {
        if (!m_hasSetup)
        {
            m_currentMove = 0;

            m_pastOutcomes = ExpandArray(SavedDataManager.m_instance.LoadIntArray("Outcomes.sav"), 6, 6, 3);
            m_opponantMovePattern = ExpandArray(SavedDataManager.m_instance.LoadIntArray("MovePatterns.sav"), 6, 3, 6);

            for (int z = 0; z < 6; z++)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        Debug.Log(opponentsMoves[z].GetMoveType() + ", " + (E_SimpleResult)x + " => " + opponentsMoves[c].GetMoveType() + " : " + m_opponantMovePattern[z, x, c]);
                    }
                }
            }

            SetMoveCounters(opponentsMoves);

            m_hasSetup = true;
        }

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
        if (m_currentMove == -1 || m_opponantPrevMove == -1 || m_opponantPrevResult == E_SimpleResult.NONE)
        {
            // do not include hyper move
            m_currentMove = Random.Range(0, m_c_numInputs - 1);
        }
        else
        {
            // most likely move for the oponent to use
            int bestIndex = 0;

            for (int z = 1; z < (int)E_MOVE_TYPE.TOTAL; z++)
            {
                if (m_repetedMovesCount > 0 && (z == m_opponantPrevMove || bestIndex == m_opponantPrevMove))
                {
                    float repeateModifier = Mathf.Pow(m_repeateMoveProbabilityFactor, m_repetedMovesCount);
                    float zProbability = m_opponantMovePattern[m_opponantPrevMove, (int)m_opponantPrevResult, z];
                    float bestIndexProbability = m_opponantMovePattern[m_opponantPrevMove, (int)m_opponantPrevResult, bestIndex];

                    if (z == m_opponantPrevMove)
                        zProbability *= repeateModifier;

                    if (bestIndex == m_opponantPrevMove)
                        bestIndexProbability *= repeateModifier;

                    if (zProbability > bestIndexProbability)
                    {
                        bestIndex = z;
                    }
                }
                else
                {
                    if (m_opponantMovePattern[m_opponantPrevMove, (int)m_opponantPrevResult, z] > m_opponantMovePattern[(int)m_opponantPrevMove, (int)m_opponantPrevResult, bestIndex])
                    {
                        bestIndex = z;
                    }
                }
            }

            if (m_moveMatchups[m_c_numInputs - 1, bestIndex] == E_SimpleResult.WIN && m_me.CanUseHyper())
            {
                m_currentMove = m_c_numInputs - 1;
            }
            else
            {
                int[] posibleCounters = new int[m_c_numInputs];
                int posibleCountersCount = 0;

                for (int z = 0; z < m_c_numInputs - 1; z++)
                {
                    if (m_moveMatchups[z, bestIndex] == E_SimpleResult.WIN)
                    {
                        posibleCounters[posibleCountersCount] = z;
                        posibleCountersCount++;
                    }
                }
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
                E_RESULT oppRes = opponantsMoves[x].Use(m_moves[z]);

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

    private int PredictMoveConfidence()
    {
        // based on the previous move, previous fight result
        // returns the attacks, throws and defeces used on previus senarious
        ///int _attacks = m_data[m_previousType][m_previousResult][AI_ATTACK];
        ///int _blocks = m_data[m_previousType][m_previousResult][AI_BLOCK];
        ///int _throws = m_data[m_previousType][m_previousResult][AI_THROW];

        ///if (_attacks == _blocks && _blocks == _throws)
        ///{
        ///    return 3;
        ///}
        ///else if (_attacks != _blocks && _blocks != _throws && _throws != _attacks)
        ///{
        ///    return 1;
        ///}
        ///else
        ///{
        ///    return 2;
        ///}

        return 0;
    }
}
