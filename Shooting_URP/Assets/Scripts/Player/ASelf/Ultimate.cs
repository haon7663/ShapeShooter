using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate : MonoBehaviour
{
    private DrawPolygon m_DrawPolygon;
    private Health m_Health;
    public LineRenderer m_Invisble;
    public GameObject m_Ultimate;
    public Image m_UltimateImage;

    public float m_InvisbleTime;

    public float ultimateTime;

    private void Start()
    {
        m_DrawPolygon = GetComponent<DrawPolygon>();
        m_Health = transform.parent.GetComponentInChildren<Health>();
    }
    private void Update()
    {
        m_UltimateImage.fillAmount = ultimateTime / 90;
        if(ultimateTime < 90) ultimateTime += Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ultimateTime = 0;
            StartCoroutine(UseUltimate());
        }
        m_Health.isInvisble = m_InvisbleTime > 0;
        if (m_InvisbleTime > 0)
        {
            m_Invisble.transform.localScale = Vector3.Lerp(m_Invisble.transform.localScale, new Vector3(1, 1), Time.deltaTime * 7);
            m_Invisble.startColor = Color.Lerp(m_Invisble.startColor, new Color(0.5f, 0.5f, 0.5f, 1), Time.deltaTime * 9);
            m_Invisble.endColor = Color.Lerp(m_Invisble.endColor, new Color(1, 1, 1, 1), Time.deltaTime * 9);
            m_InvisbleTime -= Time.deltaTime;
        }
        else
        {
            m_Invisble.transform.localScale = Vector3.Lerp(m_Invisble.transform.localScale, new Vector3(0, 0), Time.deltaTime * 7);
            m_Invisble.startColor = Color.Lerp(m_Invisble.startColor, new Color(0.5f, 0.5f, 0.5f, 0), Time.deltaTime * 9);
            m_Invisble.endColor = Color.Lerp(m_Invisble.endColor, new Color(1, 1, 1, 0), Time.deltaTime * 9);
            m_InvisbleTime -= Time.deltaTime;
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
