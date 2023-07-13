using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowUI : MonoBehaviour
{
    private Health m_Health;

    private List<GameObject> textpools;

    public Text m_AngleText;
    public Image m_HPImage;

    private Text m_SelectText;
    private Image m_SelectImage;
    private DrawPolygon m_DrawPolygon;

    private Camera m_MainCamera;
    private Transform m_Canvas;

    GameObject selectText;
    GameObject selectImage;

    Vector3 hpPos = new Vector3(0, 0.5f);

    float hpLerp;

    private void Awake()
    {
        textpools = new List<GameObject>();

        m_MainCamera = Camera.main;
        m_Canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        m_DrawPolygon = GetComponentInChildren<DrawPolygon>();
        m_Health = GetComponentInChildren<Health>();
    }

    private void OnEnable()
    {
        Get();
    }

    private void LateUpdate()
    {
        selectText.transform.position = m_MainCamera.WorldToScreenPoint(transform.position);
        m_SelectText.text = m_DrawPolygon.m_AngleCount.ToString();

        selectImage.transform.position = m_MainCamera.WorldToScreenPoint(transform.position - hpPos);
        hpLerp = Mathf.Lerp(hpLerp, m_Health.curhp / m_Health.maxhp, Time.deltaTime * 25);
        m_SelectImage.fillAmount = hpLerp;
    }
    public void Get()
    {
        selectText = null;

        foreach (GameObject item in textpools)
        {
            if (!item.activeSelf)
            {
                selectText = item;
                selectText.SetActive(true);
                selectImage = item;
                selectImage.SetActive(true);
                break;
            }
        }

        if (!selectText)
        {
            selectText = Instantiate(m_AngleText.gameObject);
            selectImage = Instantiate(m_HPImage.gameObject);
            textpools.Add(selectText);
        }

        selectText.transform.SetParent(m_Canvas);
        m_SelectText = selectText.GetComponent<Text>();

        selectImage.transform.SetParent(m_Canvas);
        m_SelectImage = selectImage.transform.GetChild(0).GetComponent<Image>();
    }

    private void OnDisable()
    {
        selectText.SetActive(false);
        selectImage.SetActive(false);
    }
}
