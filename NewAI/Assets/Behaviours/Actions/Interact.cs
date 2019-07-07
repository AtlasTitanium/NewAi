using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Attack/DestroyEnergySource")]
[Help("only works on objects, npc hits object for given seconds and will then destroy object")]
public class DestroyEnergySource : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("SecondsTillDestroy")]
    public int seconds;

    private float startTime;
    public override TaskStatus OnUpdate()
    {
        if(startTime == 0){
            startTime = Time.time;
        }

        if(startTime + seconds <= Time.time){
            ec.DestroyES();
            return TaskStatus.COMPLETED;
        }
        //Debug.Log("Start time = " + startTime + " And current time is = " + Time.time);
        return TaskStatus.RUNNING;
    } 
} 

[Action("MyActions/Attack/StealthAttack")]
[Help("strong attack when invisible, parameter desides strenghtMultiplier")]
public class StealthAttack : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("Multiplier")]
    public int multiplier;

    [InParam("TimeUntillAttack")]
    public int seconds;

    private float startTime = 0;

    public override TaskStatus OnUpdate()
    {
        if(startTime == 0){
            startTime = Time.time;
        }

        if(startTime + seconds <= Time.time){
            ec.GetComponent<LightInfected>().ChangeVisibility(true);
            ec.Attack(ec.attackDamage * multiplier);
            startTime = 0;
            return TaskStatus.COMPLETED; 
        }
        
        return TaskStatus.RUNNING;
    } 
} 

[Action("MyActions/Attack/NormalAttack")]
[Help("strong attack when invisible, parameter desides strenghtMultiplier")]
public class NormalAttack : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("TimeAfterAttack")]
    public int seconds;

    private float startTime = 0;

    public override TaskStatus OnUpdate()
    {
        if(startTime == 0){
            ec.Attack(ec.attackDamage);
            startTime = Time.time;
        }

        if(startTime + seconds <= Time.time){
            startTime = 0;
            return TaskStatus.COMPLETED; 
        }
        
        return TaskStatus.RUNNING;
    } 
} 

[Action("MyActions/Attack/SpecialAttack")]
[Help("uses the special attack of the enemy")]
public class SpecialAttack : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    [InParam("TimeAfterAttack")]
    public int seconds;

    private float startTime = 0;

    public override TaskStatus OnUpdate()
    {
        if(startTime == 0){
            ec.SpecialAttack();
            startTime = Time.time;
        }

        if(startTime + seconds <= Time.time){
            startTime = 0;
            return TaskStatus.COMPLETED; 
        }
        
        return TaskStatus.RUNNING;
    } 
}
