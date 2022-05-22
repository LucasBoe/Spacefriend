using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player player;
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

        if (Input.GetMouseButtonDown(0))
        {
            player.MoveTo(point);
        }
    }
}
