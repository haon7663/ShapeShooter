using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Health m_Health;
    private Transform m_Player;

    public GameObject m_Explosion;
    public GameObject m_ExplosionParticle;
    public float m_ExpRange;
    public float m_Damage;

    bool isCalled;
    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_Health = GetComponent<Health>();
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
            GameManager.instance.Enemys.Remove(transform.parent.gameObject);
            Explosion ex = Instantiate(m_Explosion, transform.position, Quaternion.identity).GetComponent<Explosion>();
            ex.transform.rotation = transform.rotation;
            ex.m_Size = m_DrawPolygon.size;
            ex.damage = m_Damage;
            ex.m_AngleCount = m_DrawPolygon.m_AngleCount < 3 ? 3 : m_DrawPolygon.m_AngleCount;
            Instantiate(m_ExplosionParticle, transform.position, Quaternion.identity);
            m_Health.m_FollowUI.selectImage.SetActive(false);
            m_Health.m_FollowUI.selectText.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }
    }    
}
