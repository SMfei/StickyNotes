﻿using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace StickyNotes.Models
{
    public class Notification
    {
         public Notification(DateTime alarmTime)
        {
            string title = "notes";
            string content = "提醒时间到";

            // Construct the visuals of the toast
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },

                        new AdaptiveText()
                        {
                            Text = content
                        }
                    }
                }
            };
            // In a real app, these would be initialized with actual data
            int conversationId = 384928;
            // Construct the actions for the toast (inputs and buttons)
            ToastActionsCustom actions = new ToastActionsCustom()
            {
                Inputs =
                {
                     new ToastSelectionBox("snoozeTime")
                    {
                        DefaultSelectionBoxItemId = "15",
                        Items =
                        {
                            new ToastSelectionBoxItem("5", "5 分钟"),
                            new ToastSelectionBoxItem("15", "15 分钟"),
                            new ToastSelectionBoxItem("60", "1 小时"),
                            new ToastSelectionBoxItem("240", "4 小时"),
                            new ToastSelectionBoxItem("1440", "1 天")
                        }
                    }
                },

                Buttons =
                {
                    new ToastButtonSnooze()
                    {
                        SelectionBoxId = "snoozeTime"
                    },

                    new ToastButtonDismiss()
                }

            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Actions = actions,
                Scenario = ToastScenario.Reminder,
                // Arguments when the user taps body of toast
                Launch = new QueryString()
                {
                    { "action", "viewConversation" },
                    { "conversationId", conversationId.ToString() }

                }.ToString()
            };


            var toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddDays(2);

            if (alarmTime > DateTime.Now.AddSeconds(5))
            {

                // Create the scheduled notification
                var scheduledNotif = new ScheduledToastNotification(
                    toastContent.GetXml(), // Content of the toast
                    alarmTime // Time we want the toast to appear at
                    );

                // And add it to the schedule
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledNotif);
            }
        }
    }
}
