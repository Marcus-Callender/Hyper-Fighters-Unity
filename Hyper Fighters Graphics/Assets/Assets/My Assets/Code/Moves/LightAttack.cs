using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : BaseMove
{
	void Start()
	{
		base.Initialize();
		m_type = E_MOVE_TYPE.L_ATTACK;
	}

	public override E_RESULT Use(BaseMove enemyMove)
	{
		if (enemyMove.GetMoveType() == E_MOVE_TYPE.L_ATTACK)
		{
			return E_RESULT.WIN;
		}
		else if (enemyMove.GetMoveType() == E_MOVE_TYPE.H_ATTACK)
		{
			return E_RESULT.WIN;
		}
		else if (enemyMove.GetMoveType() == E_MOVE_TYPE.THROW)
		{
			return E_RESULT.WIN;
		}
		else if ((enemyMove.GetMoveType() == E_MOVE_TYPE.BLOCK) || (enemyMove.GetMoveType() == E_MOVE_TYPE.DODGE))
		{
			return E_RESULT.LOSE;
		}

		return E_RESULT.LOSE;
	}

	public override void Win(BaseMove enemyMove, FighterData enemy)
	{
		enemy.SetVelocity(1.0f, 1.0f);

		m_me.SetAnimaton(E_ANIMATIONS.LIGHT);

		enemy.takeDamage(m_damage);
		m_me.gainFocus(m_focusGain);
	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}

	public override void SetAnimation(AnimationControler animCon, Sprite[] sprites)
	{
		Animation anim = new Animation();

		anim.AddKeyFrame(sprites[18], 0.10f);
		anim.AddKeyFrame(sprites[19], 0.10f);
		anim.SetImpactTime(0.24f);
		anim.AddKeyFrame(sprites[20], 0.5f);

		animCon.AddAnim(anim, E_ANIMATIONS.LIGHT);
	}

	public override bool Update1(FighterData enemy)
	{
		m_me.SetAnimaton(E_ANIMATIONS.LIGHT);

		if (m_me.HasPassedImpactTime())
		{
			return true;
		}

		return false;
	}

	public override bool Update2(E_RESULT myResult, E_RESULT otherResult, FighterData enemy)
	{
		if (myResult == E_RESULT.WIN || myResult == E_RESULT.SP_WIN)
		{
			enemy.SetAnimaton(E_ANIMATIONS.HIT);

			if (!enemy.IsAnimating())
			{
				return true;
			}
		}

		return false;
	}
}

