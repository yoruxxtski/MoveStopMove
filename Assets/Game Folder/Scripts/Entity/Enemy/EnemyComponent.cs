using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyComponent : LoadComponent // ! Change the order of script initialization : Component Manager -> EnemyComponent -> Pooling Manager -> Spawn Manager
{
    // !!! It seems using instantiate is more efficient in this ? Why

    // TODO : Maybe I should change how I use the weapon . Creating a base weapon and inherit from it ? Will fix this after 
    // --------------------------- Attribute
    private List<Material> skinMaterialsList;
    private List<Material> pantMaterialsList;
    private List<Weapon> weaponListsList;
    private List<string> nameLists;
    private GameObject activeHairObject;

    // --------------------------- Unity Functions
    void Awake()
    {
        skinMaterialsList = ComponentManager.instance.GetSkinsMaterial();
        pantMaterialsList = ComponentManager.instance.GetPantsMaterial();
        weaponListsList = ComponentManager.instance.GetWeaponsList();
        nameLists = ComponentManager.instance.GetNameList();
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
            currentWeapon = weaponListsList[Random.Range(0, weaponListsList.Count)]; // * Define current weapon
        } else Debug.Log("EnemyComponent.cs : Can't find weaponListList");

        if (nameLists != null) {
            nameText.GetComponent<TextMeshProUGUI>().text = nameLists[Random.Range(0, nameLists.Count)];
        } else Debug.Log("EnemyComponent.cs : Can't find nameLists");

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
 
        // Activate a random hair
        int randomIndex = Random.Range(0, hairContainer.transform.childCount);
        activeHairObject = hairContainer.transform.GetChild(randomIndex).gameObject;
        activeHairObject.SetActive(true);

        levelImage.GetComponent<Image>().color = skinMaterial.color;
        nameText.GetComponent<TextMeshProUGUI>().color = skinMaterial.color;
    }

    public void DeLoadComponent() {

        pantMaterial = null;
        skinMaterial = null;

        // Remove existing weapon and hair objects
        foreach (Transform child in weaponContainer.transform)
        {
            Destroy(child.gameObject);
        }

        activeHairObject.SetActive(false);
    }
}