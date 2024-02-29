using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody bulletBody;
    [SerializeField] private CapsuleCollider bulletCollider;

    public GunController gunController;

    private float gravitationalConstant = 6.67430f;
    private float satelliteMass = 50f;

    [SerializeField]private List<Rigidbody> bulletBodies = new List<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        if(bulletBody == null)
            bulletBody = GetComponent<Rigidbody>();

        if(bulletCollider == null)
            bulletCollider = GetComponent<CapsuleCollider>();

        //Force destroy just in case
        Destroy(this.gameObject, 60f);
    }

    private void Update()
    {
        CheckGunControllerInUpdate();
    }

    private void FixedUpdate()
    {
        CheckActiveRigigBodies();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float TimeToDestroy = 0f;

        switch (gunController.bulletType)
        {
            case GunController.TypeOfBullet.parabolicPath:
                TimeToDestroy = 2f;
                break;
            case GunController.TypeOfBullet.Gravity:
                TimeToDestroy = 5f;
                break;
            case GunController.TypeOfBullet.Explosion:
                TimeToDestroy = 1f;
                break;
        }
        Destroy(this.gameObject, TimeToDestroy);
    }



    private void OnTriggerEnter(Collider other)
    {
        TriggerProps(other.gameObject);
    }

    private void TriggerProps(GameObject other) 
    {
        if(other == null) return;

        if (other.layer == LayerMask.NameToLayer("Bullet")) return;

        Rigidbody rigidbody = other.GetComponent<Rigidbody>();

        if (rigidbody == null) return;

        if (bulletBodies.Contains(rigidbody)) return;

        bulletBodies.Add(rigidbody);

        if (bulletBodies.Count < 1) return;

        switch (gunController.bulletType)
        {
            case GunController.TypeOfBullet.parabolicPath:
                break;
            case GunController.TypeOfBullet.Gravity:
                break;
            case GunController.TypeOfBullet.Explosion:

                float explosionForce = 1000f;
                float explosionRadius = 30f;

                bulletBodies[bulletBodies.Count - 1].AddExplosionForce(explosionForce, transform.position, explosionRadius);

                SpawnBulletVFX();

                break;
        }
    }

    private void CheckActiveRigigBodies() 
    {
        if (bulletBodies.Count < 1) return;

        for (int i = 0; i < bulletBodies.Count; i++) 
        {
            switch (gunController.bulletType)
            {
                case GunController.TypeOfBullet.parabolicPath:
                    break;
                case GunController.TypeOfBullet.Gravity:

                    Vector3 directionToCenter = transform.position - bulletBodies[i].transform.position;

                    float distanceSquared = directionToCenter.sqrMagnitude;
                    Vector3 force = (gravitationalConstant * satelliteMass * bulletBody.mass / distanceSquared) * directionToCenter.normalized;

                    bulletBodies[i].AddForce(force);

                    break;
                case GunController.TypeOfBullet.Explosion:
                    break;
            }

            
        }
    }

    public void MoveBullet()
    {
        if (bulletBody == null) return;

        Vector3 force = Vector3.zero;

        switch (gunController.bulletType)
        {
            case GunController.TypeOfBullet.parabolicPath:

                force = transform.TransformDirection(Vector3.forward) * 15f;

                break;
            case GunController.TypeOfBullet.Gravity:

                bulletBody.constraints = RigidbodyConstraints.FreezePositionY;
                bulletBody.isKinematic = true;

                break;
            case GunController.TypeOfBullet.Explosion:

                force = transform.TransformDirection(Vector3.forward) * 20f;
                
                break;
        }

        if(force != Vector3.zero)
            bulletBody.AddForce(force, ForceMode.Impulse);
    }

    public void CheckGunControllerInUpdate() 
    {
        if(gunController == null) return;

        switch (gunController.bulletType)
        {
            case GunController.TypeOfBullet.parabolicPath:
                break;
            case GunController.TypeOfBullet.Gravity:

                if(Vector3.Distance(gunController.transform.position, transform.position) <= 25f)
                    gameObject.transform.Translate(Vector3.forward * 5f *Time.deltaTime);
                else
                    Destroy(gameObject);

                break;
            case GunController.TypeOfBullet.Explosion:
                break;
        }
    }

    private void SpawnBulletVFX() 
    {
        if (AppManager.Instance == null) return;
        if (AppManager.Instance.weaponsSettings == null) return;
        if (AppManager.Instance.weaponsSettings.explosionVFx == null) return;

        GameObject vfx = Instantiate(AppManager.Instance.weaponsSettings.explosionVFx);

        if(vfx == null) return;

        vfx.transform.position = transform.position;

        Destroy(vfx, 2f);
    }
}
