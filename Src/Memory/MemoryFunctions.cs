namespace PersistentWeaponFlashlight.Memory
{
    using System;
#if SHVDN
    using System.Diagnostics;
#endif

    internal static unsafe class MemoryFunctions
    {
        const string UpdateWeaponComponentFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC 40 48 8B FA 48 8B D9 48 85 D2 0F 84 ?? ?? ?? ?? 80 7A 28 04 0F 85",
                     ToggleWeaponFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC 20 8A 41 49 48 8B FA 48 8B 51 10 44 8A C0 24 FE 4C 8B F9 41 F6 D0 41 80 E0 01 44 0A C0 41 80 C8 02 44 88 41 49 48 8D 0D";

        public static readonly IntPtr UpdateWeaponComponentFlashlightFunctionAddr = FindPattern(UpdateWeaponComponentFlashlightFunctionPattern),
                                      ToggleWeaponFlashlightFunctionAddr = FindPattern(ToggleWeaponFlashlightFunctionPattern);

        static readonly int CPed_WeaponManagerOffset,
                            CWeaponManager_CurrentWeaponObjectOffset,
                            CObject_WeaponOffset,
                            CWeapon_WeaponComponentFlashlightOffset,
                            CWeaponComponentFlashLight_StateOffset;

        static MemoryFunctions()
        {
            IntPtr weaponComponentFlashlightOffsetAddr = FindPattern("48 89 BB ?? ?? ?? ?? EB 16 48 89 BB");

            bool error = false;
            if (UpdateWeaponComponentFlashlightFunctionAddr == IntPtr.Zero)
            {
                Util.Log($"ERROR: could not find {nameof(UpdateWeaponComponentFlashlightFunctionAddr)}");
                error = true;
            }

            if (ToggleWeaponFlashlightFunctionAddr == IntPtr.Zero)
            {
                Util.Log($"ERROR: could not find {nameof(ToggleWeaponFlashlightFunctionAddr)}");
                error = true;
            }

            if (weaponComponentFlashlightOffsetAddr == IntPtr.Zero)
            {
                Util.Log($"ERROR: could not find {nameof(weaponComponentFlashlightOffsetAddr)}");
                error = true;
            }

            if (error)
            {
                Util.Log("Unloading...");
                Util.UnloadActivePlugin();
                return;
            }

            CWeaponManager_CurrentWeaponObjectOffset = 0x0078;
            CObject_WeaponOffset = 0x0340;
            CWeaponComponentFlashLight_StateOffset = 0x0049;

            CPed_WeaponManagerOffset = *(int*)(ToggleWeaponFlashlightFunctionAddr + 0x68);

            CWeapon_WeaponComponentFlashlightOffset = *(int*)(weaponComponentFlashlightOffsetAddr + 3);
        }

        public static long GetPedWeaponManager(IntPtr pedPtr) => *(long*)(pedPtr + CPed_WeaponManagerOffset);
        public static long GetWeaponManagerCurrentWeaponObject(long weaponMgr) => *(long*)(weaponMgr + CWeaponManager_CurrentWeaponObjectOffset);
        public static long GetObjectWeapon(long obj) => *(long*)(obj + CObject_WeaponOffset);
        public static long GetWeaponComponentFlashlight(long weapon) => *(long*)(weapon + CWeapon_WeaponComponentFlashlightOffset);
        public static bool GetComponentFlashlightIsOn(long component) => (*(byte*)(component + CWeaponComponentFlashLight_StateOffset) & 1) != 0;

#if RPH
        private static IntPtr FindPattern(string pattern) => Rage.Game.FindPattern(pattern);
#elif SHVDN
        private static IntPtr FindPattern(string pattern)
        {
            bool Compare(byte* data, byte[] bytesPattern)
            {
                for (int i = 0; i < bytesPattern.Length; i++)
                {
                    if(bytesPattern[i] != 0x00 && data[i] != bytesPattern[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            ProcessModule module = Process.GetCurrentProcess().MainModule;

            long address = module.BaseAddress.ToInt64();
            long endAddress = address + module.ModuleMemorySize;

            pattern = pattern.Replace(" ", "").Replace("??", "00");
            byte[] bytesArray = new byte[pattern.Length / 2];
            for (int i = 0; i < pattern.Length; i += 2)
            {
                bytesArray[i / 2] = byte.Parse(pattern.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }

            for (; address < endAddress; address++)
            {
                if(Compare((byte*)address, bytesArray))
                {
                    return new IntPtr(address);
                }
            }
            
            return IntPtr.Zero;
        }
#endif
    }
}
