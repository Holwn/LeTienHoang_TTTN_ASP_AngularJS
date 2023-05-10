(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http','notificationService'];

    function apiService($http, notificationService) {
        return {
            get: get,
            post: post
        }

        function post(url, data, success, failure) {
            $http.post(url, data).then(function (result) {
                success(error);
            }, function (error) {
                if (error.Status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }
    }
})(angular.module('pharma.common'));