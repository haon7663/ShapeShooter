using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DrawPolygon))]
public class Health : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;

    public float maxhp;
    public float curhp;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
    }

    private void OnDamage(float dam)
    {
        curhp -= dam;
        if(curhp < 0)
        {
            curhp = maxhp;
            m_DrawPolygon.m_AngleCount--;
            if(m_DrawPolygon.m_AngleCount < 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
