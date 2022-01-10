﻿using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IOrgSMTPDetailsBusiness : IBusiness<SMTPSettingDBEntity>
    {
        List<SMTPSettingDBEntity> GetOrgId(int OrgId, bool? active);
    }
}

