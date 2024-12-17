using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp_Text : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<Text>();
        m_RectTransform = GetComponent<RectTransform>();
        StartCoroutine(LevelUp());
    }

    private IEnumerator LevelUp()
    {
        var savePos = m_RectTransform.position + new Vector3(0, 100);
        for (float i = 0; i < 1.75f; i += Time.deltaTime)
        {
            m_Text.color = Color.Lerp(m_Text.color, new Color(1, 1, 1, 0), Time.deltaTime * 4);
            m_RectTransform.position = Vector3.Lerp(m_RectTransform.position, savePos, Time.deltaTime * 3);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        Destroy(gameObject);
        yield return null;
    }
}
