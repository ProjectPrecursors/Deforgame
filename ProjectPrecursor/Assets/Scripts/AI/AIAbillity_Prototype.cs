using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAbillity_Prototype : MonoBehaviour {

    /// <summary>
    /// Acts as the AI's Brain 
    /// Similar to Character Abillity script
    /// </summary>
    
    public EquipmentRange rangeShooter;
    public GameObject shootOrigin;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("ShootSpit", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {   
        

    }
    void ShootSpit()
    {
        rangeShooter.TempUsage(shootOrigin, rangeShooter.bulletObj);
    }
}
