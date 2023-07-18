using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StormTrooper_Patten : MonoBehaviour
{
    private EnemyRotatement m_EnemyRotatement;
    private EnemyMovement m_EnemyMovement;

    [Serializable]
    public struct SkillStruct
    {
        [Header("스킬명")]
        public string m_Name;
        [Header("오브젝트")]
        public GameObject m_Object;
        [Header("쿨타임")]
        public float m_Delay;
        [Header("시작쿨타임")]
        public float m_StartDelay;
        public float delay;
    }
    public SkillStruct[] m_SkillStruct;

    [HideInInspector] public bool isShoting;

    private void Start()
    {
        m_EnemyMovement = GetComponentInParent<EnemyMovement>();
        m_EnemyRotatement = GetComponent<EnemyRotatement>();
        for (int i = 0; i < m_SkillStruct.Length; i++)
        {
            m_SkillStruct[i].delay = m_SkillStruct[i].m_StartDelay;
        }
    }
    private void Update()
    {
        for (int i = 0; i < m_SkillStruct.Length; i++)
        {
            m_SkillStruct[i].delay -= Time.deltaTime;
            if (m_SkillStruct[i].delay < 0 && !isShoting)
            {
                m_SkillStruct[i].delay = m_SkillStruct[i].m_Delay;
                StartCoroutine(m_SkillStruct[i].m_Name, i);
            }
        }
    }

    private IEnumerator LineSaw(int value)
    {
        isShoting = true;
        m_EnemyMovement.onMove = false;
        float ranY = 0.5f;
        for (int i = 0; i < 4; i++)
        {
            ranY *= -1;
            for (int j = -5; j < 6; j++)
            {
                PoolManager.instance.Get(4, transform.position + new Vector3(9, j * 2f + ranY), Quaternion.Euler(0, 0, 180), 150, 600);
                yield return YieldInstructionCache.WaitForSeconds(0.05f);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
        isShoting = false;
        m_EnemyMovement.onMove = true;
        yield return null;
    }
    private IEnumerator TightSpread(int value)
    {
        isShoting = true;
        yield return YieldInstructionCache.WaitForSeconds(2f);
        float zRandom = Random.Range(-15, 15);
        for (int j = 0; j < 36; j++)
        {
            PoolManager.instance.Get(1, transform.position, Quaternion.Euler(0, 0, zRandom + 10f * j), 100, 400);
        }
        isShoting = false;
        yield return null;
    }
    private IEnumerator RotateSaw(int value)
    {
        isShoting = true;
        int ran = Random.Range(0, 2);
        if(ran == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    PoolManager.instance.Get(5, transform.position + new Vector3(-1, 2.5f), Quaternion.Euler(0, 0, 90 + j * 12), 100, 200);
                    PoolManager.instance.Get(5, transform.position + new Vector3(-1, -2.5f), Quaternion.Euler(0, 0, 90 + j * 12), 100, 200);
                }
                yield return YieldInstructionCache.WaitForSeconds(0.63f);
            }
        }
        else if(ran == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    PoolManager.instance.Get(5, transform.position + new Vector3(-1, 2.5f), Quaternion.Euler(0, 0, 90 + j * 12), 100, 200);
                    PoolManager.instance.Get(5, transform.position + new Vector3(-1, -2.5f), Quaternion.Euler(0, 0, 90 + j * 12), 100, 200);
                }
                yield return YieldInstructionCache.WaitForSeconds(0.63f);
            }
        }
        isShoting = false;
        yield return null;
    }

    private IEnumerator SummonExplosion(int value)
    {
        for (int i = 0; i < 3; i++)
        {
            for(int j = 1; j < 3; j++)
            {
                for (int k = -3; k < 4; k++)
                {
                    PoolManager.instance.Get(4, transform.position + new Vector3(-1, 0), Quaternion.Euler(0, 0, 180 + k * 10), 100, 1200);
                }
                Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(15, j * 4f), Quaternion.identity).GetComponent<EnemyMovement>();
                Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(15, -j * 4f), Quaternion.identity).GetComponent<EnemyMovement>();
            }
            yield return YieldInstructionCache.WaitForSeconds(1.2f);
        }
        yield return null;
    }

    private IEnumerator SummonPinned(int value)
    {
        for (int i = -1; i < 2; i += 2)
        {
            Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(17, 5 * i), Quaternion.identity);
        }
        yield return null;
    }

    private IEnumerator SummonDefault(int value)
    {
        for (int i = -1; i < 2; i += 2)
        {
            Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(10, 4.5f * i), Quaternion.identity);
        }
        yield return null;
    }
    private IEnumerator SummonWeak(int value)
    {
        Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(10, 7), Quaternion.identity);
        Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(10, -7), Quaternion.identity);
        Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(12, -4), Quaternion.identity);
        Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(12, 4), Quaternion.identity);
        yield return null;
    }
    private IEnumerator SummonItem(int value)
    {
        Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(12, -6), Quaternion.identity);
        Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(12, 6), Quaternion.identity);
        yield return null;
    }
}