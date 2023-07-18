using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Item : Item
{
    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_Rigidbody2D;
    private Health m_Health;

    public int m_Count;
    public Sprite[] m_HealthSprite;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        m_SpriteRenderer.sprite = m_HealthSprite[m_Count];

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.AddForce(Vector2.left * 150);
    }
    public override void GetItem()
    {
        m_Health.curhp += m_Count == 0 ? m_Health.maxhp * 0.2f : m_Health.maxhp * 1;
        Destroy(gameObject);
    }
}
