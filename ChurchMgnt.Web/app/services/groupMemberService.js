'use strict';
app.factory('groupMemberService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var groupMemberFactory = {};

    var _getByGroupId = function (groupId, active) {

        return $http.get(serviceBase + 'api/groupMember/getByGroupId?groupId=' + groupId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _getByFamilyMemberId = function (familyMemberId, active) {

        return $http.get(serviceBase + 'api/groupMember/getByFamilyMemberId?familyMemberId=' + familyMemberId + '&active=' + active).then(function (results) {
            return results;
        });

    };


    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/groupMember/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    //var _enable = function (id, active) {

    //    var deferred = $q.defer();

    //    var data = {
    //        id: id,
    //        active: active
    //    };

    //    $http.post(serviceBase + 'api/group/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
    //        deferred.resolve(successResponse);
    //    }, function (failerResponse) {
    //        deferred.reject(failerResponse);
    //    });

    //    return deferred.promise;
    //};

    groupMemberFactory.getByGroupId = _getByGroupId;
    groupMemberFactory.getByFamilyMemberId = _getByFamilyMemberId;
    groupMemberFactory.save = _save;
    
    

    return groupMemberFactory;
}]);