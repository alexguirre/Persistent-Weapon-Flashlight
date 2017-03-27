using System.Runtime.InteropServices;

namespace PersistentWeaponFlashlight.Memory
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct CWeapon
	{
		[FieldOffset(320)]
		public unsafe CWeaponComponentFlashLight* WeaponComponentFlashLight;
	}
}
