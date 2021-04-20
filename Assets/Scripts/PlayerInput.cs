using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //raycast de la souris vers l'écran pour voir si le joueur focus un meuble, envoie les infos au tapis et furniture pour les faire bouger
    Ray ray;
    public LayerMask carpetMask;
    public LayerMask furniture;
    public LayerMask selectionFX;

    public Carpet carpet;
    [HideInInspector]public HouseObject touchedObject;
    [HideInInspector] public GameObject touchedSelectionFx;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Physics.Raycast(ray, out hit, 1000f, carpetMask);


            if(hit.collider != null)
            {

                carpet.hasTouchedCarpet = true;
                touchedObject = hit.collider.GetComponent<HouseObject>();
            }

            Physics.Raycast(ray, out hit, 1000f, furniture);

            if (hit.collider != null)
            {

            }

            Physics.Raycast(ray, out hit, 1000f, selectionFX);

            if (hit.collider != null)
            {
                carpet.hasTouchedSelectionFX = true;
                touchedSelectionFx = hit.collider.gameObject;
            }

        }
        


    }
}
