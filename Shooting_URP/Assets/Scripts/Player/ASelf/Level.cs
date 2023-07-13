using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Camera m_Camera;

    public Image m_ExpImage;

    public float m_Exp;
    private float needExp;

    float expLerp;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_Camera = Camera.main;
        needExp = m_DrawPolygon.m_AngleCount * 30 - 50;
    }

    private void LateUpdate()
    {
        needExp = m_DrawPolygon.m_AngleCount * 30 - 50;
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
        }
        if (m_Exp >= needExp)
        {
            AddExp(0);
        }
    }
}
