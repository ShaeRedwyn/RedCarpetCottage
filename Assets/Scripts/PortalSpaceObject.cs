using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpaceObject 
{
    public Vector2Int position;
    public List<HiddenSpacePassage> hiddenSpacePassages = new List<HiddenSpacePassage>();
    public HiddenSpace hiddenSpace;
}
