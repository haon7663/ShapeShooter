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
    private Camera m_Camera;

    [Space]
    [Header("Ω∫≈»")]
    public float m_Speed;
    public float m_RotateSpeed = 0.4f;

    private float m_PinnedX;
    private float m_PinnedY;

    private Transform m_Player;
    bool onFollow, onPinned;
    bool isPinned;
    float x, y;

    private void Awake()
    {
        m_Camera = Camera.main;
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        m_EnemySprite = transform.GetChild(0);
        m_PinnedX = -m_Camera.transform.position.x + transform.position.x - 10;
        m_PinnedY = transform.position.y;

        x = 0; y = 0;
        onFollow = false; onPinned = false;
        switch (m_MoveType.ToString())
        {
            case "StraightMove":
                x = -1;
                break;

            case "SideMove":
                break;

            case "Pinned":
                x = -1;
                onPinned = true;
                break;

            case "Follow":
                onFollow = true;
                break;
        }
    }
    private void FixedUpdate()
    {
        if (isPinned)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(m_Player.transform.position.x + m_PinnedX, m_PinnedY), Time.deltaTime * 1.2f);
            m_EnemySprite.Rotate(new Vector3(0, 0, 0.6f * m_RotateSpeed));
        }
        else if (onFollow)
        {
            float angle = Mathf.Atan2(m_Player.position.y - transform.position.y, m_Player.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            m_Rigidbody2D.velocity = transform.right * m_Speed * Time.deltaTime;
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(x, y).normalized * m_Speed * Time.deltaTime;
            if (onPinned && m_Player.transform.position.x + m_PinnedX > transform.position.x)
            {
                x = 0;
                isPinned = true;
            }
        }

        m_EnemySprite.Rotate(new Vector3(0, 0, (m_Rigidbody2D.velocity.x > 0 ? m_Rigidbody2D.velocity.x : -m_Rigidbody2D.velocity.x) +
                                               (m_Rigidbody2D.velocity.y > 0 ? m_Rigidbody2D.velocity.y : -m_Rigidbody2D.velocity.y)) * m_RotateSpeed);
    }
}
