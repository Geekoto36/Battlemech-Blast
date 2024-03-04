using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : Throwable
{


    private List<GameObject> colliders = new List<GameObject>();
    [HideInInspector] public PhotonView myOwner;

    public GameObject collisionEffect;
    private bool isExecuted = false;

    private void Start()
    {

    }

    private void Update()
    {
        CheckHitColliders();
        KillTime();
    }

    private void ExecuteWeapon()
    {
        //Execute Collision effect
        ExplosionEffect();
        CheckHitColliders();

        //

        isExecuted = true;
    }


    public void ExplosionEffect()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //Bullet hit point effect
        GameObject hitObject = Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(hitObject, 3f);
        Destroy(this.gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && !isExecuted)
        {
            ExecuteWeapon();
        }
    }

    private void CheckHitColliders()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, Radius);
        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].tag == "Player" || hits[i].tag == "Enemy"
                || hits[i].tag == "EnemyRanged" || hits[i].tag == "EnemyClose")
            {
                Debug.Log(i + ": " + hits[i].name + "\n");
                colliders.Add(hits[i].gameObject);
            }
        }

        DamageByInterval(colliders);
    }

    private IEnumerator DamageByInterval(List<GameObject> hitObjects)
    {
        //Stun player for a while
        for (int i = 0; i < colliders.Count; i++)
        {
            DealDamage(hitObjects[i]);
        }
        yield return new WaitForSeconds(DamageInterval);

    }
    private void KillTime()
    {
        if (!isExecuted) return;
        Destroy(gameObject, TimeToDie);

    }
    private void DealDamage(GameObject hitObject)
    {
        if (hitObject.GetComponent<PhotonView>().Owner == myOwner.Owner && hitObject.tag == "Player") return;

        hitObject.GetComponent<HealthSystem>().LoseHealth((int)Damage, myOwner);
    }

}
