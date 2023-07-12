using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowAngleText : MonoBehaviour
{
    private List<GameObject> pools;

    public Text m_Text;

    private Text m_SelectText;
    private DrawPolygon m_DrawPolygon;

    private Camera m_MainCamera;
    private Transform m_Canvas;

    GameObject select;

    private void Awake()
    {
        pools = new List<GameObject>();

        m_MainCamera = Camera.main;
        m_Canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        m_DrawPolygon = GetComponentInChildren<DrawPolygon>();
    }

    private void OnEnable()
    {
        Get();
    }

    private void LateUpdate()
    {
        select.transform.position = m_MainCamera.WorldToScreenPoint(transform.position);
        m_SelectText.text = m_DrawPolygon.m_AngleCount.ToString();
    }

    public void Get()
    {
        select = null;

        foreach (GameObject item in pools)
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(m_Text.gameObject);
            pools.Add(select);
        }

        select.transform.SetParent(m_Canvas);
        select.transform.position = m_MainCamera.WorldToScreenPoint(transform.position);
        m_SelectText = select.GetComponent<Text>();
    }

    private void OnDisable()
    {
        select.SetActive(false);
    }
}
