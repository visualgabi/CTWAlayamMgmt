using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common
{
    public static class Constant
    {
        public const string DEFAULT_EMAIL = "default@dyil.com";

        public const string PRODUCT_ADMIN = "Product Admin";
        public const string LANGUAGE_LOOKUPCODE = "Language";
        public const string COUNTRY_LOOKUPCODE = "Country";
        public const string DENOMINATION_LOOKUPCODE = "Denomination";
        public const string ETHNICORIGIN_LOOKUPCODE = "EthnicOrigin";
        public const string EMAIL_USERCREATION_LOOKUPCODE = "User Creation";
        public const string EMAIL_PASSWORDRESET_LOOKUPCODE = "Reset Password by Admin";
        public const string EMAIL_FORGOTPASSWORD_LOOKUPCODE = "Forgot Password";

        public const string PASSWORD_RESET_EMAIL_SUBJECT = "Norahsoft ChurchMgnt application login password got reseted successfully";
        public const string ACCOUNT_CREATION_EMAIL_SUBJECT = "Norahsoft ChurchMgnt application user account got created successfully";
        public const string ACCOUNT_MODIFICATION_EMAIL_SUBJECT = "Norahsoft ChurchMgnt application user account got modified successfully";
        public const string CHANGE_PASSWORD_EMAIL_SUBJECT = "Norahsoft ChurchMgnt application login password got changed successfully";


        public const string DEFAULT_EXCEPTION_MSG = @"The application has encountered an unknown error.
                It doesn't appear to have affected your data, but our technical staff have been automatically notified and will be looking into this with the utmost urgency.";

        public const string DEFAULT_CONFLICT_EXCEPTION_MSG = @"The record you are working on has been modified by another user. The new values for this record are shown below. Changes you have made have not been saved, please resubmit.";

        public const string DEFAULT_UNIQUE_EXCEPTION_MSG = @"The application unable to save record due to unique contraint error violated, The {0} name available in the application";

        public const string FAMILY_PLEDGE_HTML = @"<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
<head>
   <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <!-- Page title set in pageTitle directive -->
    <title page-title></title>
    <link rel='stylesheet' href='app/content/css/bootstrap.min.css' />        
    <link rel='stylesheet' href='app/content/css/font-awesome.min.css' />    
    <link rel='stylesheet' href='app/content/css/style.css' />
</head >
<body><div class='col-md-12'><div class='row'>
<div class='col-md-12'>&nbsp;</div>                    
                    <div class='col-md-12'>
                        <table style='width:100%'><tr><td style='text-align: left'><b>[orgName]</b> Family Pledge Report</td>
                            <td style='text-align: right'>Page : 1 of [totalPage]</td></tr>                    
                    </table>  
                </div>            
<div class='col-md-12'><hr /></div>
<div class='col-md-12'>&nbsp;</div>
            <div class='col-md-12'> 
                     <div class='col-md-3'>
                            &nbsp;
                        </div>
                        <div class='col-md-6'>
                                    <div class='panel panel-default height'>
                                        <div class='panel-heading'>Family report generated with below contidational data</div>
                                        <div class='panel-body'>
                                            [filter]
                                        </div>
                            </div>
                        </div>
                        <div class='col-md-3'>
                            &nbsp;
                        </div>
                    </div>
                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary</div>
                            <div class='panel-body'>
                                <table class='table table-bordered table-striped'>
                                    <tr>
                                        <td>Total offering in [currency] :</td>
                                        <td>[totalOffering]</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>                   
                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by payment type</div>
                            <div class='panel-body'>
                                [summaryByPaymentType]
                            </div>
                        </div>
                    </div>

                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by fund type</div>
                            <div class='panel-body'>
                                [summaryByFundType]
                            </div>
                        </div>
                    </div>

                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by offering type</div>
                            <div class='panel-body'>
                               [summaryByOfferingType]
                            </div>
                        </div>
                    </div>
                </div>
                <div class='row'>
                    <div class='col-md-12'>
                        &nbsp;
                    </div>                    
                </div>
               [offerings]
                <div class='col-md-12'>
                    &nbsp;
                </div>
                <div class='row'>
                    <div class='form-group'>
                        <div class='col-md-12'>
                            &nbsp;
                        </div>
                    </div>
                </div></div></body>
</html>";


        public const string OFFERING_HTML = @"<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
<head>
   <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>

    <!-- Page title set in pageTitle directive -->
    <title page-title></title>

    <link rel='stylesheet' href='app/content/css/bootstrap.min.css' />        
    <link rel='stylesheet' href='app/content/css/font-awesome.min.css' />    
    <link rel='stylesheet' href='app/content/css/style.css' />
</head >
<body><div class='col-md-12'><div class='row'>
<div class='col-md-12'>&nbsp;</div>                    
                    <div class='col-md-12'>
                        <table style='width:100%'><tr><td style='text-align: left'><b>[orgName]</b> Offering Report</td>
                            <td style='text-align: right'>Page : 1 of [totalPage]</td></tr>                    
                    </table>  
                </div>            
<div class='col-md-12'><hr /></div>
<div class='col-md-12'>&nbsp;</div>
            <div class='col-md-12'> 
                     <div class='col-md-3'>
                            &nbsp;
                        </div>
                        <div class='col-md-6'>
                                    <div class='panel panel-default height'>
                                        <div class='panel-heading'>Offering report generated with below contidational data</div>
                                        <div class='panel-body'>
                                            [filter]
                                        </div>
                            </div>
                        </div>
                        <div class='col-md-3'>
                            &nbsp;
                        </div>
                    </div>
                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-4'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary</div>
                            <div class='panel-body'>
                                <table class='table table-bordered table-striped'>
                                    <tr>
                                        <td>Total offering in [currency] :</td>
                                        <td>[totalOffering]</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-4'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by source type</div>
                            <div class='panel-body'>
                                [summaryBySourceType]
                            </div>
                        </div>
                    </div>
                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-4'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by payment type</div>
                            <div class='panel-body'>
                                [summaryByPaymentType]
                            </div>
                        </div>
                    </div>

                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by fund type</div>
                            <div class='panel-body'>
                                [summaryByFundType]
                            </div>
                        </div>
                    </div>

                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by offering type</div>
                            <div class='panel-body'>
                               [summaryByOfferingType]
                            </div>
                        </div>
                    </div>
                </div>
                <div class='row'>
                    <div class='col-md-12'>
                        &nbsp;
                    </div>                    
                </div>
               [offerings]
                <div class='col-md-12'>
                    &nbsp;
                </div>
                <div class='row'>
                    <div class='form-group'>
                        <div class='col-md-12'>
                            &nbsp;
                        </div>
                    </div>
                </div></div></body>
</html>";

        public const string EXPENSE_HTML = @"<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
<head>
   <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>

    <!-- Page title set in pageTitle directive -->
    <title page-title></title>

    <link rel='stylesheet' href='app/content/css/bootstrap.min.css' />        
    <link rel='stylesheet' href='app/content/css/font-awesome.min.css' />    
    <link rel='stylesheet' href='app/content/css/style.css' />
</head >
<body><div class='col-md-12'><div class='row'>
<div class='col-md-12'>&nbsp;</div>                    
                    <div class='col-md-12'>
                        <table style='width:100%'><tr><td style='text-align: left'><b>[orgName]</b> Expense Report</td>
                            <td style='text-align: right'>Page : 1 of [totalPage]</td></tr>                    
                    </table>  
                </div>            
<div class='col-md-12'><hr /></div>
<div class='col-md-12'>&nbsp;</div>
            <div class='col-md-12'> 
                     <div class='col-md-3'>
                            &nbsp;
                        </div>
                        <div class='col-md-6'>
                                    <div class='panel panel-default height'>
                                        <div class='panel-heading'>Expense report generated with below contidational data</div>
                                        <div class='panel-body'>
                                            [filter]
                                        </div>
                            </div>
                        </div>
                        <div class='col-md-3'>
                            &nbsp;
                        </div>
                    </div>
                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary</div>
                            <div class='panel-body'>
                                <table class='table table-bordered table-striped'>
                                    <tr>
                                        <td>Total Expense in [currency] :</td>
                                        <td>[totalExpense]</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by Payment Type</div>
                            <div class='panel-body'>
                                [summaryByPaymentType]
                            </div>
                        </div>
                    </div>

                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by Account</div>
                            <div class='panel-body'>
                                [summaryByAccount]
                            </div>
                        </div>
                    </div>

                    <div class='col-xs-12 col-sm-6 col-md-6 col-lg-6'>
                        <div class='panel panel-default height'>
                            <div class='panel-heading'>Summary by Expense Type</div>
                            <div class='panel-body'>
                               [summaryByExpenseType]
                            </div>
                        </div>
                    </div>
                </div>
                <div class='row'>
                    <div class='col-md-12'>
                        &nbsp;
                    </div>                    
                </div>
               [expenses]
                <div class='col-md-12'>
                    &nbsp;
                </div>
                <div class='row'>
                    <div class='form-group'>
                        <div class='col-md-12'>
                            &nbsp;
                        </div>
                    </div>
                </div></div></body>
</html>";


        public const string CONTRIBUTION_HTML = @"
<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
<head>
   <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>

    <!-- Page title set in pageTitle directive -->
    <title page-title></title>

    <link rel='stylesheet' href='app/content/css/bootstrap.min.css' />        
    <link rel='stylesheet' href='app/content/css/font-awesome.min.css' />    
    <link rel='stylesheet' href='app/content/css/style.css' />
</head >
<body>
    <div class='col-md-12'>
        <div class='row'>
            <div class='form-group'>
                <div class='col-md-12'>
                    &nbsp;
                </div>
            </div>
        </div>
        <div class='row'>
        <div class='form-group'>
            <div class='col-md-12'>
                <div class='col-md-4'>
                    [orgLogo]                    
                </div>
                <div class='col-md-4' style='text-align: center'>
                    <p>
                        <b>[orgName] </b><br />[orgAddress1] <br />[orgCity] <br />[orgState], - [orgZipCode]
                    </p>
                </div>
                <div class='col-md-4'></div>
            </div>
            <div class='col-md-12'>
                &nbsp;
            </div>
            <div class='col-md-12'>
                <table style='width:100%'>
                    <tr>
                        <td style='text-align: left'>
                            <b>Tax Id :</b> [orgTaxId]
                        </td>
                        <td style='text-align: right'>
                            <b>Date :</b> [printDate]
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2'>
                            <hr />
                        </td>
                    </tr>
                </table>
            </div>

            <div class='col-md-12'>
                <span class='col-sd-4'>
                    <b>[donorName]</b>
                </span>
                <p>
                    [donorAddress1],<br />[donorCity],<br /> [donorState] - [donorZipCode]
                </p>
            </div>            
        </div>
    </div>
    <div class='col-md-12'>
        <div class='col-md-4'></div>
        <div class='col-md-4' style='text-align: center'>
            <p ng-if='reportType==1'>
                <b>Contribution report</b><br />
                [reportStartDate] to [reportEndDate]
            </p>          
        </div>
        <div class='col-md-4'></div>
    </div>
    <div class='row'>
            <div class='form-group'>
                <div class='col-md-12'>
                    &nbsp;
                </div>
            </div>
        </div>  
<div class='row'>
            <div class='form-group'>
                <div class='col-md-12'>
                    <table style='width:100%'><tr><td style='text-align: left'><b>Total Contribution</b> :[currency] [totalContribution]</td>
                        <td style='text-align: right'><b>Page :</b> 1 of [totalPage]</td></tr>                    
                </table>  
                </div>
            </div>
        </div>  
        <div class='row'>
            <div class='form-group'>
                <div class='col-md-12'>
                    &nbsp;
                </div>
            </div>
        </div>      
        <div class='row'>
            <div class='form-group'>
                <div class='col-md-12'>
                    <table id = 'tblFamily' class='table table-bordered table-striped'>
                        <tr>
                            <th>
                                Payment Type
                            </th>
                            <th>
                                Amount in  $
                            </th>
                            <th>
                                Offering Date
                            </th>
                            <th>
                                RefNo
                            </th>
                        </tr>
                        [offeringPage1]
                    </table>
                </div>               
            </div>
        </div>       
        [offeringOtherPages]
        <div class='col-md-12'>
            &nbsp;
        </div>

        <div class='col-md-12'>
            <p>
                [irsMsg]
            </p>           
        </div>
<div class='col-md-12'>
                    &nbsp;
                </div>
        [signature]
        <div class='row'>
            <div class='form-group'>
                <div class='col-md-12'>
                    &nbsp;
                </div>
            </div>
        </div>
        <div class='row'>
            <div class='form-group'>
                <div class='col-md-12'>
                    &nbsp;
                </div>
            </div>
        </div>
    </div>
</body>
</html>";


    }
}
