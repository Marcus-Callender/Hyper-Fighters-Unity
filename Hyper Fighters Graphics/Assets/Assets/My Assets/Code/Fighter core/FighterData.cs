﻿using System.Collections;
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
    KO_D,

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
	//int m_previousHP;
	int m_maxHP;
	string m_name;
	int m_focus;
	int m_maxFocus;
	int m_previousFocus;
	bool m_knockedDown;
	//void* m_pStatus;
	//C_Status* m_pStatuses[3];
	float[] m_velocity = new float[2];
    float m_upVel = 0.0f;
	bool m_moving = false;
	float m_friction = 1.0f;

	// true means moving left, false is moving right
	bool m_moving_Left_Right;

	public Sprite[] m_sprites;
	SpriteRenderer m_sprite;
	private float m_movementSpeed = 4.5f;

	AnimationControler m_animationControler;
	Timer m_timer;
    private bool m_isBeingThrown = false;

	bool m_wasPositionManipulated = false;
    public HealthUIManager m_healthUI;
    public FocusUIManager m_focusUI;

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

		//m_previousHP = m_hp;
		m_previousFocus = m_focus;

		m_timer = gameObject.AddComponent<Timer>();
		m_timer.Initialize(2.0f);

		InitializeAnimations();

        m_healthUI.Init(m_hp);
        m_focusUI.Init(100);
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
		m_animationControler.AddAnim(idleAnim, E_ANIMATIONS.IDLE);

		Animation walkForwardAnim = new Animation();
		walkForwardAnim.AddKeyFrame(m_sprites[9], 0.12f);
		walkForwardAnim.AddKeyFrame(m_sprites[8], 0.12f);
		walkForwardAnim.AddKeyFrame(m_sprites[7], 0.12f);
		walkForwardAnim.AddKeyFrame(m_sprites[6], 0.12f);
		walkForwardAnim.RepeatAnim();
		m_animationControler.AddAnim(walkForwardAnim, E_ANIMATIONS.WALK_FORWARD);

		Animation walkBackwardAnim = new Animation();
		walkBackwardAnim.AddKeyFrame(m_sprites[6], 0.12f);
		walkBackwardAnim.AddKeyFrame(m_sprites[7], 0.12f);
		walkBackwardAnim.AddKeyFrame(m_sprites[8], 0.12f);
		walkBackwardAnim.AddKeyFrame(m_sprites[9], 0.12f);
		walkBackwardAnim.RepeatAnim();
		m_animationControler.AddAnim(walkBackwardAnim, E_ANIMATIONS.WALK_BACKWARD);

		Animation hitAnim = new Animation();
		hitAnim.AddKeyFrame(m_sprites[85], 1.0f);
		hitAnim.AddKeyFrame(m_sprites[85], 0.1f);
		m_animationControler.AddAnim(hitAnim, E_ANIMATIONS.HIT);

		Animation ThrownAnim = new Animation();
		ThrownAnim.AddKeyFrame(m_sprites[86], 0.36f);
		ThrownAnim.AddKeyFrame(m_sprites[96], 0.2f);
		ThrownAnim.AddKeyFrame(m_sprites[89], 0.9f);
		ThrownAnim.AddKeyFrame(m_sprites[90], 0.2f);
		m_animationControler.AddAnim(ThrownAnim, E_ANIMATIONS.THROWN);

        Animation ThrowRejectAnim = new Animation();
		ThrowRejectAnim.AddKeyFrame(m_sprites[84], 0.25f);
		m_animationControler.AddAnim(ThrowRejectAnim, E_ANIMATIONS.THROW_REJECT);

        Animation KOd = new Animation();
        KOd.AddKeyFrame(m_sprites[93], 0.75f);
        KOd.AddKeyFrame(m_sprites[90], 900.0f);
        m_animationControler.AddAnim(KOd, E_ANIMATIONS.KO_D);

		m_animationControler.SetAnim((int)E_ANIMATIONS.IDLE);
		m_animationControler.F_play();
	}

	public void F_Update()
	{
		m_wasPositionManipulated = false;

		m_animationControler.F_update(Time.deltaTime);

        if (m_isBeingThrown)
        {
            if (transform.position.y <= 0.0f)
            {
                Vector3 newPos = transform.position;
                newPos.y = 0.0f;
                transform.position = newPos;

                m_upVel = 0.0f;
                m_isBeingThrown = false;
            }
            else
            {
                m_upVel -= (9.81f * Time.deltaTime);

                this.transform.position -= this.transform.right * m_velocity[1] * Time.deltaTime;

                this.transform.position += this.transform.up * m_upVel * Time.deltaTime;

                if (m_moving_Left_Right)
                {
                    this.transform.position -= this.transform.forward * m_velocity[0] * Time.deltaTime;
                }
                else
                {
                    this.transform.position += this.transform.forward * m_velocity[0] * Time.deltaTime;
                }


            }
        }
		else if (m_moving)
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

			//m_velocity[1] -= m_friction * Time.deltaTime;
			//m_velocity[0] -= m_friction * Time.deltaTime;

			m_velocity[1] *= 0.95f/* * Time.deltaTime*/;
			m_velocity[0] *= 0.95f/* * Time.deltaTime*/;

			if (m_velocity[0] <= 0.3f)
			{
				m_velocity[0] = 0.0f;
			}

			if (m_velocity[1] <= 0.3f)
			{
				m_velocity[1] = 0.0f;
			}

			if ((m_velocity[0] == 0.0f) && (m_velocity[1] == 0.0f))
			{
				m_moving = false;
				//m_sprite.sprite = m_sprites[0];
			}
		}
	}

	public void SetVelocity(float side, float back, bool stopCamera = false)
	{
        m_wasPositionManipulated = stopCamera;

		m_velocity[0] = side;
		m_velocity[1] = back;
		m_moving = true;
		m_moving_Left_Right = Random.Range(0, 2) == 0;
    }

    public void SetThrowVelocity(float side, float back, float up, bool stopCamera = false)
    {
        m_wasPositionManipulated = stopCamera;

        m_velocity[0] = side;
        m_velocity[1] = back;
        m_upVel = up;
        m_moving = true;
        m_isBeingThrown = true;
        m_moving_Left_Right = Random.Range(0, 2) == 0;
    }
    
    public void SetPosition(Vector3 otherFighterPosition, float horizontalOffset, float verticalOffset)
	{
        //Vector3 myTemp = this.transform.position;
        //Vector3 otherTemp = otherFighterPosition;

        m_wasPositionManipulated = true;

        //otherTemp += (this.transform.right * horizontalOffset);
        //otherTemp += (this.transform.up * verticalOffset);

        //this.transform.position = otherTemp;

        //Debug.Log(myTemp + " -> " + otherTemp);

        this.transform.position = otherFighterPosition;
        this.transform.position += this.transform.right * horizontalOffset;
        this.transform.position += this.transform.up * verticalOffset;
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
		//m_previousHP = m_hp;
		m_hp -= ammount;
        m_healthUI.takeDamage(ammount);
        
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

        m_focusUI.GainFocus(ammount);
	}

	/*public string GetHpUIString()
	{
		return "Health: " + m_timer.Interpolate(m_previousHP, m_hp);
	}*/

	/*public string GetFocusUIString()
	{
		return "Focus: " + m_timer.Interpolate(m_previousFocus, m_focus) + "/" + m_maxFocus;
	}*/

	public void Rest()
	{
        //m_previousHP = m_hp;

		m_previousFocus = m_focus;
        m_healthUI.Rest();
        m_focusUI.Rest();
	}

	public bool CanUseHyper()
	{
		if (m_focus == 100)
		{
			m_previousFocus = m_focus;
            gainFocus(-100);
            return true;
		}

		return false;
	}

	public float DistanceFromEnemy(FighterData enemy)
	{
		Vector3 thisPos = gameObject.transform.position;
		Vector3 enemyPos = enemy.transform.position;

		return Mathf.Sqrt(Mathf.Pow((thisPos.x - enemyPos.x), 2.0f) + Mathf.Pow((thisPos.z - enemyPos.z), 2.0f));
	}

	public bool IsAnimating()
	{
		return m_animationControler.GetPlaying();
	}

	public bool HasPassedImpactTime()
	{
		return m_animationControler.HasPassedImpactTime();
    }

    public bool HasPassedDamageTime()
    {
        return m_animationControler.HasPassedDamageTime();
    }

    public bool isMoving()
	{
		return m_moving;
	}

	public void SetAnimaton(E_ANIMATIONS z)
	{
		m_animationControler.SetAnim((int) z);
	}

	public bool CheckCurrentAnimation(E_ANIMATIONS anim)
	{
		return m_animationControler.CheckCurrentAnimation(anim);
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

	public bool GetWasPositionManipulated()
	{
		return m_wasPositionManipulated;
	}

    public bool IsKOd()
    {
        return m_hp <= 0;
    }
}