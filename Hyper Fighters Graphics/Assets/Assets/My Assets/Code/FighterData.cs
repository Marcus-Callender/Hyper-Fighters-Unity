using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterData : MonoBehaviour
{
	int m_hp;
	int m_previousHP;
	int m_maxHP;
	string m_name;
	int m_focus;
	int m_maxFocus;
	int m_previousFocus;
	bool m_knockedDown;
	//void* m_pStatus;
	//C_Status* m_pStatuses[3];
	float[] m_velocity = new float[2];
	bool m_moving = false;
	float m_friction = 0.5f;

	// true means moving left, false is moving right
	bool m_moving_Left_Right;

	public Sprite[] m_sprites;
	SpriteRenderer m_sprite;
	private float m_movementSpeed = 1.0f;

	void Start()
	{
		m_sprite = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (m_moving)
		{
			this.transform.position -= this.transform.right * m_velocity[1] * Time.deltaTime;

			if (m_moving_Left_Right)
			{
				this.transform.position -= this.transform.forward * m_velocity[0] * Time.deltaTime;
			}
			else
			{
				this.transform.position += this.transform.forward * m_velocity[0] * Time.deltaTime;
			}

			m_velocity[1] -= m_friction * Time.deltaTime;
			m_velocity[0] -= m_friction * Time.deltaTime;

			if (m_velocity[0] <= 0.0f)
			{
				m_velocity[0] = 0.0f;
			}

			if (m_velocity[1] <= 0.0f)
			{
				m_velocity[1] = 0.0f;
			}

			if ((m_velocity[0] == 0.0f) && (m_velocity[1] == 0.0f))
			{
				m_moving = false;
				m_sprite.sprite = m_sprites[0];
			}
		}
	}

	public void AddToVelocity(float side, float back)
	{
		m_velocity[0] = side;
		m_velocity[1] = back;
		m_moving = true;
		m_moving_Left_Right = Random.Range(0, 2) == 0;
	}

	public void setSprite(int id)
	{
		m_sprite.sprite = m_sprites[id];
	}

	public void WalkForward()
	{
		this.transform.position += this.transform.right * m_movementSpeed * Time.deltaTime;
	}

	public void WalkBackward()
	{
		this.transform.position -= this.transform.right * m_movementSpeed * Time.deltaTime;
	}

	public bool isMoving()
	{
		return m_moving;
	}
}
