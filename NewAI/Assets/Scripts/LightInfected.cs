using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInfected : MonoBehaviour
{
    public int specialAttackDamage = 10;

    //Abilities
    public void ChangeVisibility(bool isVisible){
        gameObject.GetComponent<MeshRenderer>().enabled = isVisible;
    }

    public void SwitchVisibility(){
        gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent<MeshRenderer>().enabled;
    }

    public void Attack(CharacterController cc){
        cc.TakeDagame(specialAttackDamage, this.gameObject);
    }
}
