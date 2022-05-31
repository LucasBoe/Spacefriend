using SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferenceHolder : SingletonBehaviour<GameReferenceHolder>
{
    public Material InteractableMaterial;

    public SoundReferences Sounds;

    [System.Serializable]
    public class SoundReferences
    {
        public Sound PointerDownSound, PointerUpSound;
        public Sound PopupOpenSound, PopupCloseSound;
    }
}
