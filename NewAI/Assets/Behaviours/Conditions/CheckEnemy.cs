using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase

[Condition("MyConditions/HealthLow")]
[Help("Checks if enemy health is lower than given parameter")]
public class HealthLow : ConditionBase
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("LowHealth")]
    public int lowHealth;
    public override bool Check()
    {
        if(ec.hp <= lowHealth){
            return true;
        }
        return false;
    }
}

[Condition("MyConditions/IsInvisibilityOn")]
[Help("Checks if enemy is invisible")]
public class InvisibiliyOn : ConditionBase
{
    [InParam("EnemyController")]
    public EnemyController ec;

    public override bool Check()
    {
        if(ec.GetComponent<LightInfected>()){
            if(!ec.GetComponent<MeshRenderer>().enabled){
                return true;
            }
        }
        return false;
    }
}
