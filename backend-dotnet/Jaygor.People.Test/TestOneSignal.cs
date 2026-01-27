//oneSignalHelper.SendNotificationToPlayerId(user.IdOneSignal, msgPush, new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() }); 
using Xunit;
using JayGor.People.Api.helpers;
using System;

public class TestOneSignal
{
    [Fact]
    public void SendOneSignalTest1()
    {
        try
        {
            oneSignalHelper.Config();
            //
            //oneSignalHelper.SendNotificationToPlayerId(" 1b7eff62-d106-4bb6-8145-71e866667385", "People Reminder!!! Hi Achá Christian, in 5 min will deadline for TASK : Test 1 Reminder, for PROJECT : People Web", new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });
            //oneSignalHelper.SendNotificationToPlayerId("966ac916-84d5-4867-b3cb-0769f13549a1", "People Reminder!!! Hi Achá Christian, in 5 min will deadline for TASK : Test 1 Reminder, for PROJECT : People Web", new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });
            // desconocido  
            oneSignalHelper.SendNotificationToPlayerId("966ac916-84d5-4867-b3cb-0769f13549a1", "If you recieve this message please send me your name and this code: 3000 to my email address  napilex@gmail.com, I'm fixing some issue with notifications. tks.", new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });

            Assert.True(true);
        }
        catch(Exception ex)
        {
            Assert.True(false);
        }

    }


}