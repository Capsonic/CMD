'use strict';

describe('Controller: RelationatorCtrl', function () {

  // load the controller's module
  beforeEach(module('mainApp'));

  var RelationatorCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    RelationatorCtrl = $controller('RelationatorCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(RelationatorCtrl.awesomeThings.length).toBe(3);
  });
});
