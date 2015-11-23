using System;
using System.Collections.Generic;
using System.Linq;


namespace WindowsPhoneUWP.UpgradeHelpers
{    
    public static class ScheduledActionService
    {
        public static void Remove(string name)
        {
            var toast = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications().Where(item => item.Tag == name).FirstOrDefault();
            if(toast != null)
            {
                Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications().Where(item => item.Tag == name).FirstOrDefault());
                return;
            }
            var backgroundtask = ((IEnumerable<Windows.ApplicationModel.Background.IBackgroundTaskRegistration>)Windows.ApplicationModel.Background.BackgroundTaskRegistration.AllTasks.Values).Where(xx => xx.Name == name).First();
            if (backgroundtask != null)
            {
                backgroundtask.Unregister(true);
                return;
            }
        }
     
    }
}