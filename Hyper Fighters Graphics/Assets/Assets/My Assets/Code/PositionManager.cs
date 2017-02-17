using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
	private GameObject[] m_charicters = new GameObject[2];
	private FighterData[] m_charicterData = new FighterData[2];
	private GameObject m_camera;
	
	void Start()
	{

	}
	
	void Update()
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

	public void Initialize(GameObject char1, GameObject char2, GameObject camera)
	{
		m_charicters[0] = char1;
		m_charicters[1] = char2;
		m_camera = camera;

		m_charicterData[0] = m_charicters[0].GetComponent<FighterData>();
		m_charicterData[1] = m_charicters[1].GetComponent<FighterData>();
	}

	public bool ResetCharicterDistance()
	{
		Vector3 char1Pos = m_charicters[0].transform.position;
		Vector3 char2Pos = m_charicters[1].transform.position;

		float distance = Mathf.Sqrt(Mathf.Pow((char1Pos.x - char2Pos.x), 2.0f) + Mathf.Pow((char1Pos.z - char2Pos.z), 2.0f));

		if (distance > 12.0f)
		{
			m_charicterData[0].WalkForward();
			m_charicterData[1].WalkForward();

			return false;
		}
		else if (distance < 8.0f)
		{
			m_charicterData[0].WalkBackward();
			m_charicterData[1].WalkBackward();

			return false;
		}

		return true;
	}
}
