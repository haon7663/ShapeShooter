using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTower : MonoBehaviour
{
    private List<GameObject> pools;

    private Transform m_ProjectileBundle;

    [Space]
    [Header("����ü")]
    public GameObject m_Projectile;

    [Space]
    [Header("���ݼӵ�")]
    public float m_Speed;
    [Header("��Ƽ��")]
    public int m_MultiCount;
    [Header("ź����")]
    public float m_SpreadAngle;
    [Header("����ź����")]
    public float m_RandomSpread;

    private float attackDelay;

    private void Start()
    {
        m_ProjectileBundle = GameObject.FindGameObjectWithTag("ProjectileBundle").transform;
        pools = new List<GameObject>();
    }

    private void Update()
    {
        if (attackDelay < 0)
        {
            for (int i = 0; i < m_MultiCount; i++)
            {
                Get(transform.position, Quaternion.Euler(0, 0, m_SpreadAngle * (i - (m_MultiCount - 1) / 2) + Random.Range(m_RandomSpread, -m_RandomSpread)));
            }
            attackDelay = m_Speed;
        }
        attackDelay -= Time.deltaTime;
    }

    public void Get(Vector3 pos, Quaternion rot)
    {
        GameObject select = null;

        foreach (GameObject item in pools)
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(m_Projectile);
            pools.Add(select);
        }

        select.transform.SetParent(m_ProjectileBundle);
        select.transform.SetPositionAndRotation(pos + transform.right, rot);
    }
}
