using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private Text m_text;
    [SerializeField] private  RectTransform m_healthBar;

    Timer m_timer;

    int m_maxHp;
    int m_hp;
    int m_previousHP;

    private float m_healthBarSize = 0.0f;

    void Start()
    {
        m_healthBarSize = m_healthBar.rect.width;

        m_timer = gameObject.AddComponent<Timer>();
        m_timer.Initialize(2.0f);
    }

    public void Init(int maxHp)
    {
        m_maxHp = maxHp;
        m_hp = maxHp;
        m_previousHP = m_hp;
    }

    public void takeDamage(int ammount)
    {
        if (ammount > m_hp)
        {
            ammount = m_hp;
        }

        m_previousHP = m_hp;
        m_hp -= ammount;

        m_timer.Play();
    }

    void Update()
    {
        m_text.text = ("Health: " + m_timer.Interpolate(m_previousHP, m_hp));
        m_healthBar.sizeDelta = new Vector2(((float)m_timer.Interpolate(m_previousHP, m_hp) / m_maxHp) * m_healthBarSize, m_healthBar.rect.height);
    }

    public void Rest()
    {
        m_previousHP = m_hp;
    }
}
