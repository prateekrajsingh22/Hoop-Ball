using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeMgmt : MonoBehaviour
{
    public static int life = 3;
    public Image[] balls;
    public static int starsCounter;

    public Sprite fullLife;
    public Sprite emptyLife;

    public void method()
    {
        foreach (Image img in balls)
        {
            img.sprite = emptyLife;
        }
        for (int i = 0; i < life; i++)
        {
            balls[i].sprite = fullLife;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        method();
    }
}
