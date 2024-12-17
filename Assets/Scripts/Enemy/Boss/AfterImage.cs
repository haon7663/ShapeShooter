using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private LineRenderer m_LineRenderer;

    public int m_AngleCount;
    public float m_Size;

    public Vector2[] saveLerp;
    float angleCount;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return YieldInstructionCache.WaitForFixedUpdate;
        m_LineRenderer.positionCount = m_AngleCount;
        angleCount = 360 / m_AngleCount;
        Array.Resize(ref saveLerp, m_AngleCount);

        int lastIndex = 0;
        for (int j = 0; j < m_AngleCount; j++)
        {
            saveLerp[j] = Vector2.Lerp(saveLerp[j], new Vector3(m_Size * Mathf.Sin(angleCount * j * Mathf.Deg2Rad), m_Size * Mathf.Cos(angleCount * j * Mathf.Deg2Rad)), Time.deltaTime * 9f);
            m_LineRenderer.SetPosition(j, saveLerp[j]);
            lastIndex++;
        }
        m_LineRenderer.startColor = Color.red;
        m_LineRenderer.endColor = Color.red;

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            m_LineRenderer.startColor -= new Color(0, 0, 0, Time.deltaTime);
            m_LineRenderer.endColor -= new Color(0, 0, 0, Time.deltaTime);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
