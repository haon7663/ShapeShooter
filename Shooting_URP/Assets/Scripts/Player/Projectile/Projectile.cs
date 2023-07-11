using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;

    public float m_Speed;
    public float m_Damage;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke(nameof(ActiveDown), 1.5f);
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = transform.right * m_Speed;
    }

    private void ActiveDown()
    {
        gameObject.SetActive(false);
    }
}
