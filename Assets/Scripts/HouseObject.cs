using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseObject : MonoBehaviour
{
    public enum Type { carpetPart, furniture, empty, wall };
    public Type type;
    public Vector3Int position;

    void Start()
    {
        position = WorldToPosition(transform.position);
    }

    private Vector3Int WorldToPosition(Vector3 worldPosition)
    {
        Vector3Int newPosition = Vector3Int.zero;

        newPosition.x = Mathf.RoundToInt(worldPosition.x );
        newPosition.y = Mathf.RoundToInt(worldPosition.y );
        newPosition.z = Mathf.RoundToInt(worldPosition.z );

        return newPosition;
    }

}
