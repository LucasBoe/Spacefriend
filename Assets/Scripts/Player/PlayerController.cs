using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player player;
    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        Vector2 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -cam.transform.position.z));

        if (Input.GetMouseButtonDown(0))
        {
            player.MoveTo(point);
        }
    }
}
