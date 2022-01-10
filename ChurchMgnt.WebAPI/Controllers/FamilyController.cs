using AlayamMgmt.Business;
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Family")]
    public class FamilyController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("Get")]
        [HttpGet]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IBusiness<FamilyDBEntity> familyBusiness = new BaseBusiness<FamilyDBEntity>(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.Get(active),false, false);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetFamiliesByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetFamiliesByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.GetFamiliesByOrgId(orgId, active),false,false);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetFamiliesIncludeMembersByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetFamiliesIncludeMembersByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.GetFamiliesIncludeMembersByOrgId(orgId, active), true, false);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetFamiliesIncludeMembersAndGroupByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetFamiliesIncludeMembersAndGroupByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.GetFamiliesIncludeMembersAndGroupsByOrgId(orgId, active), true, true);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetRecentMembersByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetRecentMembersByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.GetRecentMembersByOrgId(orgId),false, false);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentMembers");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetMembers")]
        [HttpGet]
        public HttpResponseMessage GetMembers(int orgId, DateTime? membershipBeginDate, DateTime? membershipEndDate)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.GetMembers(orgId, membershipBeginDate, membershipEndDate), false, false);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetMembers");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetVisitors")]
        [HttpGet]
        public HttpResponseMessage GetVisitors(int orgId, DateTime? visitorBeginDate, DateTime? visitorEndDate)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.GetVisitors(orgId, visitorBeginDate, visitorEndDate),false, false);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetRecentVisitorsByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetRecentVisitorsByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                List<FamilyModel> familyModels = GetFamilyModels(familyBusiness.GetRecentVisitorsByOrgId(orgId),false,false);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetMembersAndVisitorsForLineChart")]
        [HttpGet]
        public HttpResponseMessage GetMembersAndVisitorsForLineChart(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

                //IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
                IFamilyBusiness familyBusiness = new FamilyBusiness(Constant.DEFAULT_EMAIL);
                List<FamilyDBEntity> visitors = familyBusiness.GetOnThisYearVisitors(orgId);
                List<FamilyDBEntity> members = familyBusiness.GetOnThisYearMembers(orgId);

                MemberVisitoLineChartModel model = GetMembersAndVisitorsLineChartModel(visitors, members);

                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IBusiness<FamilyDBEntity> familyBusiness = new BaseBusiness<FamilyDBEntity>(claimModel.LoginUserEmail);
                FamilyModel familyModel = GetFamilyModel(familyBusiness.GetById(id),true, true);

                response = Request.CreateResponse(HttpStatusCode.OK, familyModel);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        
        [Route("GetBasicDataById")]
        [HttpGet]
        public HttpResponseMessage GetBasicDataById(int id)
        {
            HttpResponseMessage response;
            try
            {
                IBusiness<FamilyDBEntity> familyBusiness = new BaseBusiness<FamilyDBEntity>(Constant.DEFAULT_EMAIL);
                FamilyBasicModel familyModel = GetFamilyBasicModel(familyBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, familyModel);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }       


        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]FamilyModel model)
        {
            HttpResponseMessage response;

            try
            {
                ClaimModel claimModel = GetClaimModel();

                IBusiness<FamilyDBEntity> familyBusiness = new BaseBusiness<FamilyDBEntity>(claimModel.LoginUserEmail);
                familyBusiness.Save(GetFamilyDBEntity(model));

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Save");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.Name), this.GetType(), "Save");
                        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Unique);
                        return response;
                    }
                }

                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }

        }

        [CustomAuthorizeAttribute]
        [Route("Enable")]
        [HttpPost]
        public HttpResponseMessage Enable([FromBody] FamilyModel enableModel)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IBusiness<FamilyDBEntity> familyBusiness = new BaseBusiness<FamilyDBEntity>(claimModel.LoginUserEmail);
                byte[] returnValue = familyBusiness.Enable(enableModel.Id, enableModel.Active);

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

        private List<FamilyModel> GetFamilyModels(List<FamilyDBEntity> familyDBEntities, bool includeFamilyMember, bool includeFamilyMemberGroup)
        {
            List<FamilyModel> familyModels = new List<FamilyModel>();

            foreach (FamilyDBEntity x in familyDBEntities)
            {
                familyModels.Add(GetFamilyModel(x, includeFamilyMember, includeFamilyMemberGroup));
            };

            return familyModels;

        }

        private FamilyModel GetFamilyModel(FamilyDBEntity familyDBEntity, bool includeFamilyMember, bool includeFamilyMemberGroup)
        {
            
            FamilyModel model = new FamilyModel()
            {
                Active = familyDBEntity.Active,
                Address1 = familyDBEntity.Address1,
                Address2 = familyDBEntity.Address2,
                Address3 = familyDBEntity.Address3,
                City = familyDBEntity.City,
                Country = familyDBEntity.Country.Name,
                CountryId = familyDBEntity.CountryId,
                CreateDateTime = familyDBEntity.CreateDateTime,
                CreateUser = familyDBEntity.CreateUser,                
                EmailId1 = familyDBEntity.EmailId1,
                EmailId2 = familyDBEntity.EmailId2,
                EthnicOrigin = familyDBEntity.EthnicOrigin.Name,
                EthnicOriginId = familyDBEntity.EthnicOriginId,
                Id = familyDBEntity.Id,
                LastUpdateDateTime = familyDBEntity.LastUpdateDateTime,
                LastUpdateUser = familyDBEntity.LastUpdateUser,
                Name = familyDBEntity.Name,
                Phone1 = familyDBEntity.Phone1,
                Phone2 = familyDBEntity.Phone2,
                PrimaryLanguage = familyDBEntity.PrimaryLanguage.Name,
                PrimaryLanguageId = familyDBEntity.PrimaryLanguageId,
                SecondaryLanguage = familyDBEntity.SecondaryLanguage.Name,
                SecondaryLanguageId = familyDBEntity.SecondaryLanguageId,
                State = familyDBEntity.State.Name,
                StateId = familyDBEntity.StateId,                
                ZipCode = familyDBEntity.ZipCode,    
                FirstVisitedDate = familyDBEntity.FirstVisitedDate,
                MembershipStartDate = familyDBEntity.MembershipStartDate,
                MembershipStatusId = familyDBEntity.MembershipStatusId,
                MembershipStatus = familyDBEntity.MembershipStatus.Name,
                BranchId = familyDBEntity.BranchId,
                Branch = familyDBEntity.Branch.Name,                
                RowTimeStamp = familyDBEntity.RowTimeStamp,               
                MariageDate = familyDBEntity.MariageDate
            };

            if (includeFamilyMember == true)
                model.FamilyMembers = GetFamilyMemberModels(familyDBEntity.FamilyMembers, includeFamilyMemberGroup);
            else
                model.FamilyMembers = new List<FamilyMemberModel>();

            return model;
        }      

        

        private FamilyBasicModel GetFamilyBasicModel(FamilyDBEntity familyDBEntity)
        {
            return new FamilyBasicModel()
            {                
                Address1 = familyDBEntity.Address1,
                City = familyDBEntity.City,
                Country = familyDBEntity.Country.Name,                
                EmailId1 = familyDBEntity.EmailId1,                
                Name = familyDBEntity.Name,
                Phone1 = familyDBEntity.Phone1,                
                State = familyDBEntity.State.Name,                
                ZipCode = familyDBEntity.ZipCode,
                Id = familyDBEntity.Id

            };
        }

        private MemberVisitoLineChartModel GetMembersAndVisitorsLineChartModel(List<FamilyDBEntity> visitorEntities, List<FamilyDBEntity> memberEntities)
        {
            MemberVisitoLineChartModel model = new MemberVisitoLineChartModel();

            for (int iMonth = 1; iMonth <= DateTime.Now.Month; iMonth++ )
            {
                model.Labels.Add(DateTimeHelper.MonthName(iMonth));
                model.Visitors.Add(new LineChartModel() { Month = DateTimeHelper.MonthName(iMonth), Count = 0 });
                model.Members.Add(new LineChartModel() { Month = DateTimeHelper.MonthName(iMonth), Count = 0 });
            }

            model.Series.Add("Members");
            model.Series.Add("Visitors");

            List<LineChartModel> visitorsGroupByMonth = visitorEntities.GroupBy(x => x.FirstVisitedDate.Month)
                .Select(y => new LineChartModel { Month = DateTimeHelper.MonthName(y.Key), Count= y.Count() }).
                OrderBy( j => j.Month).ToList<LineChartModel>();

            List<LineChartModel> membersGroupByMonth = memberEntities.GroupBy(x => x.MembershipStartDate.Value.Month)
              .Select(y => new LineChartModel { Month = DateTimeHelper.MonthName(y.Key), Count = y.Count() }).
              OrderBy(j => j.Month).ToList<LineChartModel>();

            foreach (LineChartModel visitorGroupByMonth in visitorsGroupByMonth)
            {
                if (model.Visitors.Where(x => x.Month == visitorGroupByMonth.Month).Count() > 0)
                    model.Visitors.Where(x => x.Month == visitorGroupByMonth.Month).First().Count = visitorGroupByMonth.Count;
            }

            foreach (LineChartModel memberGroupByMonth in membersGroupByMonth)
            {
                if (model.Members.Where(x => x.Month == memberGroupByMonth.Month).Count() > 0)
                    model.Members.Where(x => x.Month == memberGroupByMonth.Month).First().Count = memberGroupByMonth.Count;
            }

            //foreach(string month in model.Labels)
            //{
            //    if (model.Visitors.Where(x => x.Month == month).Count() == 1)
            //        model.Visitors.Add(new LineChartModel() { Month = month, Count = 0 });

            //    if (model.Members.Where(x => x.Month == month).Count() == 0)
            //        model.Members.Add(new LineChartModel() { Month = month, Count = 0 });
            //}


            for (int iMonth = 1; iMonth <= 12; iMonth++)
            {
                if (model.Labels.Where(x => x == DateTimeHelper.MonthName(iMonth)).Count() == 0)
                    model.Labels.Add(DateTimeHelper.MonthName(iMonth));
            }

            model.MemberCount = model.Members.Select(x => x.Count).ToList<int>();
            model.VisitorCount = model.Visitors.Select(x => x.Count).ToList<int>();

            //model.CntData.MemberCount = model.MemberCount;
            //model.CntData.VisitorCount = model.VisitorCount;

            //model.MemberAndVisitorCount = new List<int>() { 100, 200, 300, 400, 6000 };
    //];

            return model;
        }
        private FamilyDBEntity GetFamilyDBEntity(FamilyModel familyModel)
        {
            return new FamilyDBEntity()
            {
                Active = familyModel.Active,
                Address1 = familyModel.Address1,
                Address2 = familyModel.Address2,
                Address3 = familyModel.Address3,
                City = familyModel.City,                
                CountryId = familyModel.CountryId,
                CreateDateTime = familyModel.CreateDateTime,
                CreateUser = familyModel.CreateUser,
                EmailId1 = familyModel.EmailId1,
                EmailId2 = familyModel.EmailId2,                
                EthnicOriginId = familyModel.EthnicOriginId,
                Id = familyModel.Id,
                LastUpdateDateTime = familyModel.LastUpdateDateTime,
                LastUpdateUser = familyModel.LastUpdateUser,
                Name = familyModel.Name,
                Phone1 = familyModel.Phone1,
                Phone2 = familyModel.Phone2,                
                PrimaryLanguageId = familyModel.PrimaryLanguageId,                
                SecondaryLanguageId = familyModel.SecondaryLanguageId,                
                StateId = familyModel.StateId,
                ZipCode = familyModel.ZipCode,
                FirstVisitedDate = familyModel.FirstVisitedDate,
                MembershipStartDate = familyModel.MembershipStartDate,
                MembershipStatusId = familyModel.MembershipStatusId,
                BranchId = familyModel.BranchId,                
                RowTimeStamp = familyModel.RowTimeStamp,
                MariageDate = familyModel.MariageDate
            };
        }
    }

    
}

