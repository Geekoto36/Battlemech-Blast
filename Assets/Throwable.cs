using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour, IThrowable
{


    public int m_CurrentAmmo;
    private float m_Radius;
    private float m_Damage;
    private float m_DamageInterval;
    private float m_ElapseDuration;
    private float m_TimeToExecute;
    private float m_TimeToDie;
    private float m_ShootForce;
    private float m_TorqueSpeed;

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

    }


    void Update()
    {

    }



}
