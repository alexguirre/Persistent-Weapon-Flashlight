namespace PersistentWeaponFlashlight
{
    using System;

    using Rage;

    using PersistentWeaponFlashlight.Memory;


    internal static class EntryPoint
    {
        private static bool TurnFlashlightOn;

        private const uint FlashlightWeaponNameHash = 2343591895;

        private static unsafe void Main()
        {
            while (Game.IsLoading)
                GameFiber.Sleep(1000);

            while (true)
            {
                GameFiber.Yield();

                Ped playerPed = Game.LocalPlayer.Character;

                if (!playerPed)
                    continue;

                CPed* playerPedPtr = (CPed*)playerPed.MemoryAddress;


                CPedWeaponManager* weaponManager = playerPedPtr->WeaponManager;

                if (weaponManager == null)
                    continue;

                CObject* currentWeaponObject = weaponManager->CurrentWeaponObject;

                if (currentWeaponObject == null)
                    continue;

                CWeapon* weapon = currentWeaponObject->Weapon;

                if (weapon == null)
                    continue;

                CWeaponComponentFlashLight* weaponComponentFlashLight = weapon->WeaponComponentFlashLight;

                if (weaponComponentFlashLight == null)
                    continue;

                if (weaponManager->CurrentWeaponNameHash == FlashlightWeaponNameHash) // disable WEAPON_FLASHLIGHT, it makes a weird sound when not aiming
                    continue;

                if (Game.IsControlJustPressed(0, GameControl.WeaponSpecialTwo))
                {
                    TurnFlashlightOn = !TurnFlashlightOn;
                }

                if (TurnFlashlightOn)
                {
                    weaponComponentFlashLight->State |= CWeaponComponentFlashLightState.On;

                    if (*(float*)(new IntPtr(weaponComponentFlashLight) + 0x40) == 0.0f)
                    {
                        // this offset needs to modified for the flashlight to turn on if the player has the weapon equipped but hasn't aimed yet
                        *(float*)(new IntPtr(weaponComponentFlashLight) + 0x40) = 0.1f;
                    }
                }
                else
                {
                    weaponComponentFlashLight->State &= ~CWeaponComponentFlashLightState.On;
                }
            }
        }

        private static void OnUnload(bool isTerminating)
        {

        }
    }
}
