using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI objects")]
    [SerializeField] public GameObject controlesButton;
    [SerializeField] public GameObject controlesManual;
    [SerializeField] public GameObject punto_rojo;
    [SerializeField] public GameObject punto_verde;
    [SerializeField] public GameObject panelQuestion;
    [SerializeField] public TMP_Text questionText;
    [SerializeField] public GameObject interctMessage;
    [SerializeField] public TMP_Text materialText;

    public static bool puntero = false;
    private bool manual = false;
    public bool SiClicked = false;
    public bool NoClicked = false;

    public void Start()
    {
        controlesButton.SetActive(true);
        punto_rojo.SetActive(true);
        // Ocultar los elementos de la UI que no queremos ver:
        controlesManual.SetActive(false);
        punto_verde.SetActive(false);
        panelQuestion.SetActive(false);
        interctMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si se activa el puntero o no:
        if (puntero == true) {
            punteroActivated(); // el puntero se pone verde
        } else if (puntero == false) { 
            punteroNormal(); // el puntero se pone rojo
        }
        // Si se saca el Manual de los controles:
        if (Input.GetButtonDown("Controles")) {
            manual = !manual;
        }
        if (manual) { showManual(); } // se muestra
        else if (!manual) { hideManual(); } // se esconde
    }

    // Funciones UI objetos:
    public void ShowMessage() // Mostrar objeto string mensaje
    {
         // Mostrar objeto
        //interacText.text = mensaje; // reescribir el texto con el mensaje
    } 
    public void punteroActivated() // activar el puntero = verde (cuando detecta un objeto con el q interactuar)
    {
        punto_rojo.SetActive(false);
        punto_verde.SetActive(true);
    }
    public void punteroNormal() // activar el puntero = verde (cuando detecta un objeto con el q interactuar)
    {
        punto_rojo.SetActive(true);
        punto_verde.SetActive(false);
    }
    public void showManual() // activar el Manual de los controles
    {
        controlesButton.SetActive(false);
        controlesManual.SetActive(true);
    }
    public void hideManual() // ocultar el Manual de los controles
    {
        controlesManual.SetActive(false);
        controlesButton.SetActive(true);
    }
    public void onSiClicked() // Cuando se clica el botón de "Sí" como respuesta a la pregunta
    {
        SiClicked = true;
    }
    public void onNoClicked() // Cuando se clica el botón de "NO" como respuesta a la pregunta
    {
        NoClicked = true;
    }
}
