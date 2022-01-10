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
    [RoutePrefix("api/MarriageAnniversaryRemainder")]
    public class MarriageAnniversaryRemainderController : ApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetTodayMarriageAnniversaryFamiliesByOrgId")]
        public HttpResponseMessage GetTodayMarriageAnniversaryFamiliesByOrgId(int orgId)
        {
            HttpResponseMessage response;

            try
            {
                IMarriageAnniversaryRemainderBusiness marriageAnniversaryRemainderBusiness = new MarriageAnniversaryRemainderBusiness(Constant.DEFAULT_EMAIL);
                List<MarriageAnniversaryRemainderModel> depositBusinessModels = GetBirthdayRemainderModels(marriageAnniversaryRemainderBusiness.GetTodayMarriageAnniversaryFamilies(orgId));

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
        [Route("IsAnybodyMarriageAnniversarydayTodayByOrgId")]
        public HttpResponseMessage IsAnybodyMarriageAnniversarydayTodayByOrgId(int orgId)
        {
            HttpResponseMessage response;

            try
            {
                IMarriageAnniversaryRemainderBusiness marriageAnniversaryRemainderBusinessBusiness = new MarriageAnniversaryRemainderBusiness(Constant.DEFAULT_EMAIL);
                bool result = marriageAnniversaryRemainderBusinessBusiness.IsAnybodyMarriageAnniversarydayToday(orgId) > 0;

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

        private List<MarriageAnniversaryRemainderModel> GetBirthdayRemainderModels(List<MarriageAnniversaryRemainderDBEntity> entities)
        {
            List<MarriageAnniversaryRemainderModel> models = new List<MarriageAnniversaryRemainderModel>();

            entities.ForEach(x => models.Add(GetMarriageAnniversaryRemainderModel(x)));

            return models;
        }

        private MarriageAnniversaryRemainderModel GetMarriageAnniversaryRemainderModel(MarriageAnniversaryRemainderDBEntity entity)
        {          
            string user = string.Empty;

            if (entity.User != null)
                user = entity.User.FirstName + " " + entity.User.LastName;

            int enumHusband = (int)Relationship.Husband;
            int enumWife = (int)Relationship.Wife;

            string husbandName = string.Empty;
            string wifeName = string.Empty;

            FamilyMemberDBEntity familyDBEntity = entity.Family.FamilyMembers.Where(x => x.RelationshipId == enumHusband).FirstOrDefault();

            if (familyDBEntity != null)
                husbandName = familyDBEntity.FirstName + " " + familyDBEntity.FirstName;

            familyDBEntity = entity.Family.FamilyMembers.Where(x => x.RelationshipId == enumWife).FirstOrDefault();

            if (familyDBEntity != null)
                wifeName = familyDBEntity.FirstName + " " + familyDBEntity.FirstName;

            return new MarriageAnniversaryRemainderModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                RowTimeStamp = entity.RowTimeStamp,
                MarriageDate = entity.MarriageDate,
                Family = entity.Family.Name,
                FamilyId = entity.FamilyId,
                WishedDate = entity.WishedDate,
                WishedBy = entity.WishedBy,
                WishedStatus = entity.WishedStatus,
                User = user,
                Organization = entity.Organization.Name,
                OrganizationId = entity.OrganizationId,
                EmailId = entity.Family.EmailId1,
                Phone = entity.Family.Phone1,
                Address1 = entity.Family.Address1,
                Address2 = entity.Family.Address2,
                City = entity.Family.City,
                ZipCode = entity.Family.ZipCode,
                HusbandName = husbandName,
                WifeName = wifeName
            };
        }
    }
}
