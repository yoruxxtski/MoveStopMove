using UnityEngine;

public class EnemySelected : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject SelectorSprite;

    public void ChangeSelectorSprite(bool value) {
        SelectorSprite.SetActive(value);
    }
}