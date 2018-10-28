using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour {

    public Vector3 directionPath;
    public float lifeTime;
    public float travelSpeed;
    public EquipmentClass.eqpBehaviourType typeOfTravel;

    private float lift; //FUCKING TEMPORARY

    public void Init(Vector3 dirPath, float lifeTime, float spd, EquipmentClass.eqpBehaviourType type)
    {   
        directionPath = dirPath;
        travelSpeed = spd;
        typeOfTravel = type;
        lift = 1;
        Destroy(this.gameObject, lifeTime);
    }
    // Update is called once per frame
    void Update () {
        if (typeOfTravel.Equals(EquipmentClass.eqpBehaviourType.singleStraight))
        {
            transform.position += directionPath * travelSpeed;
        }
        else if (typeOfTravel.Equals(EquipmentClass.eqpBehaviourType.singleTrajectory))
        {
            //WILL CODE A MORE PRECISE TRAJECTORY ONCE MOUSE INPUT ANGLE DONE
            lift += Physics.gravity.y/200;
            transform.position += directionPath * travelSpeed + (Vector3.up*(lift)) * travelSpeed;
        }
        

    }
}
