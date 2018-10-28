
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

    public class Address
    {
        public Int64 Offset;
        public Register Source;
        public MemoryRegion Region;
    }
}
