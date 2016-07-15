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
}).service('objectiveService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: "Objective",

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
}).service('metricService', function(crudFactory) {
    var crudInstance = new crudFactory({
        //Entity Name = WebService/API to call:
        entityName: "Metric",

        catalogs: ['ComparatorMethod', 'MetricFormat', 'MetricBasis'],

        adapter: function(theEntity, self) {
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
