function configState($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    // Set default state
    $urlRouterProvider.otherwise("/login");
    $stateProvider
       
        .state('login', {
            url: "/login",         
            templateUrl: "app/views/login.html",
            data: {
                pageTitle: 'Login'
            }
        })

           // Dashboard - Main page
        .state('error', {
            url: "/error",
            templateUrl: "app/views/error.html",
            data: {
                pageTitle: 'error'
            }
        })

         .state('fqa', {
             url: "/FAQ",
             templateUrl: "app/views/shared/faq.html",
             data: {
                 pageTitle: 'error'
             }
         })

        // Dashboard - Main page
        .state('dashboard', {
            url: "/dashboard",          
            templateUrl: "app/views/dashboard.html",
            data: {
                pageTitle: 'Dashboard'
            }
        })
         
         .state('analytics', {
             url: "/analytics",
             templateUrl: "app/views/analytics.html",
             data: {
                 pageTitle: 'Analytics'
             }
         })

        .state('orglst', {
            url: "/orglst",
            templateUrl: "app/views/shared/orglst.html",
                data: {
                    pageTitle: 'Organization list',
                    pageDesc: 'Organizationlst page which helps you to add/ update the organization '
                }
        })

         .state('orgDtls', {
             url: "/orgDtls/:id",
             templateUrl: "app/views/shared/orgDtls.html",
             data: {
                 pageTitle: 'Organization details',
                 pageDesc: 'Organization details page which helps you to view the organization details'
             }
         })

         .state('orgEdit', {
             url: "/orgEdit/:id",
             templateUrl: "app/views/shared/org.html",
             data: {
                 pageTitle: 'Organization Edit',
                 pageDesc: 'Organization edit page which helps you to edit the organization details'
             }
         })

         .state('orgAdd', {
             url: "/orgAdd",
             templateUrl: "app/views/shared/org.html",
             data: {
                 pageTitle: 'Organization Add',
                 pageDesc: 'Organization add page which helps you to add the organization details'
             }
         })

          //.state('user', {
          //    url: "/admin/user",
          //    templateUrl: "app/views/admin/user.html",
          //    data: {
          //        pageTitle: 'User List',
          //        pageDesc: 'User List page which helps you to add/ update/ maintain the organization user '
          //    }
          //})

         .state('activity', {
             abstract: true,
             url: "/activity",
             templateUrl: "app/views/layout/content.html",
             data: {
                 pageTitle: 'Activity'
             }
         })

         .state('activity.offering', {
             url: "/offering",
             templateUrl: "app/views/activity/offering.html",
             data: {
                 pageTitle: 'Offering',
                 pageDesc: 'Offering list which helps you to add/ update/ maintain the offering '
             }
         })

         .state('activity.expense', {
             url: "/expense",
             templateUrl: "app/views/activity/expense.html",
             data: {
                 pageTitle: 'Expense',
                 pageDesc: 'Expense list which helps you to add/ update/ maintain the expense '
             }
         })

         .state('activity.deposit', {
             url: "/deposit",
             templateUrl: "app/views/activity/deposit.html",
             data: {
                 pageTitle: 'Deposit',
                 pageDesc: 'Deposit list which helps you to add/ update/ maintain the deposit '
             }
         })

         .state('activity.event', {
             url: "/event",
             templateUrl: "app/views/activity/event.html",
             data: {
                 pageTitle: 'Event',
                 pageDesc: 'Event list which helps you to add/ update/ maintain the event '
             }
         })

          .state('profile', {
              abstract: true,
              url: "/profile",
              templateUrl: "app/views/layout/content.html",
              data: {
                  pageTitle: 'Profile'
              }
          })

         .state('profile.changePassword', {
             url: "/changePassword",
             templateUrl: "app/views/profile/changePassword.html",
             data: {
                 pageTitle: 'Change Password',
                 pageDesc: 'Change Password page which helps you to change your login password '
             }
         })

         .state('memberCare', {
             abstract: true,
             url: "/memberCare",
             templateUrl: "app/views/layout/content.html",
             data: {
                 pageTitle: 'Member Care'
             }
         })

        .state('memberCare.familylst', {
            url: "/familylst",
            templateUrl: "app/views/memberCare/familylst.html",
            data: {
                pageTitle: 'Family list',
                pageDesc: 'Family list which helps you to add/ update/ maintain the family '
            }
        })

         .state('memberCare.familyEdit', {
             url: "/family/:id",
             templateUrl: "app/views/memberCare/family.html",
             data: {
                 pageTitle: 'Family',
                 pageDesc: 'Family page which helps you to add/ update the family '
             }
         })

       

      .state('memberCare.familyAdd', {
          url: "/family",
          templateUrl: "app/views/memberCare/family.html",
          data: {
              pageTitle: 'Family',
              pageDesc: 'Family page which helps you to add/ update the family '
          }
      })

     .state('memberCare.familyDtls', {
         url: "/familyDtls/:id",
         templateUrl: "app/views/memberCare/familyDtls.html",
         data: {
             pageTitle: 'Family details',
             pageDesc: 'Family details page which helps you to find the family details '
         }
     })

        .state('memberCare.sponsor', {
            url: "/sponsor",
            templateUrl: "app/views/memberCare/sponsor.html",
            data: {
                pageTitle: 'Sponsor',
                pageDesc: 'Sponsor page which helps you to add/ update/ maintain the sponsor  '
            }
        })

         .state('memberCare.remainder', {
             url: "/remainder",
             templateUrl: "app/views/memberCare/remainder.html",
             data: {
                 pageTitle: 'Remainder',
                 pageDesc: 'Remainder page which list out today birthday / mariage anniversary details'
             }
         })

     .state('offering', {
         url: "/shared/offering",
         templateUrl: "app/views/shared/offering.html",
         data: {
             pageTitle: 'Offering',
             pageDesc: 'Offering page which helps you to add/ update/ maintain/ track the church offering  '
         }
     })

    .state('expense', {
        url: "/shared/expense",
        templateUrl: "app/views/shared/expense.html",
        data: {
            pageTitle: 'Expense',
            pageDesc: 'Expense page which helps you to add/ update/ maintain/ track the church expense  '
        }
    })

    .state('deposit', {
        url: "/shared/deposit",
        templateUrl: "app/views/shared/deposit.html",
        data: {
            pageTitle: 'Deposit',
            pageDesc: 'Deposit page which helps you to add/ update/ maintain/ track the church offering deposit  '
        }
    })

    .state('event', {
        url: "/shared/event",
        templateUrl: "app/views/shared/event.html",
        data: {
            pageTitle: 'Event',
            pageDesc: 'Event page which helps you to add/ update/ maintain/ track the church event  '
        }
    })

         .state('report', {
             abstract: true,
             url: "/report",
             templateUrl: "app/views/layout/content.html",
             data: {
                 pageTitle: 'Report'
             }
         })

        .state('report.offeringRpt', {
            url: "/offering",
            templateUrl: "app/views/report/offeringRpt.html",
            data: {
                pageTitle: 'Offering Report',
                pageDesc: 'Offering Report page which helps you to search/ find/ view the church offering '
            }
        })

        .state('report.expenseRpt', {
            url: "/expense",
            templateUrl: "app/views/report/expenseRpt.html",
            data: {
                pageTitle: 'Expense Report',
                pageDesc: 'Expense Report page which helps you to search/ find/ view the church expense '
            }
        })

        .state('report.depositRpt', {
            url: "/deposit",
            templateUrl: "app/views/report/depositRpt.html",
            data: {
                pageTitle: 'Deposit report',
                pageDesc: 'Deposit Report page which helps you to search/ find/ view the church deposit '
            }
        })

    .state('report.eventRpt', {
        url: "/event",
        templateUrl: "app/views/report/eventRpt.html",
        data: {
            pageTitle: 'Event report',
            pageDesc: 'Event Report page which helps you to search/ find/ view the church event '
        }
    })

    .state('report.familyOfferingRpt', {
        url: "familyOffering",
        templateUrl: "app/views/report/familyOfferingRpt.html",
        data: {
            pageTitle: 'family offering report',
            pageDesc: 'Family offering report page which helps you to search/ find/ view family offering details '
        }
    })

         .state('report.familyPledgeRpt', {
             url: "familyPledgeRpt",
             templateUrl: "app/views/report/familyPledgeRpt.html",
             data: {
                 pageTitle: 'family pledge report',
                 pageDesc: 'Family pledge report page which helps you to view family pledge details against their offering'
             }
         })

    .state('report.transactionRpt', {
        url: "/report/transaction",
        templateUrl: "app/views/report/transactionRpt.html",
        data: {
            pageTitle: 'Transaction report',
            pageDesc: 'Transaction report page which helps you to search/ find/ view transaction details '
        }
    })
      
        .state('masterData', {
            abstract: true,
            url: "/masterData",
            templateUrl: "app/views/layout/content.html",
            data: {
                pageTitle: 'master Data'
            }
        })

        .state('masterData.fundType', {            
            url: "/masterData/fundType",
            templateUrl: "app/views/masterData/fundType.html",
            data: {
                pageTitle: 'Fund Type',
                pageDesc: 'Fund type page which helps you to add/ update/ maintain the fund type  '
            }
        })

    .state('masterData.offeringType', {
        url: "/masterData/offeringType",
        templateUrl: "app/views/masterData/offeringType.html",
        data: {
            pageTitle: 'Offering Type',
            pageDesc: 'Offering type page which helps you to add/ update/ maintain the offering type  '
        }
    })

     .state('masterData.expenseType', {
         url: "/masterData/expenseType",
         templateUrl: "app/views/masterData/expenseType.html",
         data: {
             pageTitle: 'Expense Type',
             pageDesc: 'Expense type page which helps you to add/ update/ maintain the expense type  '
         }
     })

    .state('masterData.subExpenseType', {
        url: "/masterData/subExpenseType",
        templateUrl: "app/views/masterData/subExpenseType.html",
        data: {
            pageTitle: 'Sub Expense Type',
            pageDesc: 'Sub expense type page which helps you to add/ update/ maintain the sub expense type  '
        }
    })

    .state('masterData.eventType', {
        url: "/masterData/eventType",
        templateUrl: "app/views/masterData/eventType.html",
        data: {
            pageTitle: 'Event Type',
            pageDesc: 'Event type page which helps you to add/ update/ maintain the event type  '
        }
    })

     .state('yearBegActivity', {
         abstract: true,
         url: "/yearBegActivity",
         templateUrl: "app/views/layout/content.html",
         data: {
             pageTitle: 'Year Begin Activity'
         }
     })

    .state('yearBegActivity.fiscalYear', {
        url: "/yearBeginActivity/fiscalYear",
        templateUrl: "app/views/yearBeginActivity/fiscalYear.html",
        data: {
            pageTitle: 'Fiscal Year',
            pageDesc: 'Fiscal Year page which helps you to add/ update/ maintain the Fiscal Year  '
        }
    })
    
    .state('yearBegActivity.openingBalance', {
        url: "/yearBeginActivity/openingBalance",
        templateUrl: "app/views/yearBeginActivity/openingBalance.html",
        data: {
            pageTitle: 'Opening Balance',
            pageDesc: 'Opening Balance page which helps you to add/ update/ maintain the Opening Balance  '
        }
    })

    .state('yearBegActivity.fiscalYearBudget', {
        url: "/yearBeginActivity/fiscalYearBudget",
        templateUrl: "app/views/yearBeginActivity/fiscalYearBudget.html",
        data: {
            pageTitle: 'Fiscal Year Budget',
            pageDesc: 'Fiscal Year Budget page which helps you to add/ update/ maintain the Fiscal Year Budget  '
        }
    })

    .state('yearBegActivity.familyPledge', {
        url: "/yearBeginActivity/familyPledge",
        templateUrl: "app/views/yearBeginActivity/familyPledge.html",
        data: {
            pageTitle: 'Family Pledge',
            pageDesc: 'Family Pledge page which helps you to add/ update/ maintain the Family Pledge  '
        }
    })


     .state('yearEndActivity', {
         abstract: true,
         url: "/yearEndActivity",
         templateUrl: "app/views/layout/content.html",
         data: {
             pageTitle: 'Year End Activity'
         }
     })

    .state('yearEndActivity.contribution', {
        url: "/yearEndActivity/contribution",
        templateUrl: "app/views/yearEndActivity/contributionRpt.html",
        data: {
            pageTitle: 'Contribution',
            pageDesc: 'Contribution page which helps you to view the congregation contribution '
        }
    })
    
    .state('print', {
        abstract: true,
        url: "/print",
        templateUrl: "app/views/layout/content_empty.html"        
    })

        .state('print.samplePrint', {
            url: "/samplePrint",
            templateUrl: "app/views/print/samplePrint.html"
        })

    .state('print.contributionPrint', {
        url: "/contributionPrint/:fiscalYearId/:id/:startDate/:endDate/:orgId/:familyMembers/:reportType/:includeLogo/:includeSignature/:pastorName",
        templateUrl: "app/views/print/contributionPrint.html"
    })

    .state('yearEndActivity.balanceSheet', {
        url: "/yearEndActivity/balanceSheet",
        templateUrl: "app/views/yearEndActivity/balanceSheet.html",
        data: {
            pageTitle: 'Opening Balance',
            pageDesc: 'Opening Balance page which helps you to view the account balance '
        }
    })

    .state('yearEndActivity.taxFiling', {
        url: "/yearEndActivity/taxFiling",
        templateUrl: "app/views/yearEndActivity/taxFiling.html",
        data: {
            pageTitle: 'Tax Filing',
            pageDesc: 'Tax Filing page which helps you to add/ update the tax filing details'
        }
    })

    .state('admin', {
        abstract: true,
        url: "/admin",
        templateUrl: "app/views/layout/content.html",
        data: {
            pageTitle: 'Admin'
        }
    })

    .state('admin.branchlst', {
        url: "/admin/branchlst",
        templateUrl: "app/views/admin/branchlst.html",
        data: {
            pageTitle: 'Branch List',
            pageDesc: 'Branch List page which helps you to see available organization branch'
        }
    })

     .state('admin.branchEdit', {
         url: "/branch/:id",
         templateUrl: "app/views/admin/branch.html",
         data: {
             pageTitle: 'Branch',
             pageDesc: 'Branch page which helps you to add/ update the organization Branch '
         }
     })

      .state('admin.branchAdd', {
            url: "/branch",
            templateUrl: "app/views/admin/branch.html",
            data: {
                pageTitle: 'Branch',
                pageDesc: 'Branch page which helps you to add/ update the organization Branch '
            }
        })
       
     .state('admin.branchDtls', {
         url: "/branchDtls/:id",
         templateUrl: "app/views/admin/branchDtls.html",
         data: {
             pageTitle: 'Branch details',
             pageDesc: 'Branch details page which helps you to find the organization branch details '
         }
     })

    .state('admin.bank', {
        url: "/admin/bank",
        templateUrl: "app/views/admin/bank.html",
        data: {
            pageTitle: 'Bank List',
            pageDesc: 'Bank List page which helps you to add/ update/ maintain the bank '
        }
    })

    .state('admin.account', {
        url: "/admin/account",
        templateUrl: "app/views/admin/account.html",
        data: {
            pageTitle: 'Account List',
            pageDesc: 'Account List page which helps you to add/ update/ maintain the account '
        }
    })

    .state('admin.user', {
        url: "/admin/user",
        templateUrl: "app/views/admin/user.html",
        data: {
            pageTitle: 'User List',
            pageDesc: 'User List page which helps you to add/ update/ maintain the organization user '
        }
    })

    .state('admin.logo', {
        url: "/admin/logo",
        templateUrl: "app/views/admin/logo.html",
        data: {
            pageTitle: 'Logo',
            pageDesc: 'Logo page which helps you to add/ update/ maintain the organization logo imgage '
        }
    })

         .state('admin.signature', {
             url: "/admin/signature",
             templateUrl: "app/views/admin/signature.html",
             data: {
                 pageTitle: 'Pastor Signature',
                 pageDesc: 'Signature page which helps you to add/ update/ maintain the pastor siganure imgage '
             }
         })

    .state('communication', {
        abstract: true,
        url: "/communication",
        templateUrl: "app/views/layout/content.html",
        data: {
            pageTitle: 'Communication'
        }
    })      
     .state('communication.smtpSetting', {
         url: "/communication/smtpSetting",
         templateUrl: "app/views/communication/smtpSetting.html",
         data: {
             pageTitle: 'Organization SMTP Setting',
             pageDesc: 'Organization SMTP Setting page which helps you to add/ update the organization SMTP setting'
         }
     })

         .state('communication.group', {
             url: "/communication/group",
             templateUrl: "app/views/communication/group.html",
             data: {
                 pageTitle: 'Communication Group',
                 pageDesc: 'Communication Group page which helps you to add/ update the communication group'
             }
         })

          .state('communication.groupMember', {
              url: "/communication/groupMember",
              templateUrl: "app/views/communication/groupMember.html",
              data: {
                  pageTitle: 'Communication Group Member',
                  pageDesc: 'Communication Group Member page which helps you to add/ update the communication group members'
              }
          })

        .state('communication.templateLst', {
            url: "/communication/templateLst",
            templateUrl: "app/views/communication/templateLst.html",
            data: {
                pageTitle: 'Email Template List',
                pageDesc: 'Email Template List page which helps you to see available email templates'
            }
        })

    .state('communication.templateEdit', {
        url: "/template/:id",
        templateUrl: "app/views/communication/template.html",
        data: {
            pageTitle: 'Email Template',
            pageDesc: 'Email template page which helps you to add/ update the email template '
        }
    })

    .state('communication.templateAdd', {
        url: "/template",
        templateUrl: "app/views/communication/template.html",
        data: {
            pageTitle: 'Email Template',
            pageDesc: 'Email Template which helps you to add/ update the email tempalte '
        }
    })

    .state('communication.templateDtls', {
        url: "/templateDtls/:id",
        templateUrl: "app/views/communication/templateDtls.html",
        data: {
            pageTitle: 'Email Templaet details',
            pageDesc: 'Email template details page which helps you to find the email template details '
        }
    })


        .state('communication.imageUpload', {
            url: "/communication/imageUpload",
            templateUrl: "app/views/communication/imageUpload.html",
            data: {
                pageTitle: 'Image Upload',
                pageDesc: 'Image Upload page which helps you to upload the images'
            }
        })


        .state('communication.emailCompose', {
            url: "/communication/emailCompose",
            templateUrl: "app/views/communication/emailCompose.html",
            data: {
                pageTitle: 'Mail Box',
                pageDesc: 'Mail Box - Email compose'
            }
        })

      //.state('email.template', {
      //    url: "/email/template",
      //    templateUrl: "app/views/email/template.html",
      //    data: {
      //        pageTitle: 'Email template List',
      //        pageDesc: 'Email template List page which helps you to add/ update/ maintain the email template '
      //    }
      //})
}

angular
    .module('churchMgntApp')
    .config(configState)
    .run(function ($rootScope, $state, editableOptions) {
        $rootScope.$state = $state;
        //editableOptions.theme = 'bs3';
    });