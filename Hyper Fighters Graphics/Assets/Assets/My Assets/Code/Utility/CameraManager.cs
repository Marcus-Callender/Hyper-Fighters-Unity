using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private GameObject[] m_charicters = new GameObject[2];
	
	void Start()
	{

	}
	
	void Update()
	{
		//Vector3 charicterMidpint = (m_charicters[0].transform.position + m_charicters[1].transform.position) * 0.5f;
		//Quaternion CharicterRotation = m_charicters[0].transform.rotation;

		//this.transform.position = charicterMidpint;
		//this.transform.rotation = Quaternion.Inverse(CharicterRotation);
		//this.transform.localPosition.Set(this.transform.localPosition.x, this.transform.localPosition.y - 20.0f, this.transform.localPosition.z);
	}

	public void Initialize(GameObject char1, GameObject char2)
	{
		m_charicters[0] = char1;
		m_charicters[1] = char2;
	} 
}
