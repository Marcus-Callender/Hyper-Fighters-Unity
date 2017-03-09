using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
	private GameObject[] m_charicters = new GameObject[2];
	private FighterData[] m_charicterData = new FighterData[2];
	private GameObject m_camera;

	private bool m_charicterDistanceCheckStart = true;
	private bool m_charictersClose = false;
	private bool m_charictersFar = false;
	
	void Start()
	{

	}
	
	void Update()
	{
		if (!m_charicterData[0].GetWasPositionManipulated() && !m_charicterData[1].GetWasPositionManipulated())
		{
			Vector3 charicterMidpont = (m_charicters[0].transform.position + m_charicters[1].transform.position) * 0.5f;

			m_camera.transform.rotation = Quaternion.identity;
			m_camera.transform.position = charicterMidpont;

			m_camera.transform.LookAt(m_charicters[0].transform.position);
			m_camera.transform.Rotate(0.0f, 90.0f, 0.0f);

			m_camera.transform.position -= m_camera.transform.forward * 15.0f;
			m_camera.transform.position += m_camera.transform.up * 4.0f;

			m_charicters[0].transform.rotation = m_camera.transform.rotation;
			m_charicters[1].transform.rotation = m_camera.transform.rotation;
			m_charicters[1].transform.Rotate(0.0f, 180f, 0.0f);
		}
	}

	public void Initialize(GameObject char1, GameObject char2, GameObject camera)
	{
		m_charicters[0] = char1;
		m_charicters[1] = char2;
		m_camera = camera;

		m_charicterData[0] = m_charicters[0].GetComponent<FighterData>();
		m_charicterData[1] = m_charicters[1].GetComponent<FighterData>();
	}

	private float GetDistance()
	{
		Vector3 char1Pos = m_charicters[0].transform.position;
		Vector3 char2Pos = m_charicters[1].transform.position;

		return Mathf.Sqrt(Mathf.Pow((char1Pos.x - char2Pos.x), 2.0f) + Mathf.Pow((char1Pos.z - char2Pos.z), 2.0f));
	}

	public bool CharicterDistanceCheck()
	{
		if (m_charicterDistanceCheckStart)
		{
			StartCharicterDistanceCheck();
			m_charicterDistanceCheckStart = false;
		}

		if (m_charictersClose)
		{
			m_charicterData[0].WalkBackward();
			m_charicterData[1].WalkBackward();

			if (GetDistance() < 10.0f)
			{
				return false;
			}
		}
		else if (m_charictersFar)
		{
			m_charicterData[0].WalkForward();
			m_charicterData[1].WalkForward();

			if (GetDistance() > 8.0f)
			{
				return false;
			}
		}

		m_charictersClose = false;
		m_charictersFar = false;
		m_charicterDistanceCheckStart = true;
		return true;
	}

	private void StartCharicterDistanceCheck()
	{
		float distance = GetDistance();

		if (distance > 10.0f)
		{
			m_charictersFar = true;
		}
		else if (distance < 8.0f)
		{
			m_charictersClose = true;
		}
	}
}
