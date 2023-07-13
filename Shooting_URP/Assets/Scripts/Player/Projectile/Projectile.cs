using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private Transform m_Line;

    public float m_Speed;
    public float m_Damage;
    public float m_RotateSpeed;

    public bool isPlayer;
    public bool isPenetrate;
    public int m_PenetrateCount;
    private int penetrateCount;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Line = transform.GetChild(0);
    }

    private void OnEnable()
    {
        penetrateCount = m_PenetrateCount;
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = transform.right * m_Speed * Time.deltaTime;
        m_Line.Rotate(new Vector3(0, 0, (m_Rigidbody2D.velocity.x > 0 ? m_Rigidbody2D.velocity.x : -m_Rigidbody2D.velocity.x) +
                                           (m_Rigidbody2D.velocity.y > 0 ? m_Rigidbody2D.velocity.y : -m_Rigidbody2D.velocity.y)) * m_RotateSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayer && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().OnDamage(m_Damage);
            if(isPenetrate && penetrateCount > 0)
            {
                penetrateCount--;
            }
            else
            {
                ActiveDown();
            }
        }
        else if (!isPlayer && collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().OnDamage(m_Damage);
            ActiveDown();
        }
        else if (collision.CompareTag("DestroyProjectile") || collision.CompareTag("Death"))
        {
            ActiveDown();
        }
    }
    private void ActiveDown()
    {
        gameObject.SetActive(false);
    }
}
