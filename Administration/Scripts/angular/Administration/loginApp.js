var app = angular.module("app", ['trNgGrid']);
app.controller("loginController", loginController);
app.$inject = ["$scope", "adminService"];

function loginController($scope, adminService) {
    $scope.User = {};
    $scope.init = function () {

    }

    $scope.submit = function () {
        let modal = {
            USER_NAME: $scope.user,
            PASSWORD: $scope.password
        };

        ShowLoader();
        adminService.AddRecord(modal).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                if ($scope.Items === null || $scope.Items == undefined || $scope.Items === '') {
                    $scope.Items = [];
                }
                var status = $scope.Status.find(x => x.Value === d.data.Data.USED_STATE);
                d.data.Data.USEDSTATE_NAME = status.Name;
                $scope.Items.push(d.data.Data);
                $scope.Total += 1; 

                $scope.clear();
                $('#dialogModal').modal('hide');
                //$scope.getData();
                DisplayMessage('Success', 'User has been added successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }
}

app.factory('adminService', ["$http", "baseUrl", function ($http, baseUrl) {
    var fac = {};
    fac.AddRecord = function (modal) {
        return $http.post(baseUrl + '/api/Account/Login', modal);
    }
    return fac;
}]);
