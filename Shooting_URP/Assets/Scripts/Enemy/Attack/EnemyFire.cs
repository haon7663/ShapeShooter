using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private EnemyRotatement m_Rotatement;

    [Space]
    [Header("����ü��ȣ")]
    public int m_ProjectileCount;

    [Space]
    [Header("���ݷ�")]
    public float m_Damage;
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
    float sin, absolute;

    private void Start()
    {
        m_Rotatement = GetComponent<EnemyRotatement>();
    }

    private void Update()
    {
        transform.localPosition = Vector3.zero + transform.right * Mathf.Sin(sin) * Mathf.Abs(absolute) / 3;
        sin -= 5 * Time.deltaTime;
        absolute = Mathf.Lerp(absolute, 0, Time.deltaTime * 10);
        if (attackDelay < 0)
        {
            StartCoroutine(Shot(m_Rotatement.isAttackRound ? Vector3.zero : transform.right));
        }
        attackDelay -= Time.deltaTime;
    }

    private IEnumerator Shot(Vector3 pos)
    {
        isShoting = true;
        attackDelay = m_AttackDelay;
        for (int i = 0; i < m_BurstCount; i++)
        {
            sin = -1;
            absolute += 1;
            for (int j = 0; j < m_MultiCount; j++)
            {
                PoolManager.instance.Get(m_ProjectileCount, m_Rotatement.isAttackRound ? pos + transform.position + transform.right : pos + transform.position, Quaternion.Euler(0, 0, transform.localEulerAngles.z + m_SpreadAngle * (j - (m_MultiCount - 1) / 2))
                                       , m_Damage, m_ProjectileSpeed);
            }
            yield return YieldInstructionCache.WaitForSeconds(m_BurstSpeed);
        }
        isShoting = false;
        yield return null;
    }
}
