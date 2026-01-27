using System;
using System.Threading;
using System.Threading.Tasks;
using Jaygor.People.Api.helpers;
using JayGor.People.Api.helpers;
using JayGor.People.Bussinness;
using JayGor.People.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class NotificationDemon : IHostedService, IDisposable
{
	private readonly ILogger _logger;
	private Timer _timer;
    private readonly BussinnessLayer bussinnessLayer;
    private object obj;

    public NotificationDemon(ILogger<ChatDemon> logger, IServiceScopeFactory ss)
    {
        _logger = logger;

        this.bussinnessLayer = new BussinnessLayer(ss.CreateScope().ServiceProvider.GetRequiredService<IDatabaseService>());

        obj = new object();
        //using (var scope = ss.CreateScope())
        //{
        //    var ds = scope.ServiceProvider.GetRequiredService<IDatabaseService>();
        //    this.bussinnessLayer = new BussinnessLayer(ds);
        //    // var valornumerico = dbContext.GetLastActivePeriod();
        //}

        // Dont erase.
        // using (var scope = ss.CreateScope())
        // {
        //    var dbContext = scope.ServiceProvider.GetRequiredService<MySqlContextDB>();
        //    var x = dbContext.identity_roles; // .GetLastActivePeriod();
        // }
    }

 //   public NotificationDemon(ILogger<NotificationDemon> logger, IDatabaseService ds)
	//{
	//	_logger = logger;
 //       this.bussinnessLayer = new BussinnessLayer(ds);
	//}






	public Task StartAsync(CancellationToken cancellationToken)
	{
		bussinnessLayer.CommonSaveError("start Notification Demon" + DateTime.Now);

		_logger.LogInformation("Timed Background Service is starting.");

		_timer = new System.Threading.Timer(DoWork, null, TimeSpan.Zero,
			TimeSpan.FromSeconds(40));

		oneSignalHelper.Config();
        HelperEmail.Config();

		return Task.CompletedTask;
	}

	private void DoWork(object state)
	{
        lock (obj)
        {
            // ESTE HACE
            _logger.LogInformation("Timed Background Service is working.");
            Console.WriteLine(DateTime.Now);

            //bussinnessLayer.CommonSaveError("work " + DateTime.Now);

            // FUNCIONA BARBARO
            // oneSignalHelper.SendNotificationToPlayerId("4966a33c-f23e-4bab-8d80-7b8ca3140097", "test " + DateTime.Now, new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });

            try
            {
                var tasks = bussinnessLayer.GetTaskReminderByCurrentTime();

                foreach (var t in tasks)
                {
                    var user = t.IdfTaskNavigation.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation;

                    string projectName = CommonHelper.GetSubStringText(t.IdfTaskNavigation.IdfProjectNavigation.ProjectName, 30);
                    string userName = string.Format("{0} {1}", user.LastName, user.FirstName);

                    var msgPush = string.Format("{0} mins! {1}",
                                            t.IdfSettingReminderTimeNavigation.MinutesBefore,
                                            CommonHelper.GetSubStringText(t.IdfTaskNavigation.Subject, 30));

                    var msgEmail = string.Format("Reminder from Hatts Off! Hi {0}, in {1} minutes task {2} from program {3} is due",
                                           userName,
                                           t.IdfSettingReminderTimeNavigation.MinutesBefore,
                                           t.IdfTaskNavigation.Subject,
                                           projectName);

                    HelperEmail.SendEmailAsync(userName, user.Email, string.Format("Reminder from Hatts Off : {0}", t.IdfTaskNavigation.Subject), msgEmail);

                    if (!string.IsNullOrEmpty(user.IdOneSignal))
                    {
                        oneSignalHelper.SendNotificationToPlayerId(user.IdOneSignal, msgPush, new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });
                    }
                    else
                    {
                        _logger.LogError(string.Format("User {0} dont have push notification id", user.Email));
                    }

                    if (!string.IsNullOrEmpty(user.IdOneSignalWeb))
                    {
                        oneSignalHelper.SendNotificationToPlayerId(user.IdOneSignalWeb, msgPush, new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });
                    }
                    else
                    {
                        _logger.LogError(string.Format("User {0} dont have push notification id", user.Email));
                    }

                }


                // medication
                var remindersMedication = bussinnessLayer.GetMedicalRemindersByCurrentTime();

                foreach (var t in remindersMedication)
                {
                    var user = bussinnessLayer.IdentityGetUserById(t.IdUser); //  t.IdfTaskNavigation.IdfAssignedToNavigation.IdfStaffNavigation.IdfUserNavigation;

                    //string projectName = t.ProjectName;
                    //string userName = string.Format("{0} {1}", user.LastName, user.FirstName);

                    //var msgPush = string.Format("{0} mins! {1}",
                    //t.IdfSettingReminderTimeNavigation.MinutesBefore,
                    //CommonHelper.GetSubStringText(t.IdfTaskNavigation.Subject, 30));

                    var msgPush = string.Format("{0}  {1} to {2}",
                                          t.Datetime.ToShortTimeString(),
                                          t.Description,
                                          t.Client);


                    var msgEmail = string.Format("Reminder from Hatts Off! Hi {0}, you must supply {1} to {2} at {3}",
                                           t.SppDescription,
                                           t.Description,
                                           t.Client,
                                           t.Datetime.ToShortTimeString());

                    HelperEmail.SendEmailAsync(t.SppDescription, user.Email, string.Format("Reminder from Hatts Off : {0}", "Medical Reminder"), msgEmail);

                    if (!string.IsNullOrEmpty(user.IdOneSignal))
                    {
                        oneSignalHelper.SendNotificationToPlayerId(user.IdOneSignal, msgPush, new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });
                    }
                    else
                    {
                        _logger.LogError(string.Format("User {0} dont have push notification id", user.Email));
                    }

                    if (!string.IsNullOrEmpty(user.IdOneSignalWeb))
                    {
                        oneSignalHelper.SendNotificationToPlayerId(user.IdOneSignalWeb, msgPush, new data { TypeMessage = "notification", Value1 = DateTime.Now.ToString() });
                    }
                    else
                    {
                        _logger.LogError(string.Format("User {0} dont have push notification id", user.Email));
                    }

                }



            }
            catch (Exception ex)
            {
                _logger.LogError("Notification Demon error: " + ex.Message);
                bussinnessLayer.CommonSaveError(ex.Message);
            }
        }
	}


	public Task StopAsync(CancellationToken cancellationToken)
	{
		bussinnessLayer.CommonSaveError("stop " + DateTime.Now);

		_logger.LogInformation("Notification Demon was stopping.");

		_timer?.Change(Timeout.Infinite, 0);

		return Task.CompletedTask;
	}

	public void Dispose()
	{
		bussinnessLayer.CommonSaveError("Notification Demon was disposed" + DateTime.Now);
		_timer?.Dispose();
	}
}