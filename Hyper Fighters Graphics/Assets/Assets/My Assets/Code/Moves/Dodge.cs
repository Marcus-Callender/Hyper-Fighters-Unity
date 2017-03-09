using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : BaseMove
{
	void Start()
	{
		base.Initialize();
		m_type = E_MOVE_TYPE.DODGE;
	}

	public override E_RESULT Use(BaseMove enemyMove, FighterData enemy)
	{
		if ((enemyMove.GetMoveType() == E_MOVE_TYPE.L_ATTACK) || (enemyMove.GetMoveType() == E_MOVE_TYPE.H_ATTACK))
		{
			return E_RESULT.WIN;
		}
		else if (enemyMove.GetMoveType() == E_MOVE_TYPE.THROW)
		{
			return E_RESULT.LOSE;
		}
		else if ((enemyMove.GetMoveType() == E_MOVE_TYPE.BLOCK) || (enemyMove.GetMoveType() == E_MOVE_TYPE.DODGE))
		{
			return E_RESULT.LOSE;
		}

		return E_RESULT.LOSE;
	}

	public override void Win(BaseMove enemyMove, FighterData enemy)
	{
		m_me.SetVelocity(2.0f, 0.0f);
		m_me.SetAnimaton(E_ANIMATIONS.DODGE);

		enemy.takeDamage(m_damage);
		m_me.gainFocus(m_focusGain);
	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{
		m_me.SetVelocity(2.0f, 0.0f);
	}

	public override void SetAnimation(AnimationControler animCon, Sprite[] sprites)
	{
		Animation anim = new Animation();

		anim.AddKeyFrame(sprites[5], 1.0f);

		animCon.AddAnim(anim);
	}

	public override bool Update1(FighterData enemy)
	{
		m_me.SetAnimaton(E_ANIMATIONS.DODGE);

		if (!m_me.IsAnimating())
		{
			return true;
		}

		return false;
	}

	public override bool Update2(E_RESULT myResult, E_RESULT otherResult, FighterData enemy)
	{
		if (myResult == E_RESULT.WIN || myResult == E_RESULT.SP_WIN)
		{
			m_me.SetAnimaton(E_ANIMATIONS.LIGHT);

			if (m_me.HasPassedImpactTime())
			{
				enemy.SetAnimaton(E_ANIMATIONS.HIT);
			}

			if (!m_me.IsAnimating())
			{
				return true;
			}
		}
		else if (myResult == E_RESULT.LOSE && otherResult == E_RESULT.LOSE)
		{
			return true;
		}

		return false;
	}
}

