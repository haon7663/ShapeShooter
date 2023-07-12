using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Transform m_Player;

    public GameObject m_Explosion;
    public GameObject m_ExplosionParticle;
    public float m_ExpRange;

    bool isCalled;
    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnEnable()
    {
        isCalled = false;
    }

    private void Update()
    {
        Vector3 dir = m_Player.position - transform.position;
        float distance = dir.magnitude;
        if(distance < m_ExpRange)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        if(!isCalled)
        {
            isCalled = true;
            Explosion ex = Instantiate(m_Explosion, transform.position, Quaternion.identity).GetComponent<Explosion>();
            ex.transform.rotation = transform.rotation;
            ex.m_Size = m_DrawPolygon.size;
            ex.m_AngleCount = m_DrawPolygon.m_AngleCount < 3 ? 3 : m_DrawPolygon.m_AngleCount;
            Instantiate(m_ExplosionParticle, transform.position, Quaternion.identity);
            transform.parent.gameObject.SetActive(false);
        }
    }    
}
