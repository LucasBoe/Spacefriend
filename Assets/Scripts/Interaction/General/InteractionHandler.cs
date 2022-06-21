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

    PlayerMoveModule moveModule;
    RoomAgent roomAgent;

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

    private void Start()
    {
        moveModule = PlayerServiceProvider.GetMoveModule();
        roomAgent = PlayerServiceProvider.GetRoomAgent();
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
                    Interactable target = interactable;
                    moveModule.MoveTo(target.GetPoint(), false, () => target.Interact());
                }
                else
                {
                    //notify closeUps to close
                    ClickOutsideOfCloseUpEvent?.Invoke();

                    bool pointerOutsideOfCurrentRoom = IsPointOutsideOfCurrentRoom(cursorPoint, roomAgent.CurrentRoom);

                    Debug.Log("move to " + cursorPoint.ToString() + " (" + pointerOutsideOfCurrentRoom + ")");

                    //move to cursor positon
                    moveModule.MoveTo(cursorPoint, pointerOutsideOfCurrentRoom);
                }
            }
        }
    }

    private bool IsPointOutsideOfCurrentRoom(Vector3 cursorPoint, Room current)
    {
        RaycastHit2D[] roomHits = Physics2D.RaycastAll(cursorPoint, Vector2.zero, float.MaxValue, layerMask: roomLayerMask);

        foreach (RaycastHit2D hit in roomHits)
        {
            if (current == hit.collider.GetComponent<Room>())
                return false;
        }

        return true;
    }
}
