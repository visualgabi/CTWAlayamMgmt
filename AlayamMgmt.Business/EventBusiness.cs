using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class EventBusiness : BaseBusiness<EventDBEntity>, IEventBusiness
    {
        public EventBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<EventDBEntity> GetBranchId(int branchId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.BranchId == branchId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.BranchId == branchId && x.Active == active.Value).ToList();
        }
        public List<EventDBEntity> GetOrgId(int orgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.Active == active.Value).ToList();
        }
       

        public List<EventDBEntity> Report(int orgId, DateTime eventStartDate, DateTime eventEndDate, int? eventType, bool? onlySplEvent, int? orderById)
        {
            DateTime beginDate = DateTime.Parse(eventStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(eventEndDate.ToShortDateString() + " 12 PM");

            if (orderById == null)
            {
                return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.EventDate >= beginDate && x.EventDate <= endDate && x.Active == true
                        && ((eventType != null && x.EventTypeId == eventType.Value) || (eventType == null))
                        && ((onlySplEvent != null && x.IsSpecialEvent == onlySplEvent.Value) || (onlySplEvent == null))
                    ).ToList();
            }
            else
            {
                switch (orderById)
                {
                    case 1:
                                return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.EventDate >= beginDate && x.EventDate <= endDate && x.Active == true
                               && ((eventType != null && x.EventTypeId == eventType.Value) || (eventType == null))
                               && ((onlySplEvent != null && x.IsSpecialEvent == onlySplEvent.Value) || (onlySplEvent == null))
                           ).OrderBy(y => y.Branch.Name).ToList();
                    case 2:
                                return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.EventDate >= beginDate && x.EventDate <= endDate && x.Active == true
                               && ((eventType != null && x.EventTypeId == eventType.Value) || (eventType == null))
                               && ((onlySplEvent != null && x.IsSpecialEvent == onlySplEvent.Value) || (onlySplEvent == null))
                           ).OrderBy(y => y.EventType.Name).ToList();
                    case 3:
                                    return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.EventDate >= beginDate && x.EventDate <= endDate && x.Active == true
                                   && ((eventType != null && x.EventTypeId == eventType.Value) || (eventType == null))
                                   && ((onlySplEvent != null && x.IsSpecialEvent == onlySplEvent.Value) || (onlySplEvent == null))
                               ).OrderBy(y => y.Offering).ToList();
                    case 4:
                                return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.EventDate >= beginDate && x.EventDate <= endDate && x.Active == true
                               && ((eventType != null && x.EventTypeId == eventType.Value) || (eventType == null))
                               && ((onlySplEvent != null && x.IsSpecialEvent == onlySplEvent.Value) || (onlySplEvent == null))
                           ).OrderBy(y => y.Expense).ToList();                    
                    default:
                                return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.EventDate >= beginDate && x.EventDate <= endDate && x.Active == true
                                && ((eventType != null && x.EventTypeId == eventType.Value) || (eventType == null))
                                && ((onlySplEvent != null && x.IsSpecialEvent == onlySplEvent.Value) || (onlySplEvent == null))
                            ).ToList();
                }               
            }
        }


        public List<EventDBEntity> GetRecentEventsByOrgId(int orgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.Branch.ParentId == orgId && x.Active == true).OrderByDescending(y => y.Id).Take(5).ToList();
        }
    }
}
