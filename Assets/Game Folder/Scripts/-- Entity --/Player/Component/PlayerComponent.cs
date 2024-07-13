using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : LoadComponent
{   
    void Start()
    {
        LoadComponent();
    }

    public void LoadComponent() {
        if (currentWeapon != null)
        Instantiate(currentWeapon.weaponObject, weaponContainer.transform);

        // Skin
        skinContainer.GetComponent<Renderer>().material = skinMaterial;

        levelImage.GetComponent<Image>().color = skinMaterial.color;
        nameText.GetComponent<TextMeshProUGUI>().color = skinMaterial.color;
    }

    public Weapon GetCurrentWeapon() {
        return currentWeapon;
    }
}