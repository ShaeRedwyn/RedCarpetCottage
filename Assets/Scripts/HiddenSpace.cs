using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpace : MonoBehaviour
{
    public HiddenSpaceObjects[,]  hiddenSpace;
    public LevelManager levelManager;
    public PortalSpaceObject[,] portalSpace;
    public Vector2Int gridOffset;

    public void Awake()
    {
        hiddenSpace = new HiddenSpaceObjects[levelManager.roomDimension,levelManager.roomDimension];
        portalSpace = new PortalSpaceObject[levelManager.roomDimension, levelManager.roomDimension];
    }
    // Script qui fait la division de l'espace caché en cases ( tableau à 2 dimension )
    // Référence tout les objets qui sont dans l'espace et leurs emplacement
    public bool IsInHiddenSpace(Vector2Int position)
    {
        bool inHiddenSpace = true;

        if (position.x >= levelManager.roomDimension || position.x < 0 || position.y >= levelManager.roomDimension || position.y < 0)
        {
            inHiddenSpace = false;
        }

        return inHiddenSpace;
    }

    public Vector2Int GetGridPosition(Vector2Int position)
    {
        Vector2Int correctPosition;

        correctPosition = position - gridOffset;

        return correctPosition;
    }

    public Vector3 GetWorldPosition(Vector2Int position)
    {
        Vector3 correctPosition;

        correctPosition = new Vector3(position.x + gridOffset.x, 0.1f, position.y + gridOffset.y);

        return correctPosition;
    }
}