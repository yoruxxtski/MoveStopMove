using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : Singleton<StartGameManager>
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
    [SerializeField] private GameObject shopPanel;

    public void SetVibrationOnOff() {
        vibrationOn.SetActive(!vibrationOn.activeSelf);
        vibrationOff.SetActive(!vibrationOff.activeSelf);
    }

    public void SetVolumeOnOff() {
        volumeOn.SetActive(!volumeOn.activeSelf);
        volumeOff.SetActive(!volumeOff.activeSelf);
    }

    public void GoOut() {
        progressStar.GetComponent<Animator>().SetTrigger("isOut");
        goldPanel.GetComponent<Animator>().SetTrigger("isOut");
        noAdsPanel.GetComponent<Animator>().SetTrigger("isOut");
        vibrationPanel.GetComponent<Animator>().SetTrigger("isOut");
        soundPanel.GetComponent<Animator>().SetTrigger("isOut");
        zombieCity.GetComponent<Animator>().SetTrigger("isOut");
        namePanel.GetComponent<Animator>().SetTrigger("isOut");
        playPanel.GetComponent<Animator>().SetTrigger("isOut");
        weaponButton.GetComponent<Animator>().SetTrigger("isOut");
        skinButton.GetComponent<Animator>().SetTrigger("isOut");
    } 
    public void GoIn() {
        progressStar.GetComponent<Animator>().SetTrigger("isIn");
        goldPanel.GetComponent<Animator>().SetTrigger("isIn");
        noAdsPanel.GetComponent<Animator>().SetTrigger("isIn");
        vibrationPanel.GetComponent<Animator>().SetTrigger("isIn");
        soundPanel.GetComponent<Animator>().SetTrigger("isIn");
        zombieCity.GetComponent<Animator>().SetTrigger("isIn");
        namePanel.GetComponent<Animator>().SetTrigger("isIn");
        playPanel.GetComponent<Animator>().SetTrigger("isIn");
        weaponButton.GetComponent<Animator>().SetTrigger("isIn");
        skinButton.GetComponent<Animator>().SetTrigger("isIn");
    }
    public void OpenShop() {
        
        goldPanel.gameObject.GetComponent<Button>().enabled = false;
        shopPanel.SetActive(true);

        progressStar.GetComponent<Animator>().SetTrigger("isOut");
        noAdsPanel.GetComponent<Animator>().SetTrigger("isOut");
        vibrationPanel.GetComponent<Animator>().SetTrigger("isOut");
        soundPanel.GetComponent<Animator>().SetTrigger("isOut");
        zombieCity.GetComponent<Animator>().SetTrigger("isOut");
        namePanel.GetComponent<Animator>().SetTrigger("isOut");
        playPanel.GetComponent<Animator>().SetTrigger("isOut");
        weaponButton.GetComponent<Animator>().SetTrigger("isOut");
        skinButton.GetComponent<Animator>().SetTrigger("isOut");
    }
    public void CloseShop() {

        goldPanel.gameObject.GetComponent<Button>().enabled = true;
        shopPanel.SetActive(false);

        progressStar.GetComponent<Animator>().SetTrigger("isIn");
        noAdsPanel.GetComponent<Animator>().SetTrigger("isIn");
        vibrationPanel.GetComponent<Animator>().SetTrigger("isIn");
        soundPanel.GetComponent<Animator>().SetTrigger("isIn");
        zombieCity.GetComponent<Animator>().SetTrigger("isIn");
        namePanel.GetComponent<Animator>().SetTrigger("isIn");
        playPanel.GetComponent<Animator>().SetTrigger("isIn");
        weaponButton.GetComponent<Animator>().SetTrigger("isIn");
        skinButton.GetComponent<Animator>().SetTrigger("isIn");
    }
}
 