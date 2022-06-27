using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public System.Action<bool> SetRoomStateEvent;

    [SerializeField] RoomData roomData;
    public RoomData Data => roomData;
    private List<RoomSpriteRenderer> roomSpriteRenderers;
    public bool IsActive;

    private ChangeRoom_InteractionListener[] doors;
    public ChangeRoom_InteractionListener[] Doors => doors;

    private void Awake()
    {
        RoomManager.Instance.RegisterRoom(this, roomData);
        roomSpriteRenderers = new List<RoomSpriteRenderer>();
        doors = GetComponentsInChildren<ChangeRoom_InteractionListener>();
        Debug.Log("Get doors from children");
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            roomSpriteRenderers.Add(renderer.gameObject.AddComponent<RoomSpriteRenderer>());
        }
    }

    private void OnEnable()
    {
        RoomManager.OnChangeRoomEvent += OnChangeRoom;
    }
    private void OnDisable()
    {
        RoomManager.OnChangeRoomEvent -= OnChangeRoom;
    }

    private void OnChangeRoom(RoomInfo info)
    {
        SetRoomState(info.SceneBehaviour == this);
    }

    private void Start()
    {
        SetRoomSpriteRendererAlphas(0);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        RoomAgent roomAgent = collision.GetComponent<RoomAgent>();

        if (roomAgent != null && !IsActive && PlayerServiceProvider.GetPlayerMode().IsOnShip)
            RoomManager.TriggerEnterRoomEvent(roomData);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        //RoomAgent roomAgent = collision.GetComponent<RoomAgent>();
        //
        //if (roomAgent != null)
        //    SetRoomState(false);
    }

    private void SetRoomState(bool isInRoom)
    {
        SetRoomStateEvent?.Invoke(isInRoom);
        IsActive = isInRoom;

        float duration = 0.5f;

        if (!isInRoom)
            CoroutineUtil.ExecuteFloatRoutine(1f, 0f, SetRoomSpriteRendererAlphas, this, duration);
        else
            CoroutineUtil.Delay(() => CoroutineUtil.ExecuteFloatRoutine(0f, 1f, SetRoomSpriteRendererAlphas, this, duration), this, 0);
    }

    private void SetRoomSpriteRendererAlphas(float alpha)
    {
        foreach (RoomSpriteRenderer roomSpriteRenderer in roomSpriteRenderers)
        {
            //TODO: make sure placing objects later on also adds them here...
            if (roomSpriteRenderer != null) roomSpriteRenderer.SetAlpha(alpha);
        }
    }
}
