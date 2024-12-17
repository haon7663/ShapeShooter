using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Saw : MonoBehaviour
{
    private Projectile m_Projectile;
    private Transform m_Child;
    private Vector3 targetPos;
    private void OnEnable()
    {
        m_Projectile = GetComponent<Projectile>();
        m_Child = transform.GetChild(0);
        StartCoroutine(Increase());
    }

    private IEnumerator Increase()
    {
        yield return YieldInstructionCache.WaitForFixedUpdate;
        targetPos = transform.position;
        for (float i = 0; i < 1.2f; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 25);
            m_Child.localScale = Vector3.Lerp(m_Child.localScale, new Vector3(2, 2), Time.deltaTime * 4);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }
}
