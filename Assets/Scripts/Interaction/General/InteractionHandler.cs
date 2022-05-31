using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField, ReadOnly, Foldout("Info")] Interactable interactable;
    [SerializeField, ReadOnly, Foldout("Info")] bool pointerOverCloseUp;

    int interactableLayerMask, closeUpLayerMask;
    Camera mainCam;

    public static System.Action ClickOutsideOfCloseUpEvent;

    private void Awake()
    {
        interactableLayerMask = ~LayerMask.GetMask("Room", "Ignore Raycast");
        closeUpLayerMask = LayerMask.GetMask("CloseUp");
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //pointer is over ui;
        bool pointerOverUI = EventSystem.current.IsPointerOverGameObject();

        //calculate cursor position
        Vector3 point = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCam.transform.position.z));

        // CLOSE UP CHECK
        pointerOverCloseUp = Physics2D.Raycast(point, Vector2.zero, float.MaxValue, layerMask: closeUpLayerMask).collider != null;

        // HOVER INTERACTABLE
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, float.MaxValue, layerMask: interactableLayerMask);

        Interactable hovered = null;
        if (!pointerOverUI && hit.collider != null) hovered = hit.collider.GetComponent<Interactable>();

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
                    PlayerServiceProvider.GetMoveModule().WalkTo(target.GetPoint(), () => target.Interact());
                }
                else
                {
                    //notify closeUps to close
                    ClickOutsideOfCloseUpEvent?.Invoke();

                    //move to cursor positon
                    PlayerServiceProvider.GetMoveModule().WalkTo(point);
                }
            }
        }
    }
}
