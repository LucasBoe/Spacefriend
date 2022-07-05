using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFlowManager : MonoBehaviour
{
    [SerializeField] ScriptableEvent activateSpaceStationContentEvent, deactivateSpaceStationContentEvent, activateFlightContentEvent, deactivateFlightContentEvent;
    public void StartHandelingFlow()
    {
        bool startFromBeginning = true;

#if UNITY_EDITOR
        startFromBeginning = EditorPersistentDataStorage.TestFromStart;
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
