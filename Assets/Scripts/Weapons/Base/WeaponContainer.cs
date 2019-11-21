using Enums;

namespace Weapons
{
    [System.Serializable]
    public class WeaponContainer
    {
        public WeaponLauncherBase LauncherPrefab;
        public WeaponType WeaponType;
        public WeaponID WeaponID;
    }
}