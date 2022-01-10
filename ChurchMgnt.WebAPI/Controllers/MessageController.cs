using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlayamMgmt.Common.DBEntity;
using ChurchMgnt.WebAPI.Models;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : BaseApiController
    {
        [Authorize()]
        [Route("GetContributionMessageByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetContributionMessageByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IMessageBusiness orgContributionMessageBusiness = new MessageBusiness(claimModel.LoginUserEmail);
                MessageModel messageModel = GetMessageBusinessModel(orgContributionMessageBusiness.getByOrgIdAndMsgTypeId(orgId, (int) MessageType.ContributionMsg));

                response = Request.CreateResponse(HttpStatusCode.OK, messageModel);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetContributionMessageByOrgId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [Authorize()]
        [Route("GetContributionEmailMessageByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetContributionEmailMessageByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IMessageBusiness orgContributionMessageBusiness = new MessageBusiness(claimModel.LoginUserEmail);
                MessageModel messageModel = GetMessageBusinessModel(orgContributionMessageBusiness.getByOrgIdAndMsgTypeId(orgId, (int)MessageType.ContributionEmailMsg));

                response = Request.CreateResponse(HttpStatusCode.OK, messageModel);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetContributionEmailMessageByOrgId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        private MessageModel GetMessageBusinessModel(MessageDBEntity messageDBEntity)
        {
            if (messageDBEntity != null)
            {
                return new MessageModel()
                {
                    Id = messageDBEntity.Id,
                    Active = messageDBEntity.Active,
                    CreateDateTime = messageDBEntity.CreateDateTime,
                    CreateUser = messageDBEntity.CreateUser,
                    LastUpdateDateTime = messageDBEntity.LastUpdateDateTime,
                    LastUpdateUser = messageDBEntity.LastUpdateUser,
                    Message = messageDBEntity.Message,
                    Organization = messageDBEntity.Organization.Name,
                    OrganizationId = messageDBEntity.OrganizationId,
                    RowTimeStamp = messageDBEntity.RowTimeStamp,
                    MessageType = messageDBEntity.MessageType.Name,
                    MessageTypeId = messageDBEntity.MessageTypeId
                };
            }
            else
                return null;
        }
    }
}
