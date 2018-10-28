using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Range")]
public class EquipmentRange : EquipmentClass
{

    [Space(10)]
    public GameObject bulletObj;
    public float bulletLifetime;
    public float bulletSpeed;


    public override void Usage(GameObject origin, GameObject damagingObj)
    {
        Debug.Log("I am Shooting");
        CharacterMovement characterMoveScript = origin.GetComponent<CharacterMovement>();

        characterMoveScript.charSpriteObj.GetComponent<SpriteRenderer>().sprite = sprite;

        float facingRotateValue = characterMoveScript.faceDir;
        Vector3 playerFacingPos = Quaternion.AngleAxis(facingRotateValue, Vector3.up) * new Vector3(1, 0, 0);
        Vector3 weaponStartPos = origin.transform.position + characterMoveScript.charSize.y / 2 * Vector3.up + playerFacingPos;


        GameObject projectileObj = Instantiate(bulletObj, weaponStartPos, Quaternion.identity);

        projectileObj.AddComponent<ProjectileLogic>().Init(playerFacingPos, bulletLifetime, bulletSpeed, subbehaviourType);
      
        cooldownLeft = cooldownCount;
        //ADD COOLDOWN and RELOAD LIMIT
    }

    public override void TempUsage(GameObject origin, GameObject damagingObj)
    {
        Debug.Log("Tempororay Turret");
        GameObject projectileObj = Instantiate(bulletObj, origin.transform.position, Quaternion.identity);
        projectileObj.AddComponent<ProjectileLogic>().Init(origin.transform.localScale.x * Vector3.left, bulletLifetime, bulletSpeed, subbehaviourType);

    }

}
