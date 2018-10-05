(function (app) {
    app.service('apiService', apiService);
    apiService.$inject = ['$http'];
    function apiService($http) {
        return {
            get: get,
            post:post
        };
        function post(url,data,success,failure) {
            $http
                .post(url, data)
                .then(function (result) {

                    success(result);
                }, function (error) {
                    failure(error);
                });
        }
        function get(url, params, success, failure) {
            $http
                .get(url, params)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    failure(error);
                });
        }
    }
})(angular.module('app.common'));