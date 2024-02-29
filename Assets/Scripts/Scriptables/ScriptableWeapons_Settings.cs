using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObjects/WeaponsSettings", order = 1)]
public class ScriptableWeapons_Settings : ScriptableObject
{
    [Serializable]
    public class WeaponsSetting
    {
        public GunController.TypeOfBullet bulletType;
        public Color gunColor;
        public Vector3 positionToSpaw;
    }

    public GameObject gunPrefab;

    [Space(10f)]
    public GameObject explosionVFx;

    [Space(10f)]
    public List<WeaponsSetting> weaponsSetting = new List<WeaponsSetting>();
}
