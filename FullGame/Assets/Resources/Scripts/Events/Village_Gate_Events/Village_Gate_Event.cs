using UnityEngine;
using System.Collections;

public class Village_Gate_Event : Event
{
    private GameObject leftGate1, rightGate1;
    private Animator GateAnimator;

    void Start() 
    {
        base.Start();

        leftGate1 = GameObject.FindGameObjectWithTag("LeftGate1");
        rightGate1 = GameObject.FindGameObjectWithTag("RightGate1");
        GateAnimator = Trigger.GetComponent<Animator>();
    }

    protected override void ResolveEvent()
    {
       if(Trigger.name.Equals("Gate_Trigger_1"))
           Gate_Event_1();
       else
           Gate_Event_2();
    }

    private void Gate_Event_1()
    {
        GateAnimator.SetBool("Close_Left_Gate_1", true);
        GateAnimator.SetBool("Close_Right_Gate_1", true);
    }

    private void Gate_Event_2()
    {
    
    }
}
