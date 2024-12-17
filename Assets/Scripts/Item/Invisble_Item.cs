using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisble_Item : Item
{
    private Ultimate m_Ultimate;
    private Rigidbody2D m_Rigidbody2D;
    private void Start()
    {
        m_Ultimate = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponentInChildren<Ultimate>();

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.AddForce(Vector2.left * 150);
    }
    public override void GetItem()
    {
        if(m_Ultimate.m_InvisbleTime < 5) m_Ultimate.m_InvisbleTime = 5;
        Destroy(gameObject);
    }
}
