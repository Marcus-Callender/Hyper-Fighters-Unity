using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject m_charicterPrefab;

	private GameObject[] m_charicters = new GameObject[2];
	private CameraManager m_camera;

	// Use this for initialization
	void Start()
	{
		m_camera = FindObjectOfType<CameraManager>();

		m_charicters[0] = Instantiate(m_charicterPrefab);
		m_charicters[1] = Instantiate(m_charicterPrefab);

		m_charicters[0].transform.position = new Vector3(10.0f, 0.0f, 10.0f);
		m_charicters[1].transform.position = new Vector3(12.0f, 0.0f, 16.0f);

		//m_charicters[0].transform.rotation.
		//m_charicters[0].transform.rotation.

		m_camera.Initialize(m_charicters[0], m_charicters[1]);

		GetComponent<PositionManager>().Initialize(m_charicters[0], m_charicters[1], m_camera.gameObject);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
