using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MoveType
{
    StraightMove, SideMove, Pinned, Follow, Kinematic
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

    [HideInInspector] public Transform m_Follow;
    [HideInInspector] public Vector3 m_PinnedPos;
    [HideInInspector] public Vector3 m_FirstPinnedPos;
    [HideInInspector] public Vector3 m_KineticPos;
    [HideInInspector] public bool onFollow, onPinned;
    [HideInInspector] public bool isPinned, isKinetic, isSideMove;

    private Transform m_Player;
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
        m_PinnedPos = new Vector3(transform.position.x - 15, transform.position.y);
        m_FirstPinnedPos = m_PinnedPos;

        x = 0; y = 0;
        onFollow = false; onPinned = false; isKinetic = false;
        switch (m_MoveType.ToString())
        {
            case "StraightMove":
                x = -1;
                break;

            case "SideMove":
                isSideMove = true;
                x = -1;
                y = transform.position.y < 0 ? -1 : 1;
                break;

            case "Pinned":
                x = -1;
                onPinned = true;
                break;

            case "Follow":
                onFollow = true;
                break;

            case "Kinematic":
                isKinetic = true;
                break;
        }
    }
    private void FixedUpdate()
    {
        if (isKinetic)
        {
            transform.position = Vector3.Lerp(transform.position, m_Follow.position + m_KineticPos, Time.deltaTime * 4f);
            m_EnemySprite.Rotate(new Vector3(0, 0, 0.6f * m_RotateSpeed));
        }
        else if (isPinned)
        {
            transform.position = Vector3.Lerp(transform.position, m_PinnedPos, Time.deltaTime * m_Speed / 100);
            m_EnemySprite.Rotate(new Vector3(0, 0, 0.6f * m_RotateSpeed));
        }
        else if (onFollow)
        {
            float angle = Mathf.Atan2(m_Player.position.y - transform.position.y, m_Player.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            m_Rigidbody2D.velocity = transform.right * m_Speed * Time.deltaTime;
            transform.rotation = Quaternion.identity;
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(x, y).normalized * m_Speed * Time.deltaTime;
            if (onPinned && m_PinnedPos.x > transform.position.x)
            {
                x = 0;
                isPinned = true;
            }
            else if (isSideMove)
            {
                var wallhit = Physics2D.OverlapCircle(transform.position + new Vector3(0, y*0.25f), 0.3f, m_WallLayer);
                if (wallhit)
                {
                    y *= -1;
                    transform.position += new Vector3(0, y * 0.05f);
                }
            }
        }

        m_EnemySprite.Rotate(new Vector3(0, 0, (m_Rigidbody2D.velocity.x > 0 ? m_Rigidbody2D.velocity.x : -m_Rigidbody2D.velocity.x) +
                                               (m_Rigidbody2D.velocity.y > 0 ? m_Rigidbody2D.velocity.y : -m_Rigidbody2D.velocity.y)) * m_RotateSpeed);
    }
}
