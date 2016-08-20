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
        if (scope.department.EF_State != 1) {
            scope.department.EF_State = 2;
        }
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
    };

    var adaptForGridster = function(items) {
        var atLeastOneInfoGridsterMissing = items.filter(function(item) {
            return item.InfoGridster == null;
        });

        if (atLeastOneInfoGridsterMissing.length > 0) {
            items.forEach(function(item) {
                item.initCallback = onItemInit;
                item.InfoGridster = {};
                item.EF_State = 1; //1 means add, but we use addToParent which is going to add only if it is not added
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
        return item.EF_State > 0;
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

    $scope.saveMetric = function(metric) {
        $activityIndicator.startAnimating();
        metricService.save(metric).then(function(data) {
            angular.copy(metric, $scope.selectedMetric);
            angular.element('#modal-metricToSave').off('hidden.bs.modal');
            angular.element('#modal-metricToSave').modal('hide');
            $activityIndicator.stopAnimating();
        });
    };

    $scope.saveInitiative = function(initiative) {
        $activityIndicator.startAnimating();
        initiativeService.save(initiative).then(function(data) {
            angular.copy(initiative, $scope.selectedInitiative);
            angular.element('#modal-initiativeToSave').off('hidden.bs.modal');
            angular.element('#modal-initiativeToSave').modal('hide');
            $activityIndicator.stopAnimating();
        });

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
