using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;


namespace Socket_Server
{
    public partial class MainForm : Form
    {
        //TCP Server
        TcpListener tcpListener = null;
        //UI 출력용 List
        List<string> _Message = new List<string>();
        //포트
        int _port = 0;

        public MainForm()
        {
            InitializeComponent();

            this._Message.Clear();

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            //포트 파싱
            this._port = int.Parse(ConfigurationManager.ConnectionStrings["Port"].ConnectionString);

            //서버를 시작한다.
            Thread server = new Thread(new ThreadStart(ServerStart));
            server.Start();
        }

        public void ServerStart()
        {
            //포트 파싱이 안되면 서버실행 안함.
            if (this._port == 0)
            {
                viewMessage("Plz Check Port Server Close.");
                return;
            }

            //ALL IP Server OPEN
            tcpListener = new TcpListener(IPAddress.Any, this._port);
            tcpListener.Start();

            //Add Message
            viewMessage(string.Format("Server Start Open Port : {0}", this._port));

            try
            {
                while (true)
                {
                    //연결된 소켓 반환
                    Socket client = tcpListener.AcceptSocket();

                    viewMessage("Connet Client : " + client.RemoteEndPoint.ToString());

                    //전용 핸들러 제작 
                    ClientHandler handler = new ClientHandler();
                    handler.client = client;
                    //Log용 Delegate
                    handler.SendMsg += new ClientHandler.SendMessage(handler_SendMsg);

                    //메시지 추출은 Thread 에서 실행
                    Thread th = new Thread(() => handler.runClient());
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                viewMessage(ex.ToString());
                viewMessage("Socket Server Close.");
            }
            finally
            {
                tcpListener.Stop();
            }
        }

        /// <summary>
        /// Hander용 메시지 출력
        /// </summary>
        /// <param name="Msg"></param>
        void handler_SendMsg(string Msg)
        {
            viewMessage(Msg);
        }

        /// <summary>
        /// 로그 출력용
        /// </summary>
        /// <param name="message">로그 메시지</param>
        public void viewMessage(string message)
        {
            try
            {

                this.BeginInvoke(new Action(delegate()
                {
                    lb_log.BeginUpdate();

                    this._Message.Add(message);

                    while (this._Message.Count >= 1000)
                    {
                        this._Message.RemoveAt(0);
                    }

                    lb_log.DataSource = this._Message;
                    var cm = (CurrencyManager)this.BindingContext[this._Message];
                    cm.Refresh();

                    lb_log.SetSelected(lb_log.Items.Count - 1, true);


                    lb_log.EndUpdate();
                }));
            }
            catch
            {

            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpListener.Stop();
        }
    }
}
