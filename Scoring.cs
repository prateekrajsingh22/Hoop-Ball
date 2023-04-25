using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoring: MonoBehaviour
{
    public GamePassScreen gamePassScreen;
    public GameFailScreen gameFailScreen;
    public Missed_It missed_It;
    public Transform Board;
    public Transform Hoop;
    public Transform HoopPass;
    public Transform HoopTouch;
    public Transform HoopFail;
    public Transform Confetti;
    public CinemachineVirtualCamera camera1;
    public static int score;
    private bool hoopTouch;
    private string result;

    public Text coinsCountText;

    public void retry_func()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        score = 0;
        LifeMgmt.life = 3;
    }


    public void nextLevel()
    {
        SceneManager.LoadScene(2);
        LifeMgmt.life = 3;
    }


    // If failed/missed, new screen/level will reload 2 seconds after
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ExecureAfterTime2(float time)
    {
        yield return new WaitForSeconds(time);
        gamePassScreen.Setup(score);

    }



    //Getting distance from centre of hoop to center of ball.
    public float DistanceFromCentre()
    {
        float distance = Vector3.Distance(Board.GetComponent<Renderer>().bounds.center, transform.position);

        return distance;
    }


    // When ball hits/crosses the hoop
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Board")
        {
            ///////////////////  Pass  ///////////////////////
            result = "Pass";

            StartGame.coins = StartGame.coins + score;
            coinsCountText.text = StartGame.coins.ToString();

            Confetti.gameObject.SetActive(true);

            if (hoopTouch)
            {
                score = (int)((1 - DistanceFromCentre()) * 70) + (LifeMgmt.life * 100);
                HoopTouch.gameObject.SetActive(true);
            }
            else
            {
                score = (int)((1 - DistanceFromCentre()) * 100) + (LifeMgmt.life * 100);
                HoopPass.gameObject.SetActive(true);
            }

            //Show level passed Screen
            StartCoroutine(ExecureAfterTime2(1.5f));

            //If ball passes hoop's location, camera won't follow the ball
            if (camera1)
                camera1.enabled = false;

        }

        else if (other.tag == "Ground" && result != "Pass")
        {
            ///////////////// Fail //////////////////
            
            result = "Fail";
            resultFail();

            if (camera1)
                camera1.enabled = false;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == Hoop)
        {
            hoopTouch = true;
        }
    }

    public void resultFail()
    {
        HoopFail.gameObject.SetActive(true);

        //Level failed screen
        if (LifeMgmt.life == 0)
        {
            gameFailScreen.Setup();
        }
        //Level failed, but there are some lives/tries remaining
        else
        {
            missed_It.display();
            StartCoroutine(ExecuteAfterTime(2f));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hoopTouch = false;
        result = "None";
        camera1 = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        coinsCountText.text = StartGame.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
