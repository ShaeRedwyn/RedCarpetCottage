using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpaceObject : MonoBehaviour
{
    public Vector2Int position;
    public List<HiddenSpacePassage> hiddenSpacePassages;
    public HiddenSpace hiddenSpace;

    private void Start()
    {
        position = hiddenSpace.GetGridPosition(new Vector2Int((int)transform.position.x, (int)transform.position.z));
        hiddenSpace.portalSpace[position.x, position.y] = this;

        foreach ( HiddenSpacePassage hiddenSpacePassage in hiddenSpacePassages)
        {
            hiddenSpacePassage.portalSpaceObject = this;
        }
    }
}
