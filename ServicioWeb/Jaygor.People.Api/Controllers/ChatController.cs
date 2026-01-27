using System;
using Microsoft.AspNetCore.Mvc;
using JayGor.People.Bussinness;
using Microsoft.AspNetCore.Authorization;
using JayGor.People.ErrorManager;
using JayGor.People.Entities.Responses;
using JayGor.People.Api.helpers;
using System.Linq;
using System.Collections.Generic;
using JayGor.People.Entities.CustomEntities;
using JayGor.People.Entities.Requests;
using JayGor.People.DataAccess;

namespace Jaygor.People.Api.Controllers
{
    [Route("[controller]")]
    public class ChatController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BussinnessLayer bussinnessLayer; // = new BussinnessLayer();

        public ChatController(IDatabaseService ds)
        {          
            this.bussinnessLayer = new BussinnessLayer(ds);
        }

        [Authorize(Roles = "user")] // List<RoomChat> Rooms public GetLastUpdateRespose_Chat
        [HttpGet("getlastupdatechat/{idUser}/{roomVersion}/{messagesVersion}/{participantsVersion}")]
        public GetLastUpdateRespose_Chat GetLastUpdate_Chat(long idUser, long roomVersion, long messagesVersion, long participantsVersion)
        {           
            var response = new GetLastUpdateRespose_Chat();

            try
            {
                // Todo: remove idUser, get it from httpcontext :security issue.                
                if(idUser==0)
                {
                    var userRequesting = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Single().Value;
                    
                    idUser = bussinnessLayer.IdentityGetUserByEmail(userRequesting).Id;
                }


                long roomVersionAux = roomVersion;
                long participantsVersionAux = participantsVersion;
                long messagesVersionAux = messagesVersion;

                var globalParticipantsAux = new List<ParticipantUserChat>();

                response.Rooms = bussinnessLayer.GetLastUpdate_Chat(idUser, ref roomVersionAux, ref messagesVersionAux, ref participantsVersionAux,out globalParticipantsAux);
                response.GlobalParticipants = globalParticipantsAux;

                response.RoomVersion = roomVersionAux;
                response.ParticipantsVersion = participantsVersionAux;
                response.MessagesVersion = messagesVersionAux;

                response.CurrentDateTime = DateTime.Now;

                foreach(var s in response.Rooms)
                {
                    s.LastMessage = DateTime.Now;
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
            }


            // HelperChat.
            return response;
        }

		// addRemoveParticipant(idgroup, participant)
		// create group (name, participants)
		// send message (idgroup, mensaje)
		[HttpPost("sendmessage")]
        public CommonResponse SendMessage_Chat(long idUser, long idRoom, string msg)
		{
			var response = new CommonResponse();

			try
			{
                response.Result = bussinnessLayer.SendMessage(idUser, idRoom, msg);

                if(response.Result)
                {
                    HelperChat.SetChangedTimeStamp();
                }
                    
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
			}


			// HelperChat.

			return response;
		}


		[HttpPost("createroom")]
        public CommonResponse CreateRoom_Chat([FromBody] CreateRoomRequest_Chat request)
		{
			var response = new CommonResponse();

			try
			{
                response.Result = bussinnessLayer.CreateRoom_Chat(request.RoomName, request.Participants);
				
                if (response.Result)
				{
					HelperChat.SetChangedTimeStamp();
				}
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
                response.Messages.Add(new JayGor.People.Entities.Responses.GenericPair{ Id = "1", Description = ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : "")});
			}

			// HelperChat.
			return response;
		}

		[HttpPost("updatemessagestate")]
		public CommonResponse UpdateMessageState_Chat([FromBody] UpdateMessagesStateRequest_Chat request)
		{
			var response = new CommonResponse();

			try
			{
                response.Result = bussinnessLayer.UpdateMessageState_Chat(request.UserId, request.DeliveredMessagesIds, request.ReadMessagesIds);
			}
			catch (Exception ex)
			{
				response.Messages.Add(ErrorsManager.GetUnknowErrorWithDataBaseReference(bussinnessLayer.CommonSaveError(ErrorsManager.GetFormatedError(ex).Description).TagInfo));
				//response.Messages.Add(new JayGor.People.Entities.Responses.GenericPair { Id = "1", Description = ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : "") });
			}

			// HelperChat.
			return response;
		}


	}
}