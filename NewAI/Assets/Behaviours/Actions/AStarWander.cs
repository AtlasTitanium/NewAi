using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction


[Action("MyActions/AStarMove/AStarWander")]
[Help("Makes the NPC wander around within the given parameters")]
public class ShootOnce : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("AStarTarget")]
    public Transform aStarTarget;

    [InParam("WalkParameters")]
    public Vector2 walkParameters;

    private float randomXLoc = 0;
    private float randomZLoc = 0;
    public override TaskStatus OnUpdate()
    {   
        if(randomXLoc == 0){
            randomXLoc = Random.Range((ec.transform.position.x - walkParameters.x),(ec.transform.position.x + walkParameters.x));
        }
        if(randomZLoc == 0){
            randomZLoc = Random.Range((ec.transform.position.z - walkParameters.y),(ec.transform.position.z + walkParameters.y));
        }
        
        aStarTarget.position = new Vector3(randomXLoc,0,randomZLoc);

        if(Vector3.Distance(aStarTarget.position, ec.transform.position) <= 2){
            randomXLoc = 0;
            randomZLoc = 0;
            return TaskStatus.COMPLETED;
        }
        
        return TaskStatus.RUNNING;    
    } 

} 

[Action("MyActions/AStarMove/ChangeMovementSpeed")]
[Help("changes the movement speed to given speed")]
public class ChangeMovementSpeed : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("Speed")]
    public float speed;

    public override TaskStatus OnUpdate()
    {
        ec.GetComponent<Unit>().speed = speed;
        
        return TaskStatus.COMPLETED;
    } 
} 
