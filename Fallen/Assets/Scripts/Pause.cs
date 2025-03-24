using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [Header("Pause")]
    private bool pause = false;
    public GameObject panelPause;
    //public GameObject music;
    private PlayerMovement playerScript;

    void Start()
    {
        panelPause.SetActive(false);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Pause
        if (Input.GetButtonDown("Pause"))
        {
            pause = !pause;
        }
        if (pause)
        {
            Time.timeScale = 0;
            playerScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            panelPause.SetActive(true);
            //music.SetActive(false);
            if (Input.GetButtonDown("Circulo"))
            {
                Debug.Log("Main Menú");
                menu();
            }
        }
        else if (!pause)
        {
            Time.timeScale = 1;
            playerScript.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            panelPause.SetActive(false);
            //music.SetActive(true);
        }
    }

    public void ApplicationPause()
    {
        pause = !pause;
    }
    public void menu()
    {
        SceneManager.LoadScene("MainMenu"); // Volver al Menú Principal
    }
}
