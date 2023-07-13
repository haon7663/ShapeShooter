using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Arrow, Double
}

public class FireProjectile : MonoBehaviour
{
    private List<GameObject> pools;

    private Transform m_ProjectileBundle;

    public AttackType m_AttackType;

    [Serializable]
    public struct BulletStruct
    {
        public String m_Type;

        [Space]
        [Header("무기")]
        public Transform[] m_Weapon;
        [Header("멀티건 버스트")]
        public float m_MultiBurstCount;

        [Space]
        [Header("투사체")]
        public GameObject m_Projectile;

        [Space]
        [Header("공격속도")]
        public float m_Speed;
        [Header("버스트")]
        public int m_BurstCount;
        [Header("버스트속도")]
        public float m_BurstSpeed;
        [Header("멀티샷")]
        public int m_MultiCount;
        [Header("탄퍼짐")]
        public float m_SpreadAngle;
    }
    public BulletStruct[] m_BulletStruct = new BulletStruct[1];
    private Rotatement[] m_Rotatement = new Rotatement[10];
    private int typeCount;

    float attackDelay;
    bool isShoting;

    private void Start()
    {
        m_ProjectileBundle = GameObject.FindGameObjectWithTag("ProjectileBundle").transform;
        pools = new List<GameObject>();

        for(int i = 0; i < m_BulletStruct.Length; i++)
        {
            if(m_BulletStruct[i].m_Type == m_AttackType.ToString())
            {
                typeCount = i;
                break;
            }
        }

        for(int i = 0; i < m_BulletStruct[typeCount].m_Weapon.Length; i++)
        {
            m_BulletStruct[typeCount].m_Weapon[i].gameObject.SetActive(true);
            m_Rotatement[i] = m_BulletStruct[typeCount].m_Weapon[i].GetComponent<Rotatement>();
        }
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && attackDelay < 0)
        {
            StartCoroutine(m_BulletStruct[typeCount].m_Weapon.Length == 1 ? Shot(0) : MultiGun());
        }
        attackDelay -= Time.deltaTime;
    }

    private IEnumerator MultiGun()
    {
        for (int i = 0; i < m_BulletStruct[typeCount].m_Weapon.Length; i++)
        {
            StartCoroutine(Shot(i));
            yield return YieldInstructionCache.WaitForSeconds(m_BulletStruct[typeCount].m_MultiBurstCount);
        }
    }

    private IEnumerator Shot(int k)
    {
        isShoting = true;
        attackDelay = m_BulletStruct[typeCount].m_Speed;
        for (int i = 0; i < m_BulletStruct[typeCount].m_BurstCount; i++)
        {
            for (int j = 0; j < m_BulletStruct[typeCount].m_MultiCount; j++)
            {
                Get(m_BulletStruct[typeCount].m_Weapon[k], Quaternion.Euler(0, 0, m_Rotatement[k].m_Angle + m_BulletStruct[typeCount].m_SpreadAngle * (j - (m_BulletStruct[typeCount].m_MultiCount - 1) / 2)));
            }
            yield return YieldInstructionCache.WaitForSeconds(m_BulletStruct[typeCount].m_BurstSpeed);
        }
        isShoting = false;
        yield return null;
    }

    public void Get(Transform pos, Quaternion rot)
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
            select = Instantiate(m_BulletStruct[typeCount].m_Projectile);
            pools.Add(select);
        }

        select.transform.SetParent(m_ProjectileBundle);
        select.transform.SetPositionAndRotation(pos.position + pos.right*0.3f, rot);
    }
}
