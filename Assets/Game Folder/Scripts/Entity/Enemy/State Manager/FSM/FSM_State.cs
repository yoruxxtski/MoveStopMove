using System;
using UnityEngine;

[Serializable]
public class FSM_State 
{
    public StateType StateType;
    public FSM_Action[] actions;
    public FSM_Transition[] transitions;

    public void UpdateState(EnemyController enemyController) {
        ExecuteActions();
        ExecuteTransitions(enemyController);
    }
    // * Execute all actions in the states
    public void ExecuteActions() {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].Act(); 
        }
    }
    // * Check for all transitions of the states
    public void ExecuteTransitions(EnemyController enemyController) {

        if (transitions.Length <= 0 && transitions == null) return;

        for (int i = 0; i < transitions.Length;i++) {
            bool decideParam = transitions[i].decision.Decide();
            if (decideParam) {
                enemyController.ChangeState(transitions[i].trueState);
            } else {
                enemyController.ChangeState(transitions[i].falseState);
            }
        }

    }
}