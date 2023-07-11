using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    private List<GameObject>[] pools;
    private Transform m_ProjectileBundle;

    [Space]
    [Header("Projectile")]
    public GameObject[] m_Projectile;

    [Space]
    [Header("Stats")]
    public float m_AttackSpeed;

    private float attackDelay;

    private void Start()
    {
        m_ProjectileBundle = GameObject.FindGameObjectWithTag("ProjectileBundle").transform;
        pools = new List<GameObject>[m_Projectile.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && attackDelay < 0)
        {
            Get(0);
            attackDelay = m_AttackSpeed;
        }
        attackDelay -= Time.deltaTime;
    }

    public void Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
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
            select = Instantiate(m_Projectile[index]);
            pools[index].Add(select);
        }

        select.transform.SetParent(m_ProjectileBundle);
        select.transform.SetPositionAndRotation(transform.position + transform.right, transform.rotation);
    }
}
