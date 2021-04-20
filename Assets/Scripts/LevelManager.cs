using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int roomDimension;
    public HouseObject[,,] room;
    public List<HouseObject> allHouseObject;

    void Awake()
    {
        room = new HouseObject[roomDimension,roomDimension,roomDimension];

    }
    // Référence tout les objets qui sont dans la pièce et leurs emplacement

    public bool IsInRoom(Vector3Int position)
    {
        bool inRoom = true;

        if(position.x >= roomDimension || position.x < 0 || position.x >= roomDimension || position.y < 0|| position.z >= roomDimension || position.z < 0)
        {
            inRoom = false;
        }

        return inRoom;
    }
}
