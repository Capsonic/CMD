'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardCtrl
 * @description
 * # DashboardCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardCtrl', function($scope, dashboardService, $routeParams, departmentService, filterFilter, $q, $timeout, $activityIndicator, metricService, initiativeService) {
    $activityIndicator.startAnimating();
    $scope.options = {
        gridType: 'fit', //fit or scrollVertical or scrollHorizontal
        itemChangeCallback: itemChange,
        margin: 10,
        outerMargin: true,
        draggable: {
            enabled: true,
            stop: eventStop
        },
        resizable: {
            enabled: true,
            stop: eventStop
        },
        swap: true
    };

    $scope.theGridTypes = [{
        gridType: 'fit',
        Value: 'Fit'
    }, {
        gridType: 'scrollVertical',
        Value: 'Scroll Vertical'
    }, {
        gridType: 'scrollHorizontal',
        Value: 'Scroll Horizontal'
    }];


    function itemChange(gridsterItem, scope) {
        scope.department.InfoGridster.cols = scope.department.cols;
        scope.department.InfoGridster.rows = scope.department.rows;
        scope.department.InfoGridster.x = scope.department.x;
        scope.department.InfoGridster.y = scope.department.y;
        scope.department.editMode = true;
        $scope.pendingToSave = $scope.getPendingToSaveCount();
    };

    function eventStop() {};


    var theOriginalEntity;
    var theOnScreenEntity;
    switch (true) {
        case $routeParams.id !== true && $routeParams.id > 0: //Get By id
            dashboardService.loadEntity($routeParams.id).then(function(data) {

                metricService.loadCatalogs().then(function(data) {

                    initiativeService.loadCatalogs().then(function(data) {
                        $activityIndicator.stopAnimating();

                        theOriginalEntity = dashboardService.getById($routeParams.id);
                        if (!theOriginalEntity) {
                            alertify.alert('Nonexistent record.').set('modal', true).set('closable', false);
                            $scope.openingMode = 'error';
                            return;
                        }

                        theOnScreenEntity = angular.copy(theOriginalEntity);
                        $scope.afterLoadData();
                    });

                });

            });
            break;

        default:
            $scope.openingMode = 'error';
            // $activityIndicator.stopAnimating();
            alertify.alert('Verify URL parameters.').set('modal', true).set('closable', false);
            return;
    };

    $scope.getPendingToSaveCount = function() {
        var result = 0;
        for (var i = $scope.baseEntity.Departments.length - 1; i > -1; i--) {
            var current = $scope.baseEntity.Departments[i];
            if (current.editMode) {
                result++;
            }
        }
        return result;
    };

    $scope.afterLoadData = function() {
        $scope.theDashboards = metricService.catalogs.Dashboards.getAll();
        for (var catalog in metricService.catalogs) {
            if (metricService.catalogs.hasOwnProperty(catalog)) {
                $scope["cat" + catalog] = metricService.catalogs[catalog].getAll();
            }
        }

        for (var catalog in initiativeService.catalogs) {
            if (initiativeService.catalogs.hasOwnProperty(catalog)) {
                $scope["cat" + catalog] = initiativeService.catalogs[catalog].getAll();
            }
        }

        var tempCopy = angular.copy(theOnScreenEntity);
        tempCopy.Departments = [];

        $scope.baseEntity = tempCopy

        adaptForGridster(theOnScreenEntity.Departments);

        addOneByOne();

        $scope.pendingToSave = $scope.getPendingToSaveCount();
    };

    var adaptForGridster = function(items) {
        var atLeastOneInfoGridsterMissing = items.filter(function(item) {
            return item.InfoGridster == null;
        });

        if (atLeastOneInfoGridsterMissing.length > 0) {
            items.forEach(function(item) {
                item.initCallback = onItemInit;
                item.InfoGridster = {};
                item.editMode = true;
                item.areGridsterPropertiesMissing = true;
            });
        } else {
            items.forEach(function(item) {
                item.initCallback = onItemInit;
                item.cols = item.InfoGridster.cols;
                item.rows = item.InfoGridster.rows;
                item.x = item.InfoGridster.x;
                item.y = item.InfoGridster.y;
            });
        }
    };

    var addOneByOne = function(index) {
        if (index === undefined) {
            index = 0;
        }
        if (theOnScreenEntity.Departments[index]) {
            $timeout(function() {
                $scope.baseEntity.Departments.push(theOnScreenEntity.Departments[index]);
                addOneByOne(++index);
            }, 50);
        } else {
            return;
        }
    };


    $scope.addItem = function() {
        departmentService.createEntity().then(function(data) {
            var item = data;
            item.areGridsterPropertiesMissing = true;
            item.initCallback = onItemInit;
            $scope.baseEntity.Departments.push(item);
        });
    };

    function onItemInit(a, b, c) {
        for (var i = 0; i < $scope.baseEntity.Departments.length; i++) {
            var current = $scope.baseEntity.Departments[i];
            if (current.areGridsterPropertiesMissing) {
                current.InfoGridster.cols = a.cols;
                current.InfoGridster.rows = a.rows;
                current.InfoGridster.x = a.x;
                current.InfoGridster.y = a.y;

                current.cols = current.InfoGridster.cols;
                current.rows = current.InfoGridster.rows;
                current.x = current.InfoGridster.x;
                current.y = current.InfoGridster.y;

                current.areGridsterPropertiesMissing = false;
                current.initCallback = null;
                // a.item = current;
            }
        }
    };

    function itemsToSave() {
        if ($scope.baseEntity && $scope.baseEntity.Departments) {
            return filterFilter($scope.baseEntity.Departments, genericItemsToBeSaved);
        }
    };

    function genericItemsToBeSaved(item) {
        return item.editMode == true;
    };

    $scope.saveAll = function() {
        var arrItems = itemsToSave();

        var arrPromiseConstructors = [];

        arrItems.forEach(function(item) {
            var promiseConstructor = function() {
                return $scope.saveItem(item);
            }
            arrPromiseConstructors.push(promiseConstructor);
        });

        $q.serial(arrPromiseConstructors).then(function() {
            alertify.success('Dashboard saved successfully.');
            $scope.pendingToSave = $scope.getPendingToSaveCount();
        });
    };

    $scope.saveBaseEntity = function() {
        if (!$scope.baseEntity) {
            return false;
        }
        return dashboardService.save($scope.baseEntity);
    };

    $scope.saveItem = function(item) {
        return departmentService.addToParent('Dashboard', $scope.baseEntity.id, item).then(function(data) {
            item.InfoGridster = data.InfoGridster;
            item.editMode = false;
            $scope.pendingToSave = $scope.getPendingToSaveCount();
        });
    };


    $scope.modePresentation = function() {
        if (screenfull.enabled) {
            screenfull.request(angular.element('#fullscreenMe')[0]);
            angular.element('.Dashboard').css('top', 0);
        }
    };

    if (screenfull.enabled) {
        document.addEventListener(screenfull.raw.fullscreenchange, function() {
            $scope.isFullScreen = screenfull.isFullscreen;
            if (screenfull.isFullscreen) {

                angular.element('.Dashboard').css('top', 0);
            } else {
                angular.element('.Dashboard').css('top', 50);
            }
        });
    }

    $scope.metricToSave = {};
    $scope.initiativeToSave = {};
    $scope.departmentToSave = {};

    $scope.saveMetric = function(metric) {
        $activityIndicator.startAnimating();
        metricService.save(metric).then(function(data) {
            angular.copy(metric, $scope.selectedMetric);
            angular.element('#modal-metricToSave').off('hidden.bs.modal');
            angular.element('#modal-metricToSave').modal('hide');
            $scope.pendingToSave = $scope.getPendingToSaveCount();
            $activityIndicator.stopAnimating();
        });
    };

    $scope.saveInitiative = function(initiative) {
        $activityIndicator.startAnimating();
        initiativeService.save(initiative).then(function(data) {
            angular.copy(initiative, $scope.selectedInitiative);
            angular.element('#modal-initiativeToSave').off('hidden.bs.modal');
            angular.element('#modal-initiativeToSave').modal('hide');
            $scope.pendingToSave = $scope.getPendingToSaveCount();
            $activityIndicator.stopAnimating();
        });

    };


    $scope.getHiddenMetrics = function() {
        if ($scope.departmentToSave.Metrics) {
            return $scope.departmentToSave.Metrics.filter(function(metric) {
                return $scope.isHiddenForCurrentDashboard(metric);
            });
        }
    };
    $scope.getHiddenInitiatives = function() {
        if ($scope.departmentToSave.Initiatives) {
            return $scope.departmentToSave.Initiatives.filter(function(initiative) {
                return $scope.isHiddenForCurrentDashboard(initiative);
            });

        }
    };
    $scope.isHiddenForCurrentDashboard = function(metricOrInitiative) {
        if (metricOrInitiative.HiddenForDashboards) {
            var hiddenDashboards = metricOrInitiative.HiddenForDashboards.split(',');
            var oFound = hiddenDashboards.find(function(id) {
                return id == $scope.baseEntity.id;
            });
            return oFound != undefined;
        } else {
            return false;
        }
    };



    $scope.showMetric = function(metric) {
        for (var i = 0; i < metric.HiddenForDashboardsTags.length; i++) {
            var current = metric.HiddenForDashboardsTags[i];
            if (current.id == $scope.baseEntity.id) {
                metric.HiddenForDashboardsTags.splice(i, 1);
                break;
            }
        }
        metricService.save(metric);
    };
    $scope.showInitiative = function(initiative) {
        for (var i = 0; i < initiative.HiddenForDashboardsTags.length; i++) {
            var current = initiative.HiddenForDashboardsTags[i];
            if (current.id == $scope.baseEntity.id) {
                initiative.HiddenForDashboardsTags.splice(i, 1);
                break;
            }
        }
        initiativeService.save(initiative);
    };

    $scope.loadDashboardsTags = function($query, currentList) {
        return $scope.theDashboards.filter(function(item) {
            return item.Value.toLowerCase().indexOf($query.toLowerCase()) != -1;
        });
    };
    $scope.on_dashboardTag_Added = function(tagAdded, metric) {
        // metric.HiddenForDashboardsTags = [tagAdded]
    };
});
