using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : Singleton<ComponentManager>
{
    // ----------------- ATTRIBUTES
    [Header("Skin Materials")]
    [SerializeField] private List<Material> skinMaterials;
    // ------------------
    [Header("Pants Material")]
    [SerializeField] private List<Material> pantMaterials;
    // ------------------
    [Header("Weapons")]
    [SerializeField] private List<Weapon> weaponLists;
    // ------------------
    [Header("Hairs")]
    [SerializeField] private List<GameObject> hairLists;

    // ----------------- UNITY FUNCTIONS


    
    // ----------------- USER DEFINED FUNCTIONS
    public List<Material> GetSkinsMaterial() {
        return skinMaterials;
    }
    public List<Material> GetPantsMaterial() {
        return pantMaterials;
    }
    public List<Weapon> GetWeaponsList() {
        return weaponLists;
    }
    public List<GameObject> GetHairsList() {
        return hairLists;
    }
}