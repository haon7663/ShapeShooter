using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Transform m_Player;
    public Transform[] m_BackGrounds;

    public Color[] m_Color;

    public int m_BackCount;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < m_BackGrounds.Length; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                m_BackGrounds[i].GetChild(j).GetComponent<LineRenderer>().startColor = m_Color[GameManager.instance.m_StageCount];
                m_BackGrounds[i].GetChild(j).GetComponent<LineRenderer>().endColor = m_Color[GameManager.instance.m_StageCount];
            }
        }
    }

    private void Update()
    {
        m_BackCount = Mathf.CeilToInt(m_Player.position.x / 17.97f);
        for(int i = 0; i < m_BackGrounds.Length; i++)
        {
            m_BackGrounds[i].position = new Vector3(17.97f * (i - 1 + m_BackCount), 0);
        }
    }
}
