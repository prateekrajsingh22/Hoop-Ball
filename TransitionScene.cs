using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public Animator transition;

    IEnumerator sceneTransition(int levelIndex)
    {
        transition.gameObject.SetActive(true);
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

    public void PlayGame()
    {
        StartCoroutine(sceneTransition(1));
    }


    public void homeScreen()
    {
        //SceneManager.LoadScene("HomeScreen");
        StartCoroutine(sceneTransition(0));
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
