using System;
using System.Net.Sockets;
using System.Threading;

namespace ChatApp2
{
    public class ChatApp
    {
        private Connection _connection = new Connection();

        public void StartApp()
        {
            _connection.GetIP();
            _connection.GetPort();
            Socket chatSocket = _connection.Connect();
            //_connection.StartListening(chatSocket);


            StartChat();

        }
        public void StartChat()
        {
            Thread reciever = new Thread(RecieveMessages);
            reciever.Start();
            StartSendingMessages();
        }

        private void StartSendingMessages()
        {
            string msgToSend;
            while (true)
            {
                Console.Write("> ");
                msgToSend = Console.ReadLine();
                _connection.ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(msgToSend), 0, msgToSend.Length, SocketFlags.None);

            }
        }

        private void RecieveMessages()
        {
            Message recievedMsg;

            while (true)
            {
                byte[] msg = new byte[1024];
                int size = _connection.ClientSocket.Receive(msg);

                recievedMsg = new Message(System.Text.Encoding.ASCII.GetString(msg, 0, size));
                recievedMsg.DisplayRecievedMsg();
            }

        }

    }


}
