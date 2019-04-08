using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI
{
    public class Output
    {
        private static Output instance;
        public static Output Instance
        {
            get
            {
                if (instance == null) instance = new Output();
                return instance;
            }
        }

        private Action<string> ErrorDelegate;
        private Action<string> WriteDelegate;
        private Action<string> LogDelegate;

        public void SetOutputDelegate(Action<string> outputDelegate)
        {
            WriteDelegate = outputDelegate;
            FanucControllerLibrary.Output.Instance.SetOutputDelegate(outputDelegate);
        }

        public void SetLogDelegate(Action<string> logDelegate)
        {
            LogDelegate = logDelegate;
            FanucControllerLibrary.Output.Instance.SetLogDelegate(logDelegate);
        }

        public void SetErrorDelegate(Action<string> errorDelegate)
        {
            ErrorDelegate = errorDelegate;
            FanucControllerLibrary.Output.Instance.SetErrorDelegate(errorDelegate);
        }

        public void Write(string msg)
        {
            if (WriteDelegate != null)
            {
                WriteDelegate(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public void Log(string msg)
        {
            if (LogDelegate != null)
            {
                LogDelegate(msg);
            }
            else
            {
                Console.WriteLine($"log: {msg}");
            }
        }

        public void Error(string error)
        {
            if (ErrorDelegate != null)
            {
                ErrorDelegate(error);
            }
            else
            {
                Console.WriteLine($"Error: {error}");
            }
        }
    }
}
