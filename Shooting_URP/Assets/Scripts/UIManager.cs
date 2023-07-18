using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void GameStart(string sceneName)
    {
        Fade.instance.gameObject.SetActive(true);
        Fade.instance.FadeIn(sceneName);
    }
}
