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
	THROW_REJECT,

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
	float m_friction = 1.0f;

	// true means moving left, false is moving right
	bool m_moving_Left_Right;

	public Sprite[] m_sprites;
	SpriteRenderer m_sprite;
	private float m_movementSpeed = 1.5f;

	AnimationControler m_animationControler;
	Timer m_timer;

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

		m_hp = 100;
		m_focus = 0;
		m_maxFocus = 100;

		m_previousHP = m_hp;
		m_previousFocus = m_focus;

		m_timer = gameObject.AddComponent<Timer>();
		m_timer.Initialize(2.0f);

		InitializeAnimations();
	}

	private void InitializeAnimations()
	{
		Animation idleAnim = new Animation();
		idleAnim.AddKeyFrame(m_sprites[0], 0.12f);
		idleAnim.AddKeyFrame(m_sprites[1], 0.12f);
		idleAnim.AddKeyFrame(m_sprites[2], 0.12f);
		idleAnim.AddKeyFrame(m_sprites[3], 0.12f);
		idleAnim.AddKeyFrame(m_sprites[4], 0.12f);
		idleAnim.AddKeyFrame(m_sprites[3], 0.12f);
		idleAnim.AddKeyFrame(m_sprites[2], 0.12f);
		idleAnim.AddKeyFrame(m_sprites[1], 0.12f);
		idleAnim.RepeatAnim();
		m_animationControler.AddAnim(idleAnim);

		Animation walkForwardAnim = new Animation();
		walkForwardAnim.AddKeyFrame(m_sprites[9], 0.12f);
		walkForwardAnim.AddKeyFrame(m_sprites[8], 0.12f);
		walkForwardAnim.AddKeyFrame(m_sprites[7], 0.12f);
		walkForwardAnim.AddKeyFrame(m_sprites[6], 0.12f);
		walkForwardAnim.RepeatAnim();
		m_animationControler.AddAnim(walkForwardAnim);

		Animation walkBackwardAnim = new Animation();
		walkBackwardAnim.AddKeyFrame(m_sprites[6], 0.12f);
		walkBackwardAnim.AddKeyFrame(m_sprites[7], 0.12f);
		walkBackwardAnim.AddKeyFrame(m_sprites[8], 0.12f);
		walkBackwardAnim.AddKeyFrame(m_sprites[9], 0.12f);
		walkBackwardAnim.RepeatAnim();
		m_animationControler.AddAnim(walkBackwardAnim);

		Animation hitAnim = new Animation();
		hitAnim.AddKeyFrame(m_sprites[85], 1.0f);
		hitAnim.AddKeyFrame(m_sprites[85], 0.1f);
		m_animationControler.AddAnim(hitAnim);

		Animation ThrownAnim = new Animation();
		ThrownAnim.AddKeyFrame(m_sprites[86], 1.0f);
		ThrownAnim.AddKeyFrame(m_sprites[96], 1.0f);
		ThrownAnim.AddKeyFrame(m_sprites[89], 0.5f);
		ThrownAnim.AddKeyFrame(m_sprites[90], 0.5f);
		m_animationControler.AddAnim(ThrownAnim);

		Animation ThrowRejectAnim = new Animation();
		ThrowRejectAnim.AddKeyFrame(m_sprites[84], 1.0f);
		m_animationControler.AddAnim(ThrowRejectAnim);

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

	public void SetVelocity(float side, float back)
	{
		m_velocity[0] = side;
		m_velocity[1] = back;
		m_moving = true;
		m_moving_Left_Right = Random.Range(0, 2) == 0;
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

	public void takeDamage(int ammount)
	{
		m_previousHP = m_hp;
		m_hp -= ammount;

		gainFocus((int)(ammount * 0.3f));
	}

	public void gainFocus(int ammount)
	{
		m_previousFocus = m_focus;
		m_focus += ammount;

		m_timer.Play();

		if (m_focus > m_maxFocus)
		{
			m_focus = m_maxFocus;
		}
	}

	public string GetHpUIString()
	{
		return "Health: " + m_timer.Interpolate(m_hp, m_previousHP);
	}

	public string GetFocusUIString()
	{
		return "Focus: " + m_timer.Interpolate(m_previousFocus, m_focus) + "/" + m_maxFocus;
	}

	public bool IsAnimating()
	{
		return m_animationControler.GetPlaying();
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
