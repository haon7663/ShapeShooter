using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPath : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private LineRenderer m_LineRendererChild;

    public Transform m_Follow;
    public int m_AngleCount;
    public float m_Size;
    public float m_Time;

    public Vector2[] saveLerp;
    float angle, size;

    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRendererChild = transform.GetChild(0).GetComponent<LineRenderer>();
        StartCoroutine(ShowAngle());
    }

    private IEnumerator ShowAngle()
    {
        m_LineRenderer.positionCount = m_AngleCount;
        size = m_Size;
        m_Size = 0;
        angle = 360 / m_AngleCount;
        Array.Resize(ref saveLerp, m_AngleCount);

        for (float i = 0; i < m_Time; i += Time.deltaTime)
        {
            int lastIndex = 0;
            for (int j = 0; j < m_AngleCount; j++)
            {
                saveLerp[j] = Vector2.Lerp(saveLerp[j], new Vector3(m_Size * Mathf.Sin(angle * j * Mathf.Deg2Rad), m_Size * Mathf.Cos(angle * j * Mathf.Deg2Rad)), Time.deltaTime * 9f);
                m_LineRenderer.SetPosition(j, saveLerp[j]);
                lastIndex++;
            }
            m_Size = Mathf.Lerp(m_Size, size, Time.deltaTime * 4);

            m_LineRendererChild.SetPosition(0, Vector3.zero);
            m_LineRendererChild.SetPosition(1, m_Follow.position - transform.position);

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }

        while (m_LineRenderer.startColor.a > 0.01f)
        {
            int lastIndex = 0;
            for (int j = 0; j < m_AngleCount; j++)
            {
                saveLerp[j] = Vector2.Lerp(saveLerp[j], new Vector3(m_Size * Mathf.Sin(angle * j * Mathf.Deg2Rad), m_Size * Mathf.Cos(angle * j * Mathf.Deg2Rad)), Time.deltaTime * 9f);
                m_LineRenderer.SetPosition(j, saveLerp[j]);
                lastIndex++;
            }
            m_Size = Mathf.Lerp(m_Size, 0, Time.deltaTime * 4);

            m_LineRenderer.startColor = Color.Lerp(m_LineRenderer.startColor, new Color(0, 0, 0, 0), Time.deltaTime * 5);
            m_LineRenderer.endColor = Color.Lerp(m_LineRenderer.endColor, new Color(0, 0, 0, 0), Time.deltaTime * 5);
            m_LineRendererChild.startColor = Color.Lerp(m_LineRendererChild.startColor, new Color(0, 0, 0, 0), Time.deltaTime * 5);
            m_LineRendererChild.endColor = Color.Lerp(m_LineRendererChild.endColor, new Color(0, 0, 0, 0), Time.deltaTime * 5);

            m_LineRendererChild.SetPosition(0, Vector3.zero);
            m_LineRendererChild.SetPosition(1, m_Follow.position - transform.position);

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        Destroy(gameObject);
    }
}
