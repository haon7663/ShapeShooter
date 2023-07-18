using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awl_Boss : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        transform.SetParent(Camera.main.transform);
        transform.localPosition = new Vector3(25, transform.localPosition.y, 10);

        StartCoroutine(AppearAwl());
    }

    private IEnumerator AppearAwl()
    {
        for(float i = 0; i < 2; i += Time.deltaTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(15, transform.localPosition.y, 10), Time.deltaTime * 3);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        for (float i = 0; i < 3f; i += Time.deltaTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-7f, transform.localPosition.y, 10), Time.deltaTime * 5);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        for (float i = 0; i < 3f; i += Time.deltaTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(25, transform.localPosition.y, 10), Time.deltaTime * 3);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        Destroy(gameObject);
    }
}
