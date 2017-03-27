using System.Runtime.InteropServices;

namespace PersistentWeaponFlashlight.Memory
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct CObject
	{
		[FieldOffset(832)]
		public unsafe CWeapon* Weapon;
	}
}
