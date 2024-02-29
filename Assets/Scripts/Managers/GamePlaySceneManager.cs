using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelRenderReference;
    [SerializeField]private Vector3 gunsSpawnPointsReference;

    private void Start()
    {
        SpawnCharacterRenderPanel();

        SpawnWeapons();
    }

    private void SpawnCharacterRenderPanel() 
    {
        if (panelRenderReference == null) return;

        GameObject panel = Instantiate(panelRenderReference, AppManager.Instance.UIManager.UIcanvas.transform);

        if (panel == null) return;

        RectTransform rectTransform = panel.GetComponent<RectTransform>();

        if(rectTransform == null) return;

        rectTransform.anchorMax = Vector3.zero;
        rectTransform.anchorMin = Vector3.zero;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.position = new Vector2(286.1f, 265.2f);
        rectTransform.sizeDelta = new Vector2(572.3f, 530.4f);

        Image panelImage = panel.GetComponent<Image>();

        panelImage.color = Color.white;
    }

    private void SpawnWeapons() 
    {
        if(AppManager.Instance ==  null) return;
        if(AppManager.Instance.weaponsSettings.gunPrefab == null) return;

        List<ScriptableWeapons_Settings.WeaponsSetting> weaponsSettings = AppManager.Instance.weaponsSettings.weaponsSetting;

        if (weaponsSettings.Count < 1) return;
        
        for (int i=0; i < weaponsSettings.Count; i++) 
        {
            GameObject gun = Instantiate(AppManager.Instance.weaponsSettings.gunPrefab);

            if(gun == null) continue;

            gun.transform.position = gunsSpawnPointsReference;

            GunController gunController = gun.GetComponent<GunController>();

            if (gunController != null) 
            {
                gunController.originalPosition = gunsSpawnPointsReference;
                gunController.bulletType = weaponsSettings[i].bulletType;
            }

            gunsSpawnPointsReference.x += 1.2f;

            Material material = gun.GetComponent<MeshRenderer>().material;

            if(material ==  null) continue;

            material.color = weaponsSettings[i].gunColor;

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(gunsSpawnPointsReference, 0.25f);
    }
}
