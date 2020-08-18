namespace PersistentWeaponFlashlight
{
    using System;
    using System.Runtime.InteropServices;

    using Rage;

    using PersistentWeaponFlashlight.Memory;

    internal static unsafe class EntryPoint
    {
        delegate void ToggleWeaponFlashlightDelegate(long compFlashlight, IntPtr ped, long unk_0);

        /// <summary>
        /// Nops the instruction that turns off the flashlight when not aiming.
        /// </summary>
        static readonly Nopper nopper1 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 0x218, 4);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when the WeaponSpecialTwo control is pressed.
        /// </summary>
        static readonly Nopper nopper2 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 0x186, 5);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when it isn't on and the ped is aiming for components with ToggleWhenAiming set to true in their info, such as COMPONENT_FLASHLIGHT_LIGHT.
        /// </summary>
        static readonly Nopper nopper3 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 0x143, 5);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when it isn't off and the ped isn't aiming for components with ToggleWhenAiming set to true in their info, such as COMPONENT_FLASHLIGHT_LIGHT.
        /// </summary>
        static readonly Nopper nopper4 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 0x1FE, 5);

        static readonly ToggleWeaponFlashlightDelegate toggleWeaponFlashlight = Marshal.GetDelegateForFunctionPointer<ToggleWeaponFlashlightDelegate>(MemoryFunctions.ToggleWeaponFlashlightFunctionAddr);

        private static void Main()
        {
            while (Game.IsLoading)
                GameFiber.Sleep(1000);

            nopper1.Nop();
            nopper2.Nop();
            nopper3.Nop();
            nopper4.Nop();

            bool lastOn = false;
            while (true)
            {
                GameFiber.Yield();

                Ped playerPed = Game.LocalPlayer.Character;

                if (!playerPed)
                    continue;

                IntPtr playerPedPtr = playerPed.MemoryAddress;

                long weaponManager = MemoryFunctions.GetPedWeaponManager(playerPedPtr);

                if (weaponManager == 0)
                    continue;

                long currentWeaponObject = MemoryFunctions.GetWeaponManagerCurrentWeaponObject(weaponManager);

                if (currentWeaponObject == 0)
                    continue;

                long weapon = MemoryFunctions.GetObjectWeapon(currentWeaponObject);

                if (weapon == 0)
                    continue;

                long weaponComponentFlashLight = MemoryFunctions.GetWeaponComponentFlashlight(weapon);

                if (weaponComponentFlashLight == 0)
                    continue;

                if (Game.IsControlJustPressed(0, GameControl.WeaponSpecialTwo) || lastOn != MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashLight))
                {
                    toggleWeaponFlashlight(weaponComponentFlashLight, playerPedPtr, 0);
                    lastOn = MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashLight);
                }
            }
        }

        private static void OnUnload(bool isTerminating)
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
    }
}
