using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Level m_PlayerLevel;
    [HideInInspector] public FollowUI m_FollowUI;
    public EnemyExplosion m_EnemyExplosion;

    public float defhp;
    public float maxhp;
    public float curhp;

    public GameObject m_DestoryParticle;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_FollowUI = GetComponentInParent<FollowUI>();
        m_PlayerLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<Level>();
        GameManager.instance.Enemys.Add(transform.parent.gameObject);
    }

    private void OnEnable()
    {
        curhp = maxhp;
    }

    public void OnDamage(float dam)
    {
        curhp -= dam;
        m_DrawPolygon.OnDamage();
        if (curhp <= 0)
        {
            curhp = maxhp;
            m_DrawPolygon.m_AngleCount--;
            m_DrawPolygon.ChangeAngle();
            if (m_DrawPolygon.m_AngleCount < 3)
            {
                m_PlayerLevel.AddExp(10);
                Death();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            Death();
        }
    }

    public void Death()
    {
        GameManager.instance.Enemys.Remove(transform.parent.gameObject);

        m_FollowUI.selectImage.SetActive(false);
        m_FollowUI.selectText.SetActive(false);

        if (m_EnemyExplosion) m_EnemyExplosion.Explosion();
        else
        {
            Instantiate(m_DestoryParticle, transform.position, Quaternion.identity);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
