using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerType{Energy, Heal, Source}
public class Flower : MonoBehaviour
{
    public FlowerType flowerType;
    public int healHp;
    public int getEnergy;
    public int amountOfSources;

    public GameObject HUD;

    void Update(){
        HUD.transform.position = new Vector3(transform.position.x, 2, transform.position.z);
    }
}
