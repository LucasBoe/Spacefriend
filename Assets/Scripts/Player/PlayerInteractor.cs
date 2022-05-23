using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Player player;
    Interactable interactable;

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        Vector2 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -cam.transform.position.z));

        // HOVER
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
        Interactable hovered = null;
        if (hit.collider != null) hovered = hit.collider.GetComponent<Interactable>();

        if (hovered != interactable)
        {
            if (interactable != null)
                interactable.EndHover();

            if (hovered != null)
                hovered.BeginHover();
        }
        interactable = hovered;

        // CLICK
        if (Input.GetMouseButtonDown(0))
        {
            if (interactable != null)
            {
                Interactable target = interactable;
                //click on interactable
                player.MoveTo(target.GetPoint(), () => target.Interact());
            }
            else
            {
                //move to cursor positon
                player.MoveTo(point);
            }
        }
    }
}
