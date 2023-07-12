using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTower_Sub : MonoBehaviour
{
    public GameObject m_Tower;
    public Vector3[] m_TowerPosition;

    GameObject[] towers;
    Vector3[] sine;

    private void Start()
    {
        SetTower();
    }

    private void Update()
    {
        for(int i = 0; i < towers.Length; i++)
        {
            towers[i].transform.position = Vector2.Lerp(towers[i].transform.position, transform.position + m_TowerPosition[i] + new Vector3(Mathf.Sin(sine[i].x += Time.deltaTime), Mathf.Sin(sine[i].y += Time.deltaTime)) * 0.5f, Time.deltaTime*7);
        }
    }

    public void SetTower()
    {
        towers = new GameObject[2];
        sine = new Vector3[2];
        for (int i = 0; i < 2; i++)
        {
            towers[i] = Instantiate(m_Tower, transform.position, Quaternion.identity);
            sine[i] = new Vector2(i, i) * 3;
        }
    }
}
