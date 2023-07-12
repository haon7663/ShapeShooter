using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MoveType
{
    StraightMove, SideMove, Pinned, Follow
}

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private Transform m_EnemySprite;

    public MoveType m_MoveType;

    public LayerMask m_WallLayer;

    [Space]
    [Header("Ω∫≈»")]
    public float m_Speed;
    public float m_RotateSpeed = 0.4f;

    private Transform m_Player;
    bool isFollow;
    float x, y;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_EnemySprite = transform.GetChild(0);

        switch (m_MoveType.ToString())
        {
            case "StraightMove":
                x = -1;
                break;

            case "SideMove":
                break;

            case "Pinned":
                break;

            case "Follow":
                isFollow = true;
                break;
        }
    }
    private void FixedUpdate()
    {
        if(isFollow)
        {
            float angle = Mathf.Atan2(m_Player.position.y - transform.position.y, m_Player.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            m_Rigidbody2D.velocity = transform.right * m_Speed * Time.deltaTime;
        }
        else m_Rigidbody2D.velocity = new Vector2(x, y).normalized * m_Speed * Time.deltaTime;

        m_EnemySprite.Rotate(new Vector3(0, 0, (m_Rigidbody2D.velocity.x > 0 ? m_Rigidbody2D.velocity.x : -m_Rigidbody2D.velocity.x) +
                                               (m_Rigidbody2D.velocity.y > 0 ? m_Rigidbody2D.velocity.y : -m_Rigidbody2D.velocity.y)) * m_RotateSpeed);
    }
}
