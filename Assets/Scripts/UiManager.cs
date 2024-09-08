using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject pauseGeneral;
    private bool pause;
    public float time = 0;
    public TextMeshProUGUI textoTime;


    private void Start()
    {
        Time.timeScale = 1;
        time = 0;
    }
    public void Reanudar()
    {
        pauseGeneral.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        pause = !pause;
        if (pause)
        {
            pauseGeneral.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseGeneral.SetActive(false);
            Time.timeScale = 1;
        }
    }
        
    private void Update()
    {
        time = time + Time.deltaTime;

        textoTime.text = "" + time.ToString("f0");
    }
  
}


