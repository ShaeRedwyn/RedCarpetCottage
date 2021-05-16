using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePart : HouseObject
{
    public List<HousePassage> allportails;
    public bool canMoveFurniture;
    private void Start()
    {
        type = Type.furniture;
        foreach(HousePassage passage in allportails)
        {
            passage.furniturePart = this;
        }
        canMoveFurniture = true;
    }
    // dérive de house object, possède une liste de passages avec leurs orientations, référence à la furniture mère
    //
}
