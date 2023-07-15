using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffect : MonoBehaviour
{
    public static CameraEffect instance;

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
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    public IEnumerator OnDamage()
    {
        for (float i = 0; i < 0.15f; i += Time.deltaTime)
        {
            m_Vignette.intensity.value = Mathf.Lerp(m_Vignette.intensity.value, 0.3f, Time.deltaTime * 25);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            m_Vignette.intensity.value = Mathf.Lerp(m_Vignette.intensity.value, 0, Time.deltaTime * 10);
            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
        m_Vignette.intensity.value = 0;
    }
    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
