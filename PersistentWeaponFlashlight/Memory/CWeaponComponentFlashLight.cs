namespace PersistentWeaponFlashlight.Memory
{
    // System
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CWeaponComponentFlashLight
    {
        [FieldOffset(0x0008)] public CWeaponComponentFlashLightInfo* WeaponComponentFlashLightInfo;
        [FieldOffset(0x0010)] public CWeapon* OwnerWeapon;
        [FieldOffset(0x0018)] public CObject* OwnerObject;

        [FieldOffset(0x0049)] public byte FlashLightState;

        public CWeaponComponentFlashLightState State
        {
            get { return (CWeaponComponentFlashLightState)FlashLightState; }
            set { FlashLightState = (byte)value; }
        }
    }

    internal enum CWeaponComponentFlashLightState : byte
    {
        None                    = 0b00000000,
        On                      = 0b00000001,
        Unknown2                = 0b00000010,
        Unknown4                = 0b00000100,
        Unknown8                = 0b00001000,
        Unknown16               = 0b00010000,
        Unknown32               = 0b00100000,
        Unknown64               = 0b01000000,
        Unknown128              = 0b10000000,
    }

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CWeaponComponentFlashLightInfo
    {
        [FieldOffset(0x010)] public uint Name;
        [FieldOffset(0x014)] public uint Model;
        [FieldOffset(0x018)] public uint LocalizedName;
        [FieldOffset(0x01C)] public uint LocalizedDescription;
        [FieldOffset(0x020)] public uint AttachBone;

        [FieldOffset(0x038)] public byte ShowOnWheel;
        [FieldOffset(0x039)] public byte CreateObject;

        [FieldOffset(0x040)] public float MainLightIntensity; 
        //[FieldOffset(0x044)] public NativeColorBGRA MainLightColor;
        [FieldOffset(0x048)] public float MainLightRange; 
        [FieldOffset(0x04C)] public float MainLightFalloffExponent;
        [FieldOffset(0x050)] public float MainLightInnerAngle; 
        [FieldOffset(0x054)] public float MainLightOuterAngle;
        [FieldOffset(0x058)] public float MainLightCoronaIntensity;
        [FieldOffset(0x05C)] public float MainLightCoronaSize;
        [FieldOffset(0x060)] public float MainLightVolumeIntensity;
        [FieldOffset(0x064)] public float MainLightVolumeSize;
        [FieldOffset(0x068)] public float MainLightVolumeExponent;
        //[FieldOffset(0x06C)] public NativeColorBGRA MainLightVolumeOuterColor;
        [FieldOffset(0x070)] public float MainLightShadowFadeDistance;
        [FieldOffset(0x074)] public float MainLightSpecularFadeDistance;
        [FieldOffset(0x078)] public float SecondaryLightIntensity;
        //[FieldOffset(0x07C)] public NativeColorBGRA SecondaryLightColor;
        [FieldOffset(0x080)] public float SecondaryLightRange;
        [FieldOffset(0x084)] public float SecondaryLightFalloffExponent;
        [FieldOffset(0x088)] public float SecondaryLightInnerAngle;
        [FieldOffset(0x08C)] public float SecondaryLightOuterAngle;
        [FieldOffset(0x090)] public float SecondaryLightVolumeIntensity; 
        [FieldOffset(0x094)] public float SecondaryLightVolumeSize; 
        [FieldOffset(0x098)] public float SecondaryLightVolumeExponent; 
        //[FieldOffset(0x09C)] public NativeColorBGRA SecondaryLightVolumeOuterColor;
        [FieldOffset(0x0A0)] public float SecondaryLightFadeDistance; 
        [FieldOffset(0x0A4)] public float TargetDistalongAimCamera;
        [FieldOffset(0x0A8)] public ushort FlashLightBone; //0x00A8 
        [FieldOffset(0x0AA)] public ushort FlashLightBoneBulbOn; //0x00AA 
        [FieldOffset(0x0AC)] public ushort FlashLightBoneBulbOff; //0x00AC 
        [FieldOffset(0x0AE)] public byte ToggleWhenAiming; //0x00AE
    }
}
