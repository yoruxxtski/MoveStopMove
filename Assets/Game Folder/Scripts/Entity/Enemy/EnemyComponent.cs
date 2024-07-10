using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : LoadComponent // ! Change the order of script initialization : Component Manager -> EnemyComponent -> Pooling Manager -> Spawn Manager
{
    // --------------------------- Attribute
    private List<Material> skinMaterialsList;
    private List<Material> pantMaterialsList;
    private List<Weapon> weaponListsList;
    private List<GameObject> hairListsList;
    // --------------------------- Unity Functions
    void Awake()
    {
        skinMaterialsList = ComponentManager.instance.GetSkinsMaterial();
        pantMaterialsList = ComponentManager.instance.GetPantsMaterial();
        weaponListsList = ComponentManager.instance.GetWeaponsList();
        hairListsList = ComponentManager.instance.GetHairsList(); 
    }
    void OnEnable()
    {
        if (skinMaterialsList != null) {
            skinMaterial = skinMaterialsList[Random.Range(0, skinMaterialsList.Count)];
        } else Debug.Log("EnemyComponent.cs : Can't find skinMaterialsList");


        if (pantMaterialsList != null) {
            pantMaterial = pantMaterialsList[Random.Range(0, pantMaterialsList.Count)];
        } else Debug.Log("EnemyComponent.cs : Can't find pantMaterialsList");


        if (weaponListsList != null) {
            currentWeapon = weaponListsList[Random.Range(0, weaponListsList.Count)];
        } else Debug.Log("EnemyComponent.cs : Can't find weaponListList");

        if (hairListsList != null) {
            hairGameObject = hairListsList[Random.Range(0, hairListsList.Count)];
        } else Debug.Log("EnemyComponent.cs : Can't find hairListsList");

        LoadComponent();
    }

    void OnDisable()
    {
        DeLoadComponent();    
    }
    // --------------------------- User Defined Functions

    public void LoadComponent() {
        // Pant 
        pantContainer.GetComponent<Renderer>().material = pantMaterial;
        // Skin
        skinContainer.GetComponent<Renderer>().material = skinMaterial;
        // Weapon
        Instantiate(currentWeapon.weaponObject, weaponContainer.transform);
        // Hair
        Instantiate(hairGameObject, hairContainer.transform);
    }

    public void DeLoadComponent() {

        pantMaterial = null;
        skinMaterial = null;

        // Remove existing weapon and hair objects

        foreach (Transform child in weaponContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in hairContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}