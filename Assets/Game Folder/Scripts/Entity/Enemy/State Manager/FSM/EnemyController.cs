using UnityEngine;

public class EnemyController : MonoBehaviour
{
     public StateType initialState;
    public FSM_State[] stateLists;
    public FSM_State CurrentState {get; private set;}

    

    void Start()
    {
        ChangeState(initialState);
    }
    void Update()
    {
        CurrentState?.UpdateState(this);
    }


    public void ChangeState(StateType newStateType) {
        FSM_State newState = GetState(newStateType);
        if (newState == null) return;
        CurrentState = newState;
    }
    public FSM_State GetState(StateType newState) {
        for (int i = 0; i < stateLists.Length; i++) {
            if (stateLists[i].StateType == newState) {
                return stateLists[i];
            }
        }
        return null;
    }
}