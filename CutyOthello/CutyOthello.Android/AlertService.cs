using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CutyOthello.Commn;
using Android.Util;
using CutyOthello.Droid;

[assembly: Dependency(typeof(AlertService))]

namespace CutyOthello.Droid
{
    class AlertService : IAlertService
    {
        public Task<AlertResult> Show(string title, string message, string Yes, string no)
        {
            var tcs = new TaskCompletionSource<AlertResult>();

            new AlertDialog.Builder(Forms.Context)
                .SetTitle(title)
                .SetMessage(message)
                .SetIcon(Resource.Drawable.buldogSample)
                .SetNegativeButton(no, (o, e) => tcs.SetResult(new AlertResult
                {
                    AlertTitle = no,
                }))
                .SetPositiveButton(Yes, (o, e) => tcs.SetResult(new AlertResult
                {
                    AlertTitle = Yes,
                }))
                .Show();

            return tcs.Task;
        }
    }
}