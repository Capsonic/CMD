'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:objectiveBox
 * @description
 * # objectiveBox
 */
angular.module('mainApp').directive('objectiveBox', function($timeout) {
    return {
        templateUrl: 'scripts/directives/objectiveBox/objectivebox.html',
        restrict: 'E',
        link: function postLink(scope, element, attrs) {

            $timeout(function() {
                scope.$watch(function() {
                    return element.parent().width() + element.parent().height() + angular.element(window).width() + angular.element(window).height();
                }, function() {
                    resetSizes();
                });
            }, 200);

            function resetSizes() {
                switch (true) {
                    case element.parent().width() > 1200:
                        element.css('font-size', 42);
                        element.find('h2').css('font-size', 56);
                        break;
                    case element.parent().width() > 900:
                        element.css('font-size', 36);
                        element.find('h2').css('font-size', 40);
                        break;
                    case element.parent().width() > 700:
                        element.css('font-size', 18);
                        element.find('h2').css('font-size', 21);
                        break;
                    case element.parent().width() > 350:
                        element.css('font-size', 12);
                        element.find('h2').css('font-size', 16);
                        break;
                    default:
                        element.css('font-size', 10);
                        element.find('h2').css('font-size', 12);
                };
            };

            scope.theWidth = function() {
                return element.parent().width();
            };
        }
    };
});
