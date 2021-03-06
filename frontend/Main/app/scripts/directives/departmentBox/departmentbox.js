'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:departmentBox
 * @description
 * # departmentBox
 */
angular.module('mainApp').directive('departmentBox', function($timeout, metricService, initiativeService, $rootScope) {
    return {
        templateUrl: 'views/departmentbox.html',
        restrict: 'E',
        scope: {
            department: '='
        },
        compile: function() {
            return {
                pre: function preLink(scope, iElement, iAttrs) {
                    var trendModal = angular.element('body #modal-trend');
                    if (trendModal.length == 0) {
                        angular.element('#fullscreenMe').append(
                            '<div class="modal fade" id="modal-trend">' +
                            '                                <div class="modal-dialog">' +
                            '                                    <div class="modal-content">' +
                            '                                        <div class="modal-header">' +
                            '                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
                            '                                            <h4 class="modal-title">Trend Info</h4>' +
                            '                                        </div>' +
                            '                                        <div class="modal-body">' +
                            '                                            <table class="table">' +
                            '                                                <tbody>' +
                            '                                                    <tr>' +
                            '                                                        <td>' +
                            '                                                            <button class="btn-xs btn-success" style="padding:1px 1em;font-size: 1em;height:5em;width:3em;"><span class="glyphicon glyphicon-arrow-up"></span></button>' +
                            '                                                        </td>' +
                            '                                                        <td style="padding-top: 1.3em;">Indicator meets the objective and is improving.</td>' +
                            '                                                    </tr>' +
                            '                                                    <tr>' +
                            '                                                        <td>' +
                            '                                                            <button class="btn-xs btn-default" style="padding:1px 1em;font-size: 1em;height:5em;width:3em;"><span class="glyphicon glyphicon-arrow-down"></span></button>' +
                            '                                                        </td>' +
                            '                                                        <td style="padding-top: 1.3em;">Indicator meets the objective but the value is getting worse.</td>' +
                            '                                                    </tr>' +
                            '                                                    <tr>' +
                            '                                                        <td>' +
                            '                                                            <button class="btn-xs btn-warning" style="padding:1px 1em;font-size: 1em;height:5em;width:3em;"><span class="glyphicon glyphicon-arrow-up"></span></button>' +
                            '                                                        </td>' +
                            '                                                        <td style="padding-top: 1.3em;">Indicator does not meet the objective, but shows improvement.</td>' +
                            '                                                    </tr>' +
                            '                                                    <tr>' +
                            '                                                        <td>' +
                            '                                                            <button class="btn-xs btn-danger" style="padding:1px 1em;font-size: 1em;height:5em;width:3em;"><span class="glyphicon glyphicon-arrow-down"></span></button>' +
                            '                                                        </td>' +
                            '                                                        <td style="padding-top: 1.3em;">Indicator does not meet the objective and the value is getting worse.</td>' +
                            '                                                    </tr>' +
                            '                                                </tbody>' +
                            '                                            </table>' +
                            '                                        </div>' +
                            '                                        <div class="modal-footer">' +
                            '                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
                            '                                        </div>' +
                            '                                    </div>' +
                            '                                </div>' +
                            '                            </div>'
                        );
                    }
                },
                post: function postLink(scope, element, attrs) {
                    //element.hide();

                    var sizer = element.find('.Sizer');
                    var input = element.find('.Sizer.form-control');
                    sizer.on('mousedown', function(e) {
                        // e.preventDefault();
                        e.stopPropagation();
                        $timeout(function() {
                            input[0].focus();
                        });
                    });


                    $timeout(function() {
                        scope.$watch(function() {
                            return element.parent().width() + element.parent().height() + angular.element(window).width() + angular.element(window).height();
                        }, function() {
                            if (scope.$parent.$parent.isLoading) {
                                element.hide();
                                resetSizes();
                                element.show();
                            }
                        });
                    });

                    function resetSizes() {
                        $timeout(function() {
                            element.find('.initiativeMaxWidth').css('max-width', element.parent().width() * .6);
                            element.find('.metricMaxWidth').css('max-width', element.parent().width() * .75);
                        }, 200);
                    };

                    function createOpenTips() {
                        $timeout(function() {
                            scope.department.Metrics.forEach(function(oMetric) {
                                var el = $(element).find('#' + oMetric.id + '.MetricLine');
                                if (el.length > 0) {
                                    new Opentip(el, oMetric.Description || '', oMetric.Title || '', {
                                        delay: 0,
                                        fixed: false,
                                        style: null, //, 'glass'//'dark', 'alert'
                                        offset: [7, 7]
                                    });
                                }
                            });

                            scope.department.Initiatives.forEach(function(oInitiative) {
                                var el = $(element).find('#' + oInitiative.id + '.InitiativeLine');
                                if (el.length > 0) {
                                    new Opentip(el, oInitiative.Description || '', oInitiative.Title || '', {
                                        delay: 0,
                                        fixed: false,
                                        style: null, //, 'glass'//'dark', 'alert'
                                        offset: [7, 7]
                                    });
                                }
                            });
                        }, 300);
                    };

                    scope.theWidth = function() {
                        return element.parent().width();
                    };

                    var currentDashboard = function() {
                        return scope.$parent.baseEntity;
                    };

                    // scope.getMetrics = function() {
                    //     return scope.department.Metrics.filter(function(metric) {
                    //         return scope.isHiddenForCurrentDashboard(metric) == false;
                    //     }).sort(function(a, b) {
                    //         return a.InfoSort.Sort_Sequence - b.InfoSort.Sort_Sequence;
                    //     });
                    // };
                    // scope.getInitiatives = function() {
                    //     return scope.department.Initiatives.filter(function(initiative) {
                    //         return scope.isHiddenForCurrentDashboard(initiative) == false;
                    //     });
                    // };

                    scope.isShownForCurrentDashboard = function(metricOrInitiative) {
                        if (metricOrInitiative.HiddenForDashboards) {
                            var hiddenDashboards = metricOrInitiative.HiddenForDashboards.split(',');
                            var oFound = hiddenDashboards.find(function(id) {
                                return id == currentDashboard().id;
                            });

                            if (oFound != undefined) {
                                return false;
                            }
                        }

                        if (metricOrInitiative.AAA_EntityName == 'Metric') {
                            var hasCurrentYear = metricService.getMetricYearByYear(metricOrInitiative, scope.$parent.dashboardYear);
                            if (!hasCurrentYear) {
                                return false;
                            }
                        }

                        return true;
                    };

                    scope.hideMetric = function(metric) {
                        if (!metric.HiddenForDashboardsTags) {
                            metric.HiddenForDashboardsTags = [];
                        }
                        metric.HiddenForDashboardsTags.push(currentDashboard());
                        metricService.save(metric);
                    };
                    scope.hideInitiative = function(initiative) {
                        if (!initiative.HiddenForDashboardsTags) {
                            initiative.HiddenForDashboardsTags = [];
                        }
                        initiative.HiddenForDashboardsTags.push(currentDashboard());
                        initiativeService.save(initiative);
                    };


                    scope.editMetric = function(metric) {
                        metricService.loadEntity(metric.id).then(function(data) {
                            angular.copy(data, metric);
                            scope.$parent.$parent.selectedMetric = metric;
                            scope.$parent.$parent.metricToSave = angular.copy(metric);
                            // angular.copy(data, scope.$parent.$parent.metricToSave);

                            angular.element('#modal-MetricHistory').off('shown.bs.modal').on('shown.bs.modal', function(e) {
                                scope.$apply(function() {
                                    $rootScope.$broadcast('ShowMetricHistory');
                                });
                            }).off('hidden.bs.modal').on('hidden.bs.modal', function(e) {
                                scope.$apply(function() {
                                    $rootScope.$broadcast('HideMetricHistory');
                                });
                            }).modal('show');

                            // angular.element('#modal-metricToSave').modal('show');
                            // scope.$parent.$parent.resetMetricHistoryToAdd();
                        });
                    };

                    scope.editInitiative = function(initiative) {
                        scope.$parent.$parent.selectedInitiative = initiative;
                        angular.element('#modal-initiativeToSave').modal('show');
                        angular.copy(initiative, scope.$parent.initiativeToSave);
                    };

                    scope.showHidden = function(department) {
                        scope.$parent.$parent.selectedDepartment = department;
                        angular.element('#modal-showHidden').modal('show');
                        angular.copy(department, scope.$parent.departmentToSave);
                    };

                    scope.departmentToUpdate = function(dep) {
                        for (var i = 0; i < scope.$parent.$parent.baseEntity.Departments.length; i++) {
                            var current = scope.$parent.$parent.baseEntity.Departments[i];
                            if (current.id == dep.id) {
                                current.editMode = true;
                                scope.$parent.$parent.pendingToSave = scope.$parent.$parent.getPendingToSaveCount();
                                break;
                            }
                        }
                    };

                    scope.$on('metrics-bag.drop-model', function(e, el, source) {
                        var sortSequence = 0;
                        scope.department.Metrics.forEach(function(oMetric) {
                            oMetric.InfoSort.Sort_Sequence = sortSequence++;
                        });

                        for (var i = 0; i < scope.$parent.$parent.baseEntity.Departments.length; i++) {
                            var current = scope.$parent.$parent.baseEntity.Departments[i];
                            if (current.id == scope.department.id) {
                                current.editMode = true;
                                scope.$parent.$parent.pendingToSave = scope.$parent.$parent.getPendingToSaveCount();
                                break;
                            }
                        }
                    });

                    scope.$on('initiatives-bag.drop-model', function(e, el, source) {
                        var sortSequence = 0;
                        scope.department.Initiatives.forEach(function(oMetric) {
                            oMetric.InfoSort.Sort_Sequence = sortSequence++;
                        });

                        for (var i = 0; i < scope.$parent.$parent.baseEntity.Departments.length; i++) {
                            var current = scope.$parent.$parent.baseEntity.Departments[i];
                            if (current.id == scope.department.id) {
                                current.editMode = true;
                                scope.$parent.$parent.pendingToSave = scope.$parent.$parent.getPendingToSaveCount();
                                break;
                            }
                        }
                    });

                    scope.$on('showMetric', function(event, metric) {
                        scope.department.Metrics.forEach(function(oMetric) {
                            if (oMetric.id == metric.id) {
                                oMetric.HiddenForDashboardsTags = metric.HiddenForDashboardsTags;
                                oMetric.HiddenForDashboards = metric.HiddenForDashboards;
                            }
                        });
                    });

                    scope.$on('showInitiative', function(event, initiative) {
                        scope.department.Initiatives.forEach(function(oInitiative) {
                            if (oInitiative.id == initiative.id) {
                                oInitiative.HiddenForDashboardsTags = initiative.HiddenForDashboardsTags;
                                oInitiative.HiddenForDashboards = initiative.HiddenForDashboards;
                            }
                        });
                    });

                    scope.getMetricHistoryByYear = function(oMetric) {
                        return metricService.getLastMetricHistoryForYear(oMetric, scope.$parent.dashboardYear);
                    };

                    scope.getMetricYearByYear = function(oMetric) {
                        return metricService.getMetricYearByYear(oMetric, scope.$parent.dashboardYear);
                    };

                    scope.getMetricStyle = function(oMetric) {

                        var oYear = metricService.getMetricYearByYear(oMetric, scope.$parent.dashboardYear);
                        var oHistory = scope.getMetricHistoryByYear(oMetric);

                        if (oHistory && oYear.GoalValue != null && oYear.GoalValue != undefined) {
                            switch (oYear.ComparatorMethodKey) {
                                case 1: //Greater Than
                                    if (oHistory.CurrentValue < oYear.GoalValue) {
                                        return 'GoingBad';
                                    }
                                    break;
                                case 2: //Less Than
                                    if (oHistory.CurrentValue > oYear.GoalValue) {
                                        return 'GoingBad';
                                    }
                                    break;
                                case 3: //Around Than
                                    var maxValue = oYear.GoalValue + oYear.AroundRangeValue;
                                    var minValue = oYear.GoalValue - oYear.AroundRangeValue;
                                    if (oHistory.CurrentValue < minValue || oHistory.CurrentValue > maxValue) {
                                        return 'GoingBad';
                                    }
                                    break;
                                default:
                                    return '';
                            }
                            return 'GoingWell';
                        }
                        return '';
                    };

                    scope.getInitiativeStyle = function(oInitiative) {
                        if (oInitiative.ProgressValue < 100) {
                            if (oInitiative.ConvertedActualDate < oInitiative.ConvertedDueDate) {
                                return 'GoingBad';
                            } else {
                                return ''
                            }
                        } else {
                            if (oInitiative.ConvertedActualDate >= oInitiative.ConvertedDueDate) {
                                return 'GoingWell';
                            } else {
                                return ''
                            }
                        }
                    };


                    $timeout(function() {

                        scope.getTrendStyle = function(oMetric) {
                            var container = 'hidden';

                            var oMetricYear = metricService.getMetricYearByYear(oMetric, scope.$parent.dashboardYear);

                            if (oMetricYear) {

                                if (oMetricYear.MetricHistorys && oMetricYear.MetricHistorys.length > 0) {
                                    var trend;
                                    var oLastHistory = oMetricYear.MetricHistorys[oMetricYear.MetricHistorys.length - 1];
                                    var currentStatus = scope.getMetricStyle(oMetric);

                                    var arrowElement = element.find('.MetricBox#' + oMetric.id + ' span');
                                    var btnTrend = element.find('.MetricBox#' + oMetric.id + ' .btn');

                                    if (oMetricYear.MetricHistorys && oMetricYear.MetricHistorys.length > 1) {
                                        var oFirstHistory = oMetricYear.MetricHistorys[oMetricYear.MetricHistorys.length - 2];
                                        var differenceLastMetric, differenceFirstMetric;

                                        switch (oMetricYear.ComparatorMethodKey) {
                                            case 1: //Greater Than
                                                differenceLastMetric = oMetricYear.GoalValue - oLastHistory.CurrentValue;
                                                differenceFirstMetric = oMetricYear.GoalValue - oFirstHistory.CurrentValue;

                                                if (differenceLastMetric > differenceFirstMetric) {
                                                    trend = 'down';
                                                } else {
                                                    trend = 'up';
                                                }

                                                break;
                                            case 2: //Less Than
                                                differenceLastMetric = oMetricYear.GoalValue - oLastHistory.CurrentValue;
                                                differenceFirstMetric = oMetricYear.GoalValue - oFirstHistory.CurrentValue;

                                                if (differenceLastMetric < differenceFirstMetric) {
                                                    trend = 'down';
                                                } else {
                                                    trend = 'up';
                                                }

                                                break;
                                            case 3: //Around Than
                                                if (oMetricYear.GoalValue > oLastHistory.CurrentValue) {
                                                    differenceLastMetric = oMetricYear.GoalValue - oLastHistory.CurrentValue;
                                                } else {
                                                    differenceLastMetric = oLastHistory.CurrentValue - oMetricYear.GoalValue;
                                                }

                                                if (oMetricYear.GoalValue > oFirstHistory.CurrentValue) {
                                                    differenceFirstMetric = oMetricYear.GoalValue - oFirstHistory.CurrentValue;
                                                } else {
                                                    differenceFirstMetric = oFirstHistory.CurrentValue - oMetricYear.GoalValue;
                                                }

                                                if (differenceLastMetric > differenceFirstMetric) {
                                                    trend = 'down';
                                                } else {
                                                    trend = 'up';
                                                }

                                                break;
                                            default:
                                                return '';
                                        }

                                    } else {

                                        if (currentStatus == 'GoingBad') {
                                            trend = 'down';
                                        } else {
                                            trend = 'up';
                                        }

                                    }

                                    if (trend == 'up') {
                                        arrowElement.addClass('glyphicon-arrow-up');
                                        arrowElement.removeClass('glyphicon-arrow-down');
                                    } else {
                                        arrowElement.addClass('glyphicon-arrow-down');
                                        arrowElement.removeClass('glyphicon-arrow-up');
                                    }

                                    btnTrend.removeClass('btn-success');
                                    btnTrend.removeClass('btn-default');
                                    btnTrend.removeClass('btn-warning');
                                    btnTrend.removeClass('btn-danger');

                                    switch (true) {
                                        case currentStatus == 'GoingWell' && trend == 'up':
                                            btnTrend.addClass('btn-success');
                                            container = 'TrendWellAndUp';
                                            break;
                                        case currentStatus == 'GoingWell' && trend == 'down':
                                            btnTrend.addClass('btn-default');
                                            container = 'TrendWellButDown';
                                            break;
                                        case currentStatus == 'GoingBad' && trend == 'up':
                                            btnTrend.addClass('btn-warning');
                                            container = 'TrendBadButUp';
                                            break;
                                        case currentStatus == 'GoingBad' && trend == 'down':
                                            btnTrend.addClass('btn-danger');
                                            container = 'TrendBadAndDown';
                                            break;
                                    }
                                }
                            }

                            return container;
                        };

                    });

                    scope.showTrendInfo = function() {
                        angular.element('#modal-trend').modal('show');
                    };

                    scope.showMetricHistoryReadOnly = function(oMetric) {
                        metricService.loadEntity(oMetric.id).then(function(data) {
                            angular.copy(data, oMetric);
                            scope.$parent.$parent.selectedMetric = oMetric;
                            scope.$parent.$parent.metricToSave = angular.copy(oMetric);

                            angular.element('#modal-MetricHistory-ReadOnly').off('shown.bs.modal').on('shown.bs.modal', function(e) {
                                scope.$apply(function() {
                                    $rootScope.$broadcast('ShowMetricHistoryReadOnly');
                                });
                            }).modal('show');
                        });
                    };

                    createOpenTips();

                    scope.$on('departmentBoxShown', function() {
                        createOpenTips();
                    });

                    scope.$on('dashboardLoaded', function() {
                        resetSizes();
                    });

                }
            }
        }
    };
});
