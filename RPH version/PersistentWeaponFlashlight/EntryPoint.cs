namespace PersistentWeaponFlashlight
{
    using System;

    using Rage;

    using PersistentWeaponFlashlight.Memory;


    internal static unsafe class EntryPoint
    {
        private static bool turnFlashlightOn;
        
        private const uint COMPONENT_FLASHLIGHT_LIGHT = 0xDDB7390F;
        private static CWeaponComponentFlashLightInfo* componentFlashLightLightInfo;

        private static void Main()
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

                if (Game.IsControlJustPressed(0, GameControl.WeaponSpecialTwo))
                {
                    turnFlashlightOn = !turnFlashlightOn;
                }

                if (turnFlashlightOn)
                {
                    weaponComponentFlashLight->State |= CWeaponComponentFlashLightState.On;

                    if (weaponComponentFlashLight->WeaponComponentFlashLightInfo->Name == COMPONENT_FLASHLIGHT_LIGHT)
                    {
                        // set ToggleWhenAiming of the COMPONENT_FLASHLIGHT_LIGHT to false, otherwise plays the toggle sound in a loop when not aiming
                        if (weaponComponentFlashLight->WeaponComponentFlashLightInfo->ToggleWhenAiming == 1)
                        {
                            weaponComponentFlashLight->WeaponComponentFlashLightInfo->ToggleWhenAiming = 0;

                            componentFlashLightLightInfo = weaponComponentFlashLight->WeaponComponentFlashLightInfo;
                        }
                    }

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
            if(componentFlashLightLightInfo != null)
            {
                // reset ToggleWhenAiming of the COMPONENT_FLASHLIGHT_LIGHT
                componentFlashLightLightInfo->ToggleWhenAiming = 1;
            }
        }
    }
}
