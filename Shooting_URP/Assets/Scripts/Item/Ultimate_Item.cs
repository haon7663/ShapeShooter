using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Item : Item
{
    private Rigidbody2D m_Rigidbody2D;
    private Ultimate m_Ultimate;

    private void Start()
    {
        m_Ultimate = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponentInChildren<Ultimate>();

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.AddForce(Vector2.left * 150);
    }
    public override void GetItem()
    {
        m_Ultimate.m_UltimateTime = 90;
        Destroy(gameObject);
    }
}
