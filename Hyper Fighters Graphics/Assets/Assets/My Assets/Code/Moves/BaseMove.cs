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
	//int m_damage;
	//float m_speed;
	//bool m_knockDown;
	//double m_FocusGain;
	protected string m_name;
	protected E_MOVE_TYPE m_type;
	//C_FighterData* m_pMe;

	void Start()
	{
		
	}

	void Update()
	{

	}

	public virtual void Initialize()
	{

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

	public E_MOVE_TYPE GetMoveType()
	{
		return m_type;
	}
}

public class LightAttack : BaseMove
{
	void Start()
	{
		m_type = E_MOVE_TYPE.L_ATTACK;
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

	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}
}

public class HeavyAttack : BaseMove
{
	void Start()
	{
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
			return E_RESULT.LOSE;
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

	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}
}

public class Throw : BaseMove
{
	void Start()
	{
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

	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}
}

public class BLOCK : BaseMove
{
	void Start()
	{
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

	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}
}

public class DODGE : BaseMove
{
	void Start()
	{
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

	}

	public override void Lose(BaseMove enemyMove, FighterData enemy)
	{

	}
}
