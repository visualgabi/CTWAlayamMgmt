using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Mapper
{
    public static class DBEntityToModel
    {
        public static List<ExpenseModel> GetExpenseModels(List<ExpenseDBEntity> entities)
        {
            List<ExpenseModel> models = new List<ExpenseModel>();

            foreach (ExpenseDBEntity entity in entities)
            {
                models.Add(GetExpenseModel(entity));
            }

            return models;
        }

        public static ExpenseModel GetExpenseModel(ExpenseDBEntity entity)
        {
            return new ExpenseModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,                
                OrganizationId = entity.OrganizationId,
                Organization = entity.OrganizationId != null ? entity.Organization.Name : null,
                BranchId = entity.BranchId,
                Branch = entity.BranchId != null ? entity.Branch.Name : null,
                RowTimeStamp = entity.RowTimeStamp,
                Id = entity.Id,
                Amount = entity.Amount,
                Notes = entity.Notes,
                PaymentTypeId = entity.PaymentTypeId,
                PaymentType = entity.PaymentType.Name,
                TransactionNumber = entity.TransactionNumber,
                ExpenseType = entity.ExpenseType.Name,
                ExpenseTypeId = entity.ExpenseTypeId,
                SubExpenseType = entity.SubExpenseType.Name,
                SubExpenseTypeId = entity.SubExpenseTypeId,
                ExpenseDate = entity.ExpenseDate,
                AccountId = entity.AccountId,
                Account = entity.Account.Name
            };
        }


        public static List<OrgOfferingModel> GetOrgOfferingModels(List<OfferingDBEntity> entities)
        {
            List<OrgOfferingModel> models = new List<OrgOfferingModel>();

            foreach (OfferingDBEntity entity in entities)
            {
                models.Add(GetOrgOfferingModel(entity));
            }

            return models;
        }

        public static OrgOfferingModel GetOrgOfferingModel(OfferingDBEntity entity)
        {
            string intial = entity.FamilyMember != null ? entity.FamilyMember.Initial != null ? " " + entity.FamilyMember.Initial : string.Empty : string.Empty;
            string fullName = entity.FamilyMember != null ? entity.FamilyMember.FirstName + intial + " " + entity.FamilyMember.LastName : null;

            return new OrgOfferingModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                Organization = entity.Organization.Name,
                OrganizationId = entity.OrganizationId,
                RowTimeStamp = entity.RowTimeStamp,
                Id = entity.Id,
                Amount = entity.Amount,                
                FamilyMemberId = entity.FamilyMemberId,
                FamilyMember = fullName,
                SponsorId = entity.SponsorId,
                Sponsor = entity.SponsorId != null ?entity.Sponsor.Name : string.Empty,
                FundType = entity.FundType.Name,
                FundTypeId = entity.FundTypeId,
                OfferingDate = entity.OfferingDate,
                OfferingType = entity.OfferingType.Name,
                OfferingTypeId = entity.OfferingTypeId,
                Notes = entity.Notes,
                PaymentTypeId = entity.PaymentTypeId,
                PaymentType = entity.PaymentType.Name,
                TransactionNumber = entity.TransactionNumber
            };
        }


        public static List<UserModel> GetUserModels(List<UserDBEntity> users)
        {
            List<UserModel> models = new List<UserModel>();

            foreach (UserDBEntity user in users)
            {
                models.Add(GetUserModel(user)); ;
            }

            return models;

        }

        public static UserModel GetUserModel(UserDBEntity user)
        {
            if (user != null)
            {
                return new UserModel()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Active = user.Active,
                    RowTimeStamp = user.RowTimeStamp,
                    CreateDateTime = user.CreateDateTime,
                    CreateUser = user.CreateUser,
                    LastUpdateDateTime = user.LastUpdateDateTime,
                    LastUpdateUser = user.LastUpdateUser,
                    OrganizationId = user.UserRoles.First().OrganizationId != null ? user.UserRoles.First().OrganizationId : null,
                    RoleId = user.UserRoles.First().RoleId,
                    Organization = user.UserRoles.First().OrganizationId != null ? user.UserRoles.First().Organization.Name : string.Empty,
                    Role = user.UserRoles.First().Role.Name,
                    Currency = user.UserRoles.First().OrganizationId != null && user.UserRoles.First().Organization.Currency != null ? user.UserRoles.First().Organization.Currency.Name : "$"
                };
            }
            else
                return null;
        }

        public static List<RoleModel> GetRoleModels(List<RoleDBEntity> roles)
        {
            List<RoleModel> models = new List<RoleModel>();

            foreach (RoleDBEntity user in roles)
            {
                models.Add(GetRoleModel(user)); ;
            }

            return models;

        }

        public static RoleModel GetRoleModel(RoleDBEntity role)
        {
            return new RoleModel()
            {
                Name = role.Name,
                Id = role.Id,
                Active = role.Active,
                RowTimeStamp = role.RowTimeStamp,
                CreateDateTime = role.CreateDateTime,
                CreateUser = role.CreateUser,
                LastUpdateDateTime = role.LastUpdateDateTime,
                LastUpdateUser = role.LastUpdateUser              
                
            };
        }

    }
}