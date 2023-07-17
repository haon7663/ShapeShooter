using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResult : MonoBehaviour
{
    public Text m_Result;
    [Serializable]
    public struct Struct
    {
        public Text m_NameText;
        public Text m_ScoreText;
    }
    public Struct[] m_Struct;

    private float size;

    private void Start()
    {
        StartCoroutine(OnResult());
    }
    public IEnumerator OnResult()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);
        for (int i = 0; i < 3; i++)
        {
            m_Struct[i].m_NameText.enabled = true;

            size = 1.4f;
            m_Struct[i].m_NameText.color = Color.red;
            for (float j = 0; j < 0.7f; j += Time.deltaTime)
            {
                m_Struct[i].m_NameText.transform.localScale = new Vector3(size, size);
                size = Mathf.Lerp(size, 1, Time.deltaTime * 5);
                yield return YieldInstructionCache.WaitForFixedUpdate;
            }
        }
        for(int i = 0; i < 3; i++)
        {
            float setSize = 1;
            size = 1.4f;
            switch (i)
            {
                case 0:
                    m_Struct[i].m_ScoreText.text = GameManager.instance.m_DestroyScore.ToString();
                    break;
                case 1:
                    m_Struct[i].m_ScoreText.text = string.Format("{0:D2}:{1:D2}", (int)GameManager.instance.m_Minutes, (int)GameManager.instance.m_Seconds);
                    break;
                case 2:
                    size = 2.5f;
                    setSize = 1.5f;
                    m_Struct[i].m_ScoreText.text = Mathf.RoundToInt(GameManager.instance.m_DestroyScore * 50000 / (200 + (GameManager.instance.m_Minutes * 60 + GameManager.instance.m_Seconds) / 2.5f)).ToString();
                    break;
            }
            m_Struct[i].m_ScoreText.enabled = true;
            for (float j = 0; j < 0.7f; j += Time.deltaTime)
            {
                m_Struct[i].m_ScoreText.transform.localScale = new Vector3(size, size);
                size = Mathf.Lerp(size, setSize, Time.deltaTime * 5);
                yield return YieldInstructionCache.WaitForFixedUpdate;
            }
        }
    }
}
