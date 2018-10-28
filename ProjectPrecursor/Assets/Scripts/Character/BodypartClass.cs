using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BodypartClass {

    public enum bodyPartsSlot { head, upBody, lowBody, rightarm, leftarm, rightleg, leftleg, inventory, none } // 7
    public bodyPartsSlot bodySlot;
    public int numOfSecSlot;

    public EquipmentClass priSlot;
    public List<EquipmentClass> secSlot;

    public enum healthStatus { incaincapacitate, damage, healthy, none } //damaged means can be healed, but currently cannot used
    public healthStatus currHealthStats;
    public float currHealth;

    public BodypartClass()
    { //DEFAULT
        bodySlot = bodyPartsSlot.none;
        numOfSecSlot = -999;
        currHealthStats = healthStatus.none;
        currHealth = 1.0f;
        priSlot = null;
        secSlot = null;
    }

    public BodypartClass(bodyPartsSlot body, int num, healthStatus status, float currHP, EquipmentClass slot, List<EquipmentClass> slot2)
    {
        bodySlot = body;
        numOfSecSlot = num;
        currHealthStats = status;
        currHealth = currHP;
        priSlot = slot;
        secSlot = slot2;
    }
    
    public static int FindBodyPartIndex(BodypartClass.bodyPartsSlot bodyPart)
    {
        switch (bodyPart)
        {
            case BodypartClass.bodyPartsSlot.head:
                return 0;

            case BodypartClass.bodyPartsSlot.upBody:
                return 1;

            case BodypartClass.bodyPartsSlot.lowBody:
                return 2;

            case BodypartClass.bodyPartsSlot.rightarm:
                return 3;

            case BodypartClass.bodyPartsSlot.leftarm:
                return 4;

            case BodypartClass.bodyPartsSlot.rightleg:
                return 5;

            case BodypartClass.bodyPartsSlot.leftleg:
                return 6;
            default:
                return -1;
        }
    }
        
    
}
