using UnityEngine;

public class DecisionDead : FSM_Decision
{
    // ------------------------- Attribute
    private Enemy_Stats enemy_Stats;
    // ------------------------- Unity Functions
    void Awake()
    {
        enemy_Stats = GetComponent<Enemy_Stats>();
    }
    // ------------------------- User Defined Functions
    public override bool Decide()
    {
        return IsAlive();
    }

    public bool IsAlive() {
        // Neu enemy con song thi return true , neu chet thi return false
        return enemy_Stats.GetAliveState();
    }
}