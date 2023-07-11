using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class DrawPolygon : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private PolygonCollider2D m_PolygonCollider2D;

    public int m_AngleCount;
    public float m_Size;

    float angle;

    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_PolygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        angle = 360 / m_AngleCount;
        m_LineRenderer.positionCount = m_AngleCount;
        for (var i = 0; i < m_AngleCount; i++)
        {
            m_LineRenderer.SetPosition(i, new Vector3(m_Size * Mathf.Sin(angle * i * Mathf.Deg2Rad), m_Size * Mathf.Cos(angle * i * Mathf.Deg2Rad)));
            var myPoints = m_PolygonCollider2D.points;
            myPoints[i] = new Vector3(m_Size * Mathf.Sin(angle * i * Mathf.Deg2Rad), m_Size * Mathf.Cos(angle * i * Mathf.Deg2Rad));
            m_PolygonCollider2D.points = myPoints;
        }
    }
}
