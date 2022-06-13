using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField, ReadOnly, Foldout("Info")] Interactable interactable;
    [SerializeField, ReadOnly, Foldout("Info")] bool pointerOverCloseUp;

    int roomLayerMask, interactableLayerMask, closeUpLayerMask, totalShipLayerMask;
    Camera mainCam;

    public static System.Action ClickOutsideOfCloseUpEvent;
    public static System.Action<bool> ClickedInTotalViewEvent;

    private void Awake()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        interactableLayerMask = ~LayerMask.GetMask("Room", "Ignore Raycast", "CloseUp", "TotalShip");
        closeUpLayerMask = LayerMask.GetMask("CloseUp");
        totalShipLayerMask = LayerMask.GetMask("TotalShip");
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //pointer is over ui;
        bool pointerOverUI = EventSystem.current.IsPointerOverGameObject();

        //calculate cursor position
        Vector3 cursorPoint = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCam.transform.position.z));

        if (GameModeManager.Current == GameMode.Play)
            UpdatePlayModeInteractions(pointerOverUI, cursorPoint);
        else if (GameModeManager.Current == GameMode.Total)
            UpdateTotalModeInteractions(cursorPoint);
    }

    private void UpdateTotalModeInteractions(Vector3 cursorPoint)
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool outsideOfShip = (Physics2D.Raycast(cursorPoint, Vector2.zero, float.MaxValue, layerMask: totalShipLayerMask).collider == null);
            ClickedInTotalViewEvent?.Invoke(outsideOfShip);
        }
    }

    private void UpdatePlayModeInteractions(bool pointerOverUI, Vector3 cursorPoint)
    {
        // CLOSE UP CHECK
        pointerOverCloseUp = Physics2D.Raycast(cursorPoint, Vector2.zero, float.MaxValue, layerMask: closeUpLayerMask).collider != null;

        // HOVER INTERACTABLE
        RaycastHit2D[] hits = Physics2D.RaycastAll(cursorPoint, Vector2.zero, float.MaxValue, layerMask: interactableLayerMask);

        Interactable hovered = null;
        if (!pointerOverUI && hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null && interactable.CheckAllConditions())
                    hovered = interactable;
            }
        }

        if (hovered != interactable)
        {
            if (interactable != null)
                interactable.EndHover();

            if (hovered != null)
                hovered.BeginHover();
        }
        interactable = hovered;

        if (pointerOverUI) return;

        // CLICK
        if (Input.GetMouseButtonDown(0))
        {
            if (pointerOverCloseUp)
            {
                //interactables that are part of closeups are interacted with directly
                if (interactable != null)
                    interactable.Interact();
            }
            else
            {
                if (interactable != null)
                {
                    //click on interactable
                    MoveToAndInteractWith(interactable);
                }
                else
                {
                    //notify closeUps to close
                    ClickOutsideOfCloseUpEvent?.Invoke();

                    bool pointerOutsideOfRoom = true;

                    //check for click outside of room
                    RaycastHit2D[] roomHits = Physics2D.RaycastAll(cursorPoint, Vector2.zero, float.MaxValue, layerMask: roomLayerMask);

                    Room current = PlayerServiceProvider.GetRoomAgent().CurrentRoom;
                    foreach (RaycastHit2D hit in roomHits)
                    {
                        if (current == hit.collider.GetComponent<Room>())
                            pointerOutsideOfRoom = false;
                    }

                    if (pointerOutsideOfRoom)
                    {
                        //move and interact with closest door to leave room
                        MoveToAndInteractWith(PlayerServiceProvider.GetRoomAgent().GetClosestDoor(cursorPoint));
                    }
                    else
                    {
                        //move to cursor positon
                        PlayerServiceProvider.GetMoveModule().WalkTo(cursorPoint);
                    }
                }
            }
        }
    }

    private static void MoveToAndInteractWith(Interactable target)
    {
        PlayerServiceProvider.GetMoveModule().WalkTo(target.GetPoint(), () => target.Interact());
    }
}
