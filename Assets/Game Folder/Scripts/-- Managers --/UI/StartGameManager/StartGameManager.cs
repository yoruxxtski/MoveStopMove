using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private GameObject vibrationOn;
    [SerializeField] private GameObject vibrationOff;
    [SerializeField] private GameObject volumeOn;
    [SerializeField] private GameObject volumeOff;

    // -------------------------------------------
    [SerializeField] private GameObject progressStar;
    [SerializeField] private GameObject goldPanel;
    [SerializeField] private GameObject noAdsPanel;
    [SerializeField] private GameObject vibrationPanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject zombieCity;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject weaponButton;
    [SerializeField] private GameObject skinButton;

    public void SetVibrationOnOff() {
        vibrationOn.SetActive(!vibrationOn.activeSelf);
        vibrationOff.SetActive(!vibrationOff.activeSelf);
    }

    public void SetVolumeOnOff() {
        volumeOn.SetActive(!volumeOn.activeSelf);
        volumeOff.SetActive(!volumeOff.activeSelf);
    }
}
 