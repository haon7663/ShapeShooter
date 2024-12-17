using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Health m_Health;
    private Camera m_Camera;

    public Image m_ExpImage;
    public GameObject m_LevelUp;
    public GameObject m_Canvas;

    public float m_Exp;
    public float m_NeedExp;

    public float m_PersentHp;
    public float m_PersentDamage;

    float expLerp;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_Health = GetComponent<Health>();
        m_Camera = Camera.main;
        m_NeedExp = m_DrawPolygon.m_AngleCount * 40 - 50;

        m_PersentHp = (m_DrawPolygon.m_AngleCount - 3) * 0.1f + 1;
        m_PersentDamage = (m_DrawPolygon.m_AngleCount - 3) * 0.2f + 1;
        m_Health.maxhp = m_Health.defhp * m_PersentHp;
        m_Health.curhp = m_Health.maxhp;
    }

    private void LateUpdate()
    {
        m_PersentHp = (m_DrawPolygon.m_AngleCount - 3) * 0.1f + 1;
        m_PersentDamage = (m_DrawPolygon.m_AngleCount - 3) * 0.2f + 1;

        m_Health.maxhp = m_Health.defhp * m_PersentHp;
        if (m_Health.curhp > m_Health.maxhp) m_Health.curhp = m_Health.maxhp;

        m_NeedExp = m_DrawPolygon.m_AngleCount * 40 - 50;

        expLerp = Mathf.Lerp(expLerp, m_Exp / m_NeedExp, Time.deltaTime * 25);
        m_ExpImage.fillAmount = expLerp;
    }

    public void AddExp(float exp)
    {
        m_Exp += exp;
        if(m_Exp >= m_NeedExp)
        {
            var levUp = Instantiate(m_LevelUp, m_Camera.WorldToScreenPoint(transform.position), Quaternion.identity);
            levUp.transform.SetParent(m_Canvas.transform);

            m_Exp -= m_NeedExp;
            m_DrawPolygon.m_AngleCount++;
            m_DrawPolygon.ChangeAngle();
            m_PersentHp = (m_DrawPolygon.m_AngleCount - 3) * 0.1f + 1;
            m_PersentDamage = (m_DrawPolygon.m_AngleCount - 3) * 0.25f + 1;

            m_Health.maxhp = m_Health.defhp * m_PersentHp;
            m_Health.curhp = m_Health.maxhp;
        }
        if (m_Exp >= m_NeedExp)
        {
            AddExp(0);
        }
        if (m_Health.curhp > m_Health.maxhp) m_Health.curhp = m_Health.maxhp;
    }
}
