using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_ANIMATIONS
{
	IDLE,
	WALK_FORWARD,
	WALK_BACKWARD,
	HIT,
	THROWN,

	LIGHT,
	HEAVY,
	THROW,
	BLOCK,
	DODGE,
	COUNTER,

	TOTAL
};

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

	AnimationControler m_animationControler;

	void Start()
	{
		//m_sprite = gameObject.GetComponent<SpriteRenderer>();
		//m_animationControler = gameObject.AddComponent<AnimationControler>();
		//m_animationControler.F_initialize(gameObject.GetComponent<SpriteRenderer>());

		//InitializeAnimations();
	}

	public void Initialize()
	{
		m_sprite = gameObject.GetComponent<SpriteRenderer>();
		m_animationControler = gameObject.AddComponent<AnimationControler>();
		m_animationControler.F_initialize(gameObject.GetComponent<SpriteRenderer>());

		InitializeAnimations();
	}

	public void InitializeAnimations()
	{
		Animation[] animations = new Animation[(int)E_ANIMATIONS.TOTAL];

		animations[(int)E_ANIMATIONS.IDLE] = new Animation();
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[0], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[1], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[2], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[3], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[4], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[3], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[2], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].AddKeyFrame(m_sprites[1], 0.12f);
		animations[(int)E_ANIMATIONS.IDLE].RepeatAnim();
		m_animationControler.AddAnim(animations[(int)E_ANIMATIONS.IDLE]);

		animations[(int)E_ANIMATIONS.WALK_FORWARD] = new Animation();
		animations[(int)E_ANIMATIONS.WALK_FORWARD].AddKeyFrame(m_sprites[9], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_FORWARD].AddKeyFrame(m_sprites[8], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_FORWARD].AddKeyFrame(m_sprites[7], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_FORWARD].AddKeyFrame(m_sprites[6], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_FORWARD].RepeatAnim();
		m_animationControler.AddAnim(animations[(int)E_ANIMATIONS.WALK_FORWARD]);

		animations[(int)E_ANIMATIONS.WALK_BACKWARD] = new Animation();
		animations[(int)E_ANIMATIONS.WALK_BACKWARD].AddKeyFrame(m_sprites[6], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_BACKWARD].AddKeyFrame(m_sprites[7], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_BACKWARD].AddKeyFrame(m_sprites[8], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_BACKWARD].AddKeyFrame(m_sprites[9], 0.12f);
		animations[(int)E_ANIMATIONS.WALK_BACKWARD].RepeatAnim();
		m_animationControler.AddAnim(animations[(int)E_ANIMATIONS.WALK_BACKWARD]);

		animations[(int)E_ANIMATIONS.HIT] = new Animation();
		animations[(int)E_ANIMATIONS.HIT].AddKeyFrame(m_sprites[85], 0.12f);
		animations[(int)E_ANIMATIONS.HIT].RepeatAnim();
		m_animationControler.AddAnim(animations[(int)E_ANIMATIONS.HIT]);

		animations[(int)E_ANIMATIONS.THROWN] = new Animation();
		animations[(int)E_ANIMATIONS.THROWN].AddKeyFrame(m_sprites[86], 1.0f);
		animations[(int)E_ANIMATIONS.THROWN].AddKeyFrame(m_sprites[96], 1.0f);
		animations[(int)E_ANIMATIONS.THROWN].AddKeyFrame(m_sprites[89], 0.5f);
		animations[(int)E_ANIMATIONS.THROWN].AddKeyFrame(m_sprites[90], 0.5f);
		m_animationControler.AddAnim(animations[(int)E_ANIMATIONS.THROWN]);

		m_animationControler.SetAnim((int)E_ANIMATIONS.IDLE);
		m_animationControler.F_play();

	}

	void Update()
	{
		m_animationControler.F_update(Time.deltaTime);

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
		//m_sprite.sprite = m_sprites[id];
	}

	public void WalkForward()
	{
		this.transform.position += this.transform.right * m_movementSpeed * Time.deltaTime;

		m_animationControler.SetAnim((int)E_ANIMATIONS.WALK_FORWARD);
	}

	public void WalkBackward()
	{
		this.transform.position -= this.transform.right * m_movementSpeed * Time.deltaTime;

		m_animationControler.SetAnim((int)E_ANIMATIONS.WALK_BACKWARD);
	}

	public bool isMoving()
	{
		return m_moving;
	}

	public void SetAnimaton(E_ANIMATIONS z)
	{
		m_animationControler.SetAnim((int) z);
	}

	public void StartSeccondAnimation()
	{
		m_animationControler.MoveToNextAnim();
	}

	public AnimationControler GetAnimationControler()
	{
		return m_animationControler;
	}

	public Sprite[] GetSprites()
	{
		return m_sprites;
	}
}
