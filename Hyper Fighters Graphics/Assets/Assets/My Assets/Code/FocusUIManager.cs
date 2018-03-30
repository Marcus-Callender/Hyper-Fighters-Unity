using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusUIManager : MonoBehaviour
{
    [SerializeField] private Text m_text;
    [SerializeField] private RectTransform m_focusBar;
    private Image m_focusBarImage;

    Timer m_timer;

    int m_maxFocus;
    int m_focus;
    int m_previousFocus;

    private float m_focusBarSize = 0.0f;

    void Start()
    {
        m_focusBarSize = m_focusBar.rect.width;
        m_focusBarImage = m_focusBar.gameObject.GetComponent<Image>();

        m_timer = gameObject.AddComponent<Timer>();
        m_timer.Initialize(0.5f);
    }

    public void Init(int maxFocus)
    {
        m_maxFocus = maxFocus;
        m_focus = 0;
        m_previousFocus = m_focus;
    }

    public void GainFocus(int ammount)
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
        float lerpVal = ((float)m_previousFocus) / ((float)m_maxFocus / 2.0f);
        m_focusBarImage.color = Lerp(Color.blue, Color.red, lerpVal);

        m_text.text = ("Focus: " + m_timer.I_Interpolate(m_previousFocus, m_focus) + "/" + m_maxFocus);
        m_focusBar.sizeDelta = new Vector2((m_timer.F_Interpolate(m_previousFocus, m_focus) / 100.0f) * m_focusBarSize, m_focusBar.rect.height);
        m_previousFocus = m_focus;
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
