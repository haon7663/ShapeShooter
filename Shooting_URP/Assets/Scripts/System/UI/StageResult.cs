using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResult : MonoBehaviour
{
    private Ultimate m_Ultimate;
    private Animator m_Animator;
    
    public Text m_Result;
    [Serializable]
    public struct Struct
    {
        public Text m_NameText;
        public Text m_ScoreText;
    }
    public Text m_ClickContinue;
    public Struct[] m_Struct;

    private float size;

    private void Awake()
    {
        m_Ultimate = GameObject.FindGameObjectWithTag("Player").GetComponent<Ultimate>();
        m_Animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        StartCoroutine(OnResult());
    }
    public IEnumerator OnResult()
    {
        m_Ultimate.m_InvisbleTime = 99999;
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
            m_Struct[i].m_ScoreText.color = Color.white;
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
        yield return YieldInstructionCache.WaitForSeconds(1f);
        while (!Input.GetMouseButton(0))
        {
            m_ClickContinue.enabled = !m_ClickContinue.enabled;
            for (float i = 0; i < 0.5f; i += Time.deltaTime)
            {
                if (Input.GetMouseButton(0)) break;
                yield return YieldInstructionCache.WaitForFixedUpdate;
            }
        }

        m_ClickContinue.enabled = true;
        for (int i = 0; i < 12; i++)
        {            
            yield return YieldInstructionCache.WaitForSeconds(0.05f);
            m_ClickContinue.enabled = !m_ClickContinue.enabled;
        }
        yield return YieldInstructionCache.WaitForSeconds(0.14f);
        m_ClickContinue.enabled = false;
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        m_Animator.SetTrigger("fadeOut");

        if(m_Result.text == "STAGE RESULT")
        {
            GameManager.instance.m_StageCount++;
            GameManager.instance.m_WaveCount = 0;
            Camera.main.transform.GetComponent<CameraEffect>().ChangeColor();
            BackgroundScrolling.instance.SetColor();
            StartCoroutine(GameManager.instance.Waving());
        }
        else
        {
            UIManager.instance.GameStart("Main");
        }

        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            for (int j = 0; j < 3; j++)
            {
                m_Struct[j].m_NameText.color -= new Color(0, 0, 0, Time.deltaTime*2.5f);
                m_Struct[j].m_ScoreText.color -= new Color(0, 0, 0, Time.deltaTime*2.5f);
            }
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        m_Ultimate.m_InvisbleTime = 2;  
        yield return null;
    }

    public void EnableFalse()
    {
        gameObject.SetActive(false);
    }
}
