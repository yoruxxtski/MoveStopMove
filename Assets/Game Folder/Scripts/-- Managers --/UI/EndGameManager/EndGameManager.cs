using TMPro;
using UnityEngine;

public class EndGameManager : Singleton<EndGameManager>
{
    [SerializeField] private TextMeshProUGUI enemyKillsText;
    [SerializeField] private TextMeshProUGUI playerPlacementText;

    [Header("PANELS")] 
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject deadPanel;

    void OnEnable()
    {
        Player_Stats.OnPlayerDeath += PlayerKilledPanelUpdate;
    }   

    void OnDisable()
    {
        Player_Stats.OnPlayerDeath += PlayerKilledPanelUpdate;
    } 

    public void PlayerKilledPanelUpdate(GameObject enemy) {
        inGamePanel.SetActive(false);
        deadPanel.SetActive(true);

        EnemyComponent enemy_Component = enemy.GetComponent<EnemyComponent>();
        if (enemy_Component == null) Debug.Log("Not found enemy that kill");
        enemyKillsText.text = $"{enemy_Component.nameText.GetComponent<TextMeshProUGUI>().text}";
        enemyKillsText.color = enemy_Component.nameText.GetComponent<TextMeshProUGUI>().color;
        playerPlacementText.text = $"#{EnemySpawnManager.instance.GetNumberOfHumanoidAlive() + 1}";
    }
}