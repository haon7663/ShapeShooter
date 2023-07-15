using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatement : MonoBehaviour
{
    /*private Camera m_Camera;

    public float m_Angle;

    Vector2 target;

    private void Start()
    {
        m_Camera = Camera.main;
    }
    private void Update()
    {
        target = transform.position + new Vector3(1, 0);
        m_Angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
    }
    private GameObject FindNearestObjectByTag()
    {
        var objects = GameManager.instance.Enemys;
        var neareastObject = objects.OrderBy(obj =>
        {
            return Vector3.Distance(mouse, obj.transform.position);
        }).FirstOrDefault();
        return neareastObject;
    }*/

    private Camera m_Camera;

    public float m_Angle;

    Vector2 target;

    private void Start()
    {
        m_Camera = Camera.main;
    }
    private void Update()
    {
        target = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        //target = transform.position + new Vector3(1, 0);
        m_Angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
    }
}
