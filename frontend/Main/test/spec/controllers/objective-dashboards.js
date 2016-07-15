'use strict';

describe('Controller: ObjectiveDashboardsCtrl', function () {

  // load the controller's module
  beforeEach(module('mainApp'));

  var ObjectiveDashboardsCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    ObjectiveDashboardsCtrl = $controller('ObjectiveDashboardsCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(ObjectiveDashboardsCtrl.awesomeThings.length).toBe(3);
  });
});
