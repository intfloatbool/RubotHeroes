using System.Collections.Generic;

namespace Weapons
{
    using UnityEngine;
    
    public class WeaponsHolder : SingletonDoL<WeaponsHolder>
    {
        [SerializeField] private List<WeaponContainer> _weaponContainers;
        private Dictionary<WeaponID, WeaponContainer> _containersDict = new Dictionary<WeaponID, WeaponContainer>();

        protected override void Awake()
        {
            base.Awake();
            InitializeDict();
        }

        private void InitializeDict()
        {
            foreach (WeaponContainer weapContainer in _weaponContainers)
            {
                if (!_containersDict.ContainsKey(weapContainer.WeaponID))
                {
                    _containersDict.Add(weapContainer.WeaponID, weapContainer);
                }
                else
                {
                    Debug.LogError($"${gameObject.name} cannot initialize weaponContainer with ID {weapContainer.WeaponID}, already exists!!!");
                }
            }
        }

        public WeaponContainer GetWeaponContainerByID(WeaponID _weaponId)
        {
            return _containersDict.ContainsKey(_weaponId) ? 
                _containersDict[_weaponId] : null;
        }

        protected override WeaponsHolder GetLink()
        {
            return this;
        }
    }

}