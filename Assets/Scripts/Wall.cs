using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : HouseObject
{
    public LevelManager levelManager;
    private void Start()
    {
        type = Type.wall;
        position = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        levelManager.room[position.x,position.y,position.z ] = this;
    }
}
