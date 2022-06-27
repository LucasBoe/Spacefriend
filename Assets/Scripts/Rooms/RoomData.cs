using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RoomData : ScriptableObject
{
	[Scene]
	public int SceneIndex;
	public RoomBehaviour CurrentSceneBehaviour => RoomManager.Instance.GetRoomInstanceByData(this);
}
