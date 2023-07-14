using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private EnemyRotatement m_Rotatement;
    private List<GameObject> pools;
    private Transform m_ProjectileBundle;

    [Space]
    [Header("����ü")]
    public GameObject m_Projectile;

    [Space]
    [Header("���ݷ�")]
    public float m_Damage;
    [Header("����")]
    public int m_PenetrateCount;
    [Header("����ü�ӵ�")]
    public float m_ProjectileSpeed;

    [Space]
    [Header("���ݼӵ�")]
    public float m_AttackDelay;
    [Header("����Ʈ")]
    public int m_BurstCount;
    [Header("����Ʈ�ӵ�")]
    public float m_BurstSpeed;
    [Header("��Ƽ��")]
    public int m_MultiCount;
    [Header("ź����")]
    public float m_SpreadAngle;

    [HideInInspector] public bool isShoting;
    private float attackDelay;

    private void Start()
    {
        m_Rotatement = GetComponent<EnemyRotatement>();
        m_ProjectileBundle = GameObject.FindGameObjectWithTag("ProjectileBundle").transform;

        pools = new List<GameObject>();
    }

    private void Update()
    {
        if (attackDelay < 0)
        {
            StartCoroutine(Shot(m_Rotatement.isAttackRound ? Vector3.zero : transform.right));
        }
        attackDelay -= Time.deltaTime;
    }

    private IEnumerator Shot(Vector2 pos)
    {
        isShoting = true;
        attackDelay = m_AttackDelay;
        for (int i = 0; i < m_BurstCount; i++)
        {
            for (int j = 0; j < m_MultiCount; j++)
            {
                Get(pos, Quaternion.Euler(0, 0, transform.localEulerAngles.z + m_SpreadAngle * (j - (m_MultiCount - 1) / 2)));
            }
            yield return YieldInstructionCache.WaitForSeconds(m_BurstSpeed);
        }
        isShoting = false;
        yield return null;
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

        var projectile = select.GetComponent<Projectile>();

        projectile.realDamage = m_Damage;
        projectile.penetrateCount = m_PenetrateCount;
        projectile.speed = m_ProjectileSpeed;
        projectile.isPlayer = false;

        select.transform.SetParent(m_ProjectileBundle);
        select.transform.SetPositionAndRotation(m_Rotatement.isAttackRound ? pos + transform.position + transform.right : pos + transform.position, rot);
    }
}
