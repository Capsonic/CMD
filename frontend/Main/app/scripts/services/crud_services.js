'use strict';

/**
 * @ngdoc service
 * @name mainApp.CRUDServices
 * @description
 * # CRUDServices
 * Service in the mainApp.
 */
angular.module('CMD.CRUDServices', [])

.service('dashboardService', function(crudFactory, $filter, metricService, initiativeService) {

    function getFormattedValue(value, format) {
        if (value != null && value != '') {
            switch (format) {
                case 1: //Numeric
                    return $filter('number')(value, 2);
                case 2: //Currency
                    return '$ ' + $filter('number')(value, 2);
                case 3: //Percentage
                    return $filter('number')(value, 2) + '%';
                default:
                    return value;
            }
        }
        return value;
    };

    function getFormattedEquality(equality) {
        switch (equality) {
            case 1:
                return '>';
            case 2:
                return '<';
            case 3:
                return '<>';
            default:
                return '';
        }
    };

    function getMetricsBasisValue(id) {
        switch (id) {
            case 1:
                return 'Hourly';
            case 2:
                return 'Daily';
            case 3:
                return 'Weekly';
            case 4:
                return 'Monthly';
            case 5:
                return 'Bimonthy';
            case 6:
                return 'Quarterly';
            case 7:
                return 'Biannual';
            case 8:
                return 'Yearly';
            default:
                return '';
        }
    }

    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Dashboard',

        catalogs: ['Users'],

        adapter: function(theEntity, self) {

            theEntity.OwnersTags = getDashboardsFromIds(theEntity.Owners, self.catalogs.Users);

            theEntity.Departments.forEach(function(department) {

                department.Metrics.forEach(function(metric) {
                    metricService.adapt(metric);
                });
                department.Initiatives.forEach(function(initiative) {
                    initiative.ConvertedActualDate = initiative.ActualDate ? moment(initiative.ActualDate, moment.ISO_8601).toDate() : null;
                    initiative.ConvertedDueDate = initiative.DueDate ? moment(initiative.DueDate, moment.ISO_8601).toDate() : null;
                    initiative.HiddenForDashboardsTags = getDashboardsFromIds(initiative.HiddenForDashboards, initiativeService.catalogs.Dashboards);
                });

            });
            return theEntity;
        },

        adapterIn: function(theEntity) {

        },

        adapterOut: function(theEntity, self) {
            theEntity.Owners = adaptUsersTags(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;

}).service('departmentService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Department',

        catalogs: [],

        adapter: function(theEntity) {
            return theEntity;
        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {
            //self.validate(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;

}).service('metricService', function(crudFactory, $filter) {

    function getFormattedValue(value, format) {
        if (value != null && value != '') {
            switch (format) {
                case 1: //Numeric
                    return $filter('number')(value, 2);
                case 2: //Currency
                    return '$ ' + $filter('number')(value, 2);
                case 3: //Percentage
                    return $filter('number')(value, 2) + '%';
                default:
                    return value;
            }
        }
        return value;
    };

    function getFormattedEquality(equality) {
        switch (equality) {
            case 1:
                return '>';
            case 2:
                return '<';
            case 3:
                return '<>';
            default:
                return '';
        }
    };

    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Metric',

        catalogs: ['ComparatorMethod', 'MetricFormat', 'MetricBasis', 'Dashboards'],

        adapter: function(theEntity, self) {
            theEntity.FormattedCurrentValue = getFormattedValue(theEntity.CurrentValue, theEntity.FormatKey);
            theEntity.FormattedGoalValue = getFormattedValue(theEntity.GoalValue, theEntity.FormatKey);
            theEntity.BasisValue = self.catalogs.MetricBasis.getById(theEntity.BasisKey).Value;
            theEntity.EqualityValue = getFormattedEquality(theEntity.ComparatorMethodKey);
            theEntity.HiddenForDashboardsTags = getDashboardsFromIds(theEntity.HiddenForDashboards, self.catalogs.Dashboards);


            //Metric Years Sort
            theEntity.MetricYears.sort(function(a, b) {
                return a.Value - b.Value;
            });

            theEntity.SelectedMetricYear = null;
            if (theEntity.MetricYears.length > 0) {
                theEntity.SelectedMetricYear = theEntity.MetricYears[0];
            }


            //Metric History
            /*theEntity.MetricHistorys.forEach(function(item) {
                item = crudInstance.adaptMetricHistory(item, theEntity);
            });

            theEntity.MetricHistorys.sort(function(a, b) {
                return a.ConvertedMetricDate - b.ConvertedMetricDate;
            });

            theEntity.LastMetrics = theEntity.MetricHistorys.slice(-1);*/

            return theEntity;
        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {
            theEntity.HiddenForDashboards = adaptHiddenForDashboards(theEntity);

            // theEntity.MetricHistorys.forEach(function(item) {
            //     item.MetricDate = item.ConvertedMetricDate ? item.ConvertedMetricDate.toJSON() : null;
            // });
        },

        dependencies: [

        ]
    });

    crudInstance.getFormattedValue = getFormattedValue;
    crudInstance.getFormattedEquality = getFormattedEquality;

    crudInstance.adaptMetricHistory = function(metricHistory, metric) {
        metricHistory.FormattedCurrentValue = getFormattedValue(metricHistory.CurrentValue, metric.FormatKey);
        metricHistory.FormattedGoalValue = getFormattedValue(metricHistory.GoalValue, metric.FormatKey);
        metricHistory.EqualityValue = getFormattedEquality(metric.ComparatorMethodKey);
        metricHistory.ConvertedMetricDate = metricHistory.MetricDate ? moment(metricHistory.MetricDate, moment.ISO_8601).toDate() : null;
        return metricHistory;
    };

    return crudInstance;

}).service('metricHistoryService', function(crudFactory, $filter) {

    function getFormattedValue(value, format) {
        if (value != null && value != '') {
            switch (format) {
                case 1: //Numeric
                    return $filter('number')(value, 2);
                case 2: //Currency
                    return '$ ' + $filter('number')(value, 2);
                case 3: //Percentage
                    return $filter('number')(value, 2) + '%';
                default:
                    return value;
            }
        }
        return value;
    };

    function getFormattedEquality(equality) {
        switch (equality) {
            case 1:
                return '>';
            case 2:
                return '<';
            case 3:
                return '<>';
            default:
                return '';
        }
    };

    var crudInstance = new crudFactory({

        entityName: 'MetricHistory',

        catalogs: [],

        adapter: function(theEntity, self) {

            theEntity.FormattedCurrentValue = getFormattedValue(theEntity.CurrentValue, theEntity.FormatKey);
            theEntity.FormattedGoalValue = getFormattedValue(theEntity.GoalValue, theEntity.FormatKey);
            theEntity.EqualityValue = getFormattedEquality(theEntity.ComparatorMethodKey);
            theEntity.ConvertedMetricDate = theEntity.MetricDate ? moment(theEntity.MetricDate, moment.ISO_8601).toDate() : null;

            theEntity.ConvertedMetricYear = theEntity.ConvertedMetricDate ? theEntity.ConvertedMetricDate.getFullYear() : null;
            //theEntity.ConvertedMetricYear = theEntity.ConvertedMetricDate ? theEntity.ConvertedMetricDate.getFullYear() : null;
            theEntity.ConvertedMetricDay = theEntity.ConvertedMetricDate ? theEntity.ConvertedMetricDate.getDate() : null;
            return theEntity;

        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {
            theEntity.MetricDate = theEntity.ConvertedMetricDate ? theEntity.ConvertedMetricDate.toJSON() : null;
        },

        dependencies: [

        ]
    });

    crudInstance.getFormattedValue = getFormattedValue;
    crudInstance.getFormattedEquality = getFormattedEquality;

    return crudInstance;

}).service('initiativeService', function(crudFactory) {

    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Initiative',

        catalogs: ['Dashboards'],

        adapter: function(theEntity, self) {
            theEntity.ConvertedActualDate = theEntity.ActualDate ? moment(theEntity.ActualDate, moment.ISO_8601).toDate() : null;
            theEntity.ConvertedDueDate = theEntity.DueDate ? moment(theEntity.DueDate, moment.ISO_8601).toDate() : null;
            theEntity.HiddenForDashboardsTags = getDashboardsFromIds(theEntity.HiddenForDashboards, self.catalogs.Dashboards);
            return theEntity;
        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {
            theEntity.DueDate = theEntity.ConvertedDueDate ? theEntity.ConvertedDueDate.toJSON() : null;
            theEntity.ActualDate = theEntity.ConvertedActualDate ? theEntity.ConvertedActualDate.toJSON() : null;
            theEntity.HiddenForDashboards = adaptHiddenForDashboards(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;

}).service('userService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'User',

        catalogs: [],

        adapter: function(theEntity) {
            return theEntity;
        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {},

        dependencies: [

        ]
    });

    crudInstance.getByUserName = function(sUserName) {
        var _arrAllRecords = crudInstance.getAll();
        for (var i = 0; i < _arrAllRecords.length; i++) {
            if (_arrAllRecords[i].UserName == sUserName) {
                return _arrAllRecords[i];
            }
        }
        return {
            id: -1,
            Value: ''
        };
    };

    crudInstance.getUsersInRoles = function(arrRoles) {
        var _arrAllRecords = crudInstance.getAll();
        var result = [];
        for (var i = 0; i < _arrAllRecords.length; i++) {
            if (arrRoles.indexOf(_arrAllRecords[i].Role) > -1) {
                result.push(_arrAllRecords[i]);
            }
        }
        result.push(_arrAllRecords[0]);
        return result;
    };

    return crudInstance;
});

function adaptHiddenForDashboards(theEntity) {
    var result = [];
    if (theEntity.HiddenForDashboardsTags) {
        for (var i = 0; i < theEntity.HiddenForDashboardsTags.length; i++) {
            var current = theEntity.HiddenForDashboardsTags[i];
            result.push(current.id);
        }
    }
    theEntity.HiddenForDashboardsTags = [];
    return result.join(',');
}

function getDashboardsFromIds(sIDs, dashboardsCatalog) {
    if (sIDs != null && sIDs.length > 0) {
        var arrIDs = sIDs.split(',');
        return arrIDs.map(function(sID) {
            return dashboardsCatalog.getById(sID);
        });
    } else {
        return [];
    }
}


function adaptUsersTags(theEntity) {
    var result = [];
    if (theEntity.OwnersTags) {
        for (var i = 0; i < theEntity.OwnersTags.length; i++) {
            var current = theEntity.OwnersTags[i];
            result.push(current.id);
        }
    }
    theEntity.OwnersTags = [];
    return result.join(',');
}

function getUsersFromIds(sIDs, usersCatalog) {
    if (sIDs != null && sIDs.length > 0) {
        var arrIDs = sIDs.split(',');
        return arrIDs.map(function(sID) {
            return usersCatalog.getById(sID);
        });
    } else {
        return [];
    }
}
