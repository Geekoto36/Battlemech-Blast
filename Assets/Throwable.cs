using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Weapon, IThrowable, IWeapon
{

    private PlayerAttackController attackController;



    public int m_CurrentAmmo;
    [SerializeField] protected float m_Radius;
    [SerializeField] protected float m_Damage;
    [SerializeField] protected float m_DamageInterval;
    [SerializeField] protected float m_ElapseDuration;
    [SerializeField] protected float m_TimeToExecute;
    [SerializeField] protected float m_TimeToDie;
    [SerializeField] protected float m_ShootForce;
    [SerializeField] protected float m_TorqueSpeed;

    [SerializeField] private bool m_isFiring;



    #region Interface_Variables

    public int CurrentAmmo { get => m_CurrentAmmo; }
    public float Radius => m_Radius;
    public float Damage => m_Damage;
    public float DamageInterval => m_DamageInterval;
    public float ElapseDuration => m_ElapseDuration;
    public float TimeToExecute => m_TimeToExecute;
    public float TimeToDie => m_TimeToDie;
    public float ShootForce => m_ShootForce;
    public float TorqueSpeed => m_TorqueSpeed;





    #endregion


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

    public void IncreaseAmmo()
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
