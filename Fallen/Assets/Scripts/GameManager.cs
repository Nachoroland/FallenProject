using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Declarar variables:
    private UIManager uiManager;
    private GameObject question;
    private TMP_Text questionText;

    private bool questActive = false;
    private bool instActive = false;
    private bool materialFase = false;

    private int orden = 1; // Orden de los mensajes o preguntas a mostrar por la UI --> define la línea a seguir del juego
    private float density = 0.1f;

    [Header("UI objects")]
    [SerializeField] public GameObject instructiosPanel;
    [SerializeField] public TMP_Text instructiosText;
    [SerializeField] public GameObject materialList;
    [SerializeField] public TMP_Text listText;
    [SerializeField] public GameObject tabla1;
    [SerializeField] public GameObject tabla2;

    // Textos GUI:
    private string text1 = "¡BIENVENIDO!\r\n\r\nLa primera fase del juego consiste en determinar el riesgo de incendio " +
        "de la nave industrial.\nPara ello necesitaremos obtener dos datos: el Nivel de Riesgo Intrínseco y " +
        "el Tipo de edificio.";
    private string text2 = "Primero calcularemos el Nivel de Riesgo Intrínseco. Para ello necesitamos obtener " +
        "la Densidad de Carga Térmica (Qs).\r\n\r\nPorfavor, busca los materiales necesarios para realizar este cálculo " +
        "y añadelos a la lista. Cuando termines, pulsa el botón ▲. ";
    private string text3 = "Si ya se has elegido todos los materiales necesarios para realizar este cálculo: " +
        "\r\n\r\n ¿Quieres cálcular la Densidad de Carga Térmica (Qs)?";
    private string text4 => $"Qs = {density} Mcal/m2\r\n\r\n ¡Recuerda este resultado!";
    private string text5 = "Según el valor de Densidad de Carga Térmica (Qs) obtenido hay que determinar el " +
        "Nivel de Riesgo Intrínseco.\r\n\r\nMira la siguiente tabla:";
    private string text6 = "Es hora de obtener el segundo dato: el Tipo de Edificio.\r\n\r\nSal fuera para " +
        "observar el entorno. Cuando estés listo pulsa ▲.";
    private string text7 = "¿Qué tipo de edificio es esta industria?\r\n\r\nMira la siguiente tabla:";
    private string text8 = "Introduce el tipo de Edificio:\r\n\r\n___B___:";
    private string textFin = "¡ENHORABUENA!\r\n\r\nHas superado la primera fase del juego.";


    // Start is called before the first frame update
    void Start()
    {
        // Obtener elementos gráficos UI:
        uiManager = FindObjectOfType<UIManager>();
        question = uiManager.panelQuestion;
        questionText = uiManager.questionText;
        instActive = true;
        instructiosPanel.SetActive(true);
        instructiosText.text = text1;
        materialList.SetActive(false);
        tabla1.SetActive(false);
        tabla2.SetActive(false);
    }

    // Update is called once per frame:
    void Update()
    {
        if (instActive && materialFase == false)
        {
            if (Input.GetButtonDown("Circulo")) // siguiente panel de Instrucciones 
            {
                //Debug.Log("Next pulsado");
                orden++;
                instructiosPanel.SetActive(false); // quitar panel
            }
        }
        
        if (Input.GetButtonDown("Triangulo")) // Botón del Cálculo 
        {
            showQuestion(orden); // pregunta si realizar cálculo  
        }
        //Debug.Log(orden);
        LogicaJuego(orden);  // Función con los distintos pasos del juego indicados por el 'orden'.

        if (questActive)
        {
            if (Input.GetButtonDown("Circulo")) // sí 
            {
                Debug.Log("Calcular Qs");
                materialList.SetActive(false);
                question.SetActive(false); // oculta panel pregunta
                density = calcularDensidad(); // Cálculo de la Densidad de carga térmica
                orden++;
                questActive = false;
                instActive = true;
                materialFase = false;
            }
            if (Input.GetButtonDown("X")) // no
            {
                question.SetActive(false); // quitar pregunta
                materialFase = true;
            }
        }
    }

    private void LogicaJuego(int orden)
    { // Según el orden, muestra la pregunta o mensaje o realiza la acción correspondiente:
        switch (orden)
        { 
            case 2:
                instructiosPanel.SetActive(true);
                instructiosText.text = text2; // se pone el texto correspondiente
                break;
            case 3: // lista materiales
                instActive = false;
                materialFase = true;
                materialList.SetActive(true);
                break;
            case 4: // Qs = resultado
                instructiosPanel.SetActive(true);
                instructiosText.text = text4; // se pone el texto correspondiente
                break;
            case 5: 
                instructiosPanel.SetActive(true);
                instructiosText.text = text5; // se pone el texto correspondiente
                break;
            case 6: // Tabla 1- NRI
                instructiosPanel.SetActive(true);
                tabla1.SetActive(true);
                break;
            case 7: // instrucciones edificio
                tabla1.SetActive(false);
                instructiosPanel.SetActive(true);
                instructiosText.text = text6; // se pone el texto correspondiente
                break;
            case 8:
                Debug.Log("edificio");
                instActive = false;
                break;
            case 9:
                tabla2.SetActive(true);
                instructiosPanel.SetActive(true);
                break;
            case 10:
                tabla2.SetActive(false);
                instructiosPanel.SetActive(true);
                instructiosText.text = text8;
                break;
            case 11:
                instructiosPanel.SetActive(true);
                instructiosText.text = textFin;
                break;
            default:
                break;
        }
    }

    private void showQuestion(int orden)
    {
        question.SetActive(true);// pregunta 
        if (orden == 3)
        {
            questionText.text = text3; // se pone el texto correspondiente

        } else if (orden == 8)
        {
            questionText.text = text7; // se pone el texto correspondiente

        }
        questActive = true;
        materialFase = false;
    }

    private float calcularDensidad() // Función para calcular de la Densidad de Carga Térmica
    {
        // Variables Datos:
        float G_i; // masa
        float q_i; // poder calorífico / Mcal
        // coeficiente adimensional que pondera el grado de peligrosidad:
        float C_i_A = 1.6f; // combustibilidad ALTA
        float C_i_B = 1.0f; // combustibilidad ALTA
        float R_a = 1.5f; // coeficiente adimensional que corrige el grado de peligrosid:
        float A = 750f; // superficie
        float correctDensity = 1253.4f;

        // CÁLCULOS:
        // 1º- se obtienen las Mcal de cada material: (creo q Gi y qi no se va a usar)
        // 2º- se multiplica por el coeficiente adimensiona Ci:
        //density = (xd/A) * R_a; // cálculo final

        density = correctDensity;
        /* / Corregir:
        if (density == correctDensity) {

        }*/
        return density;
    }
}
