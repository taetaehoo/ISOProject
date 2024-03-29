﻿using System;
using System.IO.Ports;

class Program
{
    private static string bufferedString = "";

    static void Main()
    {
        string portName = "COM4";
        int baudRate = 9600;

        SerialPort serialPort = new SerialPort(portName, baudRate);

        try
        {
            serialPort.Open();
            serialPort.DataReceived += SerialPort_DataReceived;

            Console.WriteLine("Bluetooth 시리얼 통신이 연결되었습니다. 데이터를 수신합니다.");

            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오류: {ex.Message}");
        }
        finally
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }
    }

    private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort serialPort = (SerialPort)sender;
        string receivedData = serialPort.ReadExisting();

        bufferedString += receivedData;
        
        if (getLastChar(receivedData) == ']')
        {
            printReceivedData();
            bufferedString = "";
        }

    }

    private static char getLastChar(string data)
    {
        string trimedData = data.Trim();
        int trimedDataLength = trimedData.Length;
        return trimedDataLength > 0 ? trimedData[trimedData.Length - 1] : ' ';
    }

    private static void printReceivedData()
    {
        Console.WriteLine($"수신된 데이터 : {bufferedString.Trim()}");
    }
}