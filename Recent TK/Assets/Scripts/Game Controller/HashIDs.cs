using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int dyingState;
    public int deadBool;
    public int walkState;
    public int shoutState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int runningBool;
    public int backwardsBool;

    private void Awake()
    {
        dyingState = Animator.StringToHash("Baser Layer.Dying");
        deadBool = Animator.StringToHash("Dead");
        walkState = Animator.StringToHash("walk");
        shoutState = Animator.StringToHash("Shouting.shout");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");
        runningBool = Animator.StringToHash("Running");
        backwardsBool = Animator.StringToHash("Backwards");
    }
}
