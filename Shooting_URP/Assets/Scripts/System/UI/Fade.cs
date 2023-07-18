using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    private Animator m_Animator;

    public string m_SceneName;

    private void Awake()
    {
        instance = this;
        m_Animator = GetComponent<Animator>();
    }
    public void FadeIn(string sceneName)
    {
        m_SceneName = sceneName;
        m_Animator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        m_Animator.SetTrigger("FadeOut");
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(m_SceneName);
    }
}
