using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common
{
    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    };

    public enum Level
    {
        Debug,
        Information,
        Warnings,
        Error,
        Fatal
    }

    public enum ExceptionType
    {
        InternalServer=1,
        Unique,
        Concurrency        
    }

    public enum Role
    {
        Admin = 1,
        Pastor = 2,
        FinanceOfficer = 3,
        BusinessCoordinator = 4,
        LeadPastor = 5
    }

    public enum MessageType
    {
        ContributionMsg = 1,
        ContributionEmailMsg = 2,
        AccountCreation = 3,
        PasswordReset = 4,
        ChangePassword = 5,
        AccountModification = 6
    }

    public enum PrintReportType
    {
        Contribution = 1,
        Expense = 2,
        Offering = 3,
        FamilyPledge = 4
    }

    public enum EmailGroup
    {
        OrganizationGroup = 1,
        BranchGroup = 2,
        CustomizedGroup = 3,
        AddressToGroup = 4
    }

    public enum BuildInEmailGroupType
    {
        Family = 1,
        Mens = 2,
        Womens = 3
    }

    public enum EntityType
    {
        FamilyMember = 1,
        Family = 2
    }

    public enum Relationship
    {
        Husband = 1,
        Wife = 2,
        Baby = 3,
        Kid = 4,
        Youth = 5,
        Adult = 6
    }

}
