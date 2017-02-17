using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControler : MonoBehaviour
{
	private SpriteRenderer m_spriteRenderer;

	private Animation[] m_animations = new Animation[20];
	private int m_numAnims = 0;
	private int m_curretAnim = 0;
	private int m_nextAnim = -1;

	public void F_initialize(SpriteRenderer spriteRenderer)
	{
		m_spriteRenderer = spriteRenderer;
	}

	public void AddAnim(Animation newAnim)
	{
		m_animations[m_curretAnim] = newAnim;

		m_numAnims++;
	}

	public void F_update(float deltaTime)
	{
		m_animations[m_curretAnim].F_update(deltaTime);
	}

	public void F_play()
	{
		m_animations[m_curretAnim].Play();
	}

	public void MoveToNextAnim()
	{
		if (m_nextAnim != -1)
		{
			m_curretAnim = m_nextAnim;
			m_nextAnim = -1;
		}
	}

	public bool F_getPlaying()
	{
		return m_animations[m_curretAnim].GetPlaying();
	}

	public void Stop()
	{
		m_animations[m_curretAnim].Stop();
	}

	public bool SetAnim(int z)
	{
		if (z >= m_numAnims)
		{
			Debug.Log("Anim z not recognised.");
			return false;
		}

		if (z != m_curretAnim)
		{
			m_animations[z].Play();
		}

		m_curretAnim = z;

		return true;
	}

	public bool SetNextAnim(int z)
	{
		if (z >= m_numAnims)
		{
			Debug.Log("Anim z not recognised.");
			return false;
		}

		m_nextAnim = z;

		return true;
	}

	public bool GetPlaying()
	{
		return m_animations[m_curretAnim].GetPlaying();
	}

	public void SetPause(bool pause)
	{
		m_animations[m_curretAnim].SetPause(pause);
	}
}


public class Animation
{
	private SpriteRenderer m_spriteRenderer;
	private bool m_playing = true;
	private float m_currentAnimTime = 0.0f;
	private int m_currentKey = 0;

	public List<Sprite> m_sprites;
	public List<float> m_displayTimes;
	public int m_numberKeys = 0;

	private bool m_repeat = false;
	private bool m_paused = false;

	public void Initialize(SpriteRenderer spriteRenderer)
	{
		m_spriteRenderer = spriteRenderer;

		m_sprites = new List<Sprite>();
		m_displayTimes = new List<float>();
	}

	public void AddKeyFrame(Sprite sprite, float displayTime)
	{
		m_sprites.Add(sprite);
		m_displayTimes.Add(displayTime);
		m_numberKeys++;
	}

	public void F_update(float deltaTime)
	{
		if (!m_paused)
		{
			if (m_playing)
			{
				m_currentAnimTime += deltaTime;

				if (m_currentKey < (m_numberKeys - 1))
				{
					if (m_currentAnimTime > GetAnimationTime(m_currentKey))
					{
						m_currentKey++;
						m_spriteRenderer.sprite = m_sprites[m_currentKey];

						if (m_sprites[m_currentKey] == null)
						{
							m_spriteRenderer.enabled = false;
						}
						else
						{
							m_spriteRenderer.enabled = true;
						}
					}
				}
				else
				{
					if (m_currentAnimTime > GetAnimationTime())
					{
						if (m_repeat)
						{
							Play();
						}
						else
						{
							Stop();
						}
					}
				}
			}
		}
	}

	public void Play()
	{
		m_currentAnimTime = 0.0f;
		m_playing = true;
		m_spriteRenderer.sprite = m_sprites[0];
		m_spriteRenderer.enabled = true;
		m_currentKey = 0;
	}

	public void Stop()
	{
		m_currentAnimTime = 0.0f;
		m_playing = false;
		m_spriteRenderer.sprite = null;
		m_spriteRenderer.enabled = false;
		m_currentKey = 0;
	}

	private float GetAnimationTime()
	{
		float time = 0.0f;

		for (int z = 0; z < m_displayTimes.Count; z++)
		{
			time += m_displayTimes[z];
		}

		return time;
	}

	private float GetAnimationTime(int index)
	{
		float time = 0.0f;

		for (int z = 0; z <= index; z++)
		{
			time += m_displayTimes[z];
		}

		return time;
	}

	public bool GetPlaying()
	{
		return m_playing;
	}

	public void RepeatAnim()
	{
		m_repeat = true;
	}

	public void SetPause(bool pause)
	{
		m_paused = pause;
	}
}

