using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    [SerializeField] float scrollDelta;

    // Update is called once per frame
    void Update()
    {
        scrollDelta += Input.mouseScrollDelta.y;

        if (scrollDelta >= 1)
        {
            ZoomHandler.Instance.TryZoomIn();
            scrollDelta = 0;
        } else if (scrollDelta <= -1)
        {
            ZoomHandler.Instance.TryZoomOut();
            scrollDelta = 0;
        }
    }
}
