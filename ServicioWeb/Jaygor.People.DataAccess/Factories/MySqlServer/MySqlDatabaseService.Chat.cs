using System;
using System.Linq;
using System.Collections.Generic;
//using JayGor.People.DataAccess.MySql;
using JayGor.People.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using JayGor.People.Entities.Responses;
using JayGor.People.Entities.CustomEntities;
using Microsoft.EntityFrameworkCore.Storage;
using JayGor.People.DataAccess.MySql;

namespace JayGor.People.DataAccess.Factories.MySqlServer
{
    public partial class MySqlDatabaseService : IDatabaseService
    {
        public List<IdentityusersCustom_Chat> GetUsersForPush_Chat()
        {
            return context.chat_identity_users_timestamp                                   
                            .Where(c => (c.ClientRoomVersion != c.ServerRoomVersion ||
                                   c.ClientMessagesVersion != c.ServerMessagesVersion ||
                                   c.ClientParticipantsVersion != c.ServerParticipantsVersion) &&
                                   c.IdfIdentityUserNavigation.State == "A") // &&
                                                                             //(DateTime.Now.Subtract(c.DateLastSentPush).TotalSeconds < 60)
                            .Select(p => new IdentityusersCustom_Chat
                            {
                                Id = p.Id,
                                FirstName = p.IdfIdentityUserNavigation.FirstName,
                                LastName = p.IdfIdentityUserNavigation.LastName,
                                IdOneSignal = p.IdfIdentityUserNavigation.IdOneSignal,
                                Email = p.IdfIdentityUserNavigation.Email,
                                ServerRoomVersion = p.ServerRoomVersion,
                                ServerMessagesVersion = p.ServerMessagesVersion,
                                ServerParticipantsVersion = p.ServerParticipantsVersion
                            }).ToList();
        }

		public List<IdentityusersCustom_Chat> GetUsersForPush_Chat2()
		{
			return context.chat_identity_users_timestamp
									.Where(c => (c.ClientRoomVersion != c.ServerRoomVersion ||
										   c.ClientMessagesVersion != c.ServerMessagesVersion ||
										   c.ClientParticipantsVersion != c.ServerParticipantsVersion) &&
										   c.IdfIdentityUserNavigation.State == "A" &&
										   c.DatePushSent.Subtract(DateTime.Now).TotalMinutes > 3) // &&
																								   //(DateTime.Now.Subtract(c.DateLastSentPush).TotalSeconds < 60)
									.Select(p => new IdentityusersCustom_Chat
									{
										Id = p.Id,
										FirstName = p.IdfIdentityUserNavigation.FirstName,
										LastName = p.IdfIdentityUserNavigation.LastName,
										IdOneSignal = p.IdfIdentityUserNavigation.IdOneSignal,
										Email = p.IdfIdentityUserNavigation.Email,
										ServerRoomVersion = p.ServerRoomVersion,
										ServerMessagesVersion = p.ServerMessagesVersion,
										ServerParticipantsVersion = p.ServerParticipantsVersion
									}).ToList();				
		}

		public List<RoomChat> GetRooms_Chat(long idUser)
        {
                return context.chat_room_participants
                                    .Where(c => c.IdfIdentityUser == idUser &&
                                           c.IdfChatRoomNavigation.State != "D" &&
                                           c.DateTo == null)
                                    .Select(p => new RoomChat
                                    {
                                        Id = p.IdfChatRoom,
                                        Name = p.IdfChatRoomNavigation.Name,
                                    }).Distinct()
                                    .ToList();
        }

        public List<ParticipantRoomChat> GetRoomParticipant_Chat(long idChatRoom)
        {
                return context.chat_room_participants
                                    .Where(c => c.IdfChatRoom == idChatRoom &&
                                           c.DateTo == null)
                                    .Select(p => new ParticipantRoomChat
                                    {
                                        Id = p.Id,
                                        Name = string.Format("{0} {1}", p.IdfIdentityUserNavigation.LastName, p.IdfIdentityUserNavigation.FirstName),
                                        IdfUser = p.IdfIdentityUser,
                                        ImgUser = context.identity_images.Where(c => c.Id == p.IdfIdentityUserNavigation.IdfImg).Single().Name
                                    }).ToList();
        }

        public List<MessageChat> GetUnDeliveredMessages_Chat(long idUser)
        {
                return context.chat_messages
                                    .Include(c => c.chat_message_participant_state)
                                    .Include(c => c.IdfIdentityUserSenderNavigation)
                                    .Join(context.chat_message_participant_state,
                                          m => m.Id,
                                          s => s.IdfMessage,
                                          (m, s) => new { M = m, S = s })
                                    .Where(ms => ms.S.Delivered == "0" &&
                                          ms.S.IdfParticipantNavigation.IdfIdentityUserNavigation.Id == idUser
                                          ).ToList()
                                   .Select(p => new MessageChat
                                   {
                                       Id = p.M.Id,
                                       Msg = p.M.Messages,
                                       State = "D",
                                       SenderId = p.M.IdfIdentityUserSenderNavigation.Id,
                                       SenderName = string.Format("{0} {1}", p.M.IdfIdentityUserSenderNavigation.LastName, p.M.IdfIdentityUserSenderNavigation.FirstName),
                                       IdRoom = p.M.IdfChatRoom
                                   }).OrderByDescending(c => c.Id)
                                   .ToList();               
        }

        public List<MessageChat> GetMessages_Chat(long idUser, long idChatRoom, long lastMessageId = 0)
        {
                return lastMessageId == 0 ?
                                    context.chat_messages
                                    .Where(c => c.IdfChatRoom == idChatRoom)
                                   .Select(p => new MessageChat
                                   {
                                       Id = p.Id,
                                       Msg = p.Messages,
                                       State = "D",
                                       SenderId = p.IdfIdentityUserSenderNavigation.Id,
                                       SenderName = string.Format("{0} {1}", p.IdfIdentityUserSenderNavigation.LastName, p.IdfIdentityUserSenderNavigation.FirstName),
                                       DateSent = p.Date.ToString("d MMM, HH:MM", System.Globalization.CultureInfo.InvariantCulture)
                                   })
                                   .OrderBy(c => c.Id)
                                   .ToList()
                                           :
                                    context.chat_messages
                                           .Where(c => c.IdfChatRoom == idChatRoom && c.Id < lastMessageId)
                                   .Select(p => new MessageChat
                                   {
                                       Id = p.Id,
                                       Msg = p.Messages,
                                       State = "D",
                                       SenderId = p.IdfIdentityUserSenderNavigation.Id,
                                       SenderName = string.Format("{0} {1}", p.IdfIdentityUserSenderNavigation.LastName, p.IdfIdentityUserSenderNavigation.FirstName),
                                       DateSent = p.Date.ToString("d MMM, HH:MM", System.Globalization.CultureInfo.InvariantCulture)
                                   })
                                   .OrderBy(c => c.Id)
                                   .ToList();
        }

        private long GetIdentityuserTimeStamp(long idUser, MySqlContextDB context, IDbContextTransaction transaction)
        {
            long result = 0;
            var c = context.chat_identity_users_timestamp.Where(cc => cc.IdfIdentityUser == idUser).FirstOrDefault();

            if (c == null)
            {
                var newrecord = new chat_identity_users_timestamp
                {
                    IdfIdentityUser = idUser,
                    DatePushSent = DateTime.Now,
                    ClientMessagesVersion = 0,
                    ClientRoomVersion = 0,
                    ClientParticipantsVersion = 0,
                    ServerRoomVersion = 0,
                    ServerMessagesVersion = 0,
                    ServerParticipantsVersion = 0
                };

                context.chat_identity_users_timestamp.Add(newrecord);
                context.SaveChanges();
                result = newrecord.Id;
            }
            else
            {
                result = c.Id;
            }

            return result;
        }


        public void UpdateIdentityUserTimeStamp_Chat(long idUser,
                                        long roomVersion,
                                        long participantsVersion,
                                        long messagesVersion,
                                        string updateClientORserver)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {
                var c = context.chat_identity_users_timestamp.Where(d => d.Id == this.GetIdentityuserTimeStamp(idUser, context, transaction)).Single();

                if (updateClientORserver == "C")
                {
                    c.ClientRoomVersion = roomVersion;
                    c.ClientMessagesVersion = messagesVersion;
                    c.ClientParticipantsVersion = participantsVersion;
                }
                else
                {
                    c.ServerRoomVersion = roomVersion;
                    c.ServerMessagesVersion = messagesVersion;
                    c.ServerParticipantsVersion = participantsVersion;
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }


        public chat_identity_users_timestamp GetIdentityUserTimeStamp_Chat(long idUser)
        {
            return context.chat_identity_users_timestamp.Where(c => c.IdfIdentityUser == idUser).Single();                
        }

        public List<ParticipantUserChat> GetGlobalParticipant_Chat()
        {
                return context.staff
                        .Where(c => c.State != "D" &&
                               c.IdfUserNavigation.State != "D")
                        .Select(p => new ParticipantUserChat
                        {
                            Id = p.IdfUserNavigation.Id,
                            Name = string.Format("{0} {1}", p.IdfUserNavigation.LastName, p.IdfUserNavigation.FirstName),
                            ImgUser = context.identity_images.Where(c => c.Id == p.IdfUserNavigation.IdfImg).Single().Name
                        }).ToList();
        }


        public bool CreateRoom_Chat(string roomName, List<ParticipantUserChat> participants)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {
                var newRoom = new chat_rooms
                {
                    Name = roomName,
                    State = "C"
                };

                context.chat_rooms.Add(newRoom);
                context.SaveChanges();

                foreach (var p in participants)
                {
                    var newp = new chat_room_participants
                    {
                        IdfChatRoom = newRoom.Id,
                        DateFrom = DateTime.Now,
                        IdfIdentityUser = p.Id // .IdfUser
                    };

                    context.chat_room_participants.Add(newp);
                    context.SaveChanges();
                    context.chat_identity_users_timestamp.Where(d => d.Id == this.GetIdentityuserTimeStamp(p.Id, context, transaction)).Single().ServerRoomVersion += 1;
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return true;
        }

        public bool AddParticipants_Chat(long idRoomChat, List<ParticipantUserChat> participants)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {
                foreach (var p in participants)
                {
                    var recordToAdd = context.chat_room_participants.Where(c => c.IdfIdentityUser == p.Id && c.DateTo != null && c.IdfChatRoom == idRoomChat).FirstOrDefault();

                    if (recordToAdd == null)
                    {
                        var newRecord = new chat_room_participants
                        {
                            DateFrom = DateTime.Now,
                            IdfChatRoom = idRoomChat,
                            IdfIdentityUser = p.Id
                        };

                        context.chat_room_participants.Add(newRecord);
                        context.SaveChanges();
                        context.chat_identity_users_timestamp.Where(d => d.Id == this.GetIdentityuserTimeStamp(p.Id, context, transaction)).Single().ServerParticipantsVersion += 1;
                    }
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return true;
        }

        public bool RemoveParticipants_Chat(long idRoomChat, List<ParticipantRoomChat> participants)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {
                foreach (var p in participants)
                {
                    var recordToRemove = context.chat_room_participants.Where(c => c.Id == p.Id).Single();
                    recordToRemove.DateTo = DateTime.Now;
                }

                context.SaveChanges();
                foreach (var crp in context.chat_room_participants.Where(c => c.DateTo == null && c.IdfChatRoom == idRoomChat).ToList())
                {
                    context.chat_identity_users_timestamp.Where(d => d.Id == this.GetIdentityuserTimeStamp(crp.IdfIdentityUser, context, transaction)).Single().ServerParticipantsVersion += 1;
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }                

            return true;
        }

        public bool SendMessage(long idUser, long idRoom, string msg)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {
                var newmsg = new chat_messages
                {
                    Date = DateTime.Now,
                    IdfChatRoom = idRoom,
                    IdfIdentityUserSender = idUser,
                    Messages = msg
                };

                context.chat_messages.Add(newmsg);
                context.SaveChanges();

                // states
                foreach (var crp in context.chat_room_participants.Include(i => i.IdfIdentityUserNavigation).ThenInclude(v => v.chat_identity_users_timestamp).Where(c => c.DateTo == null && c.IdfChatRoom == idRoom).ToList())
                {
                    var newstate = new chat_message_participant_state
                    {
                        IdfMessage = newmsg.Id,
                        IdfParticipant = crp.Id,
                        Read = "0",
                        Delivered = "0"
                    };

                    context.chat_message_participant_state.Add(newstate);
                    crp.IdfIdentityUserNavigation.chat_identity_users_timestamp.FirstOrDefault().ServerMessagesVersion += 1;
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        
    
            return true;
        }

        public bool UpdateMessageState_Chat(long UserId, List<long> DeliveredMessagesIds, List<long> ReadMessagesIds)
        {
	
            context.chat_message_participant_state
                   .Where(c => c.IdfParticipantNavigation.IdfIdentityUserNavigation.Id == UserId 
                            && DeliveredMessagesIds.Contains(c.IdfMessage))
                   .ToList()
                   .ForEach(c => c.Delivered = "1");

		    context.chat_message_participant_state
				   .Where(c => c.IdfParticipantNavigation.IdfIdentityUserNavigation.Id == UserId
							&& ReadMessagesIds.Contains(c.IdfMessage))
				   .ToList()
                   .ForEach(c => c.Read = "1");


			context.SaveChanges();

			return true;
        }
    }
}
