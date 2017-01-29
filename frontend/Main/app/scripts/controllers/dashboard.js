'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardCtrl
 * @description
 * # DashboardCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardCtrl', function($scope, dashboardService, $routeParams, departmentService, filterFilter, $q, $timeout, $activityIndicator, metricService, initiativeService, metricYearService, $interval) {
    $activityIndicator.startAnimating();
    $scope.isLoading = true;
    $scope.isDesignMode = false;
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
    var dashboardKey = null;

    var load = function() {
        dashboardService.loadEntity(dashboardKey).then(function(data) {

            metricYearService.loadCatalogs().then(function(data) {

                initiativeService.loadCatalogs().then(function(data) {
                    $activityIndicator.stopAnimating();

                    theOriginalEntity = dashboardService.getById(dashboardKey);
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
    }

    switch (true) {
        case $routeParams.id !== true && $routeParams.id > 0: //Get By id
            dashboardKey = $routeParams.id;
            load();
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
        $scope.theDashboards = metricYearService.catalogs.Dashboards.getAll();
        for (var catalog in metricYearService.catalogs) {
            if (metricYearService.catalogs.hasOwnProperty(catalog)) {
                $scope['cat' + catalog] = metricYearService.catalogs[catalog].getAll();
            }
        }

        for (var catalog in initiativeService.catalogs) {
            if (initiativeService.catalogs.hasOwnProperty(catalog)) {
                $scope['cat' + catalog] = initiativeService.catalogs[catalog].getAll();
            }
        }

        fulfillSortingInfo(theOnScreenEntity);

        adaptForGridster(theOnScreenEntity.Departments);

        if (!$scope.baseEntity || !angular.equals(theOnScreenEntity, $scope.baseEntity)) {
            var tempCopy = angular.copy(theOnScreenEntity);
            tempCopy.Departments = [];

            $scope.baseEntity = tempCopy;


            $scope.availableYears = getAvailableYears(theOnScreenEntity.Departments);
            if (!$scope.dashboardYear) {
                $scope.dashboardYear = $scope.availableYears.slice(-1)[0];
            } else {
                if ($scope.availableYears.indexOf($scope.dashboardYear) == -1) {
                    $scope.dashboardYear = null;
                }
            }

            //addOneByOne must be at the end of this function
            //coz it uses timeout to run more code.
            addOneByOne();
        }

    };

    function fulfillSortingInfo(oDashboard) {
        oDashboard.Departments.forEach(function(oDepartment) {
            var sortSequenceMetrics = 0;
            var sortSequenceInitiatives = 0;

            oDepartment.Metrics.sort(function(a, b) {
                if (a.InfoSort && b.InfoSort) {
                    return a.InfoSort.Sort_Sequence - b.InfoSort.Sort_Sequence;
                }
            }).forEach(function(oMetric) {
                if (!oMetric.InfoSort) {
                    oMetric.InfoSort = {
                        SortKey: -1,
                        Sort_Sequence: sortSequenceMetrics++
                    };
                    oDepartment.editMode = true;
                }
            });

            oDepartment.Initiatives.sort(function(a, b) {
                if (a.InfoSort && b.InfoSort) {
                    return a.InfoSort.Sort_Sequence - b.InfoSort.Sort_Sequence;
                }
            }).forEach(function(oInitiative) {
                if (!oInitiative.InfoSort) {
                    oInitiative.InfoSort = {
                        SortKey: -1,
                        Sort_Sequence: sortSequenceMetrics++
                    };
                    oDepartment.editMode = true;
                }
            });


        });


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
            $timeout(function() {
                $scope.isLoading = false;
                $scope.pendingToSave = $scope.getPendingToSaveCount();
                $scope.$broadcast('dashboardLoaded');
            }, 500);
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
            item.Metrics.forEach(function(oMetric) {
                var oFound = data.Metrics.find(function(bMetric) {
                    return oMetric.id == bMetric.id;
                })
                oMetric.InfoSort = oFound.InfoSort;
            });

            item.Initiatives.forEach(function(oInitiative) {
                var oFound = data.Initiatives.find(function(bInitiative) {
                    return oInitiative.id == bInitiative.id;
                })
                oInitiative.InfoSort = oFound.InfoSort;
            });

            item.editMode = false;
            $scope.pendingToSave = $scope.getPendingToSaveCount();
        });
    };

    $scope.modePresentation = function() {
        $scope.isDesignMode = !$scope.isDesignMode;
    };

    $scope.fullScreen = function() {
        if (screenfull.enabled) {
            screenfull.request(angular.element('#fullscreenMe')[0]);
            // angular.element('.Dashboard').css('top', 0);
        }
    };

    if (screenfull.enabled) {
        document.addEventListener(screenfull.raw.fullscreenchange, function() {
            $scope.isDesignMode = !screenfull.isFullscreen;
            $scope.isFullScreen = screenfull.isFullscreen;
            $scope.options.draggable.enabled = !screenfull.isFullscreen;
            // if (screenfull.isFullscreen) {
            //     angular.element('.Dashboard').css('top', 0);
            // } else {
            //     angular.element('.Dashboard').css('top', 50);
            // }
        });
    }

    $scope.metricToSave = null;
    $scope.initiativeToSave = {};
    $scope.departmentToSave = {};

    $scope.saveMetric = function(metric) {
        $activityIndicator.startAnimating();
        metricService.save(metric).then(function(data) {
            angular.copy(data, $scope.selectedMetric);
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
        metricService.save(metric).then(function() {
            $scope.$broadcast('showMetric', metric);
        });

    };
    $scope.showInitiative = function(initiative) {
        for (var i = 0; i < initiative.HiddenForDashboardsTags.length; i++) {
            var current = initiative.HiddenForDashboardsTags[i];
            if (current.id == $scope.baseEntity.id) {
                initiative.HiddenForDashboardsTags.splice(i, 1);
                break;
            }
        }
        initiativeService.save(initiative).then(function() {
            $scope.$broadcast('showInitiative', initiative);
        });
    };

    $scope.loadDashboardsTags = function($query, currentList) {
        return $scope.theDashboards.filter(function(item) {
            return item.Value.toLowerCase().indexOf($query.toLowerCase()) != -1;
        });
    };
    $scope.on_dashboardTag_Added = function(tagAdded, metric) {
        metric.editoMode = true;
        // metric.HiddenForDashboardsTags = [tagAdded]
    };

    $scope.on_change_sortingMetricsOrInitiatives = function(newValue) {
        $scope.options.draggable.enabled = !newValue;
    };

    $scope.pendingToSave = 0;

    $scope.saveAllMetricHistorys = function() {
        $scope.$broadcast('SaveMetricHistory');
    };

    $scope.RemoveMetricYear = function(metricYear) {
        $scope.$broadcast('DeleteMetricYear', metricYear);
    };

    $scope.$on('HideMetricHistory', function() {
        load();
    });

    $scope.on_input_change = function(oItem) {
        oItem.editMode = true;
    };

    $scope.$on('RefreshMetric', function(scope, oMetric) {
        load();
    });

    var getAvailableYears = function(departments) {
        var result = [];
        if (departments) {
            departments.forEach(function(department) {
                department.Metrics.forEach(function(metric) {
                    metric.MetricYears.forEach(function(year) {
                        result.push(year.Value);
                    });
                });
            });
        }

        result = removeDuplicates(result);

        result.sort(function(a, b) {
            return a - b;
        });

        return result;
    };

    var removeDuplicates = function(arr) {
        var uniqueElements = [];
        jQuery.each(arr, function(i, el) {
            if (jQuery.inArray(jQuery.trim(el), uniqueElements) === -1) uniqueElements.push(jQuery.trim(el));
        });
        return uniqueElements;
    };

    $interval(function() {
        if (!$scope.isDesignMode) {
            load();
        }
    }, 30000, null, false);

});
