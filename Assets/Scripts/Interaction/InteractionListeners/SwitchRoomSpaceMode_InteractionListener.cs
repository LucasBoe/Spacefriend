using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoomSpaceMode_InteractionListener : MonoBehaviour, IInteractionListener
{
    //[SerializeField] SpaceTransitionRoom transitionRoom;
    [SerializeField] Room spaceRoom;
    [SerializeField] bool space = true;
    [SerializeField] bool addSpaceSuite = true;

    public void Interact()
    {
        SpaceAgent spaceAgent = PlayerServiceProvider.GetSpaceAgent();
        spaceAgent.SetSpaceMode(space);
        //transitionRoom.SetRoomState(isSpace: space);
        Room.TriggerEnterRoomEvent?.Invoke(spaceRoom);
        if (addSpaceSuite) PlayerServiceProvider.SetPlayerSkin(space ? PlayerSkinType.Astronaut : PlayerSkinType.Clothes);
    }
}
