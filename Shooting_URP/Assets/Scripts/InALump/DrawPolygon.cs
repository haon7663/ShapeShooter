using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class DrawPolygon : MonoBehaviour
{
    public LineRenderer m_LineRenderer;
    public LineRenderer m_FireLineRenderer;
    public LineRenderer m_LookLineRenderer;
    private PolygonCollider2D m_PolygonCollider2D;

    public int m_AngleCount;
    public float m_Size;
    public float hitTime;
    public bool isPlayer;

    Vector2[] saveLerp;
    Color saveStartColor, saveEndColor;
    float angle;

    [HideInInspector] public float size;

    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_PolygonCollider2D = GetComponent<PolygonCollider2D>();
        saveStartColor = m_LineRenderer.startColor;
        saveEndColor = m_LineRenderer.endColor;

        size = m_Size;
        ChangeAngle();

        if (m_FireLineRenderer != null)
        {
            for (int i = 0; i < 9; i++)
            {
                m_FireLineRenderer.SetPosition(i, m_FireLineRenderer.GetPosition(i) * size / 0.3f);
            }
        }
        if (m_LookLineRenderer != null)
        {
            for (int i = 0; i < 2; i++)
            {
                m_LookLineRenderer.SetPosition(i, m_LookLineRenderer.GetPosition(i) * size / 0.3f);
            }
        }
    }

    private void Update()
    {
        if (hitTime > 0)
        {
            m_LineRenderer.startColor = Color.red;
            m_LineRenderer.endColor = Color.red;
            hitTime -= Time.deltaTime;
        }
        else if (hitTime <= 0)
        {
            m_LineRenderer.startColor = saveStartColor;
            m_LineRenderer.endColor = saveEndColor;
        }

        int lastIndex = 0;
        for (var i = 0; i < m_AngleCount; i++)
        {
            saveLerp[i] = Vector2.Lerp(saveLerp[i], new Vector3(m_Size * Mathf.Sin(angle * i * Mathf.Deg2Rad), 
                                                                m_Size * Mathf.Cos(angle * i * Mathf.Deg2Rad)), Time.deltaTime * 9f);
            m_LineRenderer.SetPosition(i, saveLerp[i]); //라인렌더러 다각형 구현

            var myPoints = m_PolygonCollider2D.points;
            myPoints[i] = new Vector2(size * Mathf.Sin(angle * i * Mathf.Deg2Rad), 
                                      size * Mathf.Cos(angle * i * Mathf.Deg2Rad)) * (isPlayer ? 0.5f : 1f);
            m_PolygonCollider2D.points = myPoints; //폴리곤콜라이더 다각형 구현
            lastIndex++;
        }
        var lastPoints = m_PolygonCollider2D.points;
        lastPoints[lastIndex] = new Vector2(0, size) * (isPlayer ? 0.5f : 1f);
        m_PolygonCollider2D.points = lastPoints;

        m_Size = Mathf.Lerp(m_Size, size, Time.deltaTime * 50);
    }

    public void OnDamage()
    {
        hitTime = 0.05f;
        if(m_Size < size * 1.2f + 1.8f) m_Size = size * 1.3f + 0.95f;
    }
    public void ChangeAngle()
    {
        angle = 360 / m_AngleCount;
        var myPoints = m_PolygonCollider2D.points;
        m_PolygonCollider2D.points = myPoints;
        m_LineRenderer.positionCount = m_AngleCount;
        Array.Resize(ref saveLerp, m_AngleCount);

        m_Size = size * 5f + 2f;
    }
}
