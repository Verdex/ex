
using System;
using System.Collections.Generic;

namespace ex.vm
{
    public class VirtualMachine
    {
        private UInt64 Register1;
        private UInt64 Register2;
        private UInt64 Register3;
        private UInt64 Register4;
        private UInt64 Register5;
        private UInt64 Register6;
        private UInt64 Register7;
        private int StackPointer;
        private int BasePointer;
        private int InstructionPointer;

        public void Run( List<SInstruction> instructions )
        {
            InstructionPointer = 0;
            while( InstructionPointer < instructions.Count )
            {
                switch(instructions[InstructionPointer])
                {
                    case MoveRegisterToRegister instr when instr.Source == Register.BasePointer:
                        SetRegister( (UInt64)BasePointer, instr.Destination );
                        break;
                    case MoveRegisterToRegister instr when instr.Source == Register.StackPointer:
                        SetRegister( (UInt64)StackPointer, instr.Destination );
                        break;
                    case MoveRegisterToRegister instr when instr.Source == Register.InstructionPointer:
                        SetRegister( (UInt64)InstructionPointer, instr.Destination );
                        break;
                    case MoveRegisterToRegister instr:
                        var r = GetGeneralRegister( instr.Source ); 
                        SetRegister( r, instr.Destination );
                        break;
                    case MoveValueToRegister instr:
                        SetRegister( instr.Value, instr.Destination );
                        break;
                    default:
                        throw new Exception( $"Unknown Instruction encountered {instructions[InstructionPointer].GetType()}" );
                }
            }
        }

        private UInt64 GetGeneralRegister( Register register )
        {
            switch(register)
            {
                case Register.R1:
                    return Register1;
                case Register.R2:
                    return Register2;
                case Register.R3:
                    return Register3;
                case Register.R4:
                    return Register4;
                case Register.R5:
                    return Register5;
                case Register.R6:
                    return Register6;
                case Register.R7:
                    return Register7;
                default:
                    throw new Exception( "Attempting to get unknown Register Value : {register}");
            }
        }

        private void SetRegister( UInt64 value, Register register )
        {
            switch(register)
            {
                case Register.R1:
                    Register1 = value;
                    break;
                case Register.R2:
                    Register2 = value;
                    break;
                case Register.R3:
                    Register3 = value;
                    break;
                case Register.R4:
                    Register4 = value;
                    break;
                case Register.R5:
                    Register5 = value;
                    break;
                case Register.R6:
                    Register6 = value;
                    break;
                case Register.R7:
                    Register7 = value;
                    break;
                case Register.StackPointer:
                    if ( value <= int.MaxValue )
                    {
                        StackPointer = (int)value;
                    }
                    else
                    {
                        throw new Exception( $"Attempt to put too large value into Stack Pointer : {value}" );    
                    }
                    break;
                case Register.BasePointer:
                    if ( value <= int.MaxValue )
                    {
                        BasePointer = (int)value;
                    }
                    else
                    {
                        throw new Exception( $"Attempt to put too large value into Base Pointer : {value}" );    
                    }
                    break;
                case Register.InstructionPointer:
                    if ( value <= int.MaxValue )
                    {
                        InstructionPointer = (int)value;
                    }
                    else
                    {
                        throw new Exception( $"Attempt to put too large value into Instruction Pointer : {value}" );    
                    }
                    break;
                default:
                    throw new Exception( "Attempting to set unknown register : {register}" );
            }
        }
    }
}
