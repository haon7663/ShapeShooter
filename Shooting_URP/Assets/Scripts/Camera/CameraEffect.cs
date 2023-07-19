using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffect : MonoBehaviour
{
    public static CameraEffect instance;

    public Color[] m_Colors;

    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(0f);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
        ChangeColor();
    }

    public void ChangeColor()
    {
        Camera.main.backgroundColor = m_Colors[GameManager.instance.m_StageCount];
    }

    public IEnumerator OnDamage()
    {
        for (float i = 0; i < 0.2f; i += Time.deltaTime)
        {
            m_Vignette.intensity.value = Mathf.Lerp(m_Vignette.intensity.value, 0.5f, Time.deltaTime * 25);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            m_Vignette.intensity.value = Mathf.Lerp(m_Vignette.intensity.value, 0, Time.deltaTime * 8);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        m_Vignette.intensity.value = 0;
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
