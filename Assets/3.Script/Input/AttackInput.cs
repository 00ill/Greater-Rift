using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    InteractInput interactInput;
    //AttackHandler attackHandler;

    private void Awake()
    {
        interactInput = GetComponent<InteractInput>();
        //attackHandler = GetComponent<AttackHandler>();
    }

    public bool AttackCheck()
    {
        return interactInput.attackTarget != null;
    }
}
