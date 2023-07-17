using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    private Animator m_Animator;

    private void Awake()
    {
        instance = this;
        m_Animator = GetComponent<Animator>();
    }
    public void FadeIn()
    {
        m_Animator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        m_Animator.SetTrigger("FadeOut");
    }
}
