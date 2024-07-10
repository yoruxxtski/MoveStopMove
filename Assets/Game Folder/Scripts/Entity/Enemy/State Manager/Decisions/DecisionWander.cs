using UnityEngine;

public class DecisionWander : FSM_Decision
{   // ------------------------------ Attribute
    private ActionWander actionWander;

    // ------------------------------ Unity Functions
    void Awake()
    {
        actionWander = GetComponent<ActionWander>();
    }

    // ------------------------------ User Function
    public override bool Decide()
    {
        return CheckDecisionWander();
    }

    public bool CheckDecisionWander() {
        return !actionWander.GetIdleState();
    }
}