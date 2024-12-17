using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public Health m_Health;
    public Level m_Level;
    public Ultimate m_Ultimate;
    public DrawPolygon m_DrawPolygon;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(m_Ultimate.m_InvisbleTime > 0)
            {
                m_Ultimate.m_InvisbleTime = 0;
            }
            else m_Ultimate.m_InvisbleTime = 99999;
        }
        if (Input.GetKeyDown(KeyCode.F2) && m_DrawPolygon.m_AngleCount < 12)
        {
            m_Level.AddExp(m_Level.m_NeedExp - m_Level.m_Exp);
        }
        if (Input.GetKeyDown(KeyCode.F3) && m_DrawPolygon.m_AngleCount > 3)
        {
            m_DrawPolygon.m_AngleCount--;
            m_DrawPolygon.ChangeAngle();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            m_Health.curhp = m_Health.maxhp;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Instantiate(GameManager.instance.ItemPersent(100), new Vector3(10, 0), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            m_Ultimate.m_UltimateTime = 90;
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            GameManager.instance.m_StageCount = GameManager.instance.m_StageCount == 1 ? 0 : 1;
            GameManager.instance.m_WaveCount = 0;
            Camera.main.transform.GetComponent<CameraEffect>().ChangeColor();
            BackgroundScrolling.instance.SetColor();
            GameManager.instance.WavingD();
        }
    }
}
