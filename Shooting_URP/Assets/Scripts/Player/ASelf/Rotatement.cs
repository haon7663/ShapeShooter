using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatement : MonoBehaviour
{
    private Camera m_Camera;

    float angle;
    Vector2 mouse;

    private void Start()
    {
        m_Camera = Camera.main;
    }
    private void Update()
    {
        mouse = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
