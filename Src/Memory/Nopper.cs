namespace PersistentWeaponFlashlight.Memory
{
    using System;

    internal unsafe class Nopper
    {
        public const int NopOpcode = 0x90;

        public IntPtr Address { get; }
        public int BytesCount { get; }

        public byte[] OriginalBytes { get; private set; }

        public bool IsNopped { get; private set; }

        public Nopper(IntPtr address, int bytesCount)
        {
            Address = address;
            BytesCount = bytesCount;
        }

        public void Nop()
        {
            if (IsNopped)
                throw new InvalidOperationException("Already nopped");

            OriginalBytes = new byte[BytesCount];

            for (int i = 0; i < BytesCount; i++)
            {
                OriginalBytes[i] = *(byte*)(Address + i);
                *(byte*)(Address + i) = NopOpcode;
            }
            
            IsNopped = true;
        }

        public void Restore()
        {
            if (!IsNopped)
                throw new InvalidOperationException("Not nopped");

            for (int i = 0; i < BytesCount; i++)
            {
                *(byte*)(Address + i) = OriginalBytes[i];
            }

            OriginalBytes = null;
            IsNopped = false;
        }
    }
}
