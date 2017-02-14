using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
	private GameObject[] m_charicters = new GameObject[2];
	private GameObject m_camera;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 charicterMidpont = (m_charicters[0].transform.position + m_charicters[1].transform.position) * 0.5f;

		m_camera.transform.position = charicterMidpont;

		m_camera.transform.LookAt(m_charicters[0].transform.position);
		Quaternion cam1stRotation = m_camera.transform.rotation;
		m_camera.transform.LookAt(m_charicters[1].transform.position);
		Quaternion cam2ndRotation = m_camera.transform.rotation;
		m_camera.transform.rotation = new Quaternion((cam1stRotation.x + cam2ndRotation.x) * 0.5f, (cam1stRotation.y + cam2ndRotation.y) * 0.5f, (cam1stRotation.z + cam2ndRotation.z) * 0.5f, (cam1stRotation.w + cam2ndRotation.w) * 0.5f);
		m_camera.transform.rotation = Quaternion.Inverse(m_camera.transform.rotation);

		m_camera.transform.localPosition.Set(m_camera.transform.localPosition.x, m_camera.transform.localPosition.y - 80.0f, m_camera.transform.localPosition.z);

		m_charicters[0].transform.rotation = Quaternion.Inverse(m_camera.transform.rotation);
		m_charicters[1].transform.rotation = Quaternion.Inverse(m_camera.transform.rotation);
	}

	public void Initialize(GameObject char1, GameObject char2, GameObject camera)
	{
		m_charicters[0] = char1;
		m_charicters[1] = char2;
		m_camera = camera;
	}
}
