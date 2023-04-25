using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public static int coins;
    private bool sound, vibr;
    public Canvas settingsMenu;
    public Canvas shopMenu;
    public Transform soundNo, vibrNo;
    public Animator transition;
    //public Transform settingsIcon;
    //public Canvas homeCanvas;
    //[SerializeField] public static Text coinsCountText;

    IEnumerator SettingsExit(float time)
    {
        yield return new WaitForSeconds(time);
        settingsMenu.gameObject.SetActive(false);
    }

    IEnumerator ShopExit(float time)
    {
        yield return new WaitForSeconds(time);
        shopMenu.gameObject.SetActive(false);
    }


    ////////// Sound & Vibration //////////
    public void Sounds()
    {
        if (sound)
        {
            sound = false;
            soundNo.gameObject.SetActive(true);
        }
        else
        {
            sound = true;
            soundNo.gameObject.SetActive(false);
        }
    }

    public void Vibration()
    {
        if (vibr)
        {
            vibr = false;
            vibrNo.gameObject.SetActive(true);
        }
        else
        {
            vibr = true;
            vibrNo.gameObject.SetActive(false);
        }
    }
    /////////// Settings Menu ////////////
    public void settingsOn()
    {
        settingsMenu.gameObject.SetActive(true);
        //settingsIcon.SetParent(settingsMenu.transform);

    }
    public void settingsOff()
    {
        //settingsIcon.SetParent(transform);
        StartCoroutine(SettingsExit(0.5f));
    }

    /////////// Shop Menu ////////////
    public void shopOn()
    {
        shopMenu.gameObject.SetActive(true);
        //settingsIcon.SetParent(settingsMenu.transform);

    }
    public void shopOff()
    {
        //settingsIcon.SetParent(transform);
        StartCoroutine(ShopExit(0.5f));
    }


    // Start is called before the first frame update
    void Start()
    {
        //coins = 0;
        sound = vibr = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
