using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Item : Item
{
    private FireProjectile m_FirePorjectile;
    private AudioSource m_AudioSource;
    private SpriteRenderer m_SpriteRenderer;
    private CircleCollider2D m_CircleCollider2D;
    private Rigidbody2D m_Rigidbody2D;
    public Sprite[] m_Sprite;

    public AttackType m_AttackType;
    int type;

    private void Awake()
    {
        m_FirePorjectile = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponentInChildren<FireProjectile>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_CircleCollider2D = GetComponent<CircleCollider2D>();
        m_AudioSource = GetComponent<AudioSource>();

        if (m_AttackType.ToString() == "Default")
            type = 0;
        else if (m_AttackType.ToString() == "Arrow")
            type = 1;
        else if (m_AttackType.ToString() == "Double")
            type = 2;
        else if (m_AttackType.ToString() == "Shot")
            type = 3;

        m_SpriteRenderer.sprite = m_Sprite[type];

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.AddForce(Vector2.left * 150);
    }
    public override void GetItem()
    {
        m_FirePorjectile.SetWeapon(m_AttackType);
        m_CircleCollider2D.enabled = false;
        m_SpriteRenderer.enabled = false;
        m_AudioSource.Play();
        Destroy(gameObject, 0.6f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }
}
