using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Models;
using AlayamMgmt.Web.Providers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
     [RoutePrefix("api/SubExpenseType")]
    public class SubExpenseTypeController : ApiController
    {
         [CustomAuthorizeAttribute]
         [Route("")]
         public HttpResponseMessage Get(bool? active)
         {
             HttpResponseMessage response;

             try
             {
                 ISubExpenseTypeBusiness subExpenseTypeBusiness = new SubExpenseTypeBusiness(Constant.DEFAULT_EMAIL);
                 List<SubExpenseTypeModel> subExpenseTypeModels = toSubExpenseTypeModels(subExpenseTypeBusiness.Get(active));
                                  
                 response = Request.CreateResponse(HttpStatusCode.OK, subExpenseTypeModels);
                 return response;
             }
             catch (Exception ex)
             {
                 LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                 response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                 return response;
             }

         }

         //[CustomAuthorizeAttribute]
         [CustomAuthorizeAttribute]
         [Route("GetByOrgId")]
         public HttpResponseMessage GetByOrgId(int OrgId, bool? active)
         {
             HttpResponseMessage response;

             try
             {
                 ISubExpenseTypeBusiness subExpenseTypeBusiness = new SubExpenseTypeBusiness(Constant.DEFAULT_EMAIL);
                 List<SubExpenseTypeModel> expenseTypeBusinessModels = toSubExpenseTypeModels(subExpenseTypeBusiness.GetOrgId(OrgId, active));

                 response = Request.CreateResponse(HttpStatusCode.OK, expenseTypeBusinessModels);
                 return response;

             }
             catch (Exception ex)
             {
                 LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetOrgId");
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
                 ISubExpenseTypeBusiness subExpenseTypeBusiness = new SubExpenseTypeBusiness(Constant.DEFAULT_EMAIL);
                 byte[] returnValue = subExpenseTypeBusiness.Enable(enableModel.Id, enableModel.Active);

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
         public HttpResponseMessage Save(SubExpenseTypeModel model)
         {
             HttpResponseMessage response;

             try
             {
                 ISubExpenseTypeBusiness subExpenseTypeBusiness = new SubExpenseTypeBusiness(Constant.DEFAULT_EMAIL);
                 subExpenseTypeBusiness.Save(toSubExpenseTypeDBEntity(model));

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


         private SubExpenseTypeDBEntity toSubExpenseTypeDBEntity(SubExpenseTypeModel model)
         {
             return new SubExpenseTypeDBEntity()
             {
                 Active = model.Active,
                 CreateDateTime = model.CreateDateTime,
                 CreateUser = model.CreateUser,
                 Description = model.Description,
                 Id = model.Id,
                 LastUpdateDateTime = model.LastUpdateDateTime,
                 LastUpdateUser = model.LastUpdateUser,
                 Name = model.Name,
                 ExpenseTypeId = model.ExpenseTypeId,
                 RowTimeStamp = model.RowTimeStamp
             };
             
         }


         private List<SubExpenseTypeModel> toSubExpenseTypeModels(List<SubExpenseTypeDBEntity> entities)
         {
             List<SubExpenseTypeModel> models = new List<SubExpenseTypeModel>();

             foreach (SubExpenseTypeDBEntity entity in entities)
             {
                     models.Add(new SubExpenseTypeModel()
                     {
                         Active = entity.Active,
                         CreateDateTime = entity.CreateDateTime,
                         CreateUser = entity.CreateUser,
                         Description = entity.Description,
                         Id = entity.Id,
                         LastUpdateDateTime = entity.LastUpdateDateTime,
                         LastUpdateUser = entity.LastUpdateUser,
                         Name = entity.Name,
                         ExpenseTypeId = entity.ExpenseTypeId,
                         ExpenseType = entity.ExpenseType.Name,
                         Organization = entity.ExpenseType.Organization.Name,
                         OrganizationId = entity.ExpenseType.OrganizationId,
                         RowTimeStamp = entity.RowTimeStamp
                     }
                );

             }

             return models;
         }
    }
}
