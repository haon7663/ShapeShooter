using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform m_Player;
    public float m_Speed;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(m_Player.position.x, 0), Time.deltaTime * m_Speed);
        transform.Translate(new Vector3(m_Speed, 0) * Time.deltaTime);
    }
}
