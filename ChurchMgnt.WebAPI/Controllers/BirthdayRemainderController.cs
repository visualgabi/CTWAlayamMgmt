using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/BirthdayRemainder")]
    public class BirthdayRemainderController : ApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetTodayBirthdayFamilyMembersByOrgId")]
        public HttpResponseMessage GetTodayBirthdayFamilyMembersByOrgId(int orgId)
        {
            HttpResponseMessage response;

            try
            {
                IBirthdayRemainderBusiness depositBusiness = new BirthdayRemainderBusiness(Constant.DEFAULT_EMAIL);
                List<BirthdayRemainderModel> depositBusinessModels = GetBirthdayRemainderModels(depositBusiness.GetTodayBirthdayFamilyMembers(orgId));

                response = Request.CreateResponse(HttpStatusCode.OK, depositBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByBranchId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("IsBirthdayRemainderAvailableByOrgId")]
        public HttpResponseMessage IsBirthdayRemainderAvailableByOrgId(int orgId)
        {
            HttpResponseMessage response;

            try
            {
                IBirthdayRemainderBusiness depositBusiness = new BirthdayRemainderBusiness(Constant.DEFAULT_EMAIL);
                bool result = depositBusiness.IsAnybodyBirthdayToday(orgId) > 0 ;

                response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByBranchId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        private List<BirthdayRemainderModel> GetBirthdayRemainderModels(List<BirthdayRemainderDBEntity> entities)
        {
            List<BirthdayRemainderModel> models = new List<BirthdayRemainderModel>();

            entities.ForEach(x => models.Add(GetBirthdayRemainderModel(x)));

            return models;
        }       

        private BirthdayRemainderModel GetBirthdayRemainderModel(BirthdayRemainderDBEntity entity)
        {
            string intial = entity.FamilyMember.Initial != null ? " " + entity.FamilyMember.Initial : string.Empty;            

            string fullName = entity.FamilyMember.FirstName + intial + " " + entity.FamilyMember.LastName;

            string emailid = string.Empty;
            string phone = string.Empty;

            if (string.IsNullOrEmpty(entity.FamilyMember.EmailId1))
                emailid = entity.FamilyMember.Family.EmailId1;
            else
                emailid = entity.FamilyMember.EmailId1;

            if (string.IsNullOrEmpty(entity.FamilyMember.Phone1))
                phone = entity.FamilyMember.Family.Phone1;
            else
                phone = entity.FamilyMember.Phone1;

            string user = string.Empty;

            if (entity.User != null)
                user = entity.User.FirstName + " " + entity.User.LastName;

            return new BirthdayRemainderModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,                
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,          
                RowTimeStamp = entity.RowTimeStamp,
                BirthDate = entity.BirthDate,
                FamilyMember = fullName,
                MemberId = entity.MemberId,
                WishedDate = entity.WishedDate,
                WishedBy = entity.WishedBy,
                WishedStatus = entity.WishedStatus,
                User = user,
                Organization = entity.Organization.Name,
                OrganizationId = entity.OrganizationId,
                EmailId = emailid,
                Phone = phone,
                Address1 = entity.FamilyMember.Family.Address1,
                Address2 = entity.FamilyMember.Family.Address2,
                City = entity.FamilyMember.Family.City,
                ZipCode = entity.FamilyMember.Family.ZipCode
            };
        }
    }
}
