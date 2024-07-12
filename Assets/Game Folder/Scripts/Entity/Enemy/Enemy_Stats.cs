using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    // -------------------------------- Attribute
    [Header("Tag")]
    [SerializeField] private TagType enemyTag;

    [Header("Alive State")]
    private bool checkAlive;

    // -------------------------------- Unity Functions
    void Awake()
    {
        checkAlive = true;
    }

    // -------------------------------- User Defined Functions

    // -------------------------------- Getter & Setter
    public bool GetAliveState() {
        return checkAlive;
    }
    public void SetAliveState(bool value) {
        checkAlive = value;
    }
    public TagType GetEnemyTag() {
        return enemyTag;
    }
}