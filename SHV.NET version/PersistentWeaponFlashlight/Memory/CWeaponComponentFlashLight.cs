using System.Runtime.InteropServices;

namespace PersistentWeaponFlashlight.Memory
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct CWeaponComponentFlashLight
	{
		public CWeaponComponentFlashLightState State
		{
			get
			{
				return (CWeaponComponentFlashLightState)this.FlashLightState;
			}
			set
			{
				this.FlashLightState = (byte)value;
			}
		}

		[FieldOffset(8)]
		public unsafe CWeaponComponentFlashLightInfo* WeaponComponentFlashLightInfo;

		[FieldOffset(16)]
		public unsafe CWeapon* OwnerWeapon;

		[FieldOffset(24)]
		public unsafe CObject* OwnerObject;

		[FieldOffset(73)]
		public byte FlashLightState;
	}
}
