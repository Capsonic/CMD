'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:objectiveBox
 * @description
 * # objectiveBox
 */
angular.module('mainApp').directive('objectiveBox', function() {
    return {
        templateUrl: 'scripts/directives/objectiveBox/objectivebox.html',
        restrict: 'E',
        link: function postLink(scope, element, attrs) {
        	
        }
    };
});
