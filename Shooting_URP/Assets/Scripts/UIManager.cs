using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private AudioSource m_AudioSource;
    [Space]
    [Header("UI")]
    public GameObject m_Pause;
    private GameObject m_DefaultPause;
    private GameObject m_SoundSetting;
    private GameObject m_Explanation;

    private void Awake()
    {
        instance = this;
        m_DefaultPause = m_Pause.transform.GetChild(0).gameObject;
        m_SoundSetting = m_Pause.transform.GetChild(1).gameObject;
        m_Explanation = m_Pause.transform.GetChild(2).gameObject;

        m_AudioSource = GetComponent<AudioSource>();
    }

    public void GameStart(string sceneName)
    {
        Fade.instance.gameObject.SetActive(true);
        Fade.instance.FadeIn(sceneName);
        m_AudioSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        m_Pause.SetActive(Time.timeScale == 0);
        m_DefaultPause.SetActive(true);
        m_SoundSetting.SetActive(false);
        m_Explanation.SetActive(false);
        m_AudioSource.Play();
    }

    public void Setting()
    {
        m_DefaultPause.SetActive(true);
        m_SoundSetting.SetActive(false);
        m_Explanation.SetActive(false);
        m_AudioSource.Play();
    }
    public void SoundSetting()
    {
        m_DefaultPause.SetActive(false);
        m_SoundSetting.SetActive(true);
        m_Explanation.SetActive(false);
        m_AudioSource.Play();
    }
    public void Explanation()
    {
        m_DefaultPause.SetActive(false);
        m_SoundSetting.SetActive(false);
        m_Explanation.SetActive(true);
        m_AudioSource.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
