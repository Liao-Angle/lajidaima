using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.Net.Sockets;
using com.dsc.kernal.utility;
using System.Reflection;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;

namespace ECPJobQueue
{
    public partial class RunKernal : ServiceBase
    {
        private ECPJobQueue.Queue.RunningQueue runningQueue = null;
        private ECPJobQueue.Queue.WaitingQueue waitingQueue = null;
        private string ipBinding = "";
        private int PORT = 8488;
        private string queryipBinding = "";
        private int queryPORT = 8489;
        private int maxRunningJob = 10;
        private System.Threading.Thread listenThread = null;
        private System.Threading.Thread queryThread = null;
        private System.Threading.Thread queueThread = null;
        private ArrayList registerJobClass = new ArrayList();
        private string runningPath = "";
        private object m_mutex = new object();
        private TcpListener QueryServerListener = null;
        private TcpListener CreateJobListener = null;
        private int maxRunningTimes = 3;
        private string email = "";
        private string sendermail = "";

        public RunKernal()
        {
            //InitializeComponent();
            this.ServiceName = "ECP JobQueue Service";
        }

        protected override void OnStart(string[] args)
        {
            writeLog("Get Parameter");
            //取得基本參數
            runningPath = System.AppDomain.CurrentDomain.BaseDirectory;
            com.dsc.kernal.utility.GlobalProperty.Filename = runningPath + @"\setting.ini";
            ipBinding = GlobalProperty.getProperty("global", "ipBinding");
            PORT = int.Parse(GlobalProperty.getProperty("global", "port"));
            queryipBinding = GlobalProperty.getProperty("global", "queryipBinding");
            queryPORT = int.Parse(GlobalProperty.getProperty("global", "queryport"));
            maxRunningJob = int.Parse(GlobalProperty.getProperty("global", "maxRunningJob"));
            maxRunningTimes = int.Parse(GlobalProperty.getProperty("global", "maxRunningTimes"));
            email = GlobalProperty.getProperty("global", "email");
            sendermail = GlobalProperty.getProperty("global", "sendermail");

            writeLog("Get register Job class");
            //取得JobClass註冊清單
            registerJobClass = GlobalProperty.GetPropertyNames("register");

            writeLog("Establish queues");
            //建立Queue
            runningQueue = new ECPJobQueue.Queue.RunningQueue();
            waitingQueue = new ECPJobQueue.Queue.WaitingQueue();

            writeLog("Restore jobs");
            //還原JobStatus
            string[] files = System.IO.Directory.GetFiles(runningPath + @"\jobs\");
            for (int i = 0; i < files.Length; i++)
            {
                string fn = Utility.getFileName(files[i]);
                string[] fns = fn.Split(new char[] { '.' });
                //fns[0]: JobID
                System.IO.StreamReader sr = new System.IO.StreamReader(files[i]);
                string datas = sr.ReadToEnd();
                sr.Close();
                string[] data = datas.Split(new string[] { "||||" }, StringSplitOptions.None);

                string path = findRegisterClass(data[0]);

                if (path.Equals(""))
                {
                    //writeLog...................
                    continue;
                }
                else
                {
                    int p = path.LastIndexOf(@"\");
                    string folder = path.Substring(0, p + 1);
                    string[] asm = data[0].Split(new char[] { '.' });

                    System.Security.Policy.Evidence evi = new System.Security.Policy.Evidence();
                    evi.AddAssembly(asm[0]);
                    evi.AddHost(new Zone(SecurityZone.MyComputer));

                    System.AppDomain apd = System.AppDomain.CreateDomain(data[0], evi, folder, folder, true);

                    com.dsc.kernal.batch.JobClass job = (com.dsc.kernal.batch.JobClass)apd.CreateInstanceAndUnwrap(asm[0], data[0]);

                    job.setJobID(fns[0]);
                    job.setParameter(data[1]);
                    job.setDependencies(data[2]);
                    job.setServicePath(runningPath);
                    job.email = email;
                    job.sendermail = sendermail;

                    waitingQueue.push(job);
                }

            }

            writeLog("Start listening createJob");
            //啟動Job 新增Listener
            listenThread = new System.Threading.Thread(new System.Threading.ThreadStart(StartServer));
            listenThread.Start();

            writeLog("Start listenting query status");
            //啟動Job Status Service
            queryThread = new System.Threading.Thread(new System.Threading.ThreadStart(StartQueryServer));
            queryThread.Start();

            writeLog("Start queue job");
            //啟動Job Queue執行
            queueThread = new System.Threading.Thread(new System.Threading.ThreadStart(StartQueueProcess));
            queueThread.Start();

            writeLog("Service start success");
                        
        }

        protected override void OnStop()
        {
            writeLog("Stop job listener");
            //停止Job Listener
            listenThread.Abort();
            CreateJobListener.Stop();

            writeLog("Stop queue job");
            //停止Queue
            queueThread.Abort();
            QueryServerListener.Stop();

            writeLog("Wait for running queue and waiting queue stop");
            while (true)
            {
                //等待RunningQueue完成
                if (runningQueue.isQueueEmpty() && waitingQueue.isQueueEmpty())
                {
                    break;
                }
                else
                {
                    com.dsc.kernal.batch.JobClass job = runningQueue.pop();
                    if (job != null)
                    {
                        if (job.getJobStatus().Equals(com.dsc.kernal.batch.JobClass.Completed))
                        {
                            job.destroy();
                        }
                        else if (!job.getJobStatus().Equals(com.dsc.kernal.batch.JobClass.Error))
                        {
                            runningQueue.push(job);
                        }
                        if (job.getJobStatus().Equals(com.dsc.kernal.batch.JobClass.Error))
                        {
                            waitingQueue.push(job);
                        }
                    }

                    //確認所有Job紀錄Status
                    job = waitingQueue.pop();
                    if (job != null)
                    {
                        job.saveStatus();
                    }

                }
                System.Threading.Thread.Sleep(100);
            }

            writeLog("Stop query status");
            //Job Status service
            queryThread.Abort();

            writeLog("Service stop success");
        }
        public string findRegisterClass(string jobID)
        {
            string[] data = null;
            for (int i = 0; i < registerJobClass.Count; i++)
            {
                data = (string[])registerJobClass[i];
                if (data[0].Equals(jobID))
                {
                    return data[1];
                }
            }
            return "";
        }
        public void StartQueueProcess()
        {
            while (true)
            {
                try
                {
                    //處理RunningQueue
                    com.dsc.kernal.batch.JobClass job = runningQueue.pop();
                    if (job != null)
                    {
                        if (job.getJobStatus().Equals(com.dsc.kernal.batch.JobClass.Completed))
                        {
                            job.destroy();
                            job = null;
                        }
                        else if (!job.getJobStatus().Equals(com.dsc.kernal.batch.JobClass.Error))
                        {
                            runningQueue.push(job);
                        }
                        else if (job.getJobStatus().Equals(com.dsc.kernal.batch.JobClass.Error))
                        {
                            if (job.getRunTimes() > maxRunningTimes)
                            {
                                //超過錯誤最大執行次數, 要儲存狀態並將檔案搬移至error
                                if (job.getJobStatus() < 1)
                                {
                                    job.saveStatus();
                                }
                                string oriPath = job.getStatusPath();
                                string targetPath = runningPath + @"\error\" + job.getJobID() + ".job";
                                if (!System.IO.File.Exists(targetPath))
                                {
                                    System.IO.File.Move(oriPath, targetPath);
                                }
                            }
                            else
                            {
                                waitingQueue.push(job);
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(100);
                    //處理Waiting Queue
                    int x = 0;
                    if (waitingQueue.getQueueCount() > 0)
                    {
                        while (runningQueue.getQueueCount() < maxRunningJob)
                        {
                            x++;
                            if (x > maxRunningJob) break;
                            job = waitingQueue.pop();
                            if (job != null)
                            {
                                if (!runningQueue.isDepency(job))
                                {
                                    if (runningQueue.getQueueCount() < maxRunningJob)
                                    {
                                        runningQueue.push(job);
                                        job.start();
                                    }
                                    else
                                    {
                                        waitingQueue.push(job);
                                    }
                                }
                                else
                                {
                                    waitingQueue.push(job);
                                }
                            }
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    //處理ready folder
                    string[] files = System.IO.Directory.GetFiles(runningPath + @"\ready\");
                    for (int i = 0; i < files.Length; i++)
                    {
                        string fn = Utility.getFileName(files[i]);
                        string[] fns = fn.Split(new char[] { '.' });
                        //fns[0]: JobID
                        System.IO.StreamReader sr = new System.IO.StreamReader(files[i]);
                        string datas = sr.ReadToEnd();
                        sr.Close();
                        string[] data = datas.Split(new string[] { "||||" }, StringSplitOptions.None);

                        string path = findRegisterClass(data[0]);

                        if (path.Equals(""))
                        {
                            //writeLog...................
                            continue;
                        }
                        else
                        {
                            int p = path.LastIndexOf(@"\");
                            string folder = path.Substring(0, p + 1);
                            string[] asm = data[0].Split(new char[] { '.' });

                            System.Security.Policy.Evidence evi = new System.Security.Policy.Evidence();
                            evi.AddAssembly(asm[0]);
                            evi.AddHost(new Zone(SecurityZone.MyComputer));

                            System.AppDomain apd = System.AppDomain.CreateDomain(data[0], evi, folder, folder, true);

                            job = (com.dsc.kernal.batch.JobClass)apd.CreateInstanceAndUnwrap(asm[0], data[0]);

                            job.setJobID(fns[0]);
                            job.setParameter(data[1]);
                            job.setDependencies(data[2]);
                            job.setServicePath(runningPath);

                            waitingQueue.push(job);
                        }
                        System.IO.File.Delete(files[i]);
                    }
                }
                catch (Exception e)
                {
                    writeLog("StartQueueProcess Exception: " + e.Message + System.Environment.NewLine + e.StackTrace);
                    string cont = "Error Type: StartQueueProcess error" + System.Environment.NewLine;
                    cont += "Error Message: " + e.Message + System.Environment.NewLine;
                    cont += "Stack Trace: " + e.StackTrace;
                    mail(cont);
                }
                System.Threading.Thread.Sleep(100);

                //Console.WriteLine("process queue......");
            }
        }
        public void StartQueryServer()
        {
            while (true)
            {
                QueryServerListener = null;
                try
                {
                    // IP Address.
                    IPAddress localAddr = IPAddress.Parse(queryipBinding);

                    // Listen to port PORT.
                    QueryServerListener = new TcpListener(localAddr, queryPORT);

                    // Start listening.
                    QueryServerListener.Start();
                    writeLog("Listening QueryServer " + queryPORT.ToString() + " port……");

                    while (true)
                    {
                        // Process suspend, waiting for socket connection.
                        Socket mySocket = QueryServerListener.AcceptSocket();

                        try
                        {
                            // Socket received
                            //Console.WriteLine("Socket connected");

                            // Receive Buffer
                            Byte[] recvBytes = new Byte[256];

                            // Read to buffer
                            Int32 bytes = mySocket.Receive(recvBytes, recvBytes.Length, SocketFlags.None);

                            // Convert to string.
                            String str = System.Text.UTF8Encoding.UTF8.GetString(recvBytes, 0, bytes);

                            //Console.WriteLine("Data Received：{0}", str);
                            string path = "";

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("<queue>");
                            sb.AppendLine("<WaitingQueue>");
                            sb.AppendLine(waitingQueue.queryStatus());
                            sb.AppendLine("</WaitingQueue>");
                            sb.AppendLine("<RunningQueue>");
                            sb.AppendLine(runningQueue.queryStatus());
                            sb.AppendLine("</RunningQueue>");
                            sb.AppendLine("</queue>");

                            path = sb.ToString();

                            recvBytes = System.Text.UTF8Encoding.UTF8.GetBytes(path);

                            // SendBack to socket
                            mySocket.Send(recvBytes, recvBytes.Length, SocketFlags.None);

                            // Close Socket
                            mySocket.Close();
                        }
                        catch (Exception ze)
                        {
                            writeLog(ze.Message + System.Environment.NewLine + ze.StackTrace);
                            string cont = "Error Type: Query Status error" + System.Environment.NewLine;
                            cont += "Error Message: " + ze.Message + System.Environment.NewLine;
                            cont += "Stack Trace: " + ze.StackTrace;
                            mail(cont);

                        }

                    }

                }
                catch (SocketException e)
                {
                    writeLog("QueryStatus SocketException: " + e.Message + System.Environment.NewLine + e.StackTrace);
                    string cont = "Error Type: Query Status socket error" + System.Environment.NewLine;
                    cont += "Error Message: " + e.Message + System.Environment.NewLine;
                    cont += "Stack Trace: " + e.StackTrace;
                    mail(cont);
                }
                finally
                {

                    QueryServerListener.Stop();
                }
                writeLog("QueryStatus Listener restart.....");
            }
        }
        public void StartServer()
        {
            while (true)
            {
                CreateJobListener = null;
                try
                {
                    // IP Address.
                    IPAddress localAddr = IPAddress.Parse(ipBinding);

                    // Listen to port PORT.
                    CreateJobListener = new TcpListener(localAddr, PORT);

                    // Start listening.
                    CreateJobListener.Start();
                    writeLog("Listening CreateJob Service " + PORT.ToString() + " port……");

                    while (true)
                    {
                        // Process suspend, waiting for socket connection.
                        Socket mySocket = CreateJobListener.AcceptSocket();

                        try
                        {
                            // Socket received
                            //Console.WriteLine("Socket connected");

                            // Receive Buffer
                            Byte[] recvBytes = new Byte[2560000];

                            // Read to buffer
                            Int32 bytes = mySocket.Receive(recvBytes, recvBytes.Length, SocketFlags.None);

                            // Convert to string.
                            String str = System.Text.UTF8Encoding.UTF8.GetString(recvBytes, 0, bytes);

                            //Console.WriteLine("Data Received：{0}", str);

                            string path = "";
                            string[] data = str.Split(new string[] { "||||" }, StringSplitOptions.None);
                            //data[0]: JobClassID
                            //data[1]: parameters
                            //data[2]: depencyID
                            path = findRegisterClass(data[0]);

                            if (path.Equals(""))
                            {
                                path = "NOT FOUND";
                            }
                            else
                            {
                                int p = path.LastIndexOf(@"\");
                                string folder = path.Substring(0, p + 1);
                                string[] asm = data[0].Split(new char[] { '.' });

                                System.Security.Policy.Evidence evi = new System.Security.Policy.Evidence();
                                evi.AddAssembly(asm[0]);
                                evi.AddHost(new Zone(SecurityZone.MyComputer));

                                System.AppDomain apd = System.AppDomain.CreateDomain(data[0], evi, folder, folder, true);

                                com.dsc.kernal.batch.JobClass job = (com.dsc.kernal.batch.JobClass)apd.CreateInstanceAndUnwrap(asm[0], data[0]);

                                job.setParameter(data[1]);
                                job.setDependencies(data[2]);
                                job.setServicePath(runningPath);
                                job.email = email;
                                job.sendermail = sendermail;

                                waitingQueue.push(job);
                                path = job.getJobID();
                            }

                            recvBytes = System.Text.UTF8Encoding.UTF8.GetBytes(path);

                            // SendBack to socket
                            mySocket.Send(recvBytes, recvBytes.Length, SocketFlags.None);

                            // Close Socket
                            mySocket.Close();
                        }
                        catch (Exception ze)
                        {
                            try
                            {
                                byte[] recvBytes = System.Text.UTF8Encoding.UTF8.GetBytes(ze.Message + System.Environment.NewLine + ze.StackTrace);

                                // SendBack to socket
                                mySocket.Send(recvBytes, recvBytes.Length, SocketFlags.None);

                                // Close Socket
                                mySocket.Close();

                            }
                            catch { };
                            writeLog(ze.Message + System.Environment.NewLine + ze.StackTrace);
                            string cont = "Error Type: Create Job error" + System.Environment.NewLine;
                            cont += "Error Message: " + ze.Message + System.Environment.NewLine;
                            cont += "Stack Trace: " + ze.StackTrace;
                            mail(cont);
                        }

                    }

                }
                catch (SocketException e)
                {
                    writeLog("CreateJob SocketException: " + e.Message + System.Environment.NewLine + e.StackTrace);
                    string cont = "Error Type: Listen socket error" + System.Environment.NewLine;
                    cont += "Error Message: " + e.Message + System.Environment.NewLine;
                    cont += "Stack Trace: " + e.StackTrace;
                    mail(cont);
                }
                finally
                {

                    CreateJobListener.Stop();
                }
                writeLog("CreateJob Listener restart.....");

            }
        }

        private void writeLog(string log)
        {
            lock (m_mutex)
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\\log\\system_" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace(@"\", "_").Replace("/", "_") + ".log";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true);
                //sw.WriteLine("Job Class: " + this.GetType().FullName);
                sw.WriteLine("Log Time: " + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null));
                sw.WriteLine("Log Content:");
                sw.WriteLine(log);
                sw.Close();
            }
        }

        private void mail(string content)
        {
            try
            {
                com.dsc.kernal.utility.MailProcessor.sendMail("127.0.0.1", email, sendermail, "[ECP Job Queue Error]", content);
            }
            catch { };

        }

    }
}
