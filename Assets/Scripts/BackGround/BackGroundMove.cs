using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    private void Start()
    {
        BackgroundScrolling.instance.m_Moves.Add(transform);
    }

    private void OnDisable()
    {
        BackgroundScrolling.instance.m_Moves.Remove(transform);
    }
}
