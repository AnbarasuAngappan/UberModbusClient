﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberModBus;

namespace UberModbusClient
{
    class Program
    {
        public static ModbusClient modbusClient;
        public static Program program;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please Enter The Meter IP Address:");
                modbusClient = new ModbusClient(Console.ReadLine(), 502);
                program = new Program();
                modbusClient.Connect();
                if (modbusClient.Connected)
                {
                    Console.WriteLine("Connected...");
                    Console.WriteLine("Enter 1: To Read the ReadHoldingRegisters");
                    Console.WriteLine("Enter 2: To Read the ReadInputRegisters");
                    Console.WriteLine("Enter 3: To Read the ReadDiscreteInput");
                    Console.WriteLine("Enter 4: To Read the ReadCoil");
                    string function = Console.ReadLine();
                    int inputKeyFunction = Convert.ToInt32(function);
                    switch (inputKeyFunction)
                    {
                        case 1:
                            program.ReadHoldingRegister();
                            break;
                        case 2:
                            program.ReadInputRegister();
                            break;
                        case 3:
                            program.ReadDiscreteInput();
                            break;
                        case 4:
                            program.ReadCoil();
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                    Console.WriteLine("Not Connected");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ReadHoldingRegister()
        {
            try
            {
                Console.WriteLine("Please Enter The Starting Address of ReadHoldingRegister:");
                string holdingregister = Console.ReadLine();
                int register = Convert.ToInt32(holdingregister);
                //Console.WriteLine("Please Enter The Quantity Length:");
                //string holdingregisterlength = Console.ReadLine();
                //int registerlength = Convert.ToInt32(holdingregisterlength);
                int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(register, 50);//40017               
                //int[] readInputRegisters = modbusClient.ReadInputRegisters(13, 10);
                //bool[] readDiscreteInputs  = modbusClient.ReadDiscreteInputs(13,10);    
                if (readHoldingRegisters != null)
                {
                    for (int i = 0; i < readHoldingRegisters.Length; i++)
                    {
                        Decimal _toDecimal = Convert.ToDecimal(readHoldingRegisters[i]);
                        Console.WriteLine((register) + ":" + "<" + _toDecimal + ">");//Console.WriteLine("Value Of HoldingRegister" + (register) + ":" + "<" + _toDecimal + ">");
                        register++;
                    }
                }
                //for (int i = 0; i < readInputRegisters.Length; i++)
                //{
                //    Console.WriteLine("Value of the InputRegisters" + (i + 1) + ":" + readInputRegisters[i]);
                //}

                //for (int i = 0; i < readDiscreteInputs.Length; i++)
                //{
                //    Console.WriteLine("Value of the ReadDiscreteInputs" + (i + 1) + ":" + readDiscreteInputs[i]);
                //}
                modbusClient.Disconnect();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ReadInputRegister()
        {
            try
            {
                Console.WriteLine("Please Enter The Starting Address of Input Register:");
                string holdingregister = Console.ReadLine();
                int register = Convert.ToInt32(holdingregister);
                int[] readInputRegisters = modbusClient.ReadInputRegisters(register, 10);
                if (readInputRegisters != null)
                {
                    for (int i = 0; i < readInputRegisters.Length; i++)
                    {
                        Decimal _toDecimal = Convert.ToDecimal(readInputRegisters[i]);
                        Console.WriteLine((register) + ":" + "<" + _toDecimal + ">");
                        register++;
                    }
                }
                modbusClient.Disconnect();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ReadDiscreteInput()
        {
            try
            {
                Console.WriteLine("Please Enter The Starting Address of DiscreteInput:");
                string discreteInput = Console.ReadLine();
                int input = Convert.ToInt32(discreteInput);

                bool[] readDiscreteInputs = modbusClient.ReadDiscreteInputs(input, 10);
                if (readDiscreteInputs != null)
                {
                    for (int i = 0; i < readDiscreteInputs.Length; i++)
                    {
                        Console.WriteLine((input) + ":" + "<" + readDiscreteInputs[i] + ">");
                        input++;
                    }
                }
                modbusClient.Disconnect();
                Console.ReadLine();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ReadCoil()
        {
            try
            {
                Console.WriteLine("Please Enter the Starting Adderess of Read Coils");
                string readCoil = Console.ReadLine();
                int coil = Convert.ToInt32(readCoil);

                bool[] readCoils = modbusClient.ReadCoils(coil, 10);
                if(readCoils != null)
                {
                    for (int i = 0; i < readCoils.Length; i++)
                    {
                        Console.WriteLine((coil) + ":" + "<" + readCoils[i] + ">");
                        coil++;
                    }
                }
                modbusClient.Disconnect();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
