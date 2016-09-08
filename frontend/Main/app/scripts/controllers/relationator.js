'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:RelationatorCtrl
 * @description
 * # RelationatorCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').factory('relationatorController', function($log, $activityIndicator, $routeParams, $filter) {
    var log = $log;

    return function(oMainConfig) {

        //INIT CONFIG/////////////////////////////////////
        var scope = oMainConfig.scope;

        var _baseService = oMainConfig.baseService;
        var _baseRelatedService = oMainConfig.baseRelatedService;

        if (!oMainConfig.entityName) {
            oMainConfig.entityName = '';
        }

        if (!oMainConfig.relatedEntityName) {
            oMainConfig.relatedEntityName = '';
        }

        var _dragulaBagName = oMainConfig.dragulaBagName;
        if (!_dragulaBagName) {
            _dragulaBagName = '';
        }

        //After Load callback
        var _afterLoadCallBack = oMainConfig.afterLoad;
        if (!_afterLoadCallBack || typeof _afterLoadCallBack != 'function') {
            _afterLoadCallBack = function() {};
        }

        //END CONFIG/////////////////////////////////////


        //SCOPE----------------------------------------------------------------------------
        //let's use normal variables (without underscore) so they can be
        //accessed in view normally
        scope.entityName = oMainConfig.entityName;
        scope.relatedEntityName = oMainConfig.relatedEntityName;
        scope.occuppiedEntities = [];
        scope.availableEntities = [];

        scope.$on(_dragulaBagName + '.drop-model', function(e, el, source) {
            var id = Number(el.attr('id'));
            var entityFoundInAvailable = scope.availableEntities.find(function(o) {
                return o.id == id;
            });

            var entityFoundInOccuppied = scope.occuppiedEntities.find(function(o) {
                return o.id == id;
            });

            var expanded;
            //Dropped in Available
            if (entityFoundInAvailable) {
                expanded = entityFoundInAvailable.expanded;
                _baseRelatedService.customPost('RemoveFromParent/' + oMainConfig.entityName + '/' + scope.baseEntity.id, entityFoundInAvailable).then(function(data) {
                    entityFoundInAvailable.expanded = expanded;
                    // _synchronizeIfFilter(entityFoundInAvailable, 'available');
                    alertify.success('Moved successfully.');
                });
            }
            //Dropped in Occuppied
            else if (entityFoundInOccuppied) {
                expanded = entityFoundInOccuppied.expanded;
                entityFoundInOccuppied[oMainConfig.entityName + 's'] = [];
                
                var oNavProperties = {};
                for (var prop in entityFoundInOccuppied) {
                    if (entityFoundInOccuppied.hasOwnProperty(prop)) {
                        if (Array.isArray(entityFoundInOccuppied[prop])) {
                            oNavProperties[prop] = entityFoundInOccuppied[prop];
                            entityFoundInOccuppied[prop] = [];
                        }
                    }
                }
                // entityFoundInOccuppied['Dashboards'] = [];
                // entityFoundInOccuppied['Metrics'] = [];
                // entityFoundInOccuppied['Initiatives'] = [];
                // entityFoundInOccuppied['Departments'] = [];
                _baseRelatedService.addToParent(oMainConfig.entityName, scope.baseEntity.id, entityFoundInOccuppied).then(function(data) {
                    entityFoundInOccuppied.expanded = expanded;
                    for (var prop in oNavProperties) {
                        if (oNavProperties.hasOwnProperty(prop)) {
                            entityFoundInOccuppied[prop] = oNavProperties[prop];
                        }
                    }
                    // _synchronizeIfFilter(entityFoundInOccuppied, 'occuppied');
                    alertify.success('Moved successfully.');
                });
            } else {
                alertify.error('Error. ' + oMainConfig.relatedEntityName + ' not found.');
            }
        });

        // function _synchronizeIfFilter(entity, place) {
        //     var oFound = null;
        //     if (place == 'available') {
        //         for (var i = 0; i < _occuppiedEntities.length; i++) {
        //             var current = _occuppiedEntities[i];
        //             if (current.id == entity.id) {
        //                 oFound = _occuppiedEntities.splice(i, 1);
        //                 break;
        //             }
        //         }
        //         if (oFound != null) {
        //             _availableEntities.push(oFound);
        //         }
        //     } else if (place == 'occuppied') {
        //         for (var i = 0; i < _availableEntities.length; i++) {
        //             var current = _availableEntities[i];
        //             if (current.id == entity.id) {
        //                 oFound = _availableEntities.splice(i, 1);
        //                 break;
        //             }
        //         }
        //         if (oFound != null) {
        //             _occuppiedEntities.push(oFound);
        //         }
        //     }
        //     scope.on_search();
        // };

        scope.on_search = function(sSearchValue) {
            // if (scope.search != undefined && scope.search.length > 0) {
            // scope.$evalAsync(function() {
            // [].push.apply(scope.occuppiedEntities, $filter('filter')(_occuppiedEntities, scope.search));
            // [].push.apply(scope.availableEntities, $filter('filter')(_availableEntities, scope.search));
            // angular.copy($filter('filter')(_occuppiedEntities, scope.search), scope.occuppiedEntities)
            // angular.copy($filter('filter')(_availableEntities, scope.search), scope.availableEntities)
            // scope.availableEntities = $filter('filter')(_availableEntities, scope.search);
            // });

            // } else {
            //     scope.$evalAsync(function() {
            //         scope.occuppiedEntities = [];
            //         scope.availableEntities = [];

            //         angular.copy(_occuppiedEntities, scope.occuppiedEntities);
            //         angular.copy(_availableEntities, scope.availableEntities);
            //     });
            // }

            $('[dragula="\"entities-bag\""]').children().css('display', 'none');

            $('[dragula="\"entities-bag\""]').children().filter(function() {
                return $(this).children('span').text().toLowerCase().search(sSearchValue.toLowerCase()) > -1;
            }).css('display', 'block');

        };

        scope.sortOccuppiedBy = function(propertyName) {
            scope.reverseOccuppied = (scope.propertyNameOccuppied === propertyName) ? !scope.reverseOccuppied : false;
            scope.propertyNameOccuppied = propertyName;
        };
        
        scope.sortAvailableBy = function(propertyName) {
            scope.reverseAvailable = (scope.propertyNameAvailable === propertyName) ? !scope.reverseAvailable : false;
            scope.propertyNameAvailable = propertyName;
        };


        var _afterLoad = function() {
            _afterLoadCallBack();
            $activityIndicator.stopAnimating();
        };

        var _occuppiedEntities;
        var _availableEntities;

        var _load = function() {
            $activityIndicator.startAnimating();
            alertify.closeAll();

            switch (true) {
                case $routeParams.id !== true && $routeParams.id > 0: //Get By id
                    _baseService.loadEntity($routeParams.id).then(function(data) {

                        var theOriginalEntity = _baseService.getById($routeParams.id);
                        if (!theOriginalEntity) {
                            alertify.alert('Nonexistent record.').set('modal', true).set('closable', false);
                            scope.openingMode = 'error';
                            return;
                        }


                        for (var catalog in _baseService.catalogs) {
                            if (_baseService.catalogs.hasOwnProperty(catalog)) {
                                scope['cat' + catalog] = _baseService.catalogs[catalog].getAll();
                            }
                        }

                        //Loading Available Related Entities
                        _baseRelatedService.loadCatalogs().then(function() {
                            _baseRelatedService.customGet('GetAvailableForEntity/' + oMainConfig.entityName + '/' + $routeParams.id).then(function(data) {
                                for (var catalog in _baseRelatedService.catalogs) {
                                    if (_baseRelatedService.catalogs.hasOwnProperty(catalog)) {
                                        scope['cat' + catalog] = _baseRelatedService.catalogs[catalog].getAll();
                                    }
                                }
                                scope.baseEntity = angular.copy(theOriginalEntity);


                                // _occuppiedEntities = scope.baseEntity[('' + oMainConfig.relatedEntityName + 's')];
                                // _occuppiedEntities.forEach(function(entity) {
                                //     return _baseRelatedService.adapt(entity);
                                // });


                                // _availableEntities = data;
                                // _availableEntities.forEach(function(entity) {
                                //     return _baseRelatedService.adapt(entity);
                                // });


                                scope.occuppiedEntities = scope.baseEntity[('' + oMainConfig.relatedEntityName + 's')];
                                scope.occuppiedEntities.forEach(function(entity) {
                                    return _baseRelatedService.adapt(entity);
                                });

                                scope.availableEntities = data;
                                scope.availableEntities.forEach(function(entity) {
                                    return _baseRelatedService.adapt(entity);
                                });

                                _afterLoad();

                                scope.on_search();
                            });
                        });
                    });
                    break;

                default:
                    scope.openingMode = 'error';
                    // $activityIndicator.stopAnimating();
                    alertify.alert('Verify URL parameters.').set('modal', true).set('closable', false);
                    return;
            };
        };

        // Public baseController API:////////////////////////////////////////////////////////////
        var oAPI = {
            load: _load,
            // unselectAll: _unselectAll
        };
        return oAPI;
    };
});
