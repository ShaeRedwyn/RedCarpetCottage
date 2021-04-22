using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{

    public List<CarpetPart> allCarpetParts;
    public CarpetPart carpetPart;
    public HiddenSpaceCarpetPart hiddenSpaceCarpetPart;
    public Transform carpetHolder;

    public PlayerInput playerInput;
    public LevelManager levelmanager;

    public bool hasTouchedCarpet;
    public bool hasTouchedSelectionFX;
    public bool isChoosingDirection;

    public GameObject selectionFX;

    public CarpetPart initialCarpet;

    public bool hasInstantiateSelectionFx = false;

    GameObject selectionFxXPlus;
    GameObject selectionFxXMinus;
    GameObject selectionFxZPlus;
    GameObject selectionFxZMinus;

    public Orientation endCarpetOrientation;

    void Start()
    {
        allCarpetParts.Add(initialCarpet);
        initialCarpet.position.x = (int)initialCarpet.transform.position.x;
        initialCarpet.position.y = (int)initialCarpet.transform.position.y;
        initialCarpet.position.z = (int)initialCarpet.transform.position.z;
        levelmanager.room[initialCarpet.position.x, initialCarpet.position.y, initialCarpet.position.z] = initialCarpet;
    }

    void Update()
    {
        if (hasTouchedCarpet == true)
        {
            if (playerInput.touchedObject == allCarpetParts[allCarpetParts.Count -1] && isChoosingDirection == false)
            {
                isChoosingDirection = true;
                if (allCarpetParts[allCarpetParts.Count - 1].position.x + 1 < levelmanager.roomDimension &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x + 1,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z] == null)
                {
                    selectionFxXPlus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(1, 0, 0), Quaternion.identity);
                    selectionFxXPlus.name = "selectionFxXPlus";
                }
                if (allCarpetParts[allCarpetParts.Count - 1].position.x - 1 >= 0 &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x - 1,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z] == null)
                {
                    selectionFxXMinus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(-1, 0, 0), Quaternion.identity);
                    selectionFxXMinus.name = "selectionFxXMinus";
                }
                if (allCarpetParts[allCarpetParts.Count - 1].position.z + 1 < levelmanager.roomDimension &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z + 1] == null)
                {
                    selectionFxZPlus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(0, 0, 1), Quaternion.identity);
                    selectionFxZPlus.name = "selectionFxZPlus";
                }
                if (allCarpetParts[allCarpetParts.Count - 1].position.z - 1 >= 0 &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z - 1] == null)
                {
                    selectionFxZMinus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(0, 0, -1), Quaternion.identity);
                    selectionFxZMinus.name = "selectionFxZMinus";
                }
            }
            hasTouchedCarpet = false;

        }

        if (hasTouchedSelectionFX == true)
        {
            isChoosingDirection = false;

            if (playerInput.touchedSelectionFx == selectionFxXPlus)
            {
                MoveCarpetIn( selectionFxXPlus);
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxXMinus)
            {
                MoveCarpetIn(selectionFxXMinus);
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZPlus)
            {
                MoveCarpetIn(selectionFxZPlus);
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZMinus)
            {
                MoveCarpetIn(selectionFxZMinus);
                DestroyFX();
            }

            hasTouchedSelectionFX = false;
        }



    }

    void DestroyFX()
    {
        Destroy(selectionFxXPlus);
        Destroy(selectionFxXMinus);
        Destroy(selectionFxZPlus);
        Destroy(selectionFxZMinus);
    }

    void MoveCarpetIn(GameObject selectedFx)
    {

        Vector3 selectedFxDirection = Vector3.zero;

        if(selectedFx == selectionFxXPlus)
        {
            selectedFxDirection = Vector3.right;
            endCarpetOrientation = Orientation.Right;
        }
        if (selectedFx == selectionFxXMinus)
        {
            selectedFxDirection = Vector3.left;
            endCarpetOrientation = Orientation.Left;
        }
        if (selectedFx == selectionFxZPlus)
        {
            selectedFxDirection = Vector3.forward;
            endCarpetOrientation = Orientation.Forward;
        }
        if (selectedFx == selectionFxZMinus)
        {
            selectedFxDirection = Vector3.back;
            endCarpetOrientation = Orientation.Backward;
        }

        Vector3Int nextPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x + (int)selectedFxDirection.x, (allCarpetParts[allCarpetParts.Count - 1].position.y + (int)selectedFxDirection.y),
            (allCarpetParts[allCarpetParts.Count - 1].position.z) + (int)selectedFxDirection.z);

        while (levelmanager.IsInRoom(nextPosition) && levelmanager.room[nextPosition.x, nextPosition.y, nextPosition.z] == null)
        {

            CarpetPart newCarpet;
            newCarpet = Instantiate(carpetPart, nextPosition, Quaternion.identity, carpetHolder);
            newCarpet.position = nextPosition;
            newCarpet.type = HouseObject.Type.carpetPart;
            allCarpetParts.Add(newCarpet);
            levelmanager.room[nextPosition.x, nextPosition.y, nextPosition.z] = newCarpet;
            nextPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x + (int)selectedFxDirection.x, (allCarpetParts[allCarpetParts.Count - 1].position.y + (int)selectedFxDirection.y),
            (allCarpetParts[allCarpetParts.Count - 1].position.z) + (int)selectedFxDirection.z);
        }


        if(levelmanager.IsInRoom(nextPosition) && levelmanager.room[nextPosition.x, nextPosition.y, nextPosition.z].type == HouseObject.Type.furniture)
        {
            
            FurniturePart furnitureInFront = (FurniturePart)levelmanager.room[nextPosition.x, nextPosition.y, nextPosition.z];
            foreach (HousePassage housePassage in furnitureInFront.allportails)
            {
                if(housePassage.orientation == endCarpetOrientation)
                {
                    housePassage.passage.PassCarpetRoomToHiddenSpace(hiddenSpaceCarpetPart);
                }
            }
        }

    }
}
