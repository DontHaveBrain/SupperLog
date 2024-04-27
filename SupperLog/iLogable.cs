using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperLog
{
    public interface iLogable
    {
        public void InitialLog(); 
        public void Info(string Msg);
        public void Debug(string Msg); 
        public void Warring(string Msg); 
        public void Error(string Msg);
    }
}
