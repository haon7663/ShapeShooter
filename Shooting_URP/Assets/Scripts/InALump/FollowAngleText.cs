using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowAngleText : MonoBehaviour
{
    public Text m_Text;

    private List<GameObject> pools;
    
    private Camera m_MainCamera;
    private Transform m_Canvas;

    GameObject select;

    private void Awake()
    {
        pools = new List<GameObject>();

        m_MainCamera = Camera.main;
        m_Canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void OnEnable()
    {
        Get();
    }

    private void LateUpdate()
    {
        select.transform.position = m_MainCamera.WorldToScreenPoint(transform.position);
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
    }
}
