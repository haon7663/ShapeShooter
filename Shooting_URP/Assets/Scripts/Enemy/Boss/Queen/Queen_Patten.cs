using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Queen_Patten : MonoBehaviour
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
        for(int i = 0; i < m_SkillStruct.Length; i++)
        {
            m_SkillStruct[i].delay -= Time.deltaTime;
            if (m_SkillStruct[i].delay < 0 && !isShoting)
            {
                m_SkillStruct[i].delay = m_SkillStruct[i].m_Delay;
                StartCoroutine(m_SkillStruct[i].m_Name, i);
            }
        }
    }

    private IEnumerator OctaSpread(int value)
    {
        isShoting = true;
        m_EnemyMovement.onMove = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                PoolManager.instance.Get(0, transform.position, Quaternion.Euler(0, 0, 30 * (j - (20 - 1) / 2) + (i * 8f)), 80, 400);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.125f);
        }
        yield return YieldInstructionCache.WaitForSeconds(0.6f);
        isShoting = false;
        m_EnemyMovement.onMove = true;
        yield return null;
    }
    private IEnumerator TripleSpread(int value)
    {
        isShoting = true;
        m_EnemyMovement.onMove = false;

        float zRandom = Random.Range(-15, 15);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 24; j++)
            {
                PoolManager.instance.Get(2, transform.position, Quaternion.Euler(0, 0, zRandom + 15 * (j - (12 - 1) / 2) + (i * 7.5f)), 100, 1250 - i * 300);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
        yield return YieldInstructionCache.WaitForSeconds(0.6f);
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
    private IEnumerator LineShot(int value)
    {
        isShoting = true;
        for (int i = 0; i < 18; i++)
        {
            float zValue = 72 - i * 8;
            PoolManager.instance.Get(3, transform.position, Quaternion.Euler(0, 0, transform.localEulerAngles.z + zValue), 100, 500);
            PoolManager.instance.Get(3, transform.position, Quaternion.Euler(0, 0, transform.localEulerAngles.z + -zValue), 100, 500);
            yield return YieldInstructionCache.WaitForSeconds(0.09f);
        }
        yield return YieldInstructionCache.WaitForSeconds(1f);
        isShoting = false;
        yield return null;
    }

    private IEnumerator SummonProtecter(int value)
    {
        for (int i = 0; i < 3; i++)
        {
            EnemyMovement protecter = Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(-6, (i-1) * 2.5f), Quaternion.identity).GetComponent<EnemyMovement>();
            protecter.m_Follow = transform;
        }
        yield return null;
    }

    private IEnumerator SummonPinned(int value)
    {
        for (int i = -1; i < 2; i+=2)
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
