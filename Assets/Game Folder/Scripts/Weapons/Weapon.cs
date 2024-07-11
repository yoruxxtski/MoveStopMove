using UnityEngine;

public enum WeaponType {
    Straight,
    Rotate
}
[CreateAssetMenu(fileName = "Weapon_")]
public class Weapon : ScriptableObject
{
    //------------------------------ Attribute
    [Header("Weapon Type")]
    public WeaponType weaponType;

    // -----------------------------
    [Header("Tag type")]
    public TagType tagType;
    // -----------------------------

    [Header("Weapon Object")]
    public GameObject weaponObject;

    // ------------------------------

    [Header("Weapon Prefabs")]
    public Projectile weaponPrefabs;
    
    // ------------------------------
}