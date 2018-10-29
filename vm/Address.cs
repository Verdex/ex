
using System;

namespace ex.vm
{
    public enum MemoryRegion
    {
        Stack,
        Heap,
        Global,
        Instruction,
    }

    public struct Address
    {
        public MemoryRegion Region; 
        public int Index;
        public PType PrimitiveType;
        public int Offset;
    }
}
