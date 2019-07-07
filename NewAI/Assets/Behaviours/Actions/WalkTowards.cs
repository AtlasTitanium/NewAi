using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction


[Action("MyActions/AStarMove/WalkTowardsEnergySlow")]
[Help("Walk the npc towards the a star target")]
public class WalkTowardsEnergySlow : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("AStarTarget")]
    public Transform aStarTarget;
    private float speed = 0.5f;

    public override TaskStatus OnUpdate()
    {
        aStarTarget.position = ec.closestEnergySource.position;

        ec.GetComponent<Unit>().speed = speed;
        
        return TaskStatus.COMPLETED;
    } 
} 

[Action("MyActions/AStarMove/WalkTowardsEnergy")]
[Help("Walk the npc towards the a star target")]
public class WalkTowardsEnergy: BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("AStarTarget")]
    public Transform aStarTarget;
    private float speed = 1;

    public override TaskStatus OnUpdate()
    {
        aStarTarget.position = ec.closestEnergySource.position;
        ec.GetComponent<Unit>().speed = speed;
        if(Vector3.Distance(ec.closestEnergySource.position,ec.transform.position) <= 5){
            ec.transform.LookAt(ec.closestEnergySource.position);
            return TaskStatus.COMPLETED;
        }
        
        return TaskStatus.RUNNING;
    } 
}

[Action("MyActions/AStarMove/RunTowardsEnergy")]
[Help("Walk the npc towards the a star target (will only finish once at the object)")]
public class RunTowardsEnergy: BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("AStarTarget")]
    public Transform aStarTarget;

    private float speed = 2;
    public override TaskStatus OnUpdate()
    {
        aStarTarget.position = ec.closestEnergySource.position;
    
        ec.GetComponent<Unit>().speed = speed;

        if(Vector3.Distance(ec.closestEnergySource.position,ec.transform.position) <= 5){
            ec.transform.LookAt(ec.closestEnergySource.position);
            return TaskStatus.COMPLETED;
        }
        
        return TaskStatus.RUNNING;
    } 
} 

[Action("MyActions/AStarMove/RunAway")]
[Help("Run away from current place")]
public class RunAway: BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("AStarTarget")]
    public Transform aStarTarget;

    [InParam("RunAwayDistance")]
    public int distance;

    private Vector3 loc = Vector3.zero;
    private float speed = 4;
    private float startTime;

    public override TaskStatus OnUpdate()
    {
        if(loc == Vector3.zero){
            aStarTarget.position = -ec.transform.forward * distance;
            loc = aStarTarget.position;
        }

        ec.GetComponent<Unit>().speed = speed;

        if(Vector3.Distance(ec.transform.position, aStarTarget.position) <= 5){
            ec.Heal(40);
            if(ec.hp >= 40){
                 return TaskStatus.COMPLETED;
            }

            ec.transform.LookAt(ec.closestEnergySource.position);
            loc = Vector3.zero;
        }
        
        return TaskStatus.RUNNING;
    } 
} 