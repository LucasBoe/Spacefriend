using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : SingletonBehaviour<GameModeManager>
{
    [SerializeField] private GameMode currentGameMode;
    public static GameMode Current => Instance.currentGameMode;
    public static System.Action<GameMode, GameMode> GameModeChangedEvent;

    private void Start()
    {
        if (currentGameMode == GameMode.StartUp)
            SetGameMode(GameMode.Play);
    }

    public static void SetGameMode(GameMode newGameMode)
    {
        Debug.Log("Change Game Mode To: " + newGameMode);
        GameMode before = Instance.currentGameMode;
        Instance.currentGameMode = newGameMode;
        GameModeChangedEvent?.Invoke(before, newGameMode);
    }
}

public enum GameMode
{
    StartUp,
    MainMenu,
    Total,
    Play,
    PauseMenu,
}

