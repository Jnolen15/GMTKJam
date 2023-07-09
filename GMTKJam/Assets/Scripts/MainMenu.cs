using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Credits;
    [SerializeField] private GameObject Tutorial;

    public void Start()
    {
        Menu.SetActive(true);
        Credits.SetActive(false);
        Tutorial.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenMenu()
    {
        Menu.SetActive(true);
        Credits.SetActive(false);
        Tutorial.SetActive(false);
    }
    public void OpenCredits()
    {
        Menu.SetActive(false);
        Credits.SetActive(true);
    }
    public void OpenTutorial()
    {
        Menu.SetActive(false);
        Tutorial.SetActive(true);
    }
}
