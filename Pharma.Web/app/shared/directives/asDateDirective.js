(function (app) {
    'use strict';

    app.directive('asDate', asDate);

    function asDate($filter) {
        return {
            require: '^ngModel',
            restrict: 'A',
            link: function (scope, element, attrs, ctrl) {
                ctrl.$formatters.splice(0, ctrl.$formatters.length);
                ctrl.$parsers.splice(0, ctrl.$parsers.length);
                ctrl.$formatters.push(function (modelValue) {
                    if (!modelValue) {
                        return;
                    }
                    return $filter('date')(new Date(modelValue), 'dd-MM-yyyy');
                });
                ctrl.$parsers.push(function (modelValue) {
                    return modelValue;
                });
            }
        };
    }
})(angular.module('pharma.common'));