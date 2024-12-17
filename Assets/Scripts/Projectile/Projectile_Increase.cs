using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Increase : MonoBehaviour
{
    private Vector3 targetPos;
    private void OnEnable()
    {
        StartCoroutine(Increase());
    }

    private IEnumerator Increase()
    {
        yield return YieldInstructionCache.WaitForFixedUpdate;
        targetPos = transform.position;
        for (float i = 0; i < 1.5f; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }
}
