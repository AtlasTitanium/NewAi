using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/LightAbilities/InvisibilityOff")]
[Help("Turn the light infected invisibility off")]
public class InvisibilityOff : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    public override TaskStatus OnUpdate()
    {
        if(ec.infectedType != InfectedType.Light){
            return TaskStatus.FAILED;
        }

        ec.GetComponent<LightInfected>().ChangeVisibility(true);
        
        return TaskStatus.COMPLETED;
    } 
} 

[Action("MyActions/LightAbilities/InvisibilityOn")]
[Help("Turn the light infected invisibility on")]
public class InvisibilityOn : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    public override TaskStatus OnUpdate()
    {
        if(ec.infectedType != InfectedType.Light){
            return TaskStatus.FAILED;
        }

        ec.GetComponent<LightInfected>().ChangeVisibility(false);
        
        return TaskStatus.COMPLETED;
    } 
} 

[Action("MyActions/LightAbilities/SwitchInvisibility")]
[Help("Turn the light infected invisibility on")]
public class InvisibilitySwitch : BasePrimitiveAction
{
    [InParam("EnemyController")]
    public EnemyController ec;

    public override TaskStatus OnUpdate()
    {
        if(ec.infectedType != InfectedType.Light){
            return TaskStatus.FAILED;
        }

        ec.GetComponent<LightInfected>().SwitchVisibility();
        
        return TaskStatus.COMPLETED;
    } 
} 