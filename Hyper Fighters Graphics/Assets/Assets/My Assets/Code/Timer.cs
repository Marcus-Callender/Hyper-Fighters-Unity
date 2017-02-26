using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	float m_timeCount = 0.0f;
	float m_time = 0.0f;
	bool m_playing = false;
	
	void Update()
	{
		if (m_playing)
		{
			m_timeCount += Time.deltaTime;

			if (m_timeCount > m_time)
			{
				m_timeCount = m_time;
				m_playing = false;
			}
		}
	}

	public void Initialize(float time)
	{
		m_time = time;
		m_timeCount = 0.0f;
		m_playing = false;
	}

	public void Play()
	{
		m_timeCount = 0.0f;
		m_playing = true;
	}

	public int Interpolate(float small, float big)
	{
		return (int)(((big - small) * (m_timeCount / m_time)) + small);
	}
}
