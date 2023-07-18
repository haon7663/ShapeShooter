using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<GameObject>[] pools;

    public GameObject[] m_Projectile = new GameObject[2];

    public Transform m_ProjectileBundle;

    private void Awake()
    {
        instance = this;
        pools = new List<GameObject>[10];
        for (int i = 0; i < m_Projectile.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public void Get(int index, Vector3 pos, Quaternion rot, float damage, float speed)
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

        var projectile = select.GetComponent<Projectile>();

        projectile.realDamage = damage;
        projectile.speed = speed;
        projectile.isPlayer = false;

        select.transform.SetParent(m_ProjectileBundle);
        select.transform.SetPositionAndRotation(pos, rot);
    }
}
