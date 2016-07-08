'use strict';

describe('Controller: DashboardsCtrl', function () {

  // load the controller's module
  beforeEach(module('mainApp'));

  var DashboardsCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    DashboardsCtrl = $controller('DashboardsCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(DashboardsCtrl.awesomeThings.length).toBe(3);
  });
});
