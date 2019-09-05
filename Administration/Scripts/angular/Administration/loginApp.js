var app = angular.module("app", []);
app.controller("loginController", loginController);
app.$inject = ["$scope", "adminService"];

function loginController($scope, adminService) {
    $scope.User = {};
    $scope.init = function () {

    }

    $scope.submit = function () {
        let model = {
            USER_NAME: $scope.user,
            PASSWORD: $scope.password
        };

        ShowLoader();
        adminService.Login(model).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                //window.location.href
                //DisplayMessage('Success', 'User has been added successfully.'); // Success
            } else {
                //DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            //DisplayServerErrorMessage(status.data); // Failed
        });
    }

    function ShowLoader() {
        $('.api-loader').fadeIn();
    }

    function HideLoader() {
        $('.api-loader').fadeOut();
    }
}

app.factory('adminService', ["$http", "baseUrl", function ($http, baseUrl) {
    var fac = {};
    fac.Login = function (model) {
        return $http.post(baseUrl + '/api/Account/Login', model);
    }
    return fac;
}]);
