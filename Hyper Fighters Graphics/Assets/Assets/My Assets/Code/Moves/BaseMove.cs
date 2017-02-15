using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum E_MOVE_TYPE
{
	L_ATTACK,
	H_ATTACK,
	THROW,
	BLOCK,
	DODGE
}

public class BaseMove : MonoBehaviour
{
	int m_damage;
	float m_speed;
	bool m_knockDown;
	double m_FocusGain;
	string m_name;
	E_MOVE_TYPE m_type;
	//C_FighterData* m_pMe;

	void Start()
	{
		
	}

	void Update()
	{

	}
}
