'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:RelationatorCtrl
 * @description
 * # RelationatorCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').factory('relationatorController', function($log, $activityIndicator, $routeParams) {
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
        if (!_afterLoadCallBack || typeof _afterLoadCallBack != "function") {
            _afterLoadCallBack = function() {};
        }

        //END CONFIG/////////////////////////////////////


        //SCOPE----------------------------------------------------------------------------
        //let's use normal variables (without underscore) so they can be
        //accessed in view normally
        scope.entityName = oMainConfig.entityName;
        scope.relatedEntityName = oMainConfig.relatedEntityName;
        scope.$on(_dragulaBagName + '.drop-model', function(e, el, source) {
            var id = Number(el.attr('id'));
            var entitiesFoundInAvailable = scope.availableEntities.find(function(o) {
                return o.id == id;
            });

            var entitiesFoundInOccuppied = scope.occuppiedEntities.find(function(o) {
                return o.id == id;
            });

            var expanded;
            if (entitiesFoundInAvailable) {
                expanded = entitiesFoundInAvailable.expanded;
                _baseRelatedService.customPost('RemoveFromParent/' + oMainConfig.entityName + '/' + scope.baseEntity.id, entitiesFoundInAvailable).then(function(data) {
                    entitiesFoundInAvailable.expanded = expanded;
                    alertify.success('Moved successfully.');
                });
            } else if (entitiesFoundInOccuppied) {
                expanded = entitiesFoundInOccuppied.expanded;
                _baseRelatedService.addToParent(oMainConfig.entityName, scope.baseEntity.id, entitiesFoundInOccuppied).then(function(data) {
                    entitiesFoundInOccuppied.expanded = expanded;
                    entitiesFoundInOccuppied.Dashboards = [];
                    alertify.success('Moved successfully.');
                });
            } else {
                alertify.error('Error. ' + oMainConfig.relatedEntityName + ' not found.');
            }
        });

        var _afterLoad = function() {
            // for (catalog in _baseService.catalogs) {
            //  if (_baseService.catalogs.hasOwnProperty(catalog)) {
            //      scope["the" + catalog + "s"] = _baseService.catalogs[catalog].getAll();
            //  }
            // }
            _afterLoadCallBack();
            $activityIndicator.stopAnimating();
        };

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

                        //Loading Available Related Entities
                        _baseRelatedService.customGet('GetAvailableForEntity/' + oMainConfig.entityName + '/' + $routeParams.id).then(function(data) {
                            scope.baseEntity = angular.copy(theOriginalEntity);
                            scope.occuppiedEntities = scope.baseEntity[('' + oMainConfig.relatedEntityName + 's')];
                            scope.availableEntities = data;
                            _afterLoad();
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
