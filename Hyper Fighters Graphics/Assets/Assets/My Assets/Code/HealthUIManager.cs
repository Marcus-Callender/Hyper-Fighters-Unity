using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private Text m_text;
    [SerializeField] private  RectTransform m_healthBar;
    private Image m_healthBarImage;

    Timer m_timer;

    int m_maxHp;
    int m_hp;
    int m_previousHP;

    private float m_healthBarSize = 0.0f;

    void Start()
    {
        m_healthBarSize = m_healthBar.rect.width;
        m_healthBarImage = m_healthBar.gameObject.GetComponent<Image>();

        m_timer = gameObject.AddComponent<Timer>();
        m_timer.Initialize(0.5f);
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
        if (m_previousHP > (m_maxHp / 2))
        {
            float lerpVal = ((float)m_previousHP - ((float)m_maxHp / 2.0f)) / ((float)m_maxHp / 2.0f);

            m_healthBarImage.color = Lerp(Color.yellow, Color.green, lerpVal);
        }
        else
        {
            float lerpVal = ((float)m_previousHP) / ((float)m_maxHp / 2.0f);

            m_healthBarImage.color = Lerp(Color.red, Color.yellow, lerpVal);
        }

        //float lerpVal = (float)m_previousHP / (float)m_maxHp;

        //m_healthBarImage.color = Color.Lerp(Color.red, Color.green, lerpVal);

        m_text.text = ("Health: " + /*m_timer.I_Interpolate(m_previousHP, m_hp)*/m_previousHP);
        m_healthBar.sizeDelta = new Vector2(m_timer.F_Interpolate(m_previousHP, m_hp) / m_maxHp * m_healthBarSize, m_healthBar.rect.height);
        m_previousHP = Mathf.RoundToInt(m_timer.F_Interpolate(m_previousHP, m_hp));
    }

    public void Rest()
    {
    }

    Color Lerp(Color a, Color b, float t)
    {
        if (t > 1.0f || t < 0.0f)
        {
            Debug.Log("Check lerp time value.");
        }

        t = Mathf.Clamp01(t);
        return new Color(
            a.r + (b.r - a.r) * t,
            a.g + (b.g - a.g) * t,
            a.b + (b.b - a.b) * t,
            a.a + (b.a - a.a) * t
        );
    }
}
