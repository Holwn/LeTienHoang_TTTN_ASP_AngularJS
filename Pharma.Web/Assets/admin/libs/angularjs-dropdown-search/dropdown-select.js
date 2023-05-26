(function (window, angular) {
  'use strict';

  const apps = angular.module('DropDownSelect', []);

  apps.run([
    '$templateCache',
    function ($templateCache) {
      $templateCache.put(
        'template/dropdownSelect.html',
        [
          '<div class="ng-dropdown-container">',
          '    <div class="ng-dropdown-text">',
          '        <span>{{ddModel[ddLabel] ? ddModel[ddLabel] : "--- Lựa chọn ---"}}</span>',
          '    </div>',
          '    <ul class="ng-dropdown-options">',
          '        <li class="ng-dropdown-item">',
          '            <input type="text" class="ng-dropdown-search" ng-model="searchText">',
          '        </li>',
          '        <li class="ng-dropdown-item" ng-click="Clear()" ng-class="{\'selected\':!ddModel[ddLabel]}">',
          '            -- Select --',
          '        </li>',
          '        <li class="ng-dropdown-item" ng-class="{\'selected\':item === ddModel}" ',
          '        ng-repeat="item in ddData | filter:ddSearchFilter" data-item="{{item}}" ng-click="SelectItem($event,item)">',
          '            {{item[ddLabel]}}',
          '        </li>',
          '    </ul>',
          '</div>'
        ].join('')
      );
    }
  ]);

  apps.directive('dropdownSelect', [
    function () {
      return {
        restrict: 'E',
        replace: true,
        scope: {
          ddModel: '=',
          ddData: '=',
          ddLabel: '@',
          ddChange: '&'
        },
        controller: [
          '$scope',
          '$element',
          '$document',
          'filterFilter',
          function ($scope, $element, $document, filterFilter) {

            $scope.searchText = '';
            $scope.ddSearchFilter = function (item) {
              return (
                !$scope.searchText ||
                (item[$scope.ddLabel] &&
                  item[$scope.ddLabel]
                  .toLowerCase()
                  .indexOf($scope.searchText.toLowerCase()) > -1)
              );
            };

            $scope.SelectItem = function ($event, item) {
              $scope.ddModel = item;
              $element.removeClass('show');
              $scope.searchText = '';
              setTimeout(function () {
                $scope.$eval($scope.ddChange);
              }, 100);
            };

            $scope.Clear = function () {
              $scope.ddModel = '';
              $element.removeClass('show');
              $scope.searchText = '';
              setTimeout(function () {
                $scope.$eval($scope.ddChange);
              }, 100);
            }

            $scope.$watch("ddData", function (newValue, oldValue) {
              if (newValue != oldValue) {
                const SelectItem = filterFilter(newValue, $scope.ddModel);
                if (!SelectItem || SelectItem.length < 1) {
                  $scope.ddModel = '';
                }
              }
            });


            const onDropDownSelectClick = function ($event) {
              const ele = angular.element($event.target);

              const isTarget = ele.closest('.ng-dropdown-text').length === 1;

              if (isTarget) {
                $element.toggleClass('show');
              }
            };

            const onDocumentMouseUp = function ($event) {
              if (!$event || !$event.target) {
                return;
              }

              const isDropdownContainer =
                $element.has($event.target).length === 1;

              if (!isDropdownContainer) {
                $element.removeClass('show');
                $scope.searchText = '';
              }
            };

            $element.on('click', onDropDownSelectClick);

            $document.on('mouseup', onDocumentMouseUp);

            $scope.$on('$destroy', function () {
              element.off('click', onDropDownSelectClick);
              $document.off('mouseup', onDocumentMouseUp);
            });
          }
        ],
        templateUrl: 'template/dropdownSelect.html' //'dropdownSelect.html'
      };
    }
  ]);
})(window, window.angular);