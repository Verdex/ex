
using System;

namespace ex.vm
{
    public interface SInstruction
    {
    }

    public class AddRegisters : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class CompareRegisters : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class CompareValueWithRegister : SInstruction
    {
        public Register Source;
        public UInt64 Value;
        public Register Destination;
    }

    public class Jump : SInstruction
    {
        public UInt64 Label;
    }

    public class JumpOnEqual : SInstruction
    {
        public UInt64 Label;
        public Register Source;
    }

    public class JumpOnNotEqual : SInstruction
    {
        public UInt64 Label;
        public Register Source;
    }

    public class JumpOnLessThan : SInstruction
    {
        public UInt64 Label;
        public Register Source;
    }

    public class JumpOnGreaterTan : SInstruction
    {
        public UInt64 Label;
        public Register Source;
    }

    public class MoveRegisterToRegister : SInstruction
    {
        public Register Source;
        public Register Destination;
    }

    public class MoveValueToRegister : SInstruction
    {
        public UInt64 Value;
        public Register Destination;
    }

    public class MoveRegisterToAddress : SInstruction
    {
        public Address Address;
        public Register Source;
    }

    public class MoveAddressToRegister : SInstruction
    {
        public Address Address;
        public Register Destination;
    }

    public class Push : SInstruction
    {
        public Register Source;
    }

    public class Pop : SInstruction
    {
        public Register Destination;
    }

    public class SystemCall : SInstruction
    {
    }
}
