using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigUI : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Health m_Health;

    public GameObject m_Bundle;

    private Image m_HPImage;
    private Text m_AngleText;
    private Text m_NameText;

    public string m_PanelName;
    public string m_BossName;
    public bool isPlayer;

    float hpLerp;

    private void Awake()
    {
        if (!isPlayer)
        {
            m_Bundle = GameObject.Find("HPBundle").transform.GetChild(1).gameObject;
        }
        m_DrawPolygon = GetComponentInChildren<DrawPolygon>();
        m_Health = GetComponentInChildren<Health>();

        m_HPImage = m_Bundle.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        m_AngleText = m_Bundle.transform.GetChild(1).GetComponent<Text>();
        m_NameText = m_Bundle.transform.GetChild(2).GetComponent<Text>();
    }

    private void OnEnable()
    {
        m_Bundle.SetActive(true);
        if (!isPlayer)
        {
            m_NameText.text = m_BossName;
        }
    }

    private void LateUpdate()
    {
        m_AngleText.text = "X" + (m_DrawPolygon.m_AngleCount-2).ToString();
        hpLerp = Mathf.Lerp(hpLerp, m_Health.curhp / m_Health.maxhp, Time.deltaTime * 25);

        m_HPImage.fillAmount = hpLerp;
    }

    private void OnDisable()
    {
        GameManager.instance.m_StageResult.SetActive(true);
        GameManager.instance.m_StageResult.GetComponent<StageResult>().m_Result.text = m_PanelName;
    }
}
