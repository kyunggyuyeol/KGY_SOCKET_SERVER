using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Configuration;

namespace Socket_Server
{
    public class ClientHandler
    {
        public Socket client;

        public delegate void SendMessage(string Msg);
        public event SendMessage SendMsg;

        string rootPath = string.Empty;

        public ClientHandler()
        {
            rootPath = ConfigurationManager.ConnectionStrings["FileRootPath"].ConnectionString;

            //없으면 디렉토리 생성
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
        }

        public void runClient()
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            //소켓의 스트림을 생성
            stream = new NetworkStream(client);

            //스트림 인코딩 설정
            Encoding encode = Encoding.GetEncoding("UTF-8");
            reader = new StreamReader(stream, encode);

            StringBuilder sb = new StringBuilder();

            try
            {
                while (true)
                {
                    string str = reader.ReadLine();
                    sb.AppendLine(str);

                    if (str.IndexOf("</ATTDATA>") > -1)
                    {
                        break;
                    }
                }

                //메시지 파싱
                string allMessage = sb.ToString();

                MessageParsing(allMessage);

                SendMsg.Invoke("[Parsing OK] Client : " + client.RemoteEndPoint.ToString());


            }
            catch (Exception ex)
            {
                SendMsg.Invoke(ex.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public void MessageParsing(string allMessage)
        {
            string eqp = Util.GetMiddleString(allMessage, "<EQP>", "</EQP>");
            string Body = Util.GetMiddleString(allMessage, "<BODY>", "</BODY>").Trim();

            string date = DateTime.Now.ToString("yyyyMMddHH");


            string eqpPath = Path.Combine(rootPath, eqp);

            if (!Directory.Exists(eqpPath))
                Directory.CreateDirectory(eqpPath);

            string filePath = Path.Combine(eqpPath, date + "_" + eqp + ".txt");

            writeLog(filePath, Body);

            ExcuteQuery(filePath);
        }

        private void ExcuteQuery(string filePath)
        {
            //파일이 존재 하면 DB 접속 
            if (File.Exists(filePath))
            {
                string query = string.Format(ConfigurationManager.ConnectionStrings["SqlQuery"].ConnectionString, filePath);
                SqlHandler sql = new SqlHandler();

                string ret = sql.executeNonQuery(query);

                if (ret == "OK")
                {
                    SendMsg.Invoke("[Insert OK] FileName : " + filePath);
                }
                else
                {
                    SendMsg.Invoke("[Insert Fail] FileName : " + filePath);
                }
            }
        }

        private void writeLog(string path, string writeString)
        {

            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter file = new StreamWriter(fs, Encoding.UTF8);
            file.WriteLine(writeString);
            file.Flush();
            file.Close();
            fs.Close();
        }
    }
}
