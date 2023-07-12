using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotatement : MonoBehaviour
{
    private EnemyFire m_EnemyFire;
    private Transform m_Player;
    [Space]
    [Header("적을 바라보는가?")]
    public bool isLookat;
    [Header("쏘면서 회전하는가?")]
    public bool isAttackRound;

    private float angle;

    Vector2 target;

    private void Start()
    {
        m_EnemyFire = GetComponent<EnemyFire>();
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (isAttackRound || !m_EnemyFire.isShoting)
        {
            if (isLookat) target = m_Player.position;
            else if(!isLookat) target = transform.position + new Vector3(-2, 0);
            angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 16);
        }
    }
}
