using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_MOVE_TYPE
{
	L_ATTACK,
	H_ATTACK,
	THROW,
	BLOCK,
	DODGE
}

public enum E_RESULT
{
	SP_LOSE,
	LOSE,
	WIN,
	SP_WIN
}

public class BaseMove : MonoBehaviour
{
	protected int m_damage;
	//float m_speed;
	//bool m_knockDown;
	protected int m_focusGain;
	//protected string m_name;
	protected E_MOVE_TYPE m_type;
	protected FighterData m_me;

	void Start()
	{
		m_me = gameObject.GetComponent<FighterData>();
	}

	public virtual void Initialize()
	{
		m_me = gameObject.GetComponent<FighterData>();
	}

	public void SetValues(int damage, int focusGain)
	{
		m_damage = damage;
		m_focusGain = focusGain;
	}

	public virtual E_RESULT Use(BaseMove enemyMove, FighterData enemy)
	{
		return E_RESULT.LOSE;
	}

	public virtual void Win(BaseMove enemyMove, FighterData enemy)
	{

	}

	public virtual void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}

	public virtual void SetAnimation(AnimationControler animCon, Sprite[] sprites)
	{
		
	}

	public virtual bool Update1(FighterData enemy)
	{
		return true;
	}

	public virtual bool Update2(E_RESULT myResult, E_RESULT otherResult, FighterData enemy)
	{
		return true;
	}

	public E_MOVE_TYPE GetMoveType()
	{
		return m_type;
	}

	public int GetDamage()
	{
		return m_damage;
	}
}

public class LightAttack : BaseMove
{
	void Start()
	{
		base.Initialize();
		m_type = E_MOVE_TYPE.L_ATTACK;
	}

	public override E_RESULT Use(BaseMove enemyMove, FighterData enemy)
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

		anim.AddKeyFrame(sprites[18], 0.12f);
		anim.AddKeyFrame(sprites[19], 0.12f);
		anim.SetImpactTime(0.24f);
		anim.AddKeyFrame(sprites[20], 0.72f);

		animCon.AddAnim(anim);
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

public class HeavyAttack : BaseMove
{
	void Start()
	{
		base.Initialize();
		m_type = E_MOVE_TYPE.H_ATTACK;
	}

	public override E_RESULT Use(BaseMove enemyMove, FighterData enemy)
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
		//m_me.SetVelocity(0.0f, -1.0f);
		//enemy.SetVelocity(0.0f, 1.0f);

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
			enemy.SetVelocity(1.0f, 1.0f);

			if (!enemy.IsAnimating())
			{
				return true;
			}
		}

		return false;
	}
}

public class Throw : BaseMove
{
	float m_timeInUpdate2 = 0.0f;

	void Start()
	{
		base.Initialize();
		m_type = E_MOVE_TYPE.THROW;
	}

	public override E_RESULT Use(BaseMove enemyMove, FighterData enemy)
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

			if (m_timeInUpdate2 < 1.0f)
			{
				enemy.SetPosition(m_me.GetComponent<Transform>().position, -1.0f, 0.5f);
			}
			else if (m_timeInUpdate2 < 2.0f)
			{
				enemy.SetPosition(m_me.GetComponent<Transform>().position, 0.0f, 1.5f);
			}
			else if (m_timeInUpdate2 < 2.5f)
			{
				enemy.SetPosition(m_me.GetComponent<Transform>().position, 1.0f, 0.5f);
			}
			else if (m_timeInUpdate2 < 3.0f)
			{
				enemy.SetPosition(m_me.GetComponent<Transform>().position, 2.0f, 0.0f);
			}

			if (!enemy.IsAnimating())
			{
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

public class Block : BaseMove
{
	void Start()
	{
		base.Initialize();
		m_type = E_MOVE_TYPE.BLOCK;
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
		m_me.SetVelocity(1.0f, 1.0f);
		m_me.SetAnimaton(E_ANIMATIONS.BLOCK);
		m_me.gainFocus((int)(enemyMove.GetDamage() * 2.0f));
	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}

	public override void SetAnimation(AnimationControler animCon, Sprite[] sprites)
	{
		Animation anim = new Animation();

		anim.AddKeyFrame(sprites[13], 1.5f);

		animCon.AddAnim(anim);
	}

	public override bool Update1(FighterData enemy)
	{
		m_me.SetAnimaton(E_ANIMATIONS.BLOCK);

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
			if (!m_me.isMoving())
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

public class DODGE : BaseMove
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
