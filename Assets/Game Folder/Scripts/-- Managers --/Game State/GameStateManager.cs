using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : Singleton<GameStateManager>
{
    public static event Action StartGamePlayer;
    [SerializeField] private GameObject startGamePanel;

    [SerializeField] private GameObject inGamePanel;

    [SerializeField] private GameObject endGamePanel;

    public static event Action potentialGameEnd;

    void Update()
    {
        if (EnemySpawnManager.instance.GetNumberOfHumanoidAlive() == 1) {
            potentialGameEnd?.Invoke();
        }       
    }

    public void StartGame() {
        StartGameManager.instance.GoOut();
        EnemySpawnManager.instance.SpawnInitialEnemies();
        StartGamePlayer?.Invoke();
        inGamePanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }
}
