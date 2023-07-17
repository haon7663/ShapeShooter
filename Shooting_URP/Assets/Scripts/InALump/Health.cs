using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Level m_PlayerLevel;
    [HideInInspector] public FollowUI m_FollowUI;
    [HideInInspector] public BigUI m_BigUI;
    public EnemyExplosion m_EnemyExplosion;

    public float defhp;
    public float maxhp;
    public float curhp;
    public float exp = 10;
    public int itemPer = 15;

    public GameObject m_DestoryParticle;

    public bool isPlayer;
    public bool isInvisble;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_FollowUI = GetComponentInParent<FollowUI>();
        m_BigUI = GetComponentInParent<BigUI>();
        m_PlayerLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<Level>();
        if(!isPlayer) GameManager.instance.Enemys.Add(transform.parent.gameObject);
    }

    private void OnEnable()
    {
        curhp = maxhp;
    }

    public void OnDamage(float dam)
    {
        if (isInvisble) return;
        curhp -= dam;
        m_DrawPolygon.OnDamage();
        if (isPlayer) StartCoroutine(CameraEffect.instance.OnDamage());
        if (curhp <= 0)
        {
            curhp = maxhp;
            m_DrawPolygon.m_AngleCount--;
            m_DrawPolygon.ChangeAngle();
            if (m_DrawPolygon.m_AngleCount < 3)
            {
                Death(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            Death(false);
        }
    }

    public void Death(bool onExp)
    {
        GameManager.instance.Enemys.Remove(transform.parent.gameObject);
        
        if(m_FollowUI)
        {
            m_FollowUI.selectImage.SetActive(false);
            m_FollowUI.selectText.SetActive(false);
        }
        else if(m_BigUI)
        {
            m_BigUI.m_Bundle.SetActive(false);
        }

        if (m_EnemyExplosion) m_EnemyExplosion.Explosion();
        else
        {
            if (!isPlayer && onExp && transform.parent.gameObject.activeSelf)
            {
                GameObject select = GameManager.instance.ItemPersent(itemPer);
                GameManager.instance.m_DestroyScore += (int)exp;
                if (select) Instantiate(select, transform.position, Quaternion.identity);
                m_PlayerLevel.AddExp(exp);
            }
            Instantiate(m_DestoryParticle, transform.position, Quaternion.identity);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
