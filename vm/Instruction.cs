
using System;

namespace ex.vm
{
    public interface SInstruction
    {
    }
    // TODO Load Effetive Address
    // TODO consider the memory regions and refactor the instructions

    public class AddRegisters : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class AddValueToRegister : SInstruction
    {
        public Register Source;
        public UInt64 Value;
        public Register Destination;
    }

    public class SubtractRegister : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class SubtractValueFromRegister : SInstruction
    {
        public Register Source;
        public UInt64 Value;
        public Register Destination;
    }

    public class MultiplyRegisters : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class MultiplyValueWithRegister : SInstruction
    {
        public Register Source;
        public UInt64 Value;
        public Register Destination;
    }

    public class DivideRegisters : SInstruction
    {
        public Register Dividend;
        public Register Divisor;
        public Register Destination;
    }

    public class RightShiftRegisterByRegister : SInstruction
    {
        public Register Target;
        public Register Amount;
        public Register Destination;
    }

    public class RightShiftRegisterByValue : SInstruction
    {
        public Register Target;
        public byte Amount;
        public Register Destination;
    }

    public class LeftShiftRegisterByRegister : SInstruction
    {
        public Register Target;
        public Register Amount;
        public Register Destination;
    }

    public class LeftShiftRegisterByValue : SInstruction
    {
        public Register Target;
        public byte Amount;
        public Register Destination;
    }

    public class BinaryAndRegisters : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class BinaryAndRegisterWithValue : SInstruction
    {
        public Register Source1;
        public UInt64 Value;
        public Register Destination;
    }

    public class BinaryOrRegisters : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class BinaryOrRegisterWithValue : SInstruction
    {
        public Register Source1;
        public UInt64 Value;
        public Register Destination;
    }

    public class BinaryXorRegisters : SInstruction
    {
        public Register Source1;
        public Register Source2;
        public Register Destination;
    }

    public class BinaryXorRegisterWithValue : SInstruction
    {
        public Register Source1;
        public UInt64 Value;
        public Register Destination;
    }

    public class NegateRegister : SInstruction
    {
        public Register Source;
        public Register Destination;
    }

    public class ComplementRegister : SInstruction
    {
        public Register Source;
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

    public class JumpOnGreaterThan : SInstruction
    {
        public UInt64 Label;
        public Register Source;
    }

    public class MoveRegisterToRegister : SInstruction
    {
        public Register Source;
        public Register Destination;
        public UInt64 Mask;
    }

    public class MoveValueToRegister : SInstruction
    {
        public UInt64 Value;
        public Register Destination;
        public UInt64 Mask;
    }

    public class MoveRegisterToAddress : SInstruction
    {
        public Address Address;
        public Register Source;
        public UInt64 Mask;
    }

    public class MoveAddressToRegister : SInstruction
    {
        public Address Address;
        public Register Destination;
        public UInt64 Mask;
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
