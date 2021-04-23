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
    public LayerMask hiddenCarpetMask;
    

    public Carpet carpet;
    [HideInInspector]public HouseObject touchedObject;
    [HideInInspector] public Furniture touchedFurniture;
    [HideInInspector] public GameObject touchedSelectionFx;
    [HideInInspector] public HiddenSpaceObjects hiddenTouchedObject;

    public Camera cameraIso;
    public Camera camera2D;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cameraIso.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Physics.Raycast(ray, out hit, 1000f, carpetMask);


            if(hit.collider != null)
            {

                carpet.hasTouchedCarpet = true;
                touchedObject = hit.collider.GetComponent<HouseObject>();
            }



            Physics.Raycast(ray, out hit, 1000f, selectionFX);

            if (hit.collider != null)
            {

                carpet.hasTouchedSelectionFX = true;
                touchedSelectionFx = hit.collider.gameObject;
            }

            ray = camera2D.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit, 1000f, hiddenCarpetMask);


            if (hit.collider != null)
            {
             
                carpet.isHiddenCarpetTouched = true;
                hiddenTouchedObject = hit.collider.GetComponent<HiddenSpaceObjects>();
            }

            Physics.Raycast(ray, out hit, 1000f, selectionFX);


            if (hit.collider != null)
            {
                carpet.hasTouchedSelectionFX = true;
                carpet.hasTouchedHiddenSelectionFX = true;
                touchedSelectionFx = hit.collider.gameObject;
            }

        }

        if (Input.GetMouseButtonDown(0)) 
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Physics.Raycast(ray, out hit, 1000f, furniture);

            if (hit.collider != null)
            {
                touchedFurniture = hit.collider.transform.parent.GetComponent<Furniture>();

                touchedFurniture.isBeingDragged = true;
            }
        }
        


    }
}
