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
using Android.Graphics;
using Android.Content.Res;
using Android.Support.V7.App;
using static Android.App.ActionBar;

[assembly: Dependency(typeof(AlertService))]

namespace CutyOthello.Droid
{
    class AlertService : IAlertService
    {
        public Task<AlertResult> ShowDialog(string title, string message,string Yes)
        {
            var tcs = new TaskCompletionSource<AlertResult>();

            Typeface typefaceOriginal = Typeface.CreateFromAsset(Forms.Context.Assets, "mini-wakuwaku-maru.otf");

            var view = new Android.Widget.TextView(Forms.Context);
            view.SetBackgroundColor(Android.Graphics.Color.LightPink);
            view.SetHeight(150);
            view.SetWidth(200);
            view.SetTextSize(ComplexUnitType.Px, 50);
            view.SetTypeface(typefaceOriginal, TypefaceStyle.Normal);
            view.SetTextColor(Android.Graphics.Color.RoyalBlue);
            view.Text = message;

            var view2 = new Android.Widget.TextView(Forms.Context);
            view2.SetBackgroundColor(Android.Graphics.Color.LightPink);
            view2.SetHeight(150);
            view2.SetWidth(200);
            view2.SetTextSize(ComplexUnitType.Px, 50);
            view2.SetTypeface(typefaceOriginal, TypefaceStyle.Normal);
            view2.SetTextColor(Android.Graphics.Color.RoyalBlue);
            view2.Text = title;

            var CustomDialog =
            new Android.Support.V7.App.AlertDialog.Builder(Forms.Context)
                .SetCustomTitle(view2)
                .SetIcon(Resource.Drawable.buldogSample)
                .SetView(view)
                .SetPositiveButton(
                Yes, (s, a) => { Console.WriteLine(""); })
                .Show();

            var button1 = CustomDialog.GetButton(-1);
            button1.SetBackgroundColor(Android.Graphics.Color.LightPink);
            button1.SetWidth(300);
            button1.SetHeight(150);
            button1.SetTextSize(ComplexUnitType.Px, 50);
            button1.SetTypeface(typefaceOriginal, TypefaceStyle.Normal);
            button1.SetTextColor(Android.Graphics.Color.RoyalBlue);

            return tcs.Task;
        }

        public Task<AlertResult> ShowYesNoDialog(string title, string message, string Yes, string No)
        {
            var tcs = new TaskCompletionSource<AlertResult>();
            AlertResult alertResult = new AlertResult();

            Typeface typefaceOriginal = Typeface.CreateFromAsset(Forms.Context.Assets, "mini-wakuwaku-maru.otf");

            var view = new Android.Widget.TextView(Forms.Context);
            view.SetBackgroundColor(Android.Graphics.Color.LightPink);
            view.SetHeight(150);
            view.SetWidth(200);
            view.SetTextSize(ComplexUnitType.Px, 50);
            view.SetTypeface(typefaceOriginal, TypefaceStyle.Normal);
            view.SetTextColor(Android.Graphics.Color.RoyalBlue);
            view.Text = message;

            var view2 = new Android.Widget.TextView(Forms.Context);
            view2.SetBackgroundColor(Android.Graphics.Color.LightPink);
            view2.SetHeight(150);
            view2.SetWidth(200);
            view2.SetTextSize(ComplexUnitType.Px, 50);
            view2.SetTypeface(typefaceOriginal, TypefaceStyle.Normal);
            view2.SetTextColor(Android.Graphics.Color.RoyalBlue);
            view2.Text = title;

            var CustomDialog = 
            new Android.Support.V7.App.AlertDialog.Builder(Forms.Context)
                .SetCustomTitle(view2)
                .SetIcon(Resource.Drawable.buldogSample)
                .SetView(view)
                .SetPositiveButton(
                Yes, (s, a) => { alertResult.isYes = true; })
                .SetNegativeButton( //Cancelボタンの処理
                No, (s, a) => { alertResult.isYes = false; })
                .Show();

            var button1 = CustomDialog.GetButton(-1);
            button1.SetBackgroundColor(Android.Graphics.Color.LightPink);
            button1.SetWidth(300);
            button1.SetHeight(150);
            button1.SetTextSize(ComplexUnitType.Px, 50);
            button1.SetTypeface(typefaceOriginal, TypefaceStyle.Normal);
            button1.SetTextColor(Android.Graphics.Color.RoyalBlue);

            var button2 = CustomDialog.GetButton(-2);
            button2.SetBackgroundColor(Android.Graphics.Color.LightPink);
            button2.SetWidth(300);
            button2.SetHeight(150);
            button2.SetTextSize(ComplexUnitType.Px, 50);
            button2.SetTypeface(typefaceOriginal, TypefaceStyle.Normal);
            button2.SetTextColor(Android.Graphics.Color.RoyalBlue);
            button2.SetLineSpacing(100, 100);
            button2.SetX(-190);

            tcs.SetResult(alertResult);
            return tcs.Task;
        }
    }
}