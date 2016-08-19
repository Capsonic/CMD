'use strict';

/**
 * @ngdoc service
 * @name mainApp.CRUDServices
 * @description
 * # CRUDServices
 * Service in the mainApp.
 */
angular.module('CMD.CRUDServices', [])

.service('dashboardService', function(crudFactory, $filter, metricService) {

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
        entityName: "Dashboard",

        catalogs: [],

        adapter: function(theEntity) {
            theEntity.Departments.forEach(function(department) {
                department.Metrics.forEach(function(metric) {
                    metric.FormattedCurrentValue = getFormattedValue(metric.CurrentValue, metric.FormatKey);
                    metric.FormattedGoalValue = getFormattedValue(metric.GoalValue, metric.FormatKey);
                    metric.BasisValue = getMetricsBasisValue(metric.BasisKey);
                    metric.EqualityValue = getFormattedEquality(metric.ComparatorMethodKey);
                    metric.HiddenForDashboardsTags = getDashboardsFromIds(metric.HiddenForDashboards, metricService.catalogs.Dashboards);
                });
            });
            return theEntity;
        },

        adapterIn: function(theEntity) {
            // theEntity.RevisionDate = moment(theEntity.RevisionDate, moment.ISO_8601).format('MM/DD/YYYY');
        },

        adapterOut: function(theEntity, self) {
            //self.validate(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;
}).service('departmentService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: "Department",

        catalogs: [],

        adapter: function(theEntity) {
            return theEntity;
        },

        adapterIn: function(theEntity) {
            // theEntity.RevisionDate = moment(theEntity.RevisionDate, moment.ISO_8601).format('MM/DD/YYYY');
        },

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
        entityName: "Metric",

        catalogs: ['ComparatorMethod', 'MetricFormat', 'MetricBasis', 'Dashboards'],

        adapter: function(theEntity, self) {
            theEntity.FormattedCurrentValue = getFormattedValue(theEntity.CurrentValue, theEntity.FormatKey);
            theEntity.FormattedGoalValue = getFormattedValue(theEntity.GoalValue, theEntity.FormatKey);
            theEntity.BasisValue = self.catalogs.MetricBasis.getById(theEntity.BasisKey).Value;
            theEntity.EqualityValue = getFormattedEquality(theEntity.ComparatorMethodKey);
            theEntity.HiddenForDashboardsTags = getDashboardsFromIds(theEntity.HiddenForDashboards, self.catalogs.Dashboards);
            return theEntity;
        },

        adapterIn: function(theEntity) {
            // theEntity.RevisionDate = moment(theEntity.RevisionDate, moment.ISO_8601).format('MM/DD/YYYY');
        },

        adapterOut: function(theEntity, self) {
            theEntity.HiddenForDashboards = adaptHiddenForDashboards(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;
}).service('initiativeService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: "Initiative",

        catalogs: ['Dashboards'],

        adapter: function(theEntity, self) {
            theEntity.ActualDate = new Date(theEntity.ActualDate);
            theEntity.DueDate = new Date(theEntity.DueDate);
            theEntity.HiddenForDashboardsTags = getDashboardsFromIds(theEntity.HiddenForDashboards, self.catalogs.Dashboards);
            return theEntity;
        },

        adapterIn: function(theEntity) {
            // theEntity.RevisionDate = moment(theEntity.RevisionDate, moment.ISO_8601).format('MM/DD/YYYY');
        },

        adapterOut: function(theEntity, self) {
            theEntity.HiddenForDashboards = adaptHiddenForDashboards(theEntity);
        },

        dependencies: [

        ]
    });

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
