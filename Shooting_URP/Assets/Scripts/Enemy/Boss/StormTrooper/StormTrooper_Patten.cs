using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StormTrooper_Patten : MonoBehaviour
{
    private EnemyRotatement m_EnemyRotatement;
    private EnemyMovement m_EnemyMovement;
    private DrawPolygon m_DrawPolygon;

    private Transform m_Player;

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

    public GameObject m_BossPath;

    [HideInInspector] public bool isShoting;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;

        m_EnemyMovement = GetComponentInParent<EnemyMovement>();
        m_EnemyRotatement = GetComponent<EnemyRotatement>();
        m_DrawPolygon = transform.parent.GetChild(0).GetComponent<DrawPolygon>();
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
        float ranY = 0.5f;
        for (int i = 0; i < 4; i++)
        {
            ranY *= -1;
            for (int j = -5; j < 6; j++)
            {
                PoolManager.instance.Get(4, new Vector3(23.15f, j * 2f + ranY), Quaternion.Euler(0, 0, 180), 100, 1200);
                yield return YieldInstructionCache.WaitForSeconds(0.05f);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
        yield return YieldInstructionCache.WaitForSeconds(0.8f);
        isShoting = false;
        yield return null;
    }
    private IEnumerator RotateSaw(int value)
    {
        isShoting = true;
        yield return StartCoroutine(BossMove(m_EnemyMovement.m_FirstPinnedPos, 0.75f));
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                PoolManager.instance.Get(5, transform.position + new Vector3(-1, 2.5f), Quaternion.Euler(0, 0, 90 + j * 12), 100, 650);
                PoolManager.instance.Get(5, transform.position + new Vector3(-1, -2.5f), Quaternion.Euler(0, 0, 90 + j * 12), 100, 650);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.63f);
        }
        isShoting = false;
        yield return null;
    }

    private IEnumerator RushToPlayer(int value)
    {
        isShoting = true;
        yield return StartCoroutine(BossMove(m_Player.position, 0.75f));
        isShoting = false;
        yield return null;
    }

    private IEnumerator RushAndRotate(int value)
    {
        isShoting = true;
        yield return StartCoroutine(BossMove(m_Player.position, 0.75f));
        yield return StartCoroutine(BossMove(m_Player.position, 0.65f));

        float saveSpeed = m_EnemyMovement.m_Speed;
        float saveRotateSpeed = m_EnemyMovement.m_RotateSpeed;
        float saveSize = m_DrawPolygon.size;

        m_EnemyMovement.onFollow = true;
        m_EnemyMovement.onPinned = false; m_EnemyMovement.isPinned = false;
        m_EnemyMovement.m_Speed = saveSpeed * 0.75f;

        for (float i = 0; i < 5; i += Time.deltaTime)
        {
            m_EnemyMovement.m_RotateSpeed = Mathf.Lerp(m_EnemyMovement.m_RotateSpeed, 2.5f, Time.deltaTime * 4);
            m_DrawPolygon.size = Mathf.Lerp(m_DrawPolygon.size, saveSize * 1.3f, Time.deltaTime * 4);
            m_DrawPolygon.hitTime = 0.1f;

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            m_EnemyMovement.m_RotateSpeed = Mathf.Lerp(m_EnemyMovement.m_RotateSpeed, saveRotateSpeed, Time.deltaTime * 8);
            m_DrawPolygon.size = Mathf.Lerp(m_DrawPolygon.size, saveSize, Time.deltaTime * 8);
            m_DrawPolygon.hitTime = 0.1f;
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        m_EnemyMovement.m_Speed = saveSpeed;
        m_EnemyMovement.onFollow = false;
        m_EnemyMovement.onPinned = true; m_EnemyMovement.isPinned = true;
        m_EnemyMovement.m_RotateSpeed = saveRotateSpeed;
        m_DrawPolygon.size = saveSize;
        m_EnemyMovement.m_PinnedPos = transform.position;
        yield return StartCoroutine(BossMove(m_EnemyMovement.m_FirstPinnedPos, 1f));
        isShoting = false;
        yield return null;
    }

    private IEnumerator SummonExplosion(int value)
    {
        isShoting = true;
        yield return StartCoroutine(BossMove(m_EnemyMovement.m_FirstPinnedPos, 0.75f));
        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                for (int k = -3; k < 4; k++)
                {
                    PoolManager.instance.Get(4, transform.position + new Vector3(-1, 0), Quaternion.Euler(0, 0, transform.localEulerAngles.z + k * 10), 100, 1200);
                }
                Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(15, j * 4f), Quaternion.identity).GetComponent<EnemyMovement>();
                Instantiate(m_SkillStruct[value].m_Object, transform.position + new Vector3(15, -j * 4f), Quaternion.identity).GetComponent<EnemyMovement>();
            }
            yield return YieldInstructionCache.WaitForSeconds(1.2f);
        }
        isShoting = false;
        yield return null;
    }
    private IEnumerator SummonFollowExplosion(int value)
    {
        isShoting = true;
        yield return StartCoroutine(BossMove(m_EnemyMovement.m_FirstPinnedPos, 0.75f));

        Instantiate(m_SkillStruct[value].m_Object, new Vector3(25f, 6), Quaternion.identity).GetComponent<EnemyMovement>();
        Instantiate(m_SkillStruct[value].m_Object, new Vector3(25f, -6), Quaternion.identity).GetComponent<EnemyMovement>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                PoolManager.instance.Get(5, transform.position + new Vector3(-1, 0), Quaternion.Euler(0, 0, transform.localEulerAngles.z + j * 10), 100, 700);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
        Instantiate(m_SkillStruct[value].m_Object, new Vector3(25f, 6), Quaternion.identity).GetComponent<EnemyMovement>();
        Instantiate(m_SkillStruct[value].m_Object, new Vector3(25f, -6), Quaternion.identity).GetComponent<EnemyMovement>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                PoolManager.instance.Get(5, transform.position + new Vector3(-1, 0), Quaternion.Euler(0, 0, transform.localEulerAngles.z + j * 10), 100, 700);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
        yield return YieldInstructionCache.WaitForSeconds(1);
        isShoting = false;
        yield return null;
    }

    private IEnumerator SummonItem(int value)
    {
        isShoting = true;
        Instantiate(m_SkillStruct[value].m_Object, new Vector3(25f, 6), Quaternion.identity);
        Instantiate(m_SkillStruct[value].m_Object, new Vector3(25f, -6), Quaternion.identity);
        yield return StartCoroutine(BossMove(m_EnemyMovement.m_FirstPinnedPos, 1));
        isShoting = false;
        yield return null;
    }

    private IEnumerator BossMove(Vector2 pos, float time)
    {
        BossPath path = Instantiate(m_BossPath, pos, Quaternion.identity).GetComponent<BossPath>();
        path.m_Size = m_DrawPolygon.size;
        path.m_AngleCount = m_DrawPolygon.m_AngleCount;
        path.m_Time = time;
        path.m_Follow = transform;

        yield return YieldInstructionCache.WaitForSeconds(time);

        m_EnemyMovement.m_PinnedPos = pos;

        yield return YieldInstructionCache.WaitForSeconds(time);
        yield return null;
    }
}