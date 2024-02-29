using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public enum TypeOfBullet 
    {
        parabolicPath,
        Gravity,
        Explosion
    }

    public TypeOfBullet bulletType;

    [SerializeField]private GameObject bulletReference;
    [SerializeField]private GameObject currentBullet;
    [SerializeField] private BoxCollider localCollider;

    private bool mayShoot = false;

    [SerializeField] private GameObject clickOnMeText;

    public Vector3 originalPosition;

    float timeToFire = 3f;

    private void Awake()
    {
        originalPosition = transform.position;

        if(localCollider == null )
            localCollider = GetComponent<BoxCollider>();
    }
    
    private void Update()
    {
        FireBullet();
    }

    private void OnMouseUp()
    {
        ClickToTakeGun();
    }

    public void SetTextEnable(bool enable) 
    {
        if (clickOnMeText == null) return;

        clickOnMeText.SetActive(enable);
    }

    private void ClickToTakeGun() 
    {
        TakeGun(AppManager.Instance.characterManager.characterPlayer.transform);

        switch (bulletType)
        {
            case TypeOfBullet.parabolicPath: 
                transform.localEulerAngles = new Vector3(-45f, 0f, 0f);
                break;
            case TypeOfBullet.Gravity:
                transform.localEulerAngles = Vector3.zero;
                break;
            case TypeOfBullet.Explosion:
                break;
        }
    }

    public void TakeGun(Transform parent) 
    {
        if (CharacterManager.Instance.currentGun != null)
            CharacterManager.Instance.currentGun.FreeGun();

        CharacterManager.Instance.currentGun = this;

        SetTextEnable(false);

        transform.SetParent(parent);
        transform.transform.localPosition = new Vector3(0.8f, 0.5f, 1.5f);

        mayShoot = true;
    }

    public void FreeGun() 
    {
        mayShoot = false;

        timeToFire = 3f;

        transform.SetParent(null);
        transform.position = originalPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        SetTextEnable(true);
    }

    public void FireBullet() 
    {
        if (!mayShoot) return;

        timeToFire += Time.deltaTime;

        if (timeToFire < 3f) return;

        timeToFire = 0f;

        currentBullet = Instantiate(bulletReference, transform);
        currentBullet.transform.localPosition = new Vector3(0, 0.5f, 0f);
        currentBullet.transform.localEulerAngles = Vector3.zero;

        currentBullet.transform.SetParent(null);
        currentBullet.transform.localScale = Vector3.one;

        BulletController bulletController = currentBullet.GetComponent<BulletController>();

        if (bulletController == null) return;

        bulletController.gunController = this;

        bulletController.MoveBullet();
    }
}
