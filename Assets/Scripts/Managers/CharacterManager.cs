using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterManager : SubManagersBase
{
    public static CharacterManager Instance = null;

    public CharacterPlayer characterPlayer;

    public GunController currentGun;

    public static Action<C_AnimatorController.AnimationStates> changeAnimation = delegate { };

    public override void Initialize(AppManager appManager)
    {
        if (appManager == null) return;

        Instance = this;

        base.Initialize(appManager);

        SpawnCharacter(appManager);
    }

    private void SpawnCharacter(AppManager appManager) 
    {
        if (appManager == null) return;

        if (appManager.characterPrefab == null) return;

        GameObject character = GameObject.Instantiate(appManager.characterPrefab, new Vector3(0, 2f, 0), Quaternion.Euler(Vector3.zero));

        if (character == null) return;

        characterPlayer = character.GetComponent<CharacterPlayer>();

        GameObject.DontDestroyOnLoad(character);
    }

    public void SetCameraAsFPS() 
    {
        if (AppManager.Instance ==  null) return;
        if (AppManager.Instance.cameraController == null) return;
        if (AppManager.Instance.characterManager == null) return;
        if (AppManager.Instance.characterManager.characterPlayer == null) return;

        AppManager.Instance.cameraController.transform.SetParent(AppManager.Instance.characterManager.characterPlayer.transform);

        AppManager.Instance.cameraController.transform.localPosition = new Vector3(0, 0.5f, 0);
    }
}
