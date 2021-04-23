using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpaceObjects : MonoBehaviour
{
    public enum Type { carpetPart, passage, empty, wall };
    public Type type;
    public Vector2Int position;
    // vector2 qui est la position dans l'espace caché, enum public qui donne le type de l'objets " empty, carpet , passage, obstacle"
}

