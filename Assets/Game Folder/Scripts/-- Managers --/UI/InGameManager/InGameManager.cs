using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : Singleton<InGameManager>
{
    [Header("Setting Panel")]
    [SerializeField] private Image onSoundImage;
    [SerializeField] private Image offSoundImage;
    [SerializeField] private Image onVibrationImage;
    [SerializeField] private Image offVibrationImage;
    [SerializeField] private GameObject settingPanel;
    private int buttonSoundIndex;
    private int buttonVibrationIndex;

    public void TurnOnSound() {
        buttonSoundIndex = 1;
        onSoundImage.gameObject.SetActive(true);
        offSoundImage.gameObject.SetActive(false);
    }

    public void TurnOffSound() {
        buttonSoundIndex = 0;
        onSoundImage.gameObject.SetActive(false);
        offSoundImage.gameObject.SetActive(true);
    }

    public void TurnOnVibration() {
        buttonVibrationIndex = 1;
        onVibrationImage.gameObject.SetActive(true);
        offVibrationImage.gameObject.SetActive(false);
    }

    public void TurnOffVibration() {
        buttonVibrationIndex = 0;
        onVibrationImage.gameObject.SetActive(false);
        offVibrationImage.gameObject.SetActive(true);
    }

    public void TurnOnOffSettingPanel() {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
}
