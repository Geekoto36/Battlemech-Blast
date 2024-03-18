using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Weapon, IWeapon
{

    private PlayerAttackController attackController;



    


    void Start()
    {
        attackController = transform.parent.GetComponent<WeaponManager>().attackController;

    }


    void Update()
    {

    }
    public void Execute()
    {
        if (attackController.IsThrowingWeapon())
        {
            attackController.ThrowWeapon();
        }
    }

    public void IncreaseAmmo(int amount)
    {

    }

    public void Reload()
    {

    }

    public override void Trigger()
    {
        throw new System.NotImplementedException();
    }
}
