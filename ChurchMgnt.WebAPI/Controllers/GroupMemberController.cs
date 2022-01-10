using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/GroupMember")]
    public class GroupMemberController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetByGroupId")]
        public HttpResponseMessage GetByGroupId(int groupId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IGroupMemberBusiness groupMemberBusiness = new GroupMemberBusiness(Constant.DEFAULT_EMAIL);
                List<GroupMemberModel> groupMemberBusinessModels = GetGroupMemberModels(groupMemberBusiness.GetByGroupId(groupId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, groupMemberBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByGroupId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetByFamilyMemberId")]
        public HttpResponseMessage GetByFamilyMemberId(int familyMemberId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IGroupMemberBusiness groupMemberBusiness = new GroupMemberBusiness(Constant.DEFAULT_EMAIL);
                List<GroupMemberModel> groupMemberBusinessModels = GetGroupMemberModels(groupMemberBusiness.GetByFamilyMemberId(familyMemberId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, groupMemberBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByFamilyMemberId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        [CustomAuthorizeAttribute]
        [Route("Enable")]
        [HttpPost]
        public HttpResponseMessage Enable([FromBody] EnableModel enableModel)
        {
            HttpResponseMessage response;
            try
            {
                IGroupMemberBusiness groupMemberBusiness = new GroupMemberBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = groupMemberBusiness.Enable(enableModel.Id, enableModel.Active);

                response = Request.CreateResponse(HttpStatusCode.OK, returnValue);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Enable");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Enable");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Enable");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("Save")]
        public HttpResponseMessage Save(List<GroupMemberModel> models)
        {
            HttpResponseMessage response;

            try
            {
                IGroupMemberBusiness groupMemberBusiness = new GroupMemberBusiness(Constant.DEFAULT_EMAIL);
                groupMemberBusiness.SaveAll(GetGroupMemberDBEntities(models));

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "SaveAll");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "SaveAll");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (DbUpdateException ex)
            {
                if (null != ex.InnerException || ex.InnerException.InnerException != null)
                {
                    var innerException = ex.InnerException.InnerException
                                               as System.Data.SqlClient.SqlException;
                    if (innerException != null &&
                           (
                               innerException.Number == 2627 ||
                               innerException.Number == 2601)
                           )
                    {
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, models[0].GroupId), this.GetType(), "Save");
                        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "SaveAll");
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Unique);
                        return response;
                    }
                }

                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "SaveAll");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "SaveAll");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }
    }
}
