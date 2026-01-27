using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JayGor.People.Api.helpers;
using JayGor.People.Bussinness;
using JayGor.People.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class ChatDemon : IHostedService , IDisposable
{
    private readonly ILogger _logger;
    private Timer _timer;
    private Timer _timer2;
    private object obj;
    private readonly BussinnessLayer bussinnessLayer;

    public ChatDemon(ILogger<ChatDemon> logger, IServiceScopeFactory ss)
	{
        _logger = logger;
        obj = new object();


        this.bussinnessLayer = new BussinnessLayer(ss.CreateScope().ServiceProvider.GetRequiredService<IDatabaseService>());

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

	public Task StartAsync(CancellationToken cancellationToken)
	{
		bussinnessLayer.CommonSaveError("start Chat Demon" + DateTime.Now);
          
         _logger.LogInformation("CHAT:: Timed Background Service is starting.");

		_timer = new System.Threading.Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
         _timer2 = new System.Threading.Timer(DoWork2, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
         
		// oneSignalHelper.Config();
		// HelperEmail.Config();
          
		return Task.CompletedTask;
	}

	private void DoWork(object state)
	{
        lock (obj)
        {
            if (HelperChat.RequireAttention())
            {
                 _logger.LogInformation("CHAT:: Timed Background Service is working.");
                Console.WriteLine(DateTime.Now);
                try
                {
                    var usersForPush = bussinnessLayer.GetUsersForPush_Chat();

                    foreach (var user in usersForPush)
                    {

                        var msgs = bussinnessLayer.GetUnDeliveredMessages_Chat(user.Id);

                        var msgPush = "";

                        int msgsCount = 0;

                        foreach(var m in msgs.OrderByDescending(c=>c.Id))
                        {
                            msgsCount++;
                            msgPush += string.Format("{0}  {1}", msgPush,  m.Msg);

                            if(msgsCount>=3)
                            {
                                break;
                            }
                        }

                         if (!string.IsNullOrEmpty(user.IdOneSignal))
                         {
                            oneSignalHelper.SendNotificationToPlayerId(user.IdOneSignal, 
                                                                       msgPush, 
                                                                       new data 
                                                                        { 
                                                                            TypeMessage = "chat", 
                                                                            Value1 = user.ServerRoomVersion.ToString(),
                                                                            Value2 = user.ServerMessagesVersion.ToString(),
                                                                            Value3 = user.ServerParticipantsVersion.ToString()                            
                                                                        });
                         }
                         else
                         {
                             _logger.LogError(string.Format("User {0} dont have push notification id", user.Email));
                         }
					}
                }
                catch (Exception ex)
                {
                    _logger.LogError("CHAT:: Notification Demon error: " + ex.Message);
                    bussinnessLayer.CommonSaveError(ex.Message);
                }

                HelperChat.SetDoneWork();
            }
        }
	}

	private void DoWork2(object state)
	{
		lock (obj)
		{
			//if (HelperChat.RequireAttention())
			//{
				_logger.LogInformation("CHAT:: Timed Background Service is working.");
				Console.WriteLine(DateTime.Now);

				try
				{
					var usersForPush = bussinnessLayer.GetUsersForPush_Chat();

					foreach (var user in usersForPush)
					{
						var msgPush = "";

						if (!string.IsNullOrEmpty(user.IdOneSignal))
						{
							oneSignalHelper.SendNotificationToPlayerId(user.IdOneSignal,
																	   msgPush,
																	   new data
																	   {
																		   TypeMessage = "chat",
																		   Value1 = user.ServerRoomVersion.ToString(),
																		   Value2 = user.ServerMessagesVersion.ToString(),
																		   Value3 = user.ServerParticipantsVersion.ToString()
																	   });
						}
						else
						{
							_logger.LogError(string.Format("User {0} dont have push notification id", user.Email));
						}
					}
				}
				catch (Exception ex)
				{
					_logger.LogError("CHAT:: Notification Demon error: " + ex.Message);
					bussinnessLayer.CommonSaveError(ex.Message);
				}

				HelperChat.SetDoneWork();
			//}
		}
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		bussinnessLayer.CommonSaveError("stop chat " + DateTime.Now);

	_logger.LogInformation("Notification Demon was stopping.");

		_timer?.Change(Timeout.Infinite, 0);

		return Task.CompletedTask;
	}

	public void Dispose()
	{
        bussinnessLayer.CommonSaveError("CHAT:: Notification Demon was disposed" + DateTime.Now);
		_timer?.Dispose();
	}
}