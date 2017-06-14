namespace PersistentWeaponFlashlight.Memory
{
    using System;

    using GTA;

    internal static class MemoryFunctions
    {
        static readonly int CPed_WeaponManagerOffset,
                            CWeaponManager_CurrentWeaponObjectOffset,
                            CObject_WeaponOffset,
                            CWeapon_WeaponComponentFlashlightOffset,
                            CWeaponComponentFlashLight_StateOffset;

        static MemoryFunctions()
        {
            switch (Game.Version)
            {
                default:
                case (GameVersion)35: // VER_1_0_1103_1_STEAM
                case (GameVersion)36: // VER_1_0_1103_1_NOSTEAM
                    CPed_WeaponManagerOffset = 0x10C8;
                    CWeaponManager_CurrentWeaponObjectOffset = 0x0078;
                    CObject_WeaponOffset = 0x0340;
                    CWeapon_WeaponComponentFlashlightOffset = 0x0148;
                    CWeaponComponentFlashLight_StateOffset = 0x0049;
                    break;
                case GameVersion.VER_1_0_1032_1_STEAM:
                case GameVersion.VER_1_0_1032_1_NOSTEAM:
                case GameVersion.VER_1_0_1011_1_STEAM:
                case GameVersion.VER_1_0_1011_1_NOSTEAM:
                    CPed_WeaponManagerOffset = 0x10C8;
                    CWeaponManager_CurrentWeaponObjectOffset = 0x0078;
                    CObject_WeaponOffset = 0x0340;
                    CWeapon_WeaponComponentFlashlightOffset = 0x0140;
                    CWeaponComponentFlashLight_StateOffset = 0x0049;
                    break;
            }
        }

        public static unsafe long GetPedWeaponManager(IntPtr pedPtr) => *(long*)(pedPtr + CPed_WeaponManagerOffset);
        public static unsafe long GetWeaponManagerCurrentWeaponObject(long weaponMgr) => *(long*)(weaponMgr + CWeaponManager_CurrentWeaponObjectOffset);
        public static unsafe long GetObjectWeapon(long obj) => *(long*)(obj + CObject_WeaponOffset);
        public static unsafe long GetWeaponComponentFlashlight(long weapon) => *(long*)(weapon + CWeapon_WeaponComponentFlashlightOffset);
        public static unsafe bool GetComponentFlashlightIsOn(long component) => (*(byte*)(component + CWeaponComponentFlashLight_StateOffset) & 1) != 0;
    }
}
