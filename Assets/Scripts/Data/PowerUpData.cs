using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { bagBooster }
[CreateAssetMenu(fileName = "PowerUp Data", menuName = "Scriptable Object/PowerUp Data", order = 1)]
public class PowerUpData : ScriptableObject
{
    public PowerUpType powerUpType;
    public int boostCount;
    
}
