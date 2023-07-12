using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Camera m_Camera;
    private Transform m_Player;
    public Transform[] m_BackGrounds;

    public int m_BackCount;

    void Start()
    {
        m_Camera = Camera.main;
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        m_BackCount = Mathf.CeilToInt(m_Player.position.x / 17.97f);
        for(int i = 0; i < m_BackGrounds.Length; i++)
        {
            m_BackGrounds[i].position = new Vector3(17.97f * (i - 1 + m_BackCount), 0);
        }
    }
}
