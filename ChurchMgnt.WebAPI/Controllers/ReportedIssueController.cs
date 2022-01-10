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
    [RoutePrefix("api/ReportedIssue")]
    public class ReportedIssueController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("Save")]
        public HttpResponseMessage Save(ReportedIssueModel model)
        {
            HttpResponseMessage response;

            try
            {
                ClaimModel claimModel = GetClaimModel();

                IReportedIssueBusiness taxFilingBusiness = new ReportedIssueBusiness(claimModel.LoginUserEmail);
                taxFilingBusiness.Save(GetReportedIssueDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.Title.ToString()), this.GetType(), "Save");
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

        private ReportedIssueDBEntity GetReportedIssueDBEntity(ReportedIssueModel model)
        {
            return new ReportedIssueDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                RowTimeStamp = model.RowTimeStamp,
                Title = model.Title,
                Description = model.Title,
                OrganizationId = model.OrganizationId,
                IssueStatusId = model.IssueStatusId
            };
        }
    }
}
