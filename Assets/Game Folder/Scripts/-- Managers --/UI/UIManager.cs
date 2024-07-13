using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI numberOfHumanoidText;

    void Start()
    {
        UpdateText();
    }

    void OnEnable()
    {
        EnemySpawnManager.OnEnemyDeath += UpdateText;
        
    }    

    void OnDisable() {
        EnemySpawnManager.OnEnemyDeath -= UpdateText;
        
    }



    public void UpdateText() {
        numberOfHumanoidText.text = $"Alive: {EnemySpawnManager.instance.GetNumberOfHumanoidAlive()}";
    }
}
