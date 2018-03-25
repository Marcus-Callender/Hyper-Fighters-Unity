using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusUIManager : MonoBehaviour
{
    [SerializeField] private Text m_text;
    [SerializeField] private RectTransform m_healthBar;

    Timer m_timer;

    int m_maxFocus;
    int m_focus;
    int m_previousFocus;

    private float m_healthBarSize = 0.0f;

    void Start()
    {
        m_healthBarSize = m_healthBar.rect.width;

        m_timer = gameObject.AddComponent<Timer>();
        m_timer.Initialize(0.5f);
    }

    public void Init(int maxFocus)
    {
        m_maxFocus = maxFocus;
        m_focus = 0;
        m_previousFocus = m_focus;
    }

    public void gainFocus(int ammount)
    {
        if ((ammount + m_focus) > 100)
        {
            ammount = 100 - m_focus;
        }

        m_previousFocus = m_focus;
        m_focus += ammount;

        m_timer.Play();
    }

    void Update()
    {
        m_text.text = ("Focus: " + m_timer.I_Interpolate(m_previousFocus, m_focus) + "/" + m_maxFocus);
        m_healthBar.sizeDelta = new Vector2((m_timer.F_Interpolate(m_previousFocus, m_focus) / 100.0f) * m_healthBarSize, m_healthBar.rect.height);
        m_previousFocus = m_focus;
    }

    public void Rest()
    {
    }
}
