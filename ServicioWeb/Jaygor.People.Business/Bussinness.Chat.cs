using System;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Responses;
using System.Linq;
using JayGor.People.Entities.Entities;
using System.Collections.Generic;
using Jaygor.People.Bussinness.helpers;

namespace JayGor.People.Bussinness
{
    public partial class BussinnessLayer
    {

        public List<MessageChat> GetUnDeliveredMessages_Chat(long idUser)
        {
            return dataAccessLayer.GetUnDeliveredMessages_Chat(idUser);    
        }

        public List<IdentityusersCustom_Chat> GetUsersForPush_Chat()
        {
            return dataAccessLayer.GetUsersForPush_Chat();
        }

		public List<IdentityusersCustom_Chat> GetUsersForPush_Chat2()
		{
			return dataAccessLayer.GetUsersForPush_Chat2();
		}
                
        public List<RoomChat> GetLastUpdate_Chat(long idUser,
                                        ref long roomVersion,
                                        ref long globalParticipantsVersion,
                                        ref long messagesVersion,
                                        out List<ParticipantUserChat> globalParticipants)
        {
            var roomsResult = new List<RoomChat>();

            globalParticipants = new List<ParticipantUserChat>();

            if(roomVersion == 0)
            {
                roomsResult = dataAccessLayer.GetRooms_Chat(idUser);

                foreach(var r in roomsResult)
                {
                    // add participants

                    r.Participants = new List<ParticipantRoomChat>();

                    var auxParticipants = dataAccessLayer.GetRoomParticipant_Chat(r.Id);

                    if (auxParticipants != null)
                    {
                        r.Participants.AddRange(auxParticipants);
                    }

                    // add messages
                    r.Messages = new List<MessageChat>();

                    var auxMessageChat = dataAccessLayer.GetMessages_Chat(idUser, r.Id, 0);

                    if (auxMessageChat != null)
                    {
                        r.Messages.AddRange(auxMessageChat);
                    }

                    r.ImgRoom = r.Participants.Count() == 0 || r.Participants.Count() > 2 ? "none" : r.Participants.Where(c => c.IdfUser != idUser).FirstOrDefault() == null ? "none" :r.Participants.Where(c => c.IdfUser != idUser).FirstOrDefault().ImgUser;
                }



                roomsResult.Where(c => c.Messages != null)
                                   .ToList()
                                   .ForEach(r => r.Messages.ForEach(m =>
                                   {
                                       m.IsYou = m.SenderId == idUser;
                                   }));



                // Global Participants = get

                globalParticipants = dataAccessLayer.GetGlobalParticipant_Chat();

                roomVersion = 1;
                globalParticipantsVersion = 1;
                messagesVersion = 1;

                dataAccessLayer.UpdateIdentityUserTimeStamp_Chat(idUser, 1, 1 , 1 ,"C");
                dataAccessLayer.UpdateIdentityUserTimeStamp_Chat(idUser, 1, 1, 1, "S");
            }
            else {

                var iuts = dataAccessLayer.GetIdentityUserTimeStamp_Chat(idUser);

                //if (iuts.ClientRoomVersion != iuts.ServerRoomVersion ||
                //    iuts.ClientMessagesVersion != iuts.ServerMessagesVersion ||
                //    iuts.ClientParticipantsVersion != iuts.ServerParticipantsVersion)
                //{
                    var roomsAux = dataAccessLayer.GetRooms_Chat(idUser);

                    // si se actualizo rooms.
                    if (iuts.ClientRoomVersion != iuts.ServerRoomVersion)
                    {

                        if (roomsAux != null)
                        {
                            roomsResult.AddRange(roomsAux);
                        }

                        foreach (var r in roomsResult)
                        {
                            // add participants

                            r.Participants = new List<ParticipantRoomChat>();


                            var auxParticipantsRoom = dataAccessLayer.GetRoomParticipant_Chat(r.Id);

                            if (auxParticipantsRoom != null)
                            {
                                r.Participants.AddRange(auxParticipantsRoom);
                            }

                            // add messages
                            // r.Messages = new List<MessageChat>();
                            // r.Messages.AddRange(dataAccessLayer.GetMessages_Chat(idUser, r.Id, 0));
                        }


                        roomsResult.ForEach(r => r.ImgRoom = r.Participants.Count() == 0 || r.Participants.Count() > 2 ? "none" : r.Participants.Where(c => c.IdfUser != idUser).FirstOrDefault() == null ? "none" : r.Participants.Where(c => c.IdfUser != idUser).FirstOrDefault().ImgUser);

                }

                    // si se actualizo messages.
                    if (iuts.ClientMessagesVersion != iuts.ServerMessagesVersion)
                    {
                        // var participantsAux = dataAccessLayer.GetRoomParticipant_Chat()
                        var messages = dataAccessLayer.GetUnDeliveredMessages_Chat(idUser);

                        foreach (var roomindex in messages.DistinctBy(c => c.IdRoom).ToList())
                        {
                            var auxRoom = roomsAux.Where(c => c.Id == roomindex.IdRoom).Single();

                            auxRoom.Messages = new List<MessageChat>();

                            if (messages != null)
                            {
                                auxRoom.Messages.AddRange(messages.Where(c=>c.IdRoom == roomindex.IdRoom));
                            }


                        // participants

                            roomsResult.Add(auxRoom);
                        }

                        roomsResult.Where(c=>c.Messages !=null)
                                   .ToList()
                                   .ForEach(r => r.Messages.ForEach(m =>
                                                 {
                                                     m.IsYou = m.SenderId == idUser;
                                                 }));

                    }

                    // si se actuaizo global pat
                    if (iuts.ClientParticipantsVersion != iuts.ServerParticipantsVersion)
                    {
                        globalParticipants = dataAccessLayer.GetGlobalParticipant_Chat();
                    }



                    //dataAccessLayer.UpdateIdentityUserTimeStamp_Chat(idUser, iuts.ServerRoomVersion, iuts.ServerParticipantsVersion , iuts.ServerMessagesVersion, "C");
                    
                    //// dataAccessLayer.UpdateIdentityUserTimeStamp_Chat(idUser, 1, 1, 1, "S");

                    roomVersion = iuts.ServerRoomVersion;
                    globalParticipantsVersion = iuts.ServerParticipantsVersion;
                    messagesVersion = iuts.ServerMessagesVersion;
                //}
            }
                       
            //roomsResult.ForEach(r => r.Messages.ForEach(m =>
            //{
            //    m.IsYou = m.SenderId == idUser;
            //}));

            //roomsResult.ForEach(r=> r.ImgRoom = r.Participants.Count() == 0 || r.Participants.Count() > 2 ? "none" : r.Participants.Where(c => c.IdfUser != idUser).FirstOrDefault() == null ? "none" : r.Participants.Where(c => c.IdfUser != idUser).FirstOrDefault().ImgUser);

            return roomsResult;
        }

        public bool SendMessage(long idUser, long idRoom, string msg)
        {
            return dataAccessLayer.SendMessage(idUser, idRoom,msg);
        }
    
        public bool CreateRoom_Chat(string roomName, List<ParticipantUserChat> participants)
        {
            return dataAccessLayer.CreateRoom_Chat(roomName, participants);
        }



        public bool UpdateMessageState_Chat(long UserId, List<long> DeliveredMessagesIds, List<long> ReadMessagesIds)
        {
            return dataAccessLayer.UpdateMessageState_Chat(UserId,DeliveredMessagesIds,ReadMessagesIds);
        }



        //public List<RoomChat> GetRooms_Chat(long idUser)
        //{
        //    return dataAccessLayer.GetRooms_Chat(idUser);    
        //}


        //public  List<ParticipantRoomChat> GetRoomParticipant_Chat(long idChatRoom);
        //List<MessageChat> GetUnDeliveredMessages_Chat(long idUser);
        //List<MessageChat> GetMessages_Chat(long idUser, long idChatRoom, long lastMessageId = 0);

        // void UpdateIdentityUserTimeStamp_Chat(long idUser, long roomVersion, long participantsVersion, long messagesVersion, string updateClientORserver);
        // chat_identity_users_timestamp GetIdentityUserTimeStamp_Chat(long idUser);
        //List<ParticipantUserChat> GetGlobalParticipant_Chat();


        //bool CreateRoom_Chat(string roomName, List<ParticipantUserChat> participants);
        

        //bool RemoveParticipants_Chat(List<ParticipantRoomChat> participants);









    }
}