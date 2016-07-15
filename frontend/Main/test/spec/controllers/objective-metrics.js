'use strict';

describe('Controller: ObjectiveMetricsCtrl', function () {

  // load the controller's module
  beforeEach(module('mainApp'));

  var ObjectiveMetricsCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    ObjectiveMetricsCtrl = $controller('ObjectiveMetricsCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(ObjectiveMetricsCtrl.awesomeThings.length).toBe(3);
  });
});
