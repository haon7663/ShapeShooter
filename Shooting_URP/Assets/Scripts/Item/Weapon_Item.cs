using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Item : Item
{
    private FireProjectile m_FirePorjectile;
    private SpriteRenderer m_SpriteRenderer;
    public Sprite[] m_Sprite;

    public AttackType m_AttackType;
    int type;

    private void Awake()
    {
        m_FirePorjectile = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponentInChildren<FireProjectile>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        if (m_AttackType.ToString() == "Default")
            type = 0;
        else if (m_AttackType.ToString() == "Arrow")
            type = 1;
        else if (m_AttackType.ToString() == "Double")
            type = 2;
        else if (m_AttackType.ToString() == "Shot")
            type = 3;

        m_SpriteRenderer.sprite = m_Sprite[type];
    }
    public override void GetItem()
    {
        m_FirePorjectile.SetWeapon(m_AttackType);
        Destroy(gameObject);
    }
}
