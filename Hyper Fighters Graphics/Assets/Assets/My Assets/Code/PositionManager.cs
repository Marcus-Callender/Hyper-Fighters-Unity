using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
	private GameObject[] m_charicters = new GameObject[2];
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

		m_camera.transform.position -= m_camera.transform.forward * 20.0f;
		m_camera.transform.position += m_camera.transform.up * 2.0f;

		m_charicters[0].transform.rotation = m_camera.transform.rotation;
		m_charicters[1].transform.rotation = m_camera.transform.rotation;
		m_charicters[1].transform.Rotate(0.0f, 180f, 0.0f);
	}

	public void Initialize(GameObject char1, GameObject char2, GameObject camera)
	{
		m_charicters[0] = char1;
		m_charicters[1] = char2;
		m_camera = camera;
	}
}
