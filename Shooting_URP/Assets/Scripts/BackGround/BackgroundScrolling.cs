using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public Transform[] m_BackGrounds;
    
    public Color[] m_Color;
    public int m_BackCount;

    public bool isMain;

    public float m_Speed;
    public float leftPosX = 0f;
    public float rightPosX = 0f;
    private void Start()
    {
        if(!isMain)
        {
            for (int i = 0; i < m_BackGrounds.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_BackGrounds[i].GetChild(j).GetComponent<LineRenderer>().startColor = m_Color[GameManager.instance.m_StageCount];
                    m_BackGrounds[i].GetChild(j).GetComponent<LineRenderer>().endColor = m_Color[GameManager.instance.m_StageCount];
                }
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < m_BackGrounds.Length; i++)
        {
            m_BackGrounds[i].position += new Vector3(-m_Speed, 0, 0) * Time.deltaTime;

            if (m_BackGrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = m_BackGrounds[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                m_BackGrounds[i].position = nextPos;
            }
        }
    }
}
