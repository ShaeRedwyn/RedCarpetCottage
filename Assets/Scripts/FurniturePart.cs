using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePart : HouseObject
{
    public List<HousePassage> allportails;
    private void Start()
    {
        type = HouseObject.Type.furniture;
    }
    // dérive de house object, possède une liste de passages avec leurs orientations, référence à la furniture mère
    //
}
