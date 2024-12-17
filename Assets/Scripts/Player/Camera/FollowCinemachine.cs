using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCinemachine : MonoBehaviour
{
    private Transform m_Player;

    public float m_PlusX;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        transform.position = new Vector3(m_Player.position.x + m_PlusX, 0);
    }
}
