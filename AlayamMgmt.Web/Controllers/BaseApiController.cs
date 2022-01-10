using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using AlayamMgmt.Web.Models;
using AlayamMgmt.Common.DBEntity;
using System.Security.Claims;

namespace AlayamMgmt.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        //private ApplicationUserManager _AppUserManager = null;

        //protected ApplicationUserManager AppUserManager
        //{
        //    get
        //    {
        //        return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //}

        protected ClaimModel GetClaimModel()
        {            
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            //if (principal != null)
            //{
                string role = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Single().Value;
                string loginUserEmail = principal.Claims.Where(c => c.Type == ClaimTypes.Name).Single().Value;
                if( role != "1")
                {
                    int? orgId = principal.Claims.Where(c => c.Type == "OrgId") == null ? (int?)null : int.Parse(principal.Claims.Where(c => c.Type == "OrgId").Single().Value);
                    return new ClaimModel() { LoginUserEmail = loginUserEmail, OrgId = orgId, Role = role };
                }                    
            else
                return new ClaimModel() { LoginUserEmail = loginUserEmail, OrgId = null, Role = role };
            //}
            //else
             //   return null;
        }

        protected List<LookupModel> GetLookModel<T>(List<T> Entities) where T : LookupDBEntity
        {
            List<LookupModel> models = new List<LookupModel>();

            foreach (LookupDBEntity entity in Entities)
            {
                models.Add(new LookupModel()
                {
                    Active = entity.Active,
                    CreateDateTime = entity.CreateDateTime,
                    CreateUser = entity.CreateUser,
                    Description = entity.Description,
                    Id = entity.Id,
                    LastUpdateDateTime = entity.LastUpdateDateTime,
                    LastUpdateUser = entity.LastUpdateUser,
                    Name = entity.Name,
                    RowTimeStamp = entity.RowTimeStamp
                }
                        );

            }

           // models.Add(new LookupModel() { Id = -1, Name = "Select Country" });

            return models;
        }

        protected OrganizationLookupModel GetOrganizationLookModel<T>(T entity) where T : OrganizationLookupDBEntity
        {
           return new OrganizationLookupModel()
                {
                    Active = entity.Active,
                    CreateDateTime = entity.CreateDateTime,
                    CreateUser = entity.CreateUser,
                    Description = entity.Description,
                    Id = entity.Id,
                    LastUpdateDateTime = entity.LastUpdateDateTime,
                    LastUpdateUser = entity.LastUpdateUser,
                    Name = entity.Name,
                    OrganizationId = entity.OrganizationId,
                    Organization = entity.Organization.Name,
                    RowTimeStamp = entity.RowTimeStamp
                };              
        }

        protected List<OrganizationLookupModel> GetOrganizationLookModels<T>(List<T> Entities) where T : OrganizationLookupDBEntity
        {
            List<OrganizationLookupModel> models = new List<OrganizationLookupModel>();

            foreach (OrganizationLookupDBEntity entity in Entities)
            {
                models.Add(new OrganizationLookupModel()
                    {
                        Active = entity.Active,
                        CreateDateTime = entity.CreateDateTime,
                        CreateUser = entity.CreateUser,
                        Description = entity.Description,
                        Id = entity.Id,
                        LastUpdateDateTime = entity.LastUpdateDateTime,
                        LastUpdateUser = entity.LastUpdateUser,
                        Name = entity.Name,
                        OrganizationId = entity.OrganizationId,
                        Organization = entity.Organization.Name,
                        RowTimeStamp = entity.RowTimeStamp
                    }
                );

            }

            // models.Add(new LookupModel() { Id = -1, Name = "Select Country" });

            return models;
        }

        protected T GetOrganizationLookDBEntity<T>(OrganizationLookupModel model) where T : OrganizationLookupDBEntity
        {
           return (T) new OrganizationLookupDBEntity()
                {
                    Active = model.Active,
                    CreateDateTime = model.CreateDateTime,
                    CreateUser = model.CreateUser,
                    Description = model.Description,
                    Id = model.Id,
                    LastUpdateDateTime = model.LastUpdateDateTime,
                    LastUpdateUser = model.LastUpdateUser,
                    Name = model.Name,
                    OrganizationId = model.OrganizationId,
                    RowTimeStamp = model.RowTimeStamp
                };
        } 



        protected List<FamilyMemberModel> GetFamilyMemberModels(List<FamilyMemberDBEntity> familyMemberDBEntities)
        {
            List<FamilyMemberModel> familyModels = new List<FamilyMemberModel>();

            foreach (FamilyMemberDBEntity x in familyMemberDBEntities)
            {
                familyModels.Add(GetFamilyMemberModel(x));
            };

            return familyModels;

        }

        protected FamilyMemberModel GetFamilyMemberModel(FamilyMemberDBEntity familyMemberDBEntity)
        {
            string intial = familyMemberDBEntity.Initial != null  ? " " + familyMemberDBEntity.Initial : string.Empty;
            string fullName = familyMemberDBEntity.FirstName + intial + " " + familyMemberDBEntity.LastName;

            return new FamilyMemberModel()
            {
                Active = familyMemberDBEntity.Active,
                DOB = familyMemberDBEntity.DOB,
                FamilyId = familyMemberDBEntity.FamilyId,
                FirstName = familyMemberDBEntity.FirstName,
                LastName = familyMemberDBEntity.LastName,
                MiddleName = familyMemberDBEntity.MiddleName,
                Initial = familyMemberDBEntity.Initial,
                Gender = familyMemberDBEntity.Gender,                
                CreateDateTime = familyMemberDBEntity.CreateDateTime,
                CreateUser = familyMemberDBEntity.CreateUser,                
                Id = familyMemberDBEntity.Id,
                LastUpdateDateTime = familyMemberDBEntity.LastUpdateDateTime,
                LastUpdateUser = familyMemberDBEntity.LastUpdateUser,                
                RowTimeStamp = familyMemberDBEntity.RowTimeStamp,
                Phone1 = familyMemberDBEntity.Phone1,
                EmailId1 = familyMemberDBEntity.EmailId1,
                Notes = familyMemberDBEntity.Notes,
                FullName = fullName,
                IsTaxPayer = familyMemberDBEntity.IsTaxPayer,
                RelationshipId = familyMemberDBEntity.RelationshipId,
                Relationship = familyMemberDBEntity.Relationship.Name
            };
        }

        protected FamilyMemberDBEntity GetFamilyMemberDBEntity(FamilyMemberModel familyMemberModel)
        {
            return new FamilyMemberDBEntity()
            {
                Active = familyMemberModel.Active,
                DOB = familyMemberModel.DOB,
                FamilyId = familyMemberModel.FamilyId,
                FirstName = familyMemberModel.FirstName,
                LastName = familyMemberModel.LastName,
                MiddleName = familyMemberModel.MiddleName,
                Initial  = familyMemberModel.Initial,
                Gender = familyMemberModel.Gender,                
                CreateDateTime = familyMemberModel.CreateDateTime,
                CreateUser = familyMemberModel.CreateUser,                
                Id = familyMemberModel.Id,
                LastUpdateDateTime = familyMemberModel.LastUpdateDateTime,
                LastUpdateUser = familyMemberModel.LastUpdateUser,                
                RowTimeStamp = familyMemberModel.RowTimeStamp,
                Phone1 = familyMemberModel.Phone1,
                EmailId1 = familyMemberModel.EmailId1,
                Notes = familyMemberModel.Notes,
                IsTaxPayer = familyMemberModel.IsTaxPayer,
                RelationshipId = familyMemberModel.RelationshipId
            };
        }   

    }

    public class ClaimModel
    {
        public string Role { get; set; }

        public int? OrgId { get; set; }

        public string LoginUserEmail { get; set; }
    }
}
