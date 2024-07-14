using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    // --------------------------------- 
    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI numberOfHumanoidText;
    [SerializeField] private GameObject heightText;
    [SerializeField] private TextMeshProUGUI killedByText;
    [SerializeField] private TextMeshProUGUI placeText;

    // ---------------------------------

    [Header("PANELS")] 
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject deadPanel;
    void Start()
    {
        UpdateText();
    }

    void OnEnable()
    {
        EnemySpawnManager.OnEnemyDeath += UpdateText;
        Player_Stats.OnPlayerIncreaseHeight += UpdateHeight;
        Player_Stats.OnPlayerDeath += PlayerKilledPanelUpdate;
    }    

    void OnDisable() {
        EnemySpawnManager.OnEnemyDeath -= UpdateText;
        Player_Stats.OnPlayerIncreaseHeight -= UpdateHeight;
        Player_Stats.OnPlayerDeath -= PlayerKilledPanelUpdate;
    }
    public void UpdateText() {
        numberOfHumanoidText.text = $"Alive: {EnemySpawnManager.instance.GetNumberOfHumanoidAlive()}";
    }
    public void UpdateHeight(float height) {
        heightText.GetComponent<TextMeshProUGUI>().text = $"{height} m";
        heightText.SetActive(true);
        StartCoroutine(DeactiveAfter2seconds());
    }

    IEnumerator DeactiveAfter2seconds() {
        yield return new WaitForSeconds(2.0f);
        heightText.SetActive(false);
    }

    public void PlayerKilledPanelUpdate(GameObject enemy) {

        deadPanel.SetActive(true);
        inGamePanel.SetActive(false);

        EnemyComponent enemy_Component = enemy.GetComponent<EnemyComponent>();
        if (enemy_Component == null) Debug.Log("Not found enemy that kill");
        killedByText.text = $"{enemy_Component.nameText.GetComponent<TextMeshProUGUI>().text}";
        killedByText.color = enemy_Component.nameText.GetComponent<TextMeshProUGUI>().color;
        placeText.text = $"#{EnemySpawnManager.instance.GetNumberOfHumanoidAlive() + 1}";
    }
}
