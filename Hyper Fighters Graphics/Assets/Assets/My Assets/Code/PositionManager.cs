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

		m_camera.transform.rotation = Quaternion.identity;
		m_camera.transform.position = charicterMidpont;

		//m_camera.transform.LookAt(m_charicters[0].transform.position);
		//Quaternion cam1stRotation = m_camera.transform.rotation;
		//m_camera.transform.LookAt(m_charicters[1].transform.position);
		//Quaternion cam2ndRotation = m_camera.transform.rotation;
		//m_camera.transform.rotation = new Quaternion((cam1stRotation.x + cam2ndRotation.x) * 0.5f, (cam1stRotation.y + cam2ndRotation.y) * 0.5f, (cam1stRotation.z + cam2ndRotation.z) * 0.5f, (cam1stRotation.w + cam2ndRotation.w) * 0.5f);
		//m_camera.transform.rotation = Quaternion.Inverse(m_camera.transform.rotation);

		float newAngle = Vector3.Angle(Vector3.zero, charicterMidpont);
		//m_camera.transform.Rotate(new Vector3(0.0f, newAngle, 0.0f));

		m_camera.transform.LookAt(m_charicters[0].transform.position);
		float angle1 = m_camera.transform.rotation.y;
		m_camera.transform.Rotate(0.0f, 90.0f, 0.0f);
		//m_camera.transform.LookAt(m_charicters[1].transform.position);
		//float angle2 = m_camera.transform.rotation.y;
		//m_camera.transform.rotation = Quaternion.identity;
		//m_camera.transform.Rotate(0.0f, (angle1 + angle2) * 0.5f, 0.0f);

		//m_camera.transform.localPosition.Set(m_camera.transform.localPosition.x, m_camera.transform.localPosition.y, m_camera.transform.localPosition.z - 80.0f);
		m_camera.transform.position -= m_camera.transform.forward * 20.0f;

		//m_charicters[0].transform.rotation = Quaternion.Inverse(m_camera.transform.rotation);
		//m_charicters[1].transform.rotation = Quaternion.Inverse(m_camera.transform.rotation);

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
