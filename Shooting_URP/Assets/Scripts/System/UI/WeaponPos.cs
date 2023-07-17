using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPos : MonoBehaviour
{
    public Sprite[] m_WeaponSprite;
    public Image[] m_WeaponImage;

    public Color m_DefaultColor;
    public Color m_FullColor;


    public void SetWeapon(int count, int index)
    {
        if(index == 0)
        {
            m_WeaponImage[0].sprite = m_WeaponSprite[index];
            for (int i = 1; i < 5; i++)
            {
                m_WeaponImage[i].enabled = false;
            }
        }
        else
        {
            for (int i = 1; i < 5; i++)
            {
                m_WeaponImage[i].enabled = true;
            }
        }
        for(int i = 0; i < 5; i++)
        {
            m_WeaponImage[i].sprite = m_WeaponSprite[index];
            m_WeaponImage[i].color = count >= 4 ? m_FullColor : (i <= count ? Color.white : m_DefaultColor);
        }
    }
}
