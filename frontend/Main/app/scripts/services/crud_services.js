'use strict';

/**
 * @ngdoc service
 * @name mainApp.CRUDServices
 * @description
 * # CRUDServices
 * Service in the mainApp.
 */
angular.module('CMD.CRUDServices', [])

.service('dashboardService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: "Dashboard",

        catalogs: [],

        adapter: function(theEntity) {
            return theEntity;
        },

        adapterIn: function(theEntity) {
            // theEntity.RevisionDate = moment(theEntity.RevisionDate, moment.ISO_8601).format('MM/DD/YYYY');
        },

        adaptToServer: function(theEntity) {
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

        adaptToServer: function(theEntity) {
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

        catalogs: ['ComparatorMethod', 'MetricFormat', 'MetricBasis'],

        adapter: function(theEntity, self) {
            theEntity.FormattedCurrentValue = getFormattedValue(theEntity.CurrentValue, theEntity.FormatKey);
            theEntity.FormattedGoalValue = getFormattedValue(theEntity.GoalValue, theEntity.FormatKey);
            theEntity.BasisValue = self.catalogs.MetricBasis.getById(theEntity.BasisKey).Value;
            theEntity.EqualityValue = getFormattedEquality(theEntity.ComparatorMethodKey);
            return theEntity;
        },

        adapterIn: function(theEntity) {
            // theEntity.RevisionDate = moment(theEntity.RevisionDate, moment.ISO_8601).format('MM/DD/YYYY');
        },

        adaptToServer: function(theEntity) {
            //self.validate(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;
}).service('initiativeService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: "Initiative",

        catalogs: [],

        adapter: function(theEntity, self) {
            theEntity.ActualDate = new Date(theEntity.ActualDate);
            theEntity.DueDate = new Date(theEntity.DueDate);
            return theEntity;
        },

        adapterIn: function(theEntity) {
            // theEntity.RevisionDate = moment(theEntity.RevisionDate, moment.ISO_8601).format('MM/DD/YYYY');
        },

        adaptToServer: function(theEntity) {
            //self.validate(theEntity);
        },

        dependencies: [

        ]
    });

    return crudInstance;
});
