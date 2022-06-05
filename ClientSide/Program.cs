using System;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace Client
{

    class Program
    {

        // Main Method
        static void Main(string[] args)
        {
            ExecuteClient();
        }

        // ExecuteClient() Method
        static void ExecuteClient()
        {

            try
            {

             
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            
                Socket sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    
                    sender.Connect(localEndPoint);

                  
                    Console.WriteLine("Socket connected to -> {0} ",
                                  sender.RemoteEndPoint.ToString());

                    Console.WriteLine("Enter the Message.");

                    string userMessage = Console.ReadLine();

                   

                    byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(userMessage);


                    SendMessage(sender, userMessage);

                    Console.WriteLine("Server Message is waiting...");

                    ReceiveMessage(sender);

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

               
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }

        public static void SendMessage(Socket clientSocket, string msg)
        {
            string tmpstring = msg + "<EOF>";
            byte[] message = Encoding.ASCII.GetBytes(tmpstring);

            
            clientSocket.Send(message);
        }

        public static void ReceiveMessage(Socket clientSocket)
        {
            byte[] messageReceived = new byte[1024];

            int byteRecv = clientSocket.Receive(messageReceived);
            Console.WriteLine("Message from Server -> {0}",
                  Encoding.ASCII.GetString(messageReceived,
                                             0, byteRecv));
        }
        public static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }



    }
}
