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
       
        // Dashboard - Main page
        .state('dashboard', {
            url: "/dashboard",          
            templateUrl: "app/views/dashboard.html",
            data: {
                pageTitle: 'Dashboard'
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


         .state('analytics', {
             url: "/analytics",
             templateUrl: "app/views/analytics.html",
             data: {
                 pageTitle: 'Analytics'
             }
         })        
       
         .state('activity', {
             abstract: true,
             url: "/activity",
             templateUrl: "app/views/layout/content.html",
             data: {
                 pageTitle: 'Activity'
             }
         })

          .state('activity.offeringlst', {
              url: "/offeringlst",
              templateUrl: "app/views/activity/offeringlst.html",
              data: {
                  pageTitle: 'Offering List',
                  pageDesc: 'Offering List page which helps you to see available offering'
              }
          })

         .state('activity.offeringEdit', {
             url: "/offering/:id",
             templateUrl: "app/views/activity/offering.html",
             data: {
                 pageTitle: 'Offering',
                 pageDesc: 'Offering page which helps you to add/ update the offering '
             }
         })

          .state('activity.offeringAdd', {
              url: "/offering",
              templateUrl: "app/views/activity/offering.html",
              data: {
                  pageTitle: 'Offering',
                  pageDesc: 'Offering page which helps you to add/ update the offering '
              }
          })

         .state('activity.offeringDtls', {
             url: "/offeringDtls/:id",
             templateUrl: "app/views/activity/offeringDtls.html",
             data: {
                 pageTitle: 'offering details',
                 pageDesc: 'Offering details page which helps you to find the offering details '
             }
         })


        .state('activity.expenselst', {
            url: "/expenselst",
            templateUrl: "app/views/activity/expenselst.html",
            data: {
                pageTitle: 'Expense List',
                pageDesc: 'Expense List page which helps you to see available expense'
            }
        })

         .state('activity.expenseEdit', {
             url: "/expense/:id",
             templateUrl: "app/views/activity/expense.html",
             data: {
                 pageTitle: 'Expense',
                 pageDesc: 'Expense page which helps you to add/ update the expense'
             }
         })

          .state('activity.expenseAdd', {
              url: "/expense",
              templateUrl: "app/views/activity/expense.html",
              data: {
                  pageTitle: 'Expense',
                  pageDesc: 'Expense page which helps you to add/ update the expense '
              }
          })

         .state('activity.expenseDtls', {
             url: "/expenseDtls/:id",
             templateUrl: "app/views/activity/expenseDtls.html",
             data: {
                 pageTitle: 'Expense details',
                 pageDesc: 'Expense details page which helps you to find the expense details '
             }
         })


        .state('activity.depositlst', {
            url: "/depositlst",
            templateUrl: "app/views/activity/depositlst.html",
            data: {
                pageTitle: 'Deposit List',
                pageDesc: 'Deposit List page which helps you to see available deposit'
            }
        })

         .state('activity.depositEdit', {
             url: "/deposit/:id",
             templateUrl: "app/views/activity/deposit.html",
             data: {
                 pageTitle: 'Deposit',
                 pageDesc: 'Deposit page which helps you to add/ update the deposit'
             }
         })

          .state('activity.depositAdd', {
              url: "/deposit",
              templateUrl: "app/views/activity/deposit.html",
              data: {
                  pageTitle: 'Deposit',
                  pageDesc: 'Deposit page which helps you to add/ update the deposit '
              }
          })

         .state('activity.depositDtls', {
             url: "/depositDtls/:id",
             templateUrl: "app/views/activity/depositDtls.html",
             data: {
                 pageTitle: 'Deposit details',
                 pageDesc: 'Deposit details page which helps you to find the deposit details '
             }
         })
       

        .state('activity.eventlst', {
            url: "/eventlst",
            templateUrl: "app/views/activity/eventlst.html",
            data: {
                pageTitle: 'Event List',
                pageDesc: 'Event List page which helps you to see available event'
            }
        })

         .state('activity.eventEdit', {
             url: "/event/:id",
             templateUrl: "app/views/activity/event.html",
             data: {
                 pageTitle: 'Event',
                 pageDesc: 'Event page which helps you to add/ update the event'
             }
         })

          .state('activity.eventAdd', {
              url: "/event",
              templateUrl: "app/views/activity/event.html",
              data: {
                  pageTitle: 'Event',
                  pageDesc: 'Event page which helps you to add/ update the event '
              }
          })

         .state('activity.eventDtls', {
             url: "/eventDtls/:id",
             templateUrl: "app/views/activity/eventDtls.html",
             data: {
                 pageTitle: 'Event details',
                 pageDesc: 'Event details page which helps you to find the event details '
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

        .state('yearEndActivity.balanceSheet', {
            url: "/yearEndActivity/balanceSheet",
            templateUrl: "app/views/yearEndActivity/balanceSheet.html",
            data: {
                pageTitle: 'Opening Balance',
                pageDesc: 'Opening Balance page which helps you to view the account balance '
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

   

    .state('admin.bank', {
        url: "/admin/bank",
        templateUrl: "app/views/admin/bank.html",
        data: {
            pageTitle: 'Bank List',
            pageDesc: 'Bank List page which helps you to add/ update/ maintain the bank '
        }
    })


        .state('admin.userlst', {
            url: "/userlst",
            templateUrl: "app/views/admin/userlst.html",
            data: {
                pageTitle: 'User List',
                pageDesc: 'User List page which helps you to see available user'
            }
        })

        .state('admin.userEdit', {
            url: "/user/:id",
            templateUrl: "app/views/admin/user.html",
            data: {
                pageTitle: 'User',
                pageDesc: 'User page which helps you to add/ update the user'
            }
        })

        .state('admin.userAdd', {
            url: "/user",
            templateUrl: "app/views/admin/user.html",
            data: {
                pageTitle: 'User',
                pageDesc: 'User page which helps you to add/ update the user '
            }
        })

        .state('admin.userDtls', {
            url: "/userDtls/:id",
            templateUrl: "app/views/admin/userDtls.html",
            data: {
                pageTitle: 'User details',
                pageDesc: 'User details page which helps you to find the user details '
            }
        })
    
}

angular
    .module('churchMgntApp')
    .config(configState)
    .run(function ($rootScope, $state, editableOptions) {
        $rootScope.$state = $state;
        //editableOptions.theme = 'bs3';
    });