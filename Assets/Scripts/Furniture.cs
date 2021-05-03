using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public List<FurniturePart> allFurnitureParts;
    public PlayerInput playerInput;
    public LevelManager levelmanager;
    public bool isGroundFurniture;
    private int minimumSurfaceHeight;
    private Vector3 intersectPoint;
    private Vector2Int hoveredTile;
    private Vector3Int moveDirection;
    bool canMoveFurniture;

    public bool isBeingDragged;
    void Start()
    {
        foreach (FurniturePart furniturePart in allFurnitureParts)
        {
            furniturePart.position.x = (int)furniturePart.transform.position.x;
            furniturePart.position.y = (int)furniturePart.transform.position.y;
            furniturePart.position.z = (int)furniturePart.transform.position.z;
            levelmanager.room[furniturePart.position.x, furniturePart.position.y, furniturePart.position.z] = furniturePart;
        }

        canMoveFurniture = true;
    }

    void Update()
    {

        foreach (FurniturePart furniturePart in allFurnitureParts)
        {
            if (furniturePart.canMoveFurniture == false)
            {
                canMoveFurniture = false;
            }
        }
        if (canMoveFurniture == true)
        {


            if (isBeingDragged && Input.GetMouseButtonUp(0))
            {
                isBeingDragged = false;
            }

            if (isBeingDragged == true)
            {

                if (isGroundFurniture == true)
                {
                    minimumSurfaceHeight = 3528;
                    foreach (FurniturePart furniturePart in allFurnitureParts)
                    {
                        if (furniturePart.position.y < minimumSurfaceHeight)
                        {
                            minimumSurfaceHeight = furniturePart.position.y;
                        }
                    }

                    Plane surface = new Plane(Vector3.up, new Vector3(0, minimumSurfaceHeight, 0));

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float intersectionDistance;
                    if (surface.Raycast(ray, out intersectionDistance))
                    {

                        intersectPoint = ray.GetPoint(intersectionDistance);
                        hoveredTile = new Vector2Int(Mathf.RoundToInt(intersectPoint.x), Mathf.RoundToInt(intersectPoint.z));

                        if (allFurnitureParts[0].position.x > hoveredTile.x)
                        {
                            moveDirection = Vector3Int.left;
                        }
                        else if (allFurnitureParts[0].position.x < hoveredTile.x)
                        {
                            moveDirection = Vector3Int.right;
                        }
                        else if (allFurnitureParts[0].position.z > hoveredTile.y)
                        {
                            moveDirection = new Vector3Int(0, 0, -1);
                        }
                        else if (allFurnitureParts[0].position.z < hoveredTile.y)
                        {
                            moveDirection = new Vector3Int(0, 0, 1);
                        }
                        else
                        {
                            moveDirection = Vector3Int.zero;
                        }

                        if (moveDirection != Vector3Int.zero)
                        {
                            bool atLeastOnePartIsBlocked = false;

                            foreach (FurniturePart furniturePart in allFurnitureParts)
                            {
                                bool isSelfInFront = false;
                                foreach (FurniturePart furniturePart2 in allFurnitureParts)
                                {
                                    if (levelmanager.IsInRoom(furniturePart.position + moveDirection) &&
                                       levelmanager.room[furniturePart.position.x + moveDirection.x, furniturePart.position.y + moveDirection.y, furniturePart.position.z + moveDirection.z] == furniturePart2)
                                    {
                                        isSelfInFront = true;
                                    }
                                }

                                if ((!levelmanager.IsInRoom(furniturePart.position + moveDirection) ||
                                (levelmanager.room[furniturePart.position.x + moveDirection.x, furniturePart.position.y + moveDirection.y, furniturePart.position.z + moveDirection.z] != null && !isSelfInFront)) ||
                                (levelmanager.IsInRoom(furniturePart.position + moveDirection + Vector3Int.down) &&
                                levelmanager.room[furniturePart.position.x + moveDirection.x, furniturePart.position.y - 1 + moveDirection.y, furniturePart.position.z + moveDirection.z].type != HouseObject.Type.wall &&
                                levelmanager.room[furniturePart.position.x + moveDirection.x, furniturePart.position.y - 1 + moveDirection.y, furniturePart.position.z + moveDirection.z].type != HouseObject.Type.furniture))
                                {
                                    atLeastOnePartIsBlocked = true;
                                }
                            }

                            if (!atLeastOnePartIsBlocked)
                            {
                                foreach (FurniturePart furniturePart in allFurnitureParts)
                                {
                                    levelmanager.room[furniturePart.position.x, furniturePart.position.y, furniturePart.position.z] = null;
                                    furniturePart.position += moveDirection;

                                    levelmanager.room[furniturePart.position.x, furniturePart.position.y, furniturePart.position.z] = furniturePart;
                                    furniturePart.transform.position = new Vector3(furniturePart.position.x, furniturePart.position.y, furniturePart.position.z);
                                }
                            }
                        }

                        /*if(levelmanager.IsInRoom(new Vector3Int(hoveredTile.x,minimumSurfaceHeight, hoveredTile.y)))
                        {
                            if(levelmanager.room[hoveredTile.x, minimumSurfaceHeight, hoveredTile.y] == null)
                            {
                                Vector3Int nextPosition = new Vector3Int(hoveredTile.x, minimumSurfaceHeight, hoveredTile.y);

                                do
                                {
                                    nextPosition += Vector3Int.right;

                                    foreach (FurniturePart furniturePart in allFurnitureParts)
                                    {
                                        if(furniturePart.position == nextPosition)
                                        {
                                            furnitureInsight = true;
                                        }
                                    }

                                } while (levelmanager.IsInRoom(nextPosition) && levelmanager.room[nextPosition.x, nextPosition.y, nextPosition.z] == null);


                            }
                        }*/
                    }
                }
            }
        }
    }

    //  une fonction move qui bouge le meuble par rapport au infos que renvoie le player controller " le pc dit ah ba la souris a bougé comme ça", donc le meuble se déplace.
    // + dans la fonction move, faire des test pour voir si y'a un tapis/ d'autres meubles, la ou le joueur veux bouger le meuble.
    //rotate le meuble
}
