var app = angular.module("app", ['trNgGrid']);
app.controller("parameterController", parameterController);
app.$inject = ["$scope", "adminService"];

function parameterController($scope, adminService) {
    $scope.pageNumber = 0;
    $scope.pageSize = 10;
    $scope.orderBy = "";
    $scope.isDesc = false;

    $scope.Items = [];
    $scope.Total = 0;
    $scope.IsEdit = false;

    $scope.Status = [];
    $scope.Units = [];

    $scope.init = function () {
        $scope.UsedState();
        $scope.clear(false);
    }

    $scope.clear = function (isreset) {
        $scope.sType = '';
        $scope.sName = '';
        $scope.sStatus = undefined;

        $scope.type = '';
        $scope.name = '';
        $scope.value = '';
        $scope.order_no = '';
        $scope.status = undefined;
    }

    $scope.setPageSize = function (pageSize) {
        $scope.pageSize = pageSize;
    }

    $scope.addModel = function () {
        $scope.clear();
        $('#dialogModal').modal('show');
        $scope.IsEdit = false;
    }

    $scope.UsedState = function () {
        $scope.Status.push({ Value: 1, Name: 'Actived' });
        $scope.Status.push({ Value: 2, Name: 'Inactived' });
        $scope.status = undefined;
    }

    //Called from on-data-required directive.
    $scope.GenerateData = function (currentPage, pageItems) {
        $scope.pageNumber = currentPage;
        $scope.getData();
    }

    $scope.search = function () {
        ShowLoader();
        $scope.getData();
        HideLoader();
    }

    $scope.getData = function () {
        //ShowLoader();
        let options = {
            TYPE: $scope.sType, NAME: $scope.sName, USED_STATE: $scope.sStatus, PageSize: $scope.pageSize, PageNumber: $scope.pageNumber, OrderBy: $scope.orderBy, IsDesc: $scope.isDesc
        };
        adminService.getData(options).then(function (d) {
            $scope.Items = d.data.Data;
            $scope.Total = d.data.Total;
            // Success
            //HideLoader();
        }, function (status) {
            HideLoader();
            //DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.editRecord = function (record) {
        $scope.IsEdit = true;
        $scope.id = record.ID;
        $scope.type = record.TYPE;
        $scope.name = record.NAME;
        $scope.value = record.VALUE;
        $scope.order_no = record.ORDER_NO;
        $scope.status = record.USED_STATE;

        $('#dialogModal').modal('show');
    }

    $scope.submit = function () {
        let record = {
            ID: $scope.IsEdit === true ? $scope.id : 0,
            TYPE: $scope.type,
            NAME: $scope.name,
            VALUE: $scope.value,
            ORDER_NO: $scope.order_no,
            USED_STATE: $scope.status,
        }

        $scope.addOrUpdate(record);
    }

    $scope.addOrUpdate = function (record) {
        if ($scope.IsEdit) {
            $scope.update(record);
        }
        else {
            $scope.add(record);
        }
        $scope.clear();
    };

    $scope.add = function (record) {
        ShowLoader();
        adminService.AddRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                //if ($scope.Items === null || $scope.Items == undefined || $scope.Items === '') {
                //    $scope.Items = [];
                //}
                //$scope.Items.unshift(d.data.Data);
                $('#dialogModal').modal('hide');
                $scope.getData();
                DisplayMessage('Success', 'User has been added successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.update = function (record) {
        ShowLoader();
        adminService.UpdateRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                //var currentRecord = _.first(_.where($scope.Items, { "ID": record.ID }));
                //var index = $scope.Items.indexOf(currentRecord);
                //$scope.Items[index] = angular.copy(record);
                $('#dialogModal').modal('hide');
                $scope.getData();
                DisplayMessage('Success', 'User has been updated successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.deleteRecord = function (record) {
        if (confirm('Are you sure you want to delete this record from the database?')) {
            ShowLoader();
            adminService.DeleteRecord(record).then(function (d) {
                // Success
                HideLoader();
                if (d.data.Code === 200) {
                    var index = $scope.Items.indexOf(record);
                    if (index > -1) {
                        $scope.Items.splice(index, 1);
                    }
                    //$scope.getData();
                    DisplayMessage('Success', 'User has been deleted successfully.'); // Success
                } else {
                    DisplayServerErrorMessage(d.data.Message); // Failed
                }
            }, function (status) {
                HideLoader();
                DisplayServerErrorMessage(status.data); // Failed
            });
        } else {
            return;
            // Do nothing!
        }
        
    }


}

app.factory('adminService', ["$http", "baseUrl", function ($http, baseUrl) {
    var fac = {};
    fac.getData = function (options) {
        return $http.get(baseUrl + '/api/Parameter/Search?TYPE=' + options.TYPE + '&NAME=' + options.NAME + '&USED_STATE=' + options.USED_STATE + '&PageSize=' + options.PageSize + '&PageNumber=' + options.PageNumber + '&OrderBy=' + options.OrderBy + '&IsDesc=' + options.IsDesc);
    }
    fac.AddRecord = function (record) {
        return $http.post(baseUrl + '/api/Parameter/Add', record);
    }
    fac.UpdateRecord = function (record) {
        return $http.post(baseUrl + '/api/Parameter/Update', record);
    }
    fac.DeleteRecord = function (record) {
        return $http.post(baseUrl + '/api/Parameter/Delete', record);
    }
    return fac;
}]);
