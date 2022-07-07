using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFlowManager : MonoBehaviour
{
    [SerializeField] ScriptableEvent activateSpaceStationContentEvent, deactivateSpaceStationContentEvent, activateFlightContentEvent, deactivateFlightContentEvent;
    [SerializeField] RoomData spaceStationData;
    public void StartHandelingFlow()
    {
        bool startFromBeginning = true;

#if UNITY_EDITOR
        startFromBeginning = EditorPersistentDataStorage.TestFromStart || EditorPersistentDataStorage.SceneStartedFromBuildIndex == spaceStationData.SceneIndex;
#endif

        if (startFromBeginning)
        {
            activateSpaceStationContentEvent?.Invoke();
            deactivateFlightContentEvent?.Invoke();
        }
        else
        {
            activateFlightContentEvent?.Invoke();
            deactivateSpaceStationContentEvent?.Invoke();
        }
    }
}
