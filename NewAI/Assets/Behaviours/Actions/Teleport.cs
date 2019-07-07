using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/VoidAbilities/TeleportToSource")]
[Help("Teleport to energy source")]
public class TeleportToSource : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    public override TaskStatus OnUpdate()
    {
        ec.transform.position = ec.closestEnergySource.position - (ec.closestEnergySource.position - ec.transform.position) * 0.4f;
        return TaskStatus.COMPLETED;
    } 
} 

[Action("MyActions/VoidAbilities/TeleportToObject")]
[Help("Teleport to given gameobject")]
public class TeleportToObject : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("Target to telport")]
    public GameObject target;

    public override TaskStatus OnUpdate()
    {
        ec.transform.position = target.transform.position - (target.transform.position - ec.transform.position) * 0.4f;
        return TaskStatus.COMPLETED;
    } 
} 

[Action("MyActions/VoidAbilities/TeleportWithCooldown")]
[Help("Teleport to given energySource with cooldown")]
public class TeleportWithCooldown : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;
    [InParam("Cooldown time")]
    public int seconds;

    [InParam("VoidInfected")]
    public VoidInfected vi;

    public override TaskStatus OnUpdate()
    {
        if(vi.hasTeleported){
            return TaskStatus.FAILED;
        } else {
            ec.transform.position = ec.closestEnergySource.position - (ec.closestEnergySource.position - ec.transform.position) * 0.4f;
            vi.Teleport(seconds);
            return TaskStatus.COMPLETED;
        }
            
        
    } 
} 