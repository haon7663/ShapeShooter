using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;

    public float maxhp;
    public float curhp;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
    }

    public void OnDamage(float dam)
    {
        curhp -= dam;
        m_DrawPolygon.OnDamage();
        if (curhp < 0)
        {
            curhp = maxhp;
            m_DrawPolygon.m_AngleCount--;
            m_DrawPolygon.ChangeAngle();
            if (m_DrawPolygon.m_AngleCount < 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
