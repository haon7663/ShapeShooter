using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison : MonoBehaviour
{
    private Health m_Health;

    private void Start()
    {
        m_Health = GetComponent<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            collision.GetComponent<Item>().GetItem();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            m_Health.OnDamage(5);
        }
    }
}
