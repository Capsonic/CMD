'use strict';

describe('Directive: metricHistoryReadOnly', function () {

  // load the directive's module
  beforeEach(module('mainApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<metric-history-read-only></metric-history-read-only>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the metricHistoryReadOnly directive');
  }));
});
