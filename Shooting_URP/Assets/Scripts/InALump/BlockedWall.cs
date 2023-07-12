using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedWall : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;

    public LayerMask m_WallLayer;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var wallhit = Physics2D.OverlapCircle(transform.position, 1, m_WallLayer);
        if(wallhit)
        {
            Debug.Log(wallhit.transform.position.z);
            m_Rigidbody2D.velocity += new Vector2(0, wallhit.transform.position.z);
        }
    }
}
