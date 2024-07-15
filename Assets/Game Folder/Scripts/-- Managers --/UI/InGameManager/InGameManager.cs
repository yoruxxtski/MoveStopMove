using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : Singleton<InGameManager>
{
    // ---------------------- Attributes
    [Header("Setting Panel")]
    [SerializeField] private Image onSoundImage;
    [SerializeField] private Image offSoundImage;
    [SerializeField] private Image onVibrationImage;
    [SerializeField] private Image offVibrationImage;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject dragPanel;
    [SerializeField] private GameObject inGamePanel;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI aliveNumberText;
    [Header("Height Text")]
    [SerializeField] private GameObject heightObject;


    // ------------------------- Unity Functions
    void Start()
    {
        UpdateTotalNumberOfPlayerAndEnemy();
    }

    void OnEnable()
    {
        EnemySpawnManager.OnEnemyDeath += UpdateTotalNumberOfPlayerAndEnemy;
        Player_Stats.OnPlayerIncreaseHeight += UpdateHeight;
    }    

    void OnDisable() {
        EnemySpawnManager.OnEnemyDeath -= UpdateTotalNumberOfPlayerAndEnemy;
        Player_Stats.OnPlayerIncreaseHeight -= UpdateHeight;
    }
    // ------------------------- User Defined Functions

    public void TurnOnSound() {
       
        onSoundImage.gameObject.SetActive(true);
        offSoundImage.gameObject.SetActive(false);
    }

    public void TurnOffSound() {
        onSoundImage.gameObject.SetActive(false);
        offSoundImage.gameObject.SetActive(true);
    }

    public void TurnOnVibration() {
        onVibrationImage.gameObject.SetActive(true);
        offVibrationImage.gameObject.SetActive(false);
    }

    public void TurnOffVibration() {
        onVibrationImage.gameObject.SetActive(false);
        offVibrationImage.gameObject.SetActive(true);
    }

    public void TurnOnOffSettingPanel() {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void UpdateTotalNumberOfPlayerAndEnemy() {
        aliveNumberText.text = $"Alive: {EnemySpawnManager.instance.GetNumberOfHumanoidAlive()}";
    }

    public void UpdateHeight(float height) {
        heightObject.GetComponent<TextMeshProUGUI>().text = $"{height} m";
        heightObject.SetActive(true);
        StartCoroutine(DeactiveAfter2seconds());
    }
    IEnumerator DeactiveAfter2seconds() {
        yield return new WaitForSeconds(2.0f);
        heightObject.SetActive(false);
    }

    public void DetectClick() {
        dragPanel.SetActive(false);
        inGamePanel.GetComponent<Button>().enabled = false;
    }
}
