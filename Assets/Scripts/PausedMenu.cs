using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenu : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private AudioSource pauseSource;
    private Button continueButton;
    // Start is called before the first frame update
    void Start()
    {
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        pause.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            continueButton.Select();
            pause.SetActive(true);
            pauseSource.Pause();
            Time.timeScale = 0;
        }
    }

    public void PauseOff() 
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        pauseSource.Play();
    }
    public void BackToMenu() 
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
