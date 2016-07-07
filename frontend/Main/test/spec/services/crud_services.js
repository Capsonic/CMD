'use strict';

describe('Service: CRUDServices', function () {

  // load the service's module
  beforeEach(module('mainApp'));

  // instantiate service
  var CRUDServices;
  beforeEach(inject(function (_CRUDServices_) {
    CRUDServices = _CRUDServices_;
  }));

  it('should do something', function () {
    expect(!!CRUDServices).toBe(true);
  });

});
