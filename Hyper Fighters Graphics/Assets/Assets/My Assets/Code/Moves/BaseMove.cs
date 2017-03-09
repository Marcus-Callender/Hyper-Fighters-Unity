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

