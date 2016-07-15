'use strict';

describe('Controller: ObjectiveInitiativesCtrl', function () {

  // load the controller's module
  beforeEach(module('mainApp'));

  var ObjectiveInitiativesCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    ObjectiveInitiativesCtrl = $controller('ObjectiveInitiativesCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(ObjectiveInitiativesCtrl.awesomeThings.length).toBe(3);
  });
});
