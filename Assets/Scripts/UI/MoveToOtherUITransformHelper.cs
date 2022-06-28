using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToOtherUITransformHelper : MonoBehaviour
{
    [SerializeField] RectTransform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
