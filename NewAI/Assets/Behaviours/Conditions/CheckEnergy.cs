using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase

[Condition("MyConditions/CheckEnergy10")]
[Help("Checks if the npc is closer than 10 units from an energy source")]
public class CheckEnergy10 : ConditionBase
{
    [InParam("EnemyController")]
    public EnemyController ec;
    public override bool Check()
    {
        if(ec.closestEnergySource != null){
            if(Vector3.Distance(ec.closestEnergySource.position, ec.transform.position) <= 10){
                return true;
            }
        }
        return false;
    }
}

[Condition("MyConditions/CheckEnergy20")]
[Help("Checks if the npc is closer than 20 units from an energy source")]
public class CheckEnergy20 : ConditionBase
{
    [InParam("EnemyController")]
    public EnemyController ec;
    public override bool Check()
    {
        if(ec.closestEnergySource != null){
            if(Vector3.Distance(ec.closestEnergySource.position, ec.transform.position) <= 20){
                return true;
            }
        }
        return false;
    }
}

[Condition("MyConditions/EnergyIsPlayer")]
[Help("Checks if the energySource is the player")]
public class EnergySourcePlayer : ConditionBase
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("PlayerTag")]
    public string playerTag;
    public override bool Check()
    {
        if(ec.closestEnergySource.tag == playerTag){
            return true;
        }
        return false;
    }
}

[Condition("MyConditions/EnergyIsObject")]
[Help("Checks if the energySource is an object instead of the player")]
public class EnergySourceObject : ConditionBase
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("PlayerTag")]
    public string playerTag;
    public override bool Check()
    {
        if(ec.closestEnergySource.tag == playerTag){
            return false;
        }
        return true;
    }
}
