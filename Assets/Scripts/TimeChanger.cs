using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChanger : Pickup
{
    [SerializeField]
    bool isAddingTime;
    [SerializeField]
    int time = 10;

    public override void Picked()
    {
        int sign;
        if (isAddingTime) sign = 1;
        else sign = -1;
        GameManager.instance.AddTime(time * sign);
        base.Picked();
    }
}
