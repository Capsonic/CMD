'use strict';

/**
 * @ngdoc service
 * @name mainApp.CRUDServices
 * @description
 * # CRUDServices
 * Service in the mainApp.
 */
angular.module('CMD.CRUDServices', [])

.service('utilsService', function($filter) {

    var service = {};

    service.getFormattedValue = function(value, format) {
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

    service.getFormattedEquality = function(equality) {
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

    service.toJavascriptDate = function(sISO_8601_Date) {
        return sISO_8601_Date ? moment(sISO_8601_Date, moment.ISO_8601).toDate() : null;
    };

    service.adaptHiddenForDashboards = function(theEntity) {
        var result = [];
        if (theEntity.HiddenForDashboardsTags) {
            for (var i = 0; i < theEntity.HiddenForDashboardsTags.length; i++) {
                var current = theEntity.HiddenForDashboardsTags[i];
                result.push(current.id);
            }
        }
        theEntity.HiddenForDashboardsTags = [];
        return result.join(',');
    };

    service.getDashboardsFromIds = function(sIDs, dashboardsCatalog) {
        if (sIDs != null && sIDs.length > 0) {
            var arrIDs = sIDs.split(',');
            return arrIDs.map(function(sID) {
                return dashboardsCatalog.getById(sID);
            });
        } else {
            return [];
        }
    };

    service.adaptUsersTags = function(theEntity) {
        var result = [];
        if (theEntity.OwnersTags) {
            for (var i = 0; i < theEntity.OwnersTags.length; i++) {
                var current = theEntity.OwnersTags[i];
                result.push(current.id);
            }
        }
        theEntity.OwnersTags = [];
        return result.join(',');
    };

    // service.getUsersFromIds = function(sIDs, usersCatalog) {
    //     if (sIDs != null && sIDs.length > 0) {
    //         var arrIDs = sIDs.split(',');
    //         return arrIDs.map(function(sID) {
    //             return usersCatalog.getById(sID);
    //         });
    //     } else {
    //         return [];
    //     }
    // };

    return service;
})

.service('dashboardService', function(crudFactory, utilsService, metricService, initiativeService) {

    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Dashboard',

        catalogs: ['Users'],

        adapter: function(theEntity, self) {

            theEntity.OwnersTags = utilsService.getDashboardsFromIds(theEntity.Owners, self.catalogs.Users);

            theEntity.Departments.forEach(function(department) {

                department.Metrics.forEach(function(metric) {
                    metricService.adapt(metric);
                });
                department.Initiatives.forEach(function(initiative) {
                    initiativeService.adapt(initiative);
                });

            });
            return theEntity;
        },

        adapterIn: function(theEntity) {

        },

        adapterOut: function(theEntity, self) {
            theEntity.Owners = utilsService.adaptUsersTags(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;

}).service('departmentService', function(crudFactory, metricService, initiativeService) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Department',

        catalogs: [],

        adapter: function(theEntity) {
            theEntity.Metrics.forEach(function(metric) {
                metricService.adapt(metric);
            });
            theEntity.Initiatives.forEach(function(initiative) {
                initiativeService.adapt(initiative);
            });
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

}).service('metricService', function(crudFactory, metricHistoryService, metricYearService) {


    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Metric',

        catalogs: [],

        adapter: function(theEntity, self) {

            theEntity.MetricYears.forEach(function(oYear) {
                //Adapt Metric Year
                metricYearService.adapt(oYear);

                oYear.MetricHistorys.forEach(function(oHistory) {
                    //Adapt Metric History
                    metricHistoryService.adapt(oHistory);
                });

                oYear.MetricHistorys.sort(function(a, b) {
                    return a.ConvertedMetricDate - b.ConvertedMetricDate;
                });
            });

            //Metric Years Sort
            theEntity.MetricYears.sort(function(a, b) {
                return b.Value - a.Value;
            });

            theEntity.SelectedMetricYear = null;
            if (theEntity.MetricYears.length > 0) {
                theEntity.SelectedMetricYear = theEntity.MetricYears[0];
            }

            return theEntity;
        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {

        },

        dependencies: [
            metricYearService
        ]
    });

    crudInstance.getMetricYearByYear = function(oMetric, iYear) {
        if (oMetric && oMetric.MetricYears && iYear) {
            return oMetric.MetricYears.find(function(oYear) {
                return oYear.Value == iYear;
            });
        }
    };

    crudInstance.getLastMetricHistoryForYear = function(oMetric, iYear) {
        if (oMetric && iYear) {
            var oMetricYear = crudInstance.getMetricYearByYear(oMetric, iYear);

            if (oMetricYear) {
                oMetricYear.MetricHistorys.sort(function(a, b) {
                    return a.ConvertedMetricDate - b.ConvertedMetricDate;
                });

                return oMetricYear.MetricHistorys.slice(-1)[0];
            }
        }

        return null;
    };

    return crudInstance;

}).service('metricHistoryService', function(crudFactory, utilsService) {

    var crudInstance = new crudFactory({

        entityName: 'MetricHistory',

        catalogs: [],

        adapter: function(theEntity, self) {

            theEntity.FormattedCurrentValue = utilsService.getFormattedValue(theEntity.CurrentValue, theEntity.FormatKey);
            theEntity.FormattedGoalValue = utilsService.getFormattedValue(theEntity.GoalValue, theEntity.FormatKey);
            theEntity.EqualityValue = utilsService.getFormattedEquality(theEntity.ComparatorMethodKey);
            theEntity.ConvertedMetricDate = utilsService.toJavascriptDate(theEntity.MetricDate);

            theEntity.ConvertedMetricYear = theEntity.ConvertedMetricDate ? theEntity.ConvertedMetricDate.getFullYear() : null;
            theEntity.ConvertedMetricMonth = theEntity.ConvertedMetricDate ? crudInstance.months[theEntity.ConvertedMetricDate.getMonth()] : null;
            theEntity.ConvertedMetricDay = theEntity.ConvertedMetricDate ? theEntity.ConvertedMetricDate.getDate() : null;
            theEntity.ConvertedMetricTime = theEntity.ConvertedMetricDate ? moment(theEntity.ConvertedMetricDate).format('h:mm:ss a') : null;

            return theEntity;

        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {
            theEntity.MetricDate = theEntity.ConvertedMetricDate ? theEntity.ConvertedMetricDate.toJSON() : null;
        },

        dependencies: [

        ]
    });

    crudInstance.months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    return crudInstance;

}).service('metricYearService', function(crudFactory, utilsService) {

    var crudInstance = new crudFactory({

        entityName: 'MetricYear',

        catalogs: ['ComparatorMethod', 'MetricFormat', 'MetricBasis', 'Dashboards'],

        adapter: function(theEntity, self) {
            theEntity.FormattedCurrentValue = utilsService.getFormattedValue(theEntity.CurrentValue, theEntity.FormatKey);
            theEntity.FormattedGoalValue = utilsService.getFormattedValue(theEntity.GoalValue, theEntity.FormatKey);
            theEntity.BasisValue = self.catalogs.MetricBasis.getById(theEntity.BasisKey).Value;
            theEntity.EqualityValue = utilsService.getFormattedEquality(theEntity.ComparatorMethodKey);
            theEntity.HiddenForDashboardsTags = utilsService.getDashboardsFromIds(theEntity.HiddenForDashboards, self.catalogs.Dashboards);

            return theEntity;
        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {
            theEntity.HiddenForDashboards = utilsService.adaptHiddenForDashboards(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;

}).service('initiativeService', function(crudFactory, utilsService) {

    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: 'Initiative',

        catalogs: ['Dashboards'],

        adapter: function(theEntity, self) {
            theEntity.ConvertedActualDate = utilsService.toJavascriptDate(theEntity.ActualDate);
            theEntity.ConvertedDueDate = utilsService.toJavascriptDate(theEntity.DueDate);
            theEntity.HiddenForDashboardsTags = utilsService.getDashboardsFromIds(theEntity.HiddenForDashboards, self.catalogs.Dashboards);
            return theEntity;
        },

        adapterIn: function(theEntity) {},

        adapterOut: function(theEntity, self) {
            theEntity.DueDate = theEntity.ConvertedDueDate ? theEntity.ConvertedDueDate.toJSON() : null;
            theEntity.ActualDate = theEntity.ConvertedActualDate ? theEntity.ConvertedActualDate.toJSON() : null;
            theEntity.HiddenForDashboards = utilsService.adaptHiddenForDashboards(theEntity);
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
