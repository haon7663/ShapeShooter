using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMove : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Length;

    private Camera m_Camera;
    Vector2 savePos;
    float sinTimer;

    private void Start()
    {
        m_Camera = Camera.main;
        savePos = transform.position;
    }
    private void Update()
    {
        sinTimer += m_Speed * Time.deltaTime;
        transform.position = new Vector3(savePos.x, savePos.y + Mathf.Sin(sinTimer) * m_Length);
    }
}
