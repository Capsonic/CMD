'use strict';

describe('Directive: yearSelectize', function () {

  // load the directive's module
  beforeEach(module('mainApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<year-selectize></year-selectize>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the yearSelectize directive');
  }));
});
