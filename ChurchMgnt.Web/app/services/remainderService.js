'use strict';
app.factory('remainderService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var remainderFactory = {};

    var _getBirthdayListByOrgId = function (orgId) {

        return $http.get(serviceBase + 'api/BirthdayRemainder/GetTodayBirthdayFamilyMembersByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _isBirthdayRemainderAvailableByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/BirthdayRemainder/IsBirthdayRemainderAvailableByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _getTodayMarriageAnniversaryFamiliesByOrgId = function (orgId) {

        return $http.get(serviceBase + 'api/MarriageAnniversaryRemainder/GetTodayMarriageAnniversaryFamiliesByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _isAnybodyMarriageAnniversarydayTodayByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/MarriageAnniversaryRemainder/IsAnybodyMarriageAnniversarydayTodayByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    remainderFactory.getBirthdayListByOrgId = _getBirthdayListByOrgId;
    remainderFactory.isBirthdayRemainderAvailableByOrgId = _isBirthdayRemainderAvailableByOrgId;

    remainderFactory.getTodayMarriageAnniversaryFamiliesByOrgId = _getTodayMarriageAnniversaryFamiliesByOrgId;
    remainderFactory.isAnybodyMarriageAnniversarydayTodayByOrgId = _isAnybodyMarriageAnniversarydayTodayByOrgId;

    return remainderFactory;
}]);