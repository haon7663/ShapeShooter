using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Explosion : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private PolygonCollider2D m_PolygonCollider2D;

    public Transform m_Follow;
    public int m_AngleCount;
    public float m_Size;

    Vector2[] saveLerp;
    float angle;

    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_PolygonCollider2D = GetComponent<PolygonCollider2D>();

        ChangeAngle();
        StartCoroutine(SizeUp());
    }

    public void ChangeAngle()
    {
        angle = 360 / m_AngleCount;
        var myPoints = m_PolygonCollider2D.points;
        m_PolygonCollider2D.points = myPoints;
        m_LineRenderer.positionCount = m_AngleCount;
        Array.Resize(ref saveLerp, m_AngleCount);
    }

    private IEnumerator SizeUp()
    {
        yield return YieldInstructionCache.WaitForFixedUpdate;
        for (float i = 0; i < 3f; i += Time.deltaTime)
        {
            transform.position = m_Follow.position;
            int lastIndex = 0;
            for (var j = 0; j < m_AngleCount; j++)
            {
                saveLerp[j] = Vector2.Lerp(saveLerp[j], new Vector3(m_Size * Mathf.Sin(angle * j * Mathf.Deg2Rad), m_Size * Mathf.Cos(angle * j * Mathf.Deg2Rad)), Time.deltaTime * 9f);
                m_LineRenderer.SetPosition(j, saveLerp[j]);

                var myPoints = m_PolygonCollider2D.points;
                myPoints[j] = new Vector3(m_Size * Mathf.Sin(angle * j * Mathf.Deg2Rad), m_Size * Mathf.Cos(angle * j * Mathf.Deg2Rad)) * 0.95f;
                m_PolygonCollider2D.points = myPoints;

                lastIndex++;
            }
            var lastPoints = m_PolygonCollider2D.points;
            lastPoints[lastIndex] = new Vector2(0, m_Size) * 0.95f;
            m_PolygonCollider2D.points = lastPoints;

            transform.Rotate(new Vector3(0, 0, (15 - m_Size) * 7 * Time.deltaTime));
            m_Size = Mathf.Lerp(m_Size, 15, Time.deltaTime * 1);
            if (i > 2f)
            {
                m_LineRenderer.startColor = Color.Lerp(m_LineRenderer.startColor, new Color(1, 1, 1, -0.1f), Time.deltaTime * 7);
                m_LineRenderer.endColor = Color.Lerp(m_LineRenderer.endColor, new Color(1, 1, 1, -0.1f), Time.deltaTime * 7);
                m_PolygonCollider2D.enabled = false;
            }
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        Destroy(gameObject);
    }
}
