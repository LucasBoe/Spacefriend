using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundSystem;

[RequireComponent(typeof(SpriteRendererMaterialInstanciator))]
public class Door : MonoBehaviour
{
    
    Material material;
    [SerializeField] Sound doorSound;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoroutineUtil.ExecuteFloatRoutine(0, 1, AnimateDoor, this, 0.5f);
        doorSound.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CoroutineUtil.ExecuteFloatRoutine(1, 0, AnimateDoor, this, 0.5f);
        doorSound.Play();
    }

    private void AnimateDoor(float value)
    {
        material.SetFloat("_Open", value);
    }
}
