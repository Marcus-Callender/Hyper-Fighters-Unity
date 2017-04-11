using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : BaseMove
{
	void Start()
	{
		base.Initialize();
		m_type = E_MOVE_TYPE.H_ATTACK;
	}

	public override E_RESULT Use(BaseMove enemyMove)
	{
		if (enemyMove.GetMoveType() == E_MOVE_TYPE.L_ATTACK)
		{
			return E_RESULT.LOSE;
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
		m_me.SetVelocity(0.0f, -2.0f);

		enemy.takeDamage(m_damage);
		m_me.gainFocus(m_focusGain);
	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}

	public override void SetAnimation(AnimationControler animCon, Sprite[] sprites)
	{
		Animation anim = new Animation();

		anim.AddKeyFrame(sprites[29], 0.12f);
		anim.AddKeyFrame(sprites[30], 0.12f);
		anim.AddKeyFrame(sprites[31], 0.12f);
		anim.AddKeyFrame(sprites[33], 0.12f);

		anim.SetImpactTime(0.48f);

		anim.AddKeyFrame(sprites[34], 0.12f);
		anim.AddKeyFrame(sprites[35], 0.12f);
		anim.AddKeyFrame(sprites[36], 0.12f);

		anim.AddKeyFrame(sprites[34], 0.12f);
		anim.AddKeyFrame(sprites[35], 0.12f);
		anim.AddKeyFrame(sprites[36], 0.12f);

		anim.AddKeyFrame(sprites[34], 0.12f);
		anim.AddKeyFrame(sprites[35], 0.12f);
		anim.AddKeyFrame(sprites[36], 0.12f);

		anim.AddKeyFrame(sprites[34], 0.12f);
		anim.AddKeyFrame(sprites[35], 0.12f);
		anim.AddKeyFrame(sprites[36], 0.12f);

		anim.AddKeyFrame(sprites[34], 0.12f);
		anim.AddKeyFrame(sprites[35], 0.12f);
		anim.AddKeyFrame(sprites[36], 0.12f);

		animCon.AddAnim(anim);
	}

	public override bool Update1(FighterData enemy)
	{
		m_me.SetAnimaton(E_ANIMATIONS.HEAVY);

		if (m_me.HasPassedImpactTime())
		{
			return true;
		}

		return false;
	}

	public override bool Update2(E_RESULT myResult, E_RESULT otherResult, FighterData enemy)
	{
		if (m_me.CheckCurrentAnimation(E_ANIMATIONS.HEAVY))
		{
			m_me.SetVelocity(0.0f, -3.0f);

			if (m_me.DistanceFromEnemy(enemy) < 1.0f)
			{
				enemy.SetVelocity(2.0f, 3.0f);
			}
		}

		if (myResult == E_RESULT.WIN || myResult == E_RESULT.SP_WIN)
		{
			enemy.SetAnimaton(E_ANIMATIONS.HIT);
			enemy.SetVelocity(1.0f, 1.0f);

			if (!enemy.IsAnimating())
			{
				return true;
			}
		}

		return false;
	}
}

