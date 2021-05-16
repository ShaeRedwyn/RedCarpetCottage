using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpaceWalls : HiddenSpaceObjects
{
    public HiddenSpace hiddenSpace;
    Vector2Int temporaryPosition;
    void Start()
    {
        type = Type.wall;
        temporaryPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
        position = hiddenSpace.GetGridPosition(temporaryPosition);
        
        hiddenSpace.hiddenSpace[position.x, position.y] = this;
    }

}
