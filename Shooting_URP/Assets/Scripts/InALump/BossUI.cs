using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Health m_Health;

    public GameObject m_BossBundle;

    private Image m_HPImage;
    private Text m_AngleText;
    private Text m_NameText;

    public string m_BossName;

    float hpLerp;

    private void Awake()
    {
        m_BossBundle = GameObject.Find("BossBundle");
        m_DrawPolygon = GetComponentInChildren<DrawPolygon>();
        m_Health = GetComponentInChildren<Health>();

        m_HPImage = m_BossBundle.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        m_AngleText = m_BossBundle.transform.GetChild(1).GetComponent<Text>();
        m_NameText = m_BossBundle.transform.GetChild(2).GetComponent<Text>();
    }

    private void OnEnable()
    {
        m_BossBundle.SetActive(true);
        m_NameText.text = m_BossName;
    }

    private void LateUpdate()
    {
        m_AngleText.text = "X" + (m_DrawPolygon.m_AngleCount-2).ToString();
        hpLerp = Mathf.Lerp(hpLerp, m_Health.curhp / m_Health.maxhp, Time.deltaTime * 25);

        m_HPImage.fillAmount = hpLerp;
    }
}
