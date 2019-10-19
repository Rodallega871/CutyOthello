using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CutyOthello.Commn
{
    public interface IAlertService
    {
        Task<AlertResult> Show(string title, string message,string Yes,string no);
    }

    public class AlertResult
    {
        public string AlertTitle { get; set; }
        public string AlertSentence { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public bool isNo { get; set; }

    }

}
