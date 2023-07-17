using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Item : Item
{
    private Ultimate m_Ultimate;

    private void Start()
    {
        m_Ultimate = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponentInChildren<Ultimate>();
    }
    public override void GetItem()
    {
        m_Ultimate.m_UltimateTime = 90;
        Destroy(gameObject);
    }
}
