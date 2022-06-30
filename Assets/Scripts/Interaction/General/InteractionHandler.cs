using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField, ReadOnly, Foldout("Info")] Interactable interactable;
    [SerializeField, ReadOnly, Foldout("Info")] bool pointerOverPanel;

    int roomLayerMask, interactableLayerMask, panelLayerMask, totalShipLayerMask;
    Camera mainCam;

    PlayerMoveModule moveModule;
    RoomAgent roomAgent;


    public static System.Action ClickOutsideOfCloseUpEvent;
    public static System.Action<bool> ClickedInTotalViewEvent;

    protected void Awake()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        interactableLayerMask = ~LayerMask.GetMask("Room", "Ignore Raycast", "CloseUp", "TotalShip");
        panelLayerMask = LayerMask.GetMask("Panel");
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
        UpdateHover(pointerOverUI, cursorPoint);
        if (pointerOverUI) return;
        UpdateClick(cursorPoint);
    }

    private void UpdateClick(Vector3 cursorPoint)
    {
        Interactable target = interactable;

        if (Input.GetMouseButtonDown(0))
        {
            // CLOSE UP CHECK
            pointerOverPanel = Physics2D.Raycast(cursorPoint, Vector2.zero, float.MaxValue, layerMask: panelLayerMask).collider != null;

            if (pointerOverPanel)
            {
                //interactables that are part of closeups are interacted with directly
                if (target != null) target.Interact();
            }
            else
            {
                //notify closeUps to close
                ClickOutsideOfCloseUpEvent?.Invoke();

                //TODO: Add different input for space mode
                if (target != null)
                    moveModule.MoveTo(target.GetPoint(), false, () => target.Interact());
                else
                    moveModule.MoveTo(cursorPoint, IsPointOutsideOfCurrentRoom(cursorPoint, roomAgent.CurrentRoom.SceneBehaviour));

            }
        }
    }

    private void UpdateHover(bool pointerOverUI, Vector3 cursorPoint)
    {
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
    }

    private bool IsPointOutsideOfCurrentRoom(Vector3 cursorPoint, RoomBehaviour current)
    {
        RaycastHit2D[] roomHits = Physics2D.RaycastAll(cursorPoint, Vector2.zero, float.MaxValue, layerMask: roomLayerMask);

        foreach (RaycastHit2D hit in roomHits)
        {
            if (current == hit.collider.GetComponent<RoomBehaviour>())
                return false;
        }

        return true;
    }
}
