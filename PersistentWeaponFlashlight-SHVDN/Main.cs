namespace PersistentWeaponFlashlight
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using GTA;

    using PersistentWeaponFlashlight.Memory;

    public unsafe class PersistantWeaponFlashlight : Script
    {
        const string UpdateWeaponComponentFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC 40 48 8B FA 48 8B D9 48 85 D2 0F 84 ?? ?? ?? ?? 80 7A 28 04 0F 85 ?? ?? ?? ??";
        const string ToggleWeaponFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC 20 8A 41 49 48 8B FA 48 8B 51 10 44 8A C0 24 FE 4C 8B F9 41 F6 D0 41 80 E0 01 44 0A C0 41 80 C8 02 44 88 41 49 48 8D 0D ?? ?? ?? ??";

        delegate void ToggleWeaponFlashlightDelegate(long compFlashlight, IntPtr ped, long unk_0);

        /// <summary>
        /// Nops the instruction that turns off the flashlight when not aiming.
        /// </summary>
        Nopper nopper1 = new Nopper(FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x218, 4);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when the WeaponSpecialTwo control is pressed.
        /// </summary>
        Nopper nopper2 = new Nopper(FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x186, 5);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when it isn't on and the ped is aiming for components with ToggleWhenAiming set to true in their info, such as COMPONENT_FLASHLIGHT_LIGHT.
        /// </summary>
        Nopper nopper3 = new Nopper(FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x143, 5);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when it isn't off and the ped isn't aiming for components with ToggleWhenAiming set to true in their info, such as COMPONENT_FLASHLIGHT_LIGHT.
        /// </summary>
        Nopper nopper4 = new Nopper(FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x1FE, 5);

        ToggleWeaponFlashlightDelegate toggleWeaponFlashlight = Marshal.GetDelegateForFunctionPointer<ToggleWeaponFlashlightDelegate>(FindPattern(ToggleWeaponFlashlightFunctionPattern));

        public PersistantWeaponFlashlight()
        {
            Tick += PersistantWeaponFlashlight_Tick;

            nopper1.Nop();
            nopper2.Nop();
            nopper3.Nop();
            nopper4.Nop();
        }
        
        bool lastOn = false;
        private void PersistantWeaponFlashlight_Tick(object sender, System.EventArgs e)
        {
            Ped playerPed = Game.Player.Character;

            if (!Entity.Exists(playerPed))
                return;

            IntPtr playerPedPtr = (IntPtr)playerPed.MemoryAddress;

            long weaponManager = MemoryFunctions.GetPedWeaponManager(playerPedPtr);

            if (weaponManager == 0)
                return;

            long currentWeaponObject = MemoryFunctions.GetWeaponManagerCurrentWeaponObject(weaponManager);

            if (currentWeaponObject == 0)
                return;

            long weapon = MemoryFunctions.GetObjectWeapon(currentWeaponObject);

            if (weapon == 0)
                return;

            long weaponComponentFlashLight = MemoryFunctions.GetWeaponComponentFlashlight(weapon);

            if (weaponComponentFlashLight == 0)
                return;

            if (Game.IsControlJustPressed(0, Control.WeaponSpecial2) || lastOn != MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashLight))
            {
                toggleWeaponFlashlight(weaponComponentFlashLight, playerPedPtr, 0);
                lastOn = MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashLight);
            }
        }

        protected override void Dispose(bool A_0)
        {
            if (nopper1.IsNopped)
                nopper1.Restore();
            if (nopper2.IsNopped)
                nopper2.Restore();
            if (nopper3.IsNopped)
                nopper3.Restore();
            if (nopper4.IsNopped)
                nopper4.Restore();
        }


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
                bytesArray[i / 2] = Byte.Parse(pattern.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
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
    }
}
