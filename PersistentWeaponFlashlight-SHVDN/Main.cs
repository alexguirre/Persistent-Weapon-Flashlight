﻿namespace PersistentWeaponFlashlight
{
    using System.Runtime.InteropServices;

    using GTA;

    using PersistentWeaponFlashlight.Memory;

    public unsafe class PersistantWeaponFlashlight : Script
    {
        const string UpdateWeaponComponentFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC 40 48 8B FA 48 8B D9 48 85 D2 0F 84 ?? ?? ?? ?? 80 7A 28 04 0F 85 ?? ?? ?? ??";
        const string ToggleWeaponFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC 20 8A 41 49 48 8B FA 48 8B 51 10 44 8A C0 24 FE 4C 8B F9 41 F6 D0 41 80 E0 01 44 0A C0 41 80 C8 02 44 88 41 49 48 8D 0D ?? ?? ?? ??";

        delegate void ToggleWeaponFlashlightDelegate(CWeaponComponentFlashLight* compFlashlight, CPed* ped, long unk_0);

        // TODO: implement FindPattern

        /// <summary>
        /// Nops the instruction that turns off the flashlight when not aiming.
        /// </summary>
        Nopper nopper1 = new Nopper(Game.FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x218, 4);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when the WeaponSpecialTwo control is pressed.
        /// </summary>
        Nopper nopper2 = new Nopper(Game.FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x186, 5);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when it isn't on and the ped is aiming for components with ToggleWhenAiming set to true in their info, such as COMPONENT_FLASHLIGHT_LIGHT.
        /// </summary>
        Nopper nopper3 = new Nopper(Game.FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x143, 5);
        /// <summary>
        /// Nops the instruction that calls the ToggleWeaponFlashlight function when it isn't off and the ped isn't aiming for components with ToggleWhenAiming set to true in their info, such as COMPONENT_FLASHLIGHT_LIGHT.
        /// </summary>
        Nopper nopper4 = new Nopper(Game.FindPattern(UpdateWeaponComponentFlashlightFunctionPattern) + 0x1FE, 5);

        ToggleWeaponFlashlightDelegate toggleWeaponFlashlight = Marshal.GetDelegateForFunctionPointer<ToggleWeaponFlashlightDelegate>(Game.FindPattern(ToggleWeaponFlashlightFunctionPattern));

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

            CPed* playerPedPtr = (CPed*)playerPed.MemoryAddress;


            CPedWeaponManager* weaponManager = playerPedPtr->WeaponManager;

            if (weaponManager == null)
                return;

            CObject* currentWeaponObject = weaponManager->CurrentWeaponObject;

            if (currentWeaponObject == null)
                return;

            CWeapon* weapon = currentWeaponObject->Weapon;

            if (weapon == null)
                return;

            CWeaponComponentFlashLight* weaponComponentFlashLight = weapon->WeaponComponentFlashLight;

            if (weaponComponentFlashLight == null)
                return;

            if (Game.IsControlJustPressed(0, Control.WeaponSpecial2) || lastOn != weaponComponentFlashLight->IsOn)
            {
                toggleWeaponFlashlight(weaponComponentFlashLight, playerPedPtr, 0);
                lastOn = weaponComponentFlashLight->IsOn;
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
    }
}