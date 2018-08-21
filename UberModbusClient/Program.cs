using System;
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
            Console.WriteLine("Please Enter The Meter IP Address:");
            modbusClient = new ModbusClient(Console.ReadLine(), 502);
            program = new Program();
            modbusClient.Connect();
            if (modbusClient.Connected)
            {
                Console.WriteLine("Connected...");
                Console.WriteLine("Enter 1 to Read the ReadHoldingRegisters");
                Console.WriteLine("Enter 2 to Read the ReadInputRegisters");
                string function = Console.ReadLine();
                int a = Convert.ToInt32(function);
                switch (a)
                {
                    case 1:                       
                        program.ReadHoldingRedister();
                        break;
                    case 2:                      
                        program.ReadInputRegister();
                        break;

                    default:
                        Console.WriteLine("Default case");
                        break;
                }  
            }
            else
                Console.WriteLine("Not Connected");
        }

        public void ReadHoldingRedister()
        {
            Console.WriteLine("Please Enter The Starting Address:");
            string holdingregister = Console.ReadLine();
            int register = Convert.ToInt32(holdingregister);

            //Console.WriteLine("Please Enter The Quantity Length:");
            //string holdingregisterlength = Console.ReadLine();
            //int registerlength = Convert.ToInt32(holdingregisterlength);

            int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(register, 50);//40017               
            //int[] readInputRegisters = modbusClient.ReadInputRegisters(13, 10);
            //bool[] readDiscreteInputs  = modbusClient.ReadDiscreteInputs(13,10);    

            for (int i = 0; i < readHoldingRegisters.Length; i++)
            {
                Decimal _toDecimal = Convert.ToDecimal(readHoldingRegisters[i]);                
                Console.WriteLine((register) + ":" + "<" + _toDecimal + ">");//Console.WriteLine("Value Of HoldingRegister" + (register) + ":" + "<" + _toDecimal + ">");
                register++;
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

        public void ReadInputRegister()
        {

        }

    }

    

    
}
