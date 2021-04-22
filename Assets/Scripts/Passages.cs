using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passages : MonoBehaviour
{
    public HousePassage housePassage;
    public HiddenSpacePassage hiddenSpacePassage;
    public LevelManager levelManager;
    public HiddenSpace hiddenSpace;

    public void Start()
    {
        housePassage.passage = this;
        hiddenSpacePassage.passage = this;
    }

    public void PassCarpetRoomToHiddenSpace(HiddenSpaceCarpetPart hiddenSpaceCarpetPrefab)
    {
        Vector2Int exitPosition = hiddenSpacePassage.portalSpaceObject.position;

        switch (hiddenSpacePassage.orientation)
        {
            case Orientation.Right:
                exitPosition += Vector2Int.right;
                break;
            case Orientation.Left:
                exitPosition += Vector2Int.left;
                break;
            case Orientation.Forward:
                exitPosition += Vector2Int.up;
                break;
            case Orientation.Backward:
                exitPosition += Vector2Int.down;
                break;

        }

        HiddenSpaceCarpetPart newCarpetPart = Instantiate(hiddenSpaceCarpetPrefab, hiddenSpace.GetWorldPosition(exitPosition), hiddenSpaceCarpetPrefab.transform.rotation);
        hiddenSpace.hiddenSpace[exitPosition.x, exitPosition.y] = newCarpetPart;
    }

    // comportement de traverser le passage : quand le tapis touche le housepassage, le tapis ressort dans le hiddenpassage
}
