using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Melee")]
public class EquipmentMelee : EquipmentClass {


    public override void Usage(GameObject origin, GameObject damagingObj)
    {
        Debug.Log("I am slashing");
        CharacterMovement characterMoveScript = origin.GetComponent<CharacterMovement>();

        characterMoveScript.charSpriteObj.GetComponent<SpriteRenderer>().sprite = sprite;
        characterMoveScript.charSpriteObj.GetComponent<Animator>().SetTrigger("isMelee");
        float facingRotateValue = characterMoveScript.faceDir;
        Vector3 playerFacingPos = Quaternion.AngleAxis(facingRotateValue, Vector3.up)* new Vector3(1,0,0);
        Vector3 weaponStartPos = origin.transform.position + characterMoveScript.charSize.y/2*Vector3.up + playerFacingPos;

        GameObject meleehitbox = Instantiate(damagingObj, weaponStartPos, Quaternion.identity);

        Destroy(meleehitbox, 1f);
    }
}
