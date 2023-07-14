using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    public WaveStruct[] m_WaveStruct = new WaveStruct[1];

    public List<GameObject> Enemys;

    private Camera m_Camera;

    private void Awake()
    {
        instance = this;
        m_Camera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(Waving());
    }

    private IEnumerator Waving()
    {
        int EnemyCount = 0;
        for(float i = 0; i < 15; i += Time.deltaTime)
        {
            var detail = m_WaveStruct[m_WaveCount].m_DetailEnemy;
            if (detail.Length > EnemyCount && !detail[EnemyCount].isCalled && i > detail[EnemyCount].m_DelayTime)
            {
                detail[EnemyCount].isCalled = true;
                Instantiate(detail[EnemyCount].m_Enemy, (Vector2)m_Camera.transform.position + new Vector2(25, 0) + detail[EnemyCount].m_Position, Quaternion.identity);
                EnemyCount++;
            }
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        while(Enemys.Count > 0) yield return YieldInstructionCache.WaitForFixedUpdate;
        m_WaveCount++;
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
