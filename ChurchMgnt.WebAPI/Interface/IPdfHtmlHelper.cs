using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchMgnt.WebAPI.Interface
{
    interface IPdfHtmlHelper
    {
        StringBuilder GenerateBody(PrintBaseModel printBaseModel);
        StringBuilder GenerateFooter(PrintBaseModel printBaseModel);
    }
}
