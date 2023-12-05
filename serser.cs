// Эти строки импортируют необходимые пространства имен для работы с сетью и текстом
using System;
using System.Net.Sockets;
using System.Text;

//Здесь определен класс FileSender, который будет отправлять файлы по сети. ipAddress и port хранят адрес и порт сервера
public class FileSender
{
    private const string ipAddress = "192.168.220.139";
    private const int port = 1777;

//В данном методе Main создается экземпляр TcpClient и устанавливается соединение с сервером по заданному адресу и порту    
    public static void Main()
    {
        using (var client = new TcpClient())
        {
            client.Connect(ipAddress, port);

//Здесь определена переменная filePath, которая содержит путь к файлу для отправки. 
//Затем вызывается метод ReadFileBytes, который считывает содержимое файла и возвращает его в виде массива байтов.
            string filePath = "Фотка";
            byte[] fileBytes = ReadFileBytes(filePath);

//Создается NetworkStream для отправки данных, а затем отправляется массив байтов fileBytes через этот поток
            NetworkStream stream = client.GetStream();
            stream.Write(fileBytes, 0, fileBytes.Length);

            Console.WriteLine("Готово");
        }
    }

//Это вспомогательный метод ReadFileBytes, который считывает содержимое файла по указанному пути и возвращает его в виде массива байтов.
//Если возникает ошибка при чтении файла, то выводится сообщение об ошибке и исключение пробрасывается выше
    private static byte[] ReadFileBytes(string filePath)
    {
        try
        {
            return System.IO.File.ReadAllBytes(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            throw;
        }
    }
}