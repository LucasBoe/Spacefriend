using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField, ReadOnly] Interactable interactable;

    int interactableLayerMask;

    private void Awake()
    {
        interactableLayerMask = ~LayerMask.GetMask("Room", "Ignore Raycast");
    }

    // Update is called once per frame
    void Update()
    {
        //stop all world interaction, the pointer is on the ui;
        bool pointerOverUI = EventSystem.current.IsPointerOverGameObject();

        Camera cam = Camera.main;
        Vector2 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -cam.transform.position.z));

        // HOVER
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

            if (interactable != null)
            {
                Interactable target = interactable;
                //click on interactable
                player.MoveModule.WalkTo(target.GetPoint(), () => target.Interact());
            }
            else
            {
                //move to cursor positon
                player.MoveModule.WalkTo(point);
            }
        }
    }
}
