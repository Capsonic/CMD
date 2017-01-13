'use strict';

describe('Directive: metricHistory', function () {

  // load the directive's module
  beforeEach(module('mainApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<metric-history></metric-history>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the metricHistory directive');
  }));
});
