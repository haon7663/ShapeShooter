using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    public GameObject m_Ultimate;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(UseUltimate());
        }
    }
    private IEnumerator UseUltimate()
    {
        for(int i = 0; i < 2; i++)
        {
            Ultimate_Explosion ultimate = Instantiate(m_Ultimate, transform.position, transform.rotation).GetComponent<Ultimate_Explosion>();
            ultimate.m_AngleCount = m_DrawPolygon.m_AngleCount;
            ultimate.m_Follow = transform;

            yield return YieldInstructionCache.WaitForSeconds(0.3f);
        }
    }
}
