'use strict';

/**
 * @ngdoc directive
 * @name reusableApp.directive:showFocus
 * @description
 * # showFocus
 */
angular.module('reusable', []);
angular.module('reusable').directive('showFocus', function($timeout) {
    return function(scope, element, attrs) {
        scope.$watch(attrs.showFocus,
            function(newValue) {
                $timeout(function() {
                    newValue && element.focus();
                }, 700);
            }, true);
    };
});
