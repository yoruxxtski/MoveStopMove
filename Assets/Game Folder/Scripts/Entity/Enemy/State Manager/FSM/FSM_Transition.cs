using System;
using UnityEngine;

[Serializable]
public class FSM_Transition 
{
    public FSM_Decision decision;
    public StateType trueState;
    public StateType falseState;
}