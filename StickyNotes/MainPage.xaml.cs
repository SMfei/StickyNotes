﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.ApplicationModel.Core;
using StickyNotes.Models;
using StickyNotes.ViewModels;
using Windows.UI.ViewManagement;


// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace StickyNotes
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(300, 200);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 200));
            var windows = CoreApplication.GetCurrentView().TitleBar;

            windows.ExtendViewIntoTitleBar = false;

            //windows.ExtendViewIntoTitleBar = true;
        }
        //TODO will delete this method,just test 
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var noteViewModel = this.DataContext as NoteViewModel;
            noteViewModel?.PullCommand.Execute(null);
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            var noteViewModel = this.DataContext as NoteViewModel;
            noteViewModel?.PushCommand.Execute(null);
        }

        private void TurnTo_Click(object sender, RoutedEventArgs e)
        {
            {
                // 此处的NewPage是另一个页面的名字
                Frame.Navigate(typeof(DesignPage), "");
            }
        }
    }
}
