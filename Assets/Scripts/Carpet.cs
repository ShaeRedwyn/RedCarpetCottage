using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{

    public List<CarpetPart> allCarpetParts;
    public List<HiddenSpaceCarpetPart> allHiddenCarpetParts;
    public HiddenSpace hiddenSpace;
    public CarpetPart flatCarpetPartPrefab;
    public CarpetPart fallingCarpetPartPrefab;
    public HiddenSpaceCarpetPart hiddenSpaceCarpetPart;
    public Transform carpetHolder;

    public PlayerInput playerInput;
    public LevelManager levelmanager;

    public bool hasTouchedCarpet;
    public bool canMoveFurniture;
    public bool hasTouchedSelectionFX;
    public bool hasTouchedHiddenSelectionFX;
    public bool isChoosingDirection;
    public bool isHiddenCarpetTouched;
    public bool carpetIsInHiddenSpace;
    public bool hasUnselectCarpet;

    public GameObject selectionFX;
    Vector2Int fxHiddenSpaceGridPosition = Vector2Int.zero;
    Vector3 fxHiddenSpaceWorldPosition;

    public CarpetPart initialCarpet;

    public bool hasInstantiateSelectionFx = false;

    GameObject selectionFxXPlus;
    GameObject selectionFxXMinus;
    GameObject selectionFxZPlus;
    GameObject selectionFxZMinus;

    public Orientation endCarpetOrientation;
    [Space]
    public float carpetMovingSpeed;

    private bool isRouling;

    void Start()
    {
        allCarpetParts.Add(initialCarpet);
        initialCarpet.position.x = (int)initialCarpet.transform.position.x;
        initialCarpet.position.y = (int)initialCarpet.transform.position.y;
        initialCarpet.position.z = (int)initialCarpet.transform.position.z;
        levelmanager.room[initialCarpet.position.x, initialCarpet.position.y, initialCarpet.position.z] = initialCarpet;
        canMoveFurniture = true;
        hasUnselectCarpet = false;
    }

    void Update()
    {
        if (hasTouchedCarpet == true && carpetIsInHiddenSpace == false)
        {
            if (playerInput.touchedObject == allCarpetParts[allCarpetParts.Count -1] && isChoosingDirection == false)
            {
                isChoosingDirection = true;
                if (allCarpetParts[allCarpetParts.Count - 1].position.x + 1 < levelmanager.roomDimension &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x + 1,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z] == null)
                {
                    selectionFxXPlus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(1, 0, 0), Quaternion.identity);
                    selectionFxXPlus.name = "selectionFxXPlus";
                }
                if (allCarpetParts[allCarpetParts.Count - 1].position.x - 1 >= 0 &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x - 1,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z] == null)
                {
                    selectionFxXMinus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(-1, 0, 0), Quaternion.identity);
                    selectionFxXMinus.name = "selectionFxXMinus";
                }
                if (allCarpetParts[allCarpetParts.Count - 1].position.z + 1 < levelmanager.roomDimension &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z + 1] == null)
                {
                    selectionFxZPlus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(0, 0, 1), Quaternion.identity);
                    selectionFxZPlus.name = "selectionFxZPlus";
                }
                if (allCarpetParts[allCarpetParts.Count - 1].position.z - 1 >= 0 &&
                    levelmanager.room[allCarpetParts[allCarpetParts.Count - 1].position.x,
                    allCarpetParts[allCarpetParts.Count - 1].position.y, allCarpetParts[allCarpetParts.Count - 1].position.z - 1] == null)
                {
                    selectionFxZMinus = Instantiate(selectionFX, allCarpetParts[allCarpetParts.Count - 1].position + new Vector3Int(0, 0, -1), Quaternion.identity);
                    selectionFxZMinus.name = "selectionFxZMinus";
                }
            }
            hasTouchedCarpet = false;

        }

        if (isHiddenCarpetTouched == true && carpetIsInHiddenSpace == true)
        {
            if (playerInput.hiddenTouchedObject == allHiddenCarpetParts[allHiddenCarpetParts.Count - 1])
            {
            
                if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + 1 < levelmanager.roomDimension &&
                hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + 1,
                allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Right) == false)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x +1;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxXPlus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxXPlus.name = "selectionFxXPlus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1 >= 0 &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Left) == false)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x - 1;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxXMinus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxXMinus.name = "selectionFxXMinus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + 1 < levelmanager.roomDimension &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + 1] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Forward) == false)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y  + 1;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxZPlus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxZPlus.name = "selectionFxZPlus";
            }
            if (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1 >= 0 &&
            hiddenSpace.hiddenSpace[allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x ,
            allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1 ] == null && IsPortalWallInFront(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position, Orientation.Backward) == false)
            {
                fxHiddenSpaceGridPosition.x = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x;
                fxHiddenSpaceGridPosition.y = allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y - 1;
                fxHiddenSpaceWorldPosition = hiddenSpace.GetWorldPosition(fxHiddenSpaceGridPosition);
                selectionFxZMinus = Instantiate(selectionFX, fxHiddenSpaceWorldPosition, Quaternion.identity);
                selectionFxZMinus.name = "selectionFxZMinus";
            }
            }
            isHiddenCarpetTouched = false;
            
        }

        if (hasTouchedSelectionFX == true)
        {
            isChoosingDirection = false;
            if (playerInput.touchedSelectionFx == selectionFxXPlus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    endCarpetOrientation = Orientation.Right;
                    StartCoroutine(MoveHiddenCarpetIn(Vector2.right));
                }
                else
                {
                    endCarpetOrientation = Orientation.Right;
                    StartCoroutine(MoveCarpetIn(Vector3.right));
                }
                
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxXMinus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    endCarpetOrientation = Orientation.Left;
                    StartCoroutine(MoveHiddenCarpetIn(Vector2.left));
                }
                else
                {
                    endCarpetOrientation = Orientation.Left;
                    StartCoroutine(MoveCarpetIn(Vector3.left));
                }
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZPlus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    endCarpetOrientation = Orientation.Forward;
                    StartCoroutine(MoveHiddenCarpetIn(Vector2.up));
                }
                else
                {
                    endCarpetOrientation = Orientation.Forward;
                    StartCoroutine(MoveCarpetIn(Vector3.forward));
                }
                DestroyFX();
            }
            else if (playerInput.touchedSelectionFx == selectionFxZMinus)
            {
                if (hasTouchedHiddenSelectionFX)
                {
                    endCarpetOrientation = Orientation.Backward;
                    StartCoroutine(MoveHiddenCarpetIn(Vector2.down));
                }
                else
                {
                    endCarpetOrientation = Orientation.Backward;
                    StartCoroutine(MoveCarpetIn(Vector3.back));
                }
                DestroyFX();
            }

            hasTouchedSelectionFX = false;
            canMoveFurniture = true;
        }

        if (hasUnselectCarpet == true)
        {
            isChoosingDirection = false;
            DestroyFX();
            hasTouchedSelectionFX = false;
            canMoveFurniture = true;
            hasUnselectCarpet = false;
        }

    }

    void DestroyFX()
    {
        Destroy(selectionFxXPlus);
        Destroy(selectionFxXMinus);
        Destroy(selectionFxZPlus);
        Destroy(selectionFxZMinus);
    }

    public IEnumerator MoveCarpetIn(Vector3 carpetDirection)
    {
        Vector3Int currentPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x, (allCarpetParts[allCarpetParts.Count - 1].position.y),
            (allCarpetParts[allCarpetParts.Count - 1].position.z));
        Vector3Int forwardPosition = currentPosition + new Vector3Int((int)carpetDirection.x, (int)carpetDirection.y, (int)carpetDirection.z);
        Vector3Int downwardPosition = currentPosition + Vector3Int.down;
        Vector3 edgePos;
        while ((levelmanager.IsInRoom(forwardPosition) && levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z] == null) ||
            (levelmanager.IsInRoom(downwardPosition) && levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] == null))
        {
            if(levelmanager.IsInRoom(downwardPosition) && levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] != null && levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z].type == HouseObject.Type.carpetPart)
            {
                playerInput.Fail();
            }
            isRouling = true;
            Vector3 newPos;
            bool isFalling = false;
            CarpetPart newCarpet;
            if(levelmanager.IsInRoom(downwardPosition) && levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] == null)
            {
                newCarpet = Instantiate(fallingCarpetPartPrefab, downwardPosition, Quaternion.identity, carpetHolder);
                newCarpet.position = downwardPosition;
                newCarpet.type = HouseObject.Type.carpetPart;
                allCarpetParts.Add(newCarpet);
                levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] = newCarpet;
                if(carpetDirection.x == 1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (carpetDirection.x == -1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (carpetDirection.z == 1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (carpetDirection.z == -1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                newPos = downwardPosition;
                edgePos = Vector3.Lerp(currentPosition, downwardPosition, 0.5f);
                isFalling = true;
            } 
            else if (levelmanager.IsInRoom(forwardPosition + Vector3Int.down) && levelmanager.room[forwardPosition.x, forwardPosition.y - 1, forwardPosition.z] == null)
            {
                Vector3Int nextDownPosition = forwardPosition + Vector3Int.down;
                newCarpet = Instantiate(fallingCarpetPartPrefab, nextDownPosition, Quaternion.identity, carpetHolder);
                newCarpet.position = nextDownPosition;
                newCarpet.type = HouseObject.Type.carpetPart;
                allCarpetParts.Add(newCarpet);
                levelmanager.room[nextDownPosition.x, nextDownPosition.y, nextDownPosition.z] = newCarpet;

                if (carpetDirection.x == 1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (carpetDirection.x == -1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (carpetDirection.z == 1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (carpetDirection.z == -1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                newPos = nextDownPosition;
                edgePos = Vector3.Lerp(nextDownPosition + Vector3.up, nextDownPosition, 0.5f);
                isFalling = true;
            }
            else
            {
                newCarpet = Instantiate(flatCarpetPartPrefab, forwardPosition, Quaternion.identity, carpetHolder);
                newCarpet.position = forwardPosition;
                newCarpet.type = HouseObject.Type.carpetPart;
                allCarpetParts.Add(newCarpet);
                levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z] = newCarpet;
                newPos = forwardPosition;

                if (carpetDirection.x == 1 || carpetDirection.x == -1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (carpetDirection.z == 1 || carpetDirection.z == -1)
                {
                    newCarpet.transform.rotation = Quaternion.Euler(0, 90, 0);
                }

                edgePos = Vector3.Lerp(currentPosition, forwardPosition, 0.5f);
            }
            float roulingTimer = 0;
            while(roulingTimer < carpetMovingSpeed)
            {
                newCarpet.transform.position = Vector3.Lerp(edgePos, newPos, roulingTimer / carpetMovingSpeed);
                if(isFalling)
                {
                    newCarpet.transform.localScale = new Vector3(1, Mathf.Lerp(0, 1, roulingTimer / carpetMovingSpeed), 1);
                }
                else
                {
                    newCarpet.transform.localScale = new Vector3(Mathf.Lerp(0, 1, roulingTimer / carpetMovingSpeed), 1, 1);
                }
                roulingTimer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            newCarpet.transform.localScale = Vector3.one;
            newCarpet.transform.position = newPos;
            newCarpet.position = new Vector3Int((int)newPos.x, (int)newPos.y,(int)newPos.z);

            currentPosition = new Vector3Int(allCarpetParts[allCarpetParts.Count - 1].position.x, (allCarpetParts[allCarpetParts.Count - 1].position.y),
                (allCarpetParts[allCarpetParts.Count - 1].position.z));
            forwardPosition = currentPosition + new Vector3Int((int)carpetDirection.x, (int)carpetDirection.y, (int)carpetDirection.z);
            downwardPosition = currentPosition + Vector3Int.down;

            if(isFalling)
            {
                if(!(levelmanager.IsInRoom(downwardPosition) && levelmanager.room[downwardPosition.x, downwardPosition.y, downwardPosition.z] == null))
                {
                    newCarpet = Instantiate(flatCarpetPartPrefab, carpetHolder);
                    if (carpetDirection.x == 1 || carpetDirection.x == -1)
                    {
                        newCarpet.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (carpetDirection.z == 1 || carpetDirection.z == -1)
                    {
                        newCarpet.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    edgePos = currentPosition - carpetDirection * 0.5f;
                    newPos = currentPosition;
                    roulingTimer = 0;
                    while (roulingTimer < carpetMovingSpeed)
                    {
                        newCarpet.transform.position = Vector3.Lerp(edgePos, newPos, roulingTimer / carpetMovingSpeed);
                        newCarpet.transform.localScale = new Vector3(Mathf.Lerp(0, 1, roulingTimer / carpetMovingSpeed), 1, 1);
                        roulingTimer += Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }
                    newCarpet.transform.localScale = Vector3.one;
                    newCarpet.transform.position = newPos;
                }
            }
        }
        isRouling = false;

        if (levelmanager.IsInRoom(forwardPosition) && levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z] != null && levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z].type == HouseObject.Type.furniture)
        {
            
            FurniturePart furnitureInFront = (FurniturePart)levelmanager.room[forwardPosition.x, forwardPosition.y, forwardPosition.z];
            foreach (HousePassage housePassage in furnitureInFront.allportails)
            {
                if(housePassage.orientation == endCarpetOrientation)
                {
                    housePassage.passage.PassCarpetRoomToHiddenSpace(hiddenSpaceCarpetPart);
                    
                    carpetIsInHiddenSpace = true;
                    
                }
            }
        }

    }

    public IEnumerator MoveHiddenCarpetIn(Vector2 carpetDirection)
    {
        bool portalInFront = false;
        Vector2Int currentPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x, allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y); 
        Passages potentialPassage = null;
        
        Vector2Int nextPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + (int)carpetDirection.x, (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + (int)carpetDirection.y));

        foreach(HiddenSpacePassage hiddenSpacePassage in hiddenSpace.portalSpace[nextPosition.x, nextPosition.y].hiddenSpacePassages)
        {
            if (hiddenSpacePassage.orientation == endCarpetOrientation)
            {

                potentialPassage = hiddenSpacePassage.passage;

                portalInFront = true;

            }
        }
        Vector3 edgeWorldPos;
        while (hiddenSpace.IsInHiddenSpace(nextPosition) && hiddenSpace.hiddenSpace[nextPosition.x, nextPosition.y] == null && portalInFront == false && IsPortalWallInFront(currentPosition, endCarpetOrientation) == false)
        {
            HiddenSpaceCarpetPart newCarpet;
            newCarpet = Instantiate(hiddenSpaceCarpetPart, hiddenSpace.GetWorldPosition(nextPosition), hiddenSpaceCarpetPart.transform.rotation);
            newCarpet.position = nextPosition;
            newCarpet.type = HiddenSpaceObjects.Type.carpetPart;
            allHiddenCarpetParts.Add(newCarpet);
            hiddenSpace.hiddenSpace[nextPosition.x, nextPosition.y] = newCarpet;

            Vector3 newWorldPos = hiddenSpace.GetWorldPosition(nextPosition);

            if (carpetDirection.x == 1 || carpetDirection.x == -1)
            {
                newCarpet.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (carpetDirection.y == 1 || carpetDirection.y == -1)
            {
                newCarpet.transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            edgeWorldPos = Vector3.Lerp(hiddenSpace.GetWorldPosition(currentPosition), hiddenSpace.GetWorldPosition(nextPosition), 0.5f);

            float roulingTimer = 0;
            while (roulingTimer < carpetMovingSpeed)
            {
                newCarpet.transform.position = Vector3.Lerp(edgeWorldPos, newWorldPos, roulingTimer / carpetMovingSpeed);
                newCarpet.transform.localScale = new Vector3(Mathf.Lerp(0, 1, roulingTimer / carpetMovingSpeed), 1, 1);

                roulingTimer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            newCarpet.transform.localScale = Vector3.one;
            newCarpet.transform.position = hiddenSpace.GetWorldPosition(nextPosition);
            newCarpet.position = nextPosition;

            currentPosition = nextPosition;
            nextPosition = new Vector2Int(allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.x + (int)carpetDirection.x, (allHiddenCarpetParts[allHiddenCarpetParts.Count - 1].position.y + (int)carpetDirection.y));

            if (hiddenSpace.IsInHiddenSpace(nextPosition))
            {
                foreach (HiddenSpacePassage hiddenSpacePassage in hiddenSpace.portalSpace[nextPosition.x, nextPosition.y].hiddenSpacePassages)
                {
                    if (hiddenSpacePassage.orientation == endCarpetOrientation)
                    {

                        potentialPassage = hiddenSpacePassage.passage;
                        portalInFront = true;

                    }
                }
            }
        }

        if (portalInFront)
        {
            potentialPassage.PassCarpetHiddenSpaceToRoom(flatCarpetPartPrefab);

            carpetIsInHiddenSpace = false;
        }
    }

    public bool IsPortalWallInFront(Vector2Int checkPosition, Orientation orientation)
    {
        bool wallPortalInFront = false;
        foreach (HiddenSpacePassage hiddenSpacePassage in hiddenSpace.portalSpace[checkPosition.x, checkPosition.y].hiddenSpacePassages)
        {
            switch (orientation)
            {
                case Orientation.Forward:
                    if (hiddenSpacePassage.orientation == Orientation.Backward)
                    {
                        wallPortalInFront = true;
                    }
                    break;
                case Orientation.Backward:
                    if (hiddenSpacePassage.orientation == Orientation.Forward)
                    {
                        wallPortalInFront = true;
                    }
                    break;
                case Orientation.Left:
                    if (hiddenSpacePassage.orientation == Orientation.Right)
                    {
                        wallPortalInFront = true;
                    }
                    break;
                case Orientation.Right:
                    if (hiddenSpacePassage.orientation == Orientation.Left)
                    {
                        wallPortalInFront = true;
                    }
                    break;
            }
        }
        return wallPortalInFront;
    }
}
