'use strict';

/**
 * @ngdoc service
 * @name mainApp.listController
 * @description
 * # listController
 * Factory in the mainApp.
 */
angular.module('mainApp').factory('listController', function($log, $activityIndicator) {
    var log = $log;

    return function(oMainConfig) {

        //INIT CONFIG/////////////////////////////////////
        var scope = oMainConfig.scope;

        var _baseService = oMainConfig.baseService;

        if (!oMainConfig.entityName) {
            oMainConfig.entityName = '';
        }

        var _modalName = oMainConfig.modalName;
        if (!_modalName) {
            _modalName = '';
        }

        //After Load callback
        var _afterLoadCallBack = oMainConfig.afterLoad;
        if (!_afterLoadCallBack || typeof _afterLoadCallBack != "function") {
            _afterLoadCallBack = function() {};
        }

        //After create entity callback
        var _afterCreateCallBack = oMainConfig.afterCreate;
        if (!_afterCreateCallBack || typeof _afterCreateCallBack != "function") {
            _afterCreateCallBack = function() {};
        }



        //END CONFIG/////////////////////////////////////


        //SCOPE----------------------------------------------------------------------------
        //let's use normal variables (without underscore) so they can be
        //accessed in view normally
        scope.listTitle = oMainConfig.entityName != '' ? oMainConfig.entityName + 's' : '';
        scope.search = {};
        scope.remove = function(oEntity, arrBelongingList) {
            alertify.confirm(
                'Are you sure you want to delete a ' + oMainConfig.entityName + '?',
                function() {
                    scope.$apply(function() {
                        $activityIndicator.startAnimating();
                        _baseService.remove(oEntity, arrBelongingList).then(function(data) {
                            $activityIndicator.stopAnimating();
                            arrBelongingList = _baseService.getAll();
                        }, function() {
                            $activityIndicator.stopAnimating();
                        });
                    });
                });
        };
        scope.take = function(objToTake, toUser) {
            _baseService.take(objToTake, toUser).then(function(data) {
                objToTake.assignedTo = toUser.Value;
                objToTake.AssignationMade = false;
            });
        };
        scope.checkItem = function(bSelected) {
            bSelected ? ++scope.selectedCount : --scope.selectedCount;
        };
        scope.deleteSelected = function(arrBelongingList) {
            if (scope.selectedCount > 0) {
                alertify.confirm(
                    'Are you sure you want to delete all entities selected?',
                    function() {
                        scope.$apply(function() {
                            _baseService.removeSelected(arrBelongingList).then(function(data) {
                                arrBelongingList = _baseService.getAll();
                                scope.selectedCount = _getSelectedCount();
                            });
                        });
                    });
            }
        };
        scope.create = function() {
            $activityIndicator.startAnimating();
            _baseService.createEntity().then(function(data) {
                scope.itemToSave = data;
                scope.modeSave = 'Create';
                _afterCreateCallBack(data);
                angular.element('#' + _modalName).modal('show');
                $activityIndicator.stopAnimating();
            });
        };

        //Updating items:*******************************
        scope.addQty = 1;
        scope.addItems = function(addQty, arrBelongingList) {
            $activityIndicator.startAnimating();
            _baseService.addBatch(addQty, arrBelongingList).then(function(data) {
                scope.selectedCount = _getSelectedCount();
                $activityIndicator.stopAnimating();
            });
        };
        scope.saveItem = function(oItem) {
            $activityIndicator.startAnimating();
            _baseService.save(oItem).then(function(data) {
                scope.selectedCount = _getSelectedCount();
                $activityIndicator.stopAnimating();
            });
        };
        scope.saveModal = function() {
            $activityIndicator.startAnimating();
            _baseService.save(scope.itemToSave).then(function(data) {
                angular.copy(scope.itemToSave, scope.selectedItem);
                angular.element('#' + _modalName).off('hidden.bs.modal');
                angular.element('#' + _modalName).modal('hide');
                $activityIndicator.stopAnimating();
            });
        };
        scope.on_input_change = function(oItem) {
            oItem.editMode = true;
        };
        scope.on_closeModal = function() {
            if (scope.modeSave != 'Create') {
                var originalItem = _baseService.getById(scope.itemToSave.id);
                angular.copy(originalItem, scope.selectedItem);
            }
        };
        scope.undoItem = function(oItem) {
            var originalItem = _baseService.getById(oItem.id);
            angular.copy(originalItem, oItem);
        };
        //Popup**************************************************************************
        scope.itemToSave = {};
        scope.dialogTitle = '';
        scope.openEditItem = function(theItem) {
            scope.modeSave = 'Update';
            scope.selectedItem = theItem;
            scope.itemToSave = angular.copy(theItem);
            angular.element('#' + _modalName).modal('show');
            angular.element('#' + _modalName).off('hidden.bs.modal').on('hidden.bs.modal', function(e) {
                scope.$apply(function() {
                    scope.on_closeModal();
                });
            })
        };
        //End Popup**********************************************************************

        //end scope----------------------------------------

        var _getSelectedCount = function(arrList) {
            var result = 0;
            var arrItems = arrList || [];
            arrItems.forEach(function(oEntity) {
                if (oEntity.checked) {
                    result++;
                }
            });
            return result;
        };

        //todo unselectAll and filters should be done in listcontroller
        scope.unselectAll = function(arrList) {
            var arrItems = arrList || [];
            arrItems.forEach(function(oEntity) {
                oEntity.checked = false;
            });
            scope.selectedCount = 0;
        };

        var _afterLoad = function() {
            // for (catalog in _baseService.catalogs) {
            //  if (_baseService.catalogs.hasOwnProperty(catalog)) {
            //      scope["the" + catalog + "s"] = _baseService.catalogs[catalog].getAll();
            //  }
            // }
            _afterLoadCallBack();
            scope.selectedCount = _getSelectedCount();
            $activityIndicator.stopAnimating();
        };

        var _load = function() {
            $activityIndicator.startAnimating();
            alertify.closeAll();
            _baseService.loadAll(true).then(function(data) {
                scope.$evalAsync(function() {
                    _afterLoad();
                });
            });
        };

        // Public baseController API:////////////////////////////////////////////////////////////
        var oAPI = {
            load: _load,
            // unselectAll: _unselectAll
        };
        return oAPI;
    };
});
