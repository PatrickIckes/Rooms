using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOFHearts;

    public Image[] hearts;
    public Sprite full;
    public Sprite empty;

    void Update()
    {
        if (hearts != null && hearts[0] != null)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].sprite = full;
                }
                else
                {
                    hearts[i].sprite = empty;
                }
                if (i < numOFHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
    }

   
}
