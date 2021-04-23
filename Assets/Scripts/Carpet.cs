using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{

    public List<CarpetPart> allCarpetParts;
    public List<HiddenSpaceCarpetPart> allHiddenCarpetParts;
    public HiddenSpace hiddenSpace;
    public CarpetPart carpetPart;
    public HiddenSpaceCarpetPart hiddenSpaceCarpetPart;
    public Transform carpetHolder;

    public PlayerInput playerInput;
    public LevelManager levelmanager;

    public bool hasTouchedCarpet;
    public bool hasTouchedSelectionFX;
    public bool hasTouchedHiddenSelectionFX;
    public bool isChoosingDirection;
    public bool isHiddenCarpetTouched;
    public bool carpetIsInHiddenSpace;

    public GameObject selectionFX;
    Vector2Int fxHiddenSpaceGridPosition = Vector2Int.zero;
    Vector3 fxHiddenSpaceWorldPosition;

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
        if (hasTouchedCarpet == true && carpetIsInHiddenSpace == false)
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
        if (isHiddenCarpetTouched == true && carpetIsInHiddenSpace == true)
        {
            if (playerInput.hiddenTouchedObject == allHiddenCarpetParts[allHiddenCarpetParts.Count - 1])
            {
            
                if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + 1 < levelmanager.roomDimension &&
                hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + 1,
                allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y] == null)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x +1;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxXPlus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxXPlus.name = "selectionFxXPlus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1 >= 0 &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y] == null)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxXMinus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxXMinus.name = "selectionFxXMinus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + 1 < levelmanager.roomDimension &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + 1] == null)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y  + 1;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxZPlus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxZPlus.name = "selectionFxZPlus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1 >= 0 &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x ,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1 ] == null)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxZMinus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxZMinus.name = "selectionFxZMinus";
            }
            }
            isHiddenCarpetTouched = false;
            
        }

        if (hasTouchedSelectionFX == true)
        {
            isChoosingDirection = false;

            if (playerInput.touchedSelectionFx == selectionFxXPlus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    MoveHiddenCarpetIn(selectionFxXPlus);
                }
                else
                {
                    MoveCarpetIn(selectionFxXPlus);
                }
                
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxXMinus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    MoveHiddenCarpetIn(selectionFxXMinus);
                }
                else
                {
                    MoveCarpetIn(selectionFxXMinus);
                }
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZPlus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    MoveHiddenCarpetIn(selectionFxZPlus);
                }
                else
                {
                    MoveCarpetIn(selectionFxZPlus);
                }
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZMinus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    MoveHiddenCarpetIn(selectionFxZMinus);
                }
                else
                {
                    MoveCarpetIn(selectionFxZMinus);
                }
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
                    carpetIsInHiddenSpace = true;
                    
                }
            }
        }

    }

    void MoveHiddenCarpetIn(GameObject selectedFx)
    {
        Vector2 selectedFxDirection = Vector2.zero;

        if (selectedFx == selectionFxXPlus)
        {
            selectedFxDirection = Vector2.right;
            endCarpetOrientation = Orientation.Right;
        }
        if (selectedFx == selectionFxXMinus)
        {
            selectedFxDirection = Vector2.left;
            endCarpetOrientation = Orientation.Left;
        }
        if (selectedFx == selectionFxZPlus)
        {
            selectedFxDirection = Vector2.up;
            endCarpetOrientation = Orientation.Forward;
        }
        if (selectedFx == selectionFxZMinus)
        {
            selectedFxDirection = Vector2.down;
            endCarpetOrientation = Orientation.Backward;
        }

        Vector2Int nextPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + (int)selectedFxDirection.x, (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + (int)selectedFxDirection.y));
        Debug.Log(nextPosition);
        while (hiddenSpace.IsInHiddenSpace(nextPosition) && hiddenSpace.hiddenSpace[nextPosition.x, nextPosition.y] == null)
        {

            HiddenSpaceCarpetPart newCarpet;
            newCarpet = Instantiate(hiddenSpaceCarpetPart, hiddenSpace.GetWorldPosition(nextPosition), hiddenSpaceCarpetPart.transform.rotation);
            newCarpet.position = nextPosition;
            newCarpet.type = HiddenSpaceObjects.Type.carpetPart;
            allHiddenCarpetParts.Add(newCarpet);
            hiddenSpace.hiddenSpace[nextPosition.x, nextPosition.y] = newCarpet;
            nextPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + (int)selectedFxDirection.x, (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + (int)selectedFxDirection.y));
        }
    }
}
