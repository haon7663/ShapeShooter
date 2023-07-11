using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;

    public float m_Speed;
    public float m_Damage;

    public bool isPlayer;
    public bool isPenetrate;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = transform.right * m_Speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayer && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().OnDamage(m_Damage);
            if(!isPenetrate)
            {
                ActiveDown();
            }
        }
        else if (!isPlayer && collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().OnDamage(m_Damage);
            ActiveDown();
        }
        else if (collision.CompareTag("DestroyProjectile"))
        {
            ActiveDown();
        }
    }
    private void ActiveDown()
    {
        gameObject.SetActive(false);
    }
}
