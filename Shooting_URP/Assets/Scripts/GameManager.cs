using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int m_StageCount;

    public int m_WaveCount;

    [Serializable]
    public struct DetailEnemyStruct
    {
        public GameObject m_Enemy;
        public Vector2 m_Position;
        public float m_DelayTime;
        [HideInInspector] public bool isCalled;
    }
    [Serializable]
    public struct WaveStruct
    {
        public DetailEnemyStruct[] m_DetailEnemy;
    }

    [Serializable]
    public struct StageStruct
    {
        public WaveStruct[] m_Wave;
    }
    public StageStruct[] m_StageStruct = new StageStruct[1];

    public List<GameObject> Enemys;

    public GameObject[] m_Items;

    public GameObject m_StageResult;
    public GameObject m_DeathZone;
    public Text m_ScoreText;
    public Text m_TimeText;

    private Camera m_Camera;

    public int m_DestroyScore;
    public float m_Minutes;
    public float m_Seconds;

    int stage;

    private void Awake()
    {
        instance = this;
        m_Camera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(Waving());
    }

    private void Update()
    {
        m_Seconds += Time.deltaTime;
        if((int)m_Seconds >= 60)
        {
            m_Seconds = 0;
            m_Minutes += 1;
        }
        m_ScoreText.text = "SCORE: " + m_DestroyScore.ToString();
        m_TimeText.text = "TIME: " + string.Format("{0:D2}:{1:D2}", (int)m_Minutes, (int)m_Seconds);
    }

    public IEnumerator Waving()
    {
        stage = m_StageCount;
        yield return YieldInstructionCache.WaitForSeconds(2f);
        int EnemyCount = 0;
        for(float i = 0; i < m_StageStruct[stage].m_Wave[m_WaveCount].m_DetailEnemy[m_StageStruct[stage].m_Wave[m_WaveCount].m_DetailEnemy.Length-1].m_DelayTime + 0.5f; i += Time.deltaTime)
        {
            if(!m_StageResult.activeSelf)
            {
                var detail = m_StageStruct[stage].m_Wave[m_WaveCount].m_DetailEnemy;
                if (detail.Length > EnemyCount && !detail[EnemyCount].isCalled && i > detail[EnemyCount].m_DelayTime)
                {
                    detail[EnemyCount].isCalled = true;
                    Instantiate(detail[EnemyCount].m_Enemy, (Vector2)m_Camera.transform.position + new Vector2(25, 0) + detail[EnemyCount].m_Position, Quaternion.identity);
                    EnemyCount++;
                }
                yield return YieldInstructionCache.WaitForFixedUpdate;
            }
            else
            {
                m_WaveCount = 0;
                break;
            }
        }
        while(Enemys.Count > 0) yield return YieldInstructionCache.WaitForFixedUpdate;
        if(++m_WaveCount < m_StageStruct[stage].m_Wave.Length)
        {
            StartCoroutine(Waving());
        }
    }

    public GameObject ItemPersent(int persent)
    {
        GameObject select = null;
        int ran = Random.Range(1, 101);
        if(persent >= 100)
        {
            select = m_Items[Random.Range(0, 3)];
        }
        else if (ran <= persent)
        {
            select = m_Items[Random.Range(3, m_Items.Length)];
        }
        return select;
    }
}

internal static class YieldInstructionCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!waitForSeconds.TryGetValue(seconds, out wfs))
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}
