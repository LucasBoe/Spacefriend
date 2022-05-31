using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoffe_MugManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer mugRenderer;
    bool mugActive = false;
    public bool MugActive
    {
        get { return mugActive; }
        set
        {
            mugRenderer.enabled = value;
            mugActive = value;
        }
    }
}
