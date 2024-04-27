using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SupperLog
{
    public class LogHelper : iLogable
    {
        void iLogable.InitialLog()
        {
            Thread thread = new Thread(initial);
            thread.IsBackground = true;
            thread.Start();
        }
        void initial()
        {
            Console.WriteLine("日志初始化成功");
            Directory.CreateDirectory("MyLog");
            string LogPathBase = Directory.GetCurrentDirectory();
            string LogPath = Path.Combine(LogPathBase, "MyLog", DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            string LogTime = DateTime.Now.ToString("yyyy-MM-dd");
            while (true)
            {
                string Msg = Global.BCCQueue.Take();
                Console.WriteLine("1");
                if (LogTime != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    LogTime = DateTime.Now.ToString("yyyy-MM-dd");
                    LogPath = Path.Combine(LogPathBase, "MyLog", DateTime.Now.ToString("yyyy-MM-dd") + ".log");
                }
                using (FileStream streamWrite = new FileStream(LogPath, FileMode.Append, FileAccess.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(Msg);
                    streamWrite.Write(bytes, 0, bytes.Length);
                }
            }
        }
         void iLogable.Debug(string Msg)
        {
            Console.WriteLine("调试信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg);
            Task.Run(async () =>
            {
                Global.BCCQueue.TryAdd("调试信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg + " \r\n ");
            });
        }
        void iLogable.Error(string Msg)
        {
            Console.WriteLine("错误信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg);
            Task.Run(() =>
            {
                Global.BCCQueue.TryAdd("错误信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg + " \r\n ");
            });
        }
        void iLogable.Info(string Msg)
        {
            Console.WriteLine("记录信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg);
            Task.Run(() =>
            {
                Global.BCCQueue.TryAdd("记录信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg + " \r\n ");
            });
        }
        void iLogable.Warring(string Msg)
        {
            Console.WriteLine("警告信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg);
            Task.Run(() =>
            {
                Global.BCCQueue.TryAdd("警告信息:" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "=>" + Msg + " \r\n ");
            });
        }
    }
}
