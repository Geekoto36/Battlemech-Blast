using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : Throwable
{


    private List<GameObject> colliders = new List<GameObject>();
    [HideInInspector] public PhotonView myOwner;

    public GameObject collisionEffect;
    private bool isExecuted = false;
    private bool isShooted = false;

    private void Start()
    {

    }

    private void Update()
    {
        CheckColliderBeforeExecute();

        ProjctileMovement(target, origin, isShooted);
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
        KillTime();
    }
    private void CheckColliderBeforeExecute()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, Radius);
        if (hit != null && !isExecuted && hit.gameObject != this.gameObject)
        {
            if (hit.GetComponent<PhotonView>() != null)
                if (hit.GetComponent<PhotonView>().Owner == myOwner.Owner)
                    return;
            Debug.Log("Collides with " + hit.name + "\n");

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
                if (hits[i].GetComponent<PhotonView>() != null)
                    if (hits[i].GetComponent<PhotonView>().Owner == myOwner.Owner)
                        return;
                Debug.Log(i + ": " + hits[i].name + "\n");
                colliders.Add(hits[i].gameObject);
            }
        }

        StartCoroutine(DamageByInterval(colliders));
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

    Vector2 target;
    Vector2 origin;
    public void ProjctileMovement(Vector2 targetPosition, Vector2 origin, bool isShooted)
    {

        this.isShooted = isShooted;

        if (!this.isShooted)
            return;

        target = targetPosition;
        this.origin = origin;
        float dist = target.x - this.origin.x;

        Vector2 nextX = Vector2.MoveTowards(transform.position, target, ShootForce * Time.deltaTime);
        //float baseY = Mathf.Lerp(this.origin.y, target.y, (nextX - target.x) / dist);
        float height = 2 * (nextX.x - this.origin.x) * (nextX.x - this.target.x) / (-0.5f * dist * dist);

        Vector3 movePosition = new Vector3(nextX.x, nextX.y + 0, transform.position.z);

        transform.position = movePosition;

    }

    public Vector2 CalculateVelocity(Vector2 target, Vector2 origin, float t)
    {
        Vector2 distance = target - origin;


        float Sy = distance.y;
        float Vy = Sy / t + 0.5f * Mathf.Abs(Physics2D.gravity.y) * t;

        Vector2 result = Vector2.zero;

        result.y = distance.y;

        return result;
    }


}
