using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : LoadComponent
{   
    [SerializeField] private ParticleSystem levelUpEffect;

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
        ParticleSystemRenderer renderer = levelUpEffect.GetComponent<ParticleSystemRenderer>();
        renderer.material = skinMaterial;
    }

    public Weapon GetCurrentWeapon() {
        return currentWeapon;
    }

    public void SetDeadSkinMaterial() {
        float darkenFactor = 0.5f;
        skinContainer.GetComponent<Renderer>().material.color = skinMaterial.color * darkenFactor;
    }

    public ParticleSystem GetParticle() {
        return levelUpEffect;
    }
}