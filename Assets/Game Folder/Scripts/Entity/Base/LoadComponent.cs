using UnityEngine;

public class LoadComponent : MonoBehaviour
{
    // -------------------------- Attribute
    
    [Header("Components")]
    public Material skinMaterial;
    public Material pantMaterial;
    public Weapon currentWeapon;
    public GameObject hairGameObject;
    

    [Header("Container for components")] 
    public GameObject hairContainer;
    public GameObject weaponContainer;
    public GameObject pantContainer;
    public GameObject skinContainer; 
    // -------------------------- Unity Functions
    // -------------------------- User Defined Functions
}