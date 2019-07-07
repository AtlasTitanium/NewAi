using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/VoidAbilities/BecomeIntangable")]
[Help("Makes the enemy become intangable for given amount of seconds")]
public class BecomeIntangable : BasePrimitiveAction
{
    [InParam("VoidInfectedBehaviour")]
    public VoidInfected vi;

    [InParam("CooldownTime")]
    public int seconds;

    public override TaskStatus OnUpdate()
    {
        if(vi.isIntangable){
            return TaskStatus.FAILED;
        } else {
            vi.IntangableOn(seconds);
            return TaskStatus.COMPLETED;
        }
    } 
} 

[Action("MyActions/VoidAbilities/ShieldSelf")]
[Help("Shields the infected")]
public class ShieldSelf : BasePrimitiveAction
{
    [InParam("VoidInfectedBehaviour")]
    public VoidInfected vi;

    [InParam("Cooldown")]
    public int seconds;

    private float startTime = 0;

    public override TaskStatus OnUpdate()
    {   
        if(startTime == 0){
            vi.ShieldSelf();
            startTime = Time.time;
            return TaskStatus.COMPLETED;
        }

        if(startTime + seconds <= Time.time){
            startTime = 0;
        }

        return TaskStatus.FAILED;
    } 
}

[Action("MyActions/VoidAbilities/ShieldAllies")]
[Help("Shields the infected")]
public class ShieldAllies : BasePrimitiveAction
{
    [InParam("VoidInfectedBehaviour")]
    public VoidInfected vi;

    [InParam("Cooldown")]
    public int seconds;

    private float startTime = 0;

    public override TaskStatus OnUpdate()
    {   
        if(startTime == 0){
            vi.ShieldAllies();
            startTime = Time.time;
            return TaskStatus.COMPLETED;
        }

        if(startTime + seconds <= Time.time){
            startTime = 0;
        }

        return TaskStatus.FAILED;
    } 
}