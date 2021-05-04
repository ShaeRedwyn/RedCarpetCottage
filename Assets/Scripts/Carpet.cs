﻿using System.Collections;
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
    public bool canMoveFurniture;
    public bool hasTouchedSelectionFX;
    public bool hasTouchedHiddenSelectionFX;
    public bool isChoosingDirection;
    public bool isHiddenCarpetTouched;
    public bool carpetIsInHiddenSpace;
    public bool hasUnselectCarpet;

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
        canMoveFurniture = true;
        hasUnselectCarpet = false;
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
                allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Right) == false)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x +1;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxXPlus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxXPlus.name = "selectionFxXPlus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1 >= 0 &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Left) == false)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxXMinus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxXMinus.name = "selectionFxXMinus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + 1 < levelmanager.roomDimension &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + 1] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Forward) == false)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y  + 1;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxZPlus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxZPlus.name = "selectionFxZPlus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1 >= 0 &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x ,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1 ] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Backward) == false)
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
                    endCarpetOrientation = Orientation.Right;
                    MoveHiddenCarpetIn(Vector2.right);
                }
                else
                {
                    endCarpetOrientation = Orientation.Right;
                    MoveCarpetIn(Vector3.right);
                }
                
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxXMinus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    endCarpetOrientation = Orientation.Left;
                    MoveHiddenCarpetIn(Vector2.left);
                }
                else
                {
                    endCarpetOrientation = Orientation.Left;
                    MoveCarpetIn(Vector3.left);
                }
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZPlus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    endCarpetOrientation = Orientation.Forward;
                    MoveHiddenCarpetIn(Vector2.up);
                }
                else
                {
                    endCarpetOrientation = Orientation.Forward;
                    MoveCarpetIn(Vector3.forward);
                }
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZMinus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    endCarpetOrientation = Orientation.Backward;
                    MoveHiddenCarpetIn(Vector2.down);
                }
                else
                {
                    endCarpetOrientation = Orientation.Backward;
                    MoveCarpetIn(Vector3.back);
                }
                DestroyFX();
            }

            hasTouchedSelectionFX = false;
            canMoveFurniture = true;
        }

        if (hasUnselectCarpet == true)
        {
            isChoosingDirection = false;
            DestroyFX();
            hasTouchedSelectionFX = false;
            canMoveFurniture = true;
            hasUnselectCarpet = false;
        }

    }

    void DestroyFX()
    {
        Destroy(selectionFxXPlus);
        Destroy(selectionFxXMinus);
        Destroy(selectionFxZPlus);
        Destroy(selectionFxZMinus);
    }

    public void MoveCarpetIn(Vector3 carpetDirection)
    {

        Vector3Int forwardPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x + (int)carpetDirection.x, (allCarpetParts[allCarpetParts.Count - 1].position.y + (int)carpetDirection.y),
            (allCarpetParts[allCarpetParts.Count - 1].position.z) + (int)carpetDirection.z);
        Vector3Int downwardPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x, (allCarpetParts[allCarpetParts.Count - 1].position.y - 1),
            (allCarpetParts[allCarpetParts.Count - 1].position.z));

        while (levelmanager.IsInRoom(forwardPosition) && levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z] == null &&
            (!levelmanager.IsInRoom(downwardPosition) || levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] != null))
        {
            CarpetPart newCarpet;
            if(levelmanager.IsInRoom(downwardPosition) && levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] == null)
            {
                newCarpet = Instantiate(carpetPart, downwardPosition, Quaternion.identity, carpetHolder);
                newCarpet.position = downwardPosition;
                newCarpet.type = HouseObject.Type.carpetPart;
                allCarpetParts.Add(newCarpet);
                levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] = newCarpet;
            } 
            else if (levelmanager.IsInRoom(forwardPosition + Vector3Int.down) && levelmanager.room[forwardPosition.x, forwardPosition.y - 1, forwardPosition.z] == null)
            {
                Vector3Int nextDownPosition = forwardPosition + Vector3Int.down;
                newCarpet = Instantiate(carpetPart, nextDownPosition, Quaternion.identity, carpetHolder);
                newCarpet.position = nextDownPosition;
                newCarpet.type = HouseObject.Type.carpetPart;
                allCarpetParts.Add(newCarpet);
                levelmanager.room[nextDownPosition.x, nextDownPosition.y, nextDownPosition.z] = newCarpet;
            }
            else
            {
                newCarpet = Instantiate(carpetPart, forwardPosition, Quaternion.identity, carpetHolder);
                newCarpet.position = forwardPosition;
                newCarpet.type = HouseObject.Type.carpetPart;
                allCarpetParts.Add(newCarpet);
                levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z] = newCarpet;
            }
            forwardPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x + (int)carpetDirection.x, (allCarpetParts[allCarpetParts.Count - 1].position.y + (int)carpetDirection.y),
            (allCarpetParts[allCarpetParts.Count - 1].position.z) + (int)carpetDirection.z);
            downwardPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x, (allCarpetParts[allCarpetParts.Count - 1].position.y - 1),
            (allCarpetParts[allCarpetParts.Count - 1].position.z));

        }


        if(levelmanager.IsInRoom(forwardPosition) && levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z].type == HouseObject.Type.furniture)
        {
            
            FurniturePart furnitureInFront = (FurniturePart)levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z];
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

    public void MoveHiddenCarpetIn(Vector2 carpetDirection)
    {
        bool portalInFront = false;
        Vector2Int currentPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x, allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y); 
        Passages potentialPassage = null;
        
        Vector2Int nextPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + (int)carpetDirection.x, (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + (int)carpetDirection.y));
        foreach(HiddenSpacePassage hiddenSpacePassage in hiddenSpace.portalSpace[nextPosition.x, nextPosition.y].hiddenSpacePassages)
        {
            if (hiddenSpacePassage.orientation == endCarpetOrientation)
            {

                potentialPassage = hiddenSpacePassage.passage;

                portalInFront = true;

            }
        }
       
        while (hiddenSpace.IsInHiddenSpace(nextPosition) && hiddenSpace.hiddenSpace[nextPosition.x, nextPosition.y] == null && portalInFront == false && IsPortalWallInFront(currentPosition, endCarpetOrientation) == false)
        {
            HiddenSpaceCarpetPart newCarpet;
            newCarpet = Instantiate(hiddenSpaceCarpetPart, hiddenSpace.GetWorldPosition(nextPosition), hiddenSpaceCarpetPart.transform.rotation);
            newCarpet.position = nextPosition;
            newCarpet.type = HiddenSpaceObjects.Type.carpetPart;
            allHiddenCarpetParts.Add(newCarpet);
            hiddenSpace.hiddenSpace[nextPosition.x, nextPosition.y] = newCarpet;
            currentPosition = nextPosition;
            nextPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + (int)carpetDirection.x, (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + (int)carpetDirection.y));
            if (hiddenSpace.IsInHiddenSpace(nextPosition))
            {
                foreach (HiddenSpacePassage hiddenSpacePassage in hiddenSpace.portalSpace[nextPosition.x, nextPosition.y].hiddenSpacePassages)
                {
                    if (hiddenSpacePassage.orientation == endCarpetOrientation)
                    {

                        potentialPassage = hiddenSpacePassage.passage;
                        portalInFront = true;

                    }
                }
            }
        }

        if (portalInFront)
        {
            potentialPassage.PassCarpetHiddenSpaceToRoom(carpetPart);

            carpetIsInHiddenSpace = false;
        }
    }

    public bool IsPortalWallInFront(Vector2Int checkPosition, Orientation orientation)
    {
        bool wallPortalInFront = false;
        foreach (HiddenSpacePassage hiddenSpacePassage in hiddenSpace.portalSpace[checkPosition.x, checkPosition.y].hiddenSpacePassages)
        {
            switch (orientation)
            {
                case Orientation.Forward:
                    if (hiddenSpacePassage.orientation == Orientation.Backward)
                    {
                        wallPortalInFront = true;
                    }
                    break;
                case Orientation.Backward:
                    if (hiddenSpacePassage.orientation == Orientation.Forward)
                    {
                        wallPortalInFront = true;
                    }
                    break;
                case Orientation.Left:
                    if (hiddenSpacePassage.orientation == Orientation.Right)
                    {
                        wallPortalInFront = true;
                    }
                    break;
                case Orientation.Right:
                    if (hiddenSpacePassage.orientation == Orientation.Left)
                    {
                        wallPortalInFront = true;
                    }
                    break;
            }
        }
        return wallPortalInFront;
    }
}
