using System;
using System.Collections;
using System.Text;

namespace ECPJobQueue.Queue
{
    public class AbstractQueue
    {
        private object m_mutex = new object();

        private ArrayList queue = new ArrayList();

        public bool isQueueEmpty()
        {
            bool ret = false;
            lock (m_mutex)
            {
                if (queue.Count == 0)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            return ret;

        }

        public int getQueueCount()
        {
            int c = 0;
            lock (m_mutex)
            {
                c = queue.Count;
            }
            return c;
        }
        public string queryStatus()
        {
            string ret = "";
            lock (m_mutex)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < queue.Count; i++)
                {
                    string tmp = "";
                    try
                    {
                        com.dsc.kernal.batch.JobClass job = (com.dsc.kernal.batch.JobClass)queue[i];
                        tmp += "<Job ID='" + job.getJobID() + "'>";
                        tmp+=job.getJobStatus().ToString();
                        tmp += "</Job>";
                        sb.Append(tmp);
                    }
                    catch { };
                }
                ret = sb.ToString();
            }
            return ret;
        }

        public bool isDepency(com.dsc.kernal.batch.JobClass job)
        {
            bool result = false;
            lock (m_mutex)
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    com.dsc.kernal.batch.JobClass jc = (com.dsc.kernal.batch.JobClass)queue[i];
                    if (jc.getJobID().Equals(job.getDependency()))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public void push(com.dsc.kernal.batch.JobClass job)
        {
            lock (m_mutex)
            {
                queue.Add(job);
            }
        }

        public com.dsc.kernal.batch.JobClass pop()
        {
            com.dsc.kernal.batch.JobClass jc = null;
            lock (m_mutex)
            {
                if (queue.Count > 0)
                {
                    jc = (com.dsc.kernal.batch.JobClass)queue[0];
                    queue.RemoveAt(0);
                }
            }
            return jc;
        }
    }
}
