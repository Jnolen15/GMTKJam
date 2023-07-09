using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Credits;

    public void Start()
    {
        Menu.SetActive(true);
        Credits.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeMenu()
    {
        Menu.SetActive(!Menu.activeSelf);
        Credits.SetActive(!Menu.activeSelf);
    }
}
