(function (app) {
    'use strict';

    app.directive('asDate', asDate);

    function asDate($filter) {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attrs, ngModelCtrl) {
                ngModelCtrl.$parsers.push(function (viewValue) {
                    // Assuming viewValue is in 'dd/MM/yyyy' format
                    var parts = viewValue.split('/');
                    var day = parseInt(parts[0], 10);
                    var month = parseInt(parts[1], 10) - 1; // Month is 0-based
                    var year = parseInt(parts[2], 10);

                    // Validate the date
                    var date = new Date(year, month, day);
                    var isValidDate = !isNaN(date.getTime());

                    if (isValidDate) {
                        ngModelCtrl.$setValidity('datefmt', true);
                        return $filter('date')(date, 'yyyy-MM-dd');
                    } else {
                        ngModelCtrl.$setValidity('datefmt', false);
                        return undefined;
                    }
                });
            }
        };
    }
})(angular.module('pharma.common'));
