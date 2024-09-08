using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public void GoGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Exit()
    {
        Debug.Log("Estas saliendo del juego ");
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
