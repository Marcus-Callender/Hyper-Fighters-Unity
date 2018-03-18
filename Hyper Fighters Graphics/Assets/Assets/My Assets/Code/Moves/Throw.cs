﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : BaseMove
{
    float m_timeInUpdate2 = 0.0f;

    void Start()
    {
        base.Initialize();
        m_type = E_MOVE_TYPE.THROW;
    }

    public override E_RESULT Use(BaseMove enemyMove)
    {
        if ((enemyMove.GetMoveType() == E_MOVE_TYPE.L_ATTACK) || (enemyMove.GetMoveType() == E_MOVE_TYPE.H_ATTACK))
        {
            return E_RESULT.LOSE;
        }
        else if (enemyMove.GetMoveType() == E_MOVE_TYPE.THROW)
        {
            return E_RESULT.LOSE;
        }
        else if ((enemyMove.GetMoveType() == E_MOVE_TYPE.BLOCK) || (enemyMove.GetMoveType() == E_MOVE_TYPE.DODGE))
        {
            return E_RESULT.WIN;
        }

        return E_RESULT.LOSE;
    }

    public override void Win(BaseMove enemyMove, FighterData enemy)
    {
        enemy.SetVelocity(1.5f, 1.5f);
        m_me.SetAnimaton(E_ANIMATIONS.THROW);
        enemy.SetAnimaton(E_ANIMATIONS.THROWN);

        enemy.takeDamage(m_damage);
        m_me.gainFocus(m_focusGain);
    }

    public override void Lose(BaseMove enemyMove, FighterData enemy)
    {
    }

    public override void SetAnimation(AnimationControler animCon, Sprite[] sprites)
    {
        Animation anim = new Animation();

        anim.AddKeyFrame(sprites[49], 0.12f);
        anim.AddKeyFrame(sprites[56], 0.12f);
        anim.AddKeyFrame(sprites[57], 0.12f);
        anim.AddKeyFrame(sprites[58], 0.72f);

        animCon.AddAnim(anim);
    }

    public override bool Update1(FighterData enemy)
    {
        //m_me.SetAnimaton(E_ANIMATIONS.THROW);

        //if (!m_me.IsAnimating())
        //{
        //	m_me.SetAnimaton(E_ANIMATIONS.IDLE);
        //	m_timeInUpdate2 = 0.0f;
        //	return true;
        //}

        m_me.WalkForward();

        if (m_me.DistanceFromEnemy(enemy) < 5.0f)
        {
            return true;
        }

        return false;
    }

    public override bool Update2(E_RESULT myResult, E_RESULT otherResult, FighterData enemy)
    {
        if (myResult == E_RESULT.WIN || myResult == E_RESULT.SP_WIN)
        {
            m_me.SetAnimaton(E_ANIMATIONS.THROW);
            enemy.SetAnimaton(E_ANIMATIONS.THROWN);

            m_timeInUpdate2 += Time.deltaTime;

            if (m_me.GetAnimationControler().GetCurrentFrame() == 1)
            {

                enemy.SetPosition(m_me.GetComponent<Transform>().position, 2.0f, 1.5f);
            }

            else if (enemy.GetAnimationControler().GetCurrentFrame() == 0)
            {
                //if (m_timeInUpdate2 > 1.0f)
                //{
                //    Debug.Log("Not In Sync, Time is: " + m_timeInUpdate2);
                //}

                //enemy.SetPosition(m_me.GetComponent<Transform>().position, -1.0f, 0.5f);
                enemy.SetPosition(m_me.GetComponent<Transform>().position, -2.0f, 1.5f);
            }
            /*else
            {
                enemy.SetVelocity(-2.0f, 3.0f - m_timeInUpdate2, true);
            }*/
            else if (enemy.GetAnimationControler().GetCurrentFrame() == 1)
            {
                //if (m_timeInUpdate2 > 2.0f)
                //{
                //    Debug.Log("Not In Sync, Time is: " + m_timeInUpdate2);
                //}

                //enemy.SetPosition(m_me.GetComponent<Transform>().position, 0.0f, 1.5f);

                ///enemy.SetPosition(m_me.GetComponent<Transform>().position, -2.5f, 3.5f);
                enemy.SetThrowVelocity(-2.5f, 3.5f, 3.0f);

            }
            else if (enemy.GetAnimationControler().GetCurrentFrame() == 2)
            {
                //if (m_timeInUpdate2 > 2.5f)
                //{
                //    Debug.Log("Not In Sync, Time is: " + m_timeInUpdate2);
                //}

                //enemy.SetPosition(m_me.GetComponent<Transform>().position, 1.0f, 0.5f);
                ///enemy.SetPosition(m_me.GetComponent<Transform>().position, -5.0f, 2.5f);
            }
            else if (enemy.GetAnimationControler().GetCurrentFrame() == 3)
            {
                //if (m_timeInUpdate2 > 3.0f)
                //{
                //    Debug.Log("Not In Sync, Time is: " + m_timeInUpdate2);
                //}

                //enemy.SetPosition(m_me.GetComponent<Transform>().position, 2.0f, 0.0f);
                ///enemy.SetPosition(m_me.GetComponent<Transform>().position, -10.0f, 0.0f);
            }

            if (enemy.gameObject.transform.position.y <= 0.0f)
            {
                Vector3 enemyPos = enemy.gameObject.transform.position;
                enemyPos.y = 0.0f;
                enemy.gameObject.transform.position = enemyPos;

                return true;
            }
        }
        else if (myResult == E_RESULT.LOSE && otherResult == E_RESULT.LOSE)
        {
            m_me.SetAnimaton(E_ANIMATIONS.THROW_REJECT);
            m_me.SetVelocity(0.0f, 1.0f);

            if (!m_me.IsAnimating())
            {
                return true;
            }
        }

        return false;
    }
}

