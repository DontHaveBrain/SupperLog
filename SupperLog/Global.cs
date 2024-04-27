using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SupperLog
{
    partial class Global
    { 
        public static BlockingCollection<string> BCCQueue=new BlockingCollection<string>(new ConcurrentQueue<string>());
    }
}
