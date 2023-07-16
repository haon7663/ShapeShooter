using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public TrailRenderer m_TrailRenderer;
    [HideInInspector] public Transform line;

    [HideInInspector] public bool isTrail;
    [HideInInspector] public bool isPlayer;
    [HideInInspector] public float speed;
    [HideInInspector] public float realDamage;
    [HideInInspector] public int penetrateCount;

    public float rotateSpeed;
    private Collider2D collisionCalled;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (!line) line = transform.GetChild(0);
    }
    private void OnEnable()
    {
        collisionCalled = null;
        if (isTrail)
        {
            m_TrailRenderer.enabled = false;
            Invoke(nameof(OnTrail), m_TrailRenderer.time + 0.01f);
        }
    }
    private void OnTrail()
    {
        m_TrailRenderer.enabled = true;
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = transform.right * speed * Time.deltaTime;
        line.Rotate(new Vector3(0, 0, (m_Rigidbody2D.velocity.x > 0 ? m_Rigidbody2D.velocity.x : -m_Rigidbody2D.velocity.x) +
                                      (m_Rigidbody2D.velocity.y > 0 ? m_Rigidbody2D.velocity.y : -m_Rigidbody2D.velocity.y)) * rotateSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayer && collision.CompareTag("Enemy") && collisionCalled != collision)
        {
            collision.GetComponent<Health>().OnDamage(realDamage);
            if(penetrateCount > 0)
            {
                collisionCalled = collision;
                realDamage *= 0.5f;
                penetrateCount--;
            }
            else
            {
                ActiveDown();
            }
        }
        else if (!isPlayer && collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().OnDamage(realDamage);
            ActiveDown();
        }
        else if (collision.CompareTag("DestroyProjectile") || collision.CompareTag("Death") || (collision.CompareTag("Ultimate") && !isPlayer))
        {
            ActiveDown();
        }
    }
    private void ActiveDown()
    {
        gameObject.SetActive(false);
    }
}
