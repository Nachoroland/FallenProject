using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Variables de Raycast:
    LayerMask mask;
    private new Transform camera;
    public float rayDistance;

    void Start()
    {
        mask = LayerMask.GetMask("Interactable");
        camera = transform.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.position, camera.forward * rayDistance, Color.red); // dibujar rayo
        // Raycast(origen, dirección, out hit, distancia, máscara)
        RaycastHit hit; // declarar
        if (Physics.Raycast(camera.position, camera.forward, out hit, rayDistance, mask))
        {
            Deselected();
            Debug.Log("object");
            Selected(); // activar puntero
            if (Input.GetButtonDown("Cuadrado")) // Mostrar el mensaje del Material:
            {
                Debug.Log("Cuadrado pulsado");
                hit.transform.GetComponent<Interactable>().Interact();
            }
        }
        else
        {
            //Debug.Log("nada");
            Deselected(); // Si no hay colisión dentro del rango, desactiva el elemento de la interfaz:
        }
    }

    void Selected() {
        UIManager.puntero = true;
    }
    void Deselected()
    {
        UIManager.puntero = false;
    }
}
