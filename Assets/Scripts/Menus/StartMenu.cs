using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;

    private void Start()
    {
        Cursor.visible = false;
    }
    public void StartTheGame()
    {
        transitionAnimator.Play("TransitionExit");
    }

    public void GameExit() 
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
