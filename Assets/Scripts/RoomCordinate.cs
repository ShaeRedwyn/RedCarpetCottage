using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomCordinate 
{
    public int x, y, z;
    public Vector3 ToWorldCoordinate()
    {
        Vector3 coord;

        coord.x = x + 0.5f;
        coord.y = y + 0.5f;
        coord.z = z + 0.5f;

        return coord;
    }

    public RoomCordinate(int _x, int _y, int _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
    public RoomCordinate()
    {
        x = 0;
        y = 0;
        z = 0;
    }

    public static RoomCordinate WorldToRoomCoordinate(Vector3 pos)
    {
        RoomCordinate coord = new RoomCordinate();
        coord.x = Mathf.RoundToInt(pos.x - 0.5f);
        coord.y = Mathf.RoundToInt(pos.y - 0.5f);
        coord.z = Mathf.RoundToInt(pos.z - 0.5f);
        return coord;
    }

    public override string ToString()
    {
        return x + "-" + y + "-" + z;
    }

    public override bool Equals(object obj)
    {
        return obj is RoomCordinate cordinate &&
               x == cordinate.x &&
               y == cordinate.y &&
               z == cordinate.z;
    }

    public override int GetHashCode()
    {
        int hashCode = 373119288;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        hashCode = hashCode * -1521134295 + z.GetHashCode();
        return hashCode;
    }

    public static RoomCordinate operator +(RoomCordinate a, RoomCordinate b)
        => new RoomCordinate(a.x + b.x, a.y + b.y, a.z + b.z);


    public static RoomCordinate operator -(RoomCordinate a, RoomCordinate b)
        => new RoomCordinate(a.x - b.x, a.y - b.y, a.z - b.z);

    public static bool operator ==(RoomCordinate a, RoomCordinate b)
    {
        if (a.x == b.x && a.y == b.y && a.z == b.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(RoomCordinate a, RoomCordinate b)
    {
        if (a.x != b.x || a.y != b.y || a.z != b.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
