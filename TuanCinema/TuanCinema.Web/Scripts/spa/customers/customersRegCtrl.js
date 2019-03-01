(function (app) {
    'use strict';

    app.controller('customersRegCtrl', customersRegCtrl);

    customersRegCtrl.$inject = ['$scope', '$location', '$rootScope', 'apiService'];

    function customersRegCtrl($scope, $location, $rootScope, apiService) {

        $scope.newCustomer = {};

        $scope.Register = Register;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.submission = {
            successMessages: ['Thêm phim thành công sẽ được hiển thị ở đây.'],
            errorMessages: ['Lỗi thêm phim sẽ được hiển thị ở đây.']
        };

        function Register() {
            apiService.post('/api/customers/register', $scope.newCustomer,
           registerCustomerSucceded,
           registerCustomerFailed);
        }

        function registerCustomerSucceded(response) {
            $scope.submission.errorMessages = ['Lỗi thêm phim sẽ được hiển thị ở đây.'];
            console.log(response);
            var customerRegistered = response.data;
            $scope.submission.successMessages = [];
            $scope.submission.successMessages.push($scope.newCustomer.LastName + ' has been successfully registed');
            $scope.submission.successMessages.push('Check ' + customerRegistered.UniqueKey + ' for reference number');
            $scope.newCustomer = {};
        }

        function registerCustomerFailed(response) {
            console.log(response);
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };
    }

})(angular.module('TuanCinema'));