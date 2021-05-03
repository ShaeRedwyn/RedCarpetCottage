using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passages : MonoBehaviour
{
    public HousePassage housePassage;
    public HiddenSpacePassage hiddenSpacePassage;
    public LevelManager levelManager;
    public HiddenSpace hiddenSpace;
    public Carpet carpet;
    

    public void Start()
    {
        housePassage.passage = this;
        hiddenSpacePassage.passage = this;
    }

    private void Update()
    {
        UpdateHiddenPassagePosition();
    }

    private void UpdateHiddenPassagePosition()
    {

        if (hiddenSpacePassage.portalSpaceObject == null || hiddenSpacePassage.portalSpaceObject.position.x == housePassage.furniturePart.position.x ||
            hiddenSpacePassage.portalSpaceObject.position.y == housePassage.furniturePart.position.z || hiddenSpacePassage.orientation == housePassage.orientation )
        {
            if(hiddenSpacePassage.portalSpaceObject != null)
            {
                hiddenSpacePassage.portalSpaceObject.hiddenSpacePassages.Remove(hiddenSpacePassage);
            }

            hiddenSpacePassage.portalSpaceObject = hiddenSpace.portalSpace[housePassage.furniturePart.position.x,housePassage.furniturePart.position.z] ;
            hiddenSpacePassage.transform.position = hiddenSpace.GetWorldPosition(hiddenSpacePassage.portalSpaceObject.position);
            hiddenSpacePassage.portalSpaceObject.hiddenSpacePassages.Add(hiddenSpacePassage);

            switch (housePassage.orientation)
            {
                case Orientation.Right:
                    hiddenSpacePassage.orientation = Orientation.Right;
                    hiddenSpacePassage.transform.position += Vector3.left*0.5f +Vector3.up*0.1f;
                    hiddenSpacePassage.transform.rotation = Quaternion.Euler(90, 0, 180);
                    break;
                case Orientation.Left:
                    hiddenSpacePassage.orientation = Orientation.Left;
                    hiddenSpacePassage.transform.position += Vector3.right * 0.5f + Vector3.up * 0.1f;
                    hiddenSpacePassage.transform.rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case Orientation.Forward:
                    hiddenSpacePassage.orientation = Orientation.Forward;
                    hiddenSpacePassage.transform.position += Vector3.back * 0.5f + Vector3.up * 0.1f;
                    hiddenSpacePassage.transform.rotation = Quaternion.Euler(90, 0, -90);
                    break;
                case Orientation.Backward:
                    hiddenSpacePassage.orientation = Orientation.Backward;
                    hiddenSpacePassage.transform.position += Vector3.forward * 0.5f + Vector3.up * 0.1f;
                    hiddenSpacePassage.transform.rotation = Quaternion.Euler(90, 0, 90);
                    break;
            }
        }
    }

    public void PassCarpetRoomToHiddenSpace(HiddenSpaceCarpetPart hiddenSpaceCarpetPrefab)
    {
        Vector2Int exitPosition = hiddenSpacePassage.portalSpaceObject.position;
        Vector2Int carpetdirection = Vector2Int.zero;

        switch (hiddenSpacePassage.orientation)
        {
            case Orientation.Right:
                carpetdirection = Vector2Int.left;
                break;
            case Orientation.Left:
                carpetdirection = Vector2Int.right;
                break;
            case Orientation.Forward:
                carpetdirection = Vector2Int.down;
                break;
            case Orientation.Backward:
                carpetdirection = Vector2Int.up;
                break;

        }
        exitPosition += carpetdirection;
        HiddenSpaceCarpetPart newCarpetPart = Instantiate(hiddenSpaceCarpetPrefab, hiddenSpace.GetWorldPosition(exitPosition), hiddenSpaceCarpetPrefab.transform.rotation);
        hiddenSpace.hiddenSpace[exitPosition.x , exitPosition.y] = newCarpetPart;
        newCarpetPart.position = exitPosition;
        carpet.allHiddenCarpetParts.Add(newCarpetPart);
        carpet.MoveHiddenCarpetIn(carpetdirection);

        housePassage.furniturePart.canMoveFurniture = false;
    }

    public void PassCarpetHiddenSpaceToRoom(CarpetPart carpetPartPrefab)
    {
        Vector3Int exitPosition = housePassage.furniturePart.position;
        Vector3Int carpetdirection = Vector3Int.zero;

        switch (hiddenSpacePassage.orientation)
        {
            case Orientation.Right:
                carpetdirection = Vector3Int.left;
                break;
            case Orientation.Left:
                carpetdirection = Vector3Int.right;
                break;
            case Orientation.Forward:
                carpetdirection = new Vector3Int(0,0,-1);
                break;
            case Orientation.Backward:
                carpetdirection = new Vector3Int(0,0,1);
                break;

        }
        exitPosition += carpetdirection;
        CarpetPart newCarpetPart = Instantiate(carpetPartPrefab, exitPosition, carpetPartPrefab.transform.rotation);
        levelManager.room[exitPosition.x, exitPosition.y, exitPosition.z] = newCarpetPart;
        newCarpetPart.position = exitPosition;
        carpet.allCarpetParts.Add(newCarpetPart);
        carpet.MoveCarpetIn(carpetdirection);

        housePassage.furniturePart.canMoveFurniture = false;
    }
    // comportement de traverser le passage : quand le tapis touche le housepassage, le tapis ressort dans le hiddenpassage
}
