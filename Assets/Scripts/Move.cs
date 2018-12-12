using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public int moveId;
    public string name;
    public float timeIncrease;
    public float cooldownTime;
    public Buttons[] sequence;

    public Move(int id, string name, float timeIncrease, float cooldownTime, params Buttons[] sequence)
    {
        this.moveId = id;
        this.name = name;
        this.timeIncrease = timeIncrease;
        this.cooldownTime = cooldownTime;
        this.sequence = sequence;
    }
}
