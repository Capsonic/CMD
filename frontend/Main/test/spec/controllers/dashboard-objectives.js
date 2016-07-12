'use strict';

describe('Controller: DashboardObjectivesCtrl', function () {

  // load the controller's module
  beforeEach(module('mainApp'));

  var DashboardObjectivesCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    DashboardObjectivesCtrl = $controller('DashboardObjectivesCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(DashboardObjectivesCtrl.awesomeThings.length).toBe(3);
  });
});
