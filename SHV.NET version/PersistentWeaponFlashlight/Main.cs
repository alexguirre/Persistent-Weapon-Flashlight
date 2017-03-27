using GTA;
using PersistentWeaponFlashlight.Memory;
using System;

namespace PersistentWeaponFlashlight
{
    public class PersistantWeaponFlashlight : Script
    {
        private static bool turnFlashlightOn;

        private const uint COMPONENT_FLASHLIGHT_LIGHT = 3719772431u;

        private unsafe static CWeaponComponentFlashLightInfo* componentFlashLightLightInfo;

        public PersistantWeaponFlashlight()
        {
            Interval = 0;
            Tick += PersistantWeaponFlashlight_Tick;
        }

        private unsafe void PersistantWeaponFlashlight_Tick(object sender, EventArgs e)
        {
            if (Game.IsLoading) return;

            Ped plrPed = Game.Player.Character;

            if (plrPed == null || !plrPed.Exists() || !plrPed.IsAlive) return;

            CPed* ptr = (CPed*)((void*)plrPed.MemoryAddress);
            CPedWeaponManager* weaponManager = ptr->WeaponManager;

            if (weaponManager == null) return;

            CObject* currentWeaponObject = weaponManager->CurrentWeaponObject;

            if (currentWeaponObject == null) return;

            CWeapon* weapon = currentWeaponObject->Weapon;

            if (weapon == null) return;

            CWeaponComponentFlashLight* weaponComponentFlashLight = weapon->WeaponComponentFlashLight;

            if (weaponComponentFlashLight == null) return;

            if (Game.IsControlJustPressed(0, Control.WeaponSpecial2))
            {
                turnFlashlightOn = !turnFlashlightOn;
            }

            if (turnFlashlightOn)
            {
                weaponComponentFlashLight->State |= CWeaponComponentFlashLightState.On;

                if (weaponComponentFlashLight->WeaponComponentFlashLightInfo->Name == 3719772431u && weaponComponentFlashLight->WeaponComponentFlashLightInfo->ToggleWhenAiming == 1)
                {
                    weaponComponentFlashLight->WeaponComponentFlashLightInfo->ToggleWhenAiming = 0;
                    componentFlashLightLightInfo = weaponComponentFlashLight->WeaponComponentFlashLightInfo;
                }

                if (*(float*)((void*)(new IntPtr((void*)weaponComponentFlashLight) + 64)) == 0f)
                {
                    *(float*)((void*)(new IntPtr((void*)weaponComponentFlashLight) + 64)) = 0.1f;
                }
            }
            else
            {
                weaponComponentFlashLight->State &= (CWeaponComponentFlashLightState)254;
            }
        }
    }
}
