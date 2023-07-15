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

    public float m_Exp;
    private float needExp;

    public float m_PersentHp;
    public float m_PersentDamage;

    float expLerp;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_Health = GetComponent<Health>();
        m_Camera = Camera.main;
        needExp = m_DrawPolygon.m_AngleCount * 30 - 50;
    }

    private void LateUpdate()
    {
        m_PersentHp = (m_DrawPolygon.m_AngleCount - 3) * 0.1f + 1;
        m_PersentDamage = (m_DrawPolygon.m_AngleCount - 3) * 0.2f + 1;

        m_Health.maxhp = m_Health.defhp * m_PersentHp;

        needExp = m_DrawPolygon.m_AngleCount * 30 - 40;

        m_ExpImage.transform.position = m_Camera.WorldToScreenPoint(transform.position + new Vector3(-0.4f, -0.5f));
        expLerp = Mathf.Lerp(expLerp, m_Exp / needExp, Time.deltaTime * 25);
        m_ExpImage.fillAmount = expLerp;
    }

    public void AddExp(float exp)
    {
        m_Exp += exp;
        if(m_Exp >= needExp)
        {
            m_Exp -= needExp;
            m_DrawPolygon.m_AngleCount++;
            m_DrawPolygon.ChangeAngle();
            m_PersentHp = (m_DrawPolygon.m_AngleCount - 3) * 0.1f + 1;
            m_PersentDamage = (m_DrawPolygon.m_AngleCount - 3) * 0.25f + 1;

            m_Health.maxhp = m_Health.defhp * m_PersentHp;
            m_Health.curhp = m_Health.maxhp;
        }
        if (m_Exp >= needExp)
        {
            AddExp(0);
        }
    }
}
