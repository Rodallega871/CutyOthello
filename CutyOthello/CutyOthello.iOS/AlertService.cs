using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CutyOthello.Commn;
using CutyOthello.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AlertService))]

namespace CutyOthello.iOS
{
    class AlertService : IAlertService
    {
        public Task<AlertResult> ShowDialog(string title, string message, string Yes)
        {
            var tcs = new TaskCompletionSource<AlertResult>();

            var alert = new UIAlertView(title, message, null, Yes,null);
            alert.Show();

            return tcs.Task;
        }

        public Task<AlertResult> ShowYesNoDialog(string title, string message, string Yes, string no)
        {
            var tcs = new TaskCompletionSource<AlertResult>();

            var alert = new UIAlertView(title, message, null, Yes, new string[] { no });
            alert.Show();

            return tcs.Task;
        }
    }
}