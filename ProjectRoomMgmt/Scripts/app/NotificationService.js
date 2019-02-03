'use strict';
angular.module('kaiptcapp')
    .factory('NotificationService', [
        '$http', '$q', function($http, $q) {

            var NotificationServiceFactory = {};

            var _alert = function(message, title, position, style) {

                if (typeof (title) === 'undefined') title = "Error";
                if (typeof (position) === 'undefined') position = "bottom-right";
                if (typeof (style) === 'undefined') style = "circle";

                $('body').pgNotification({
                    style: style,
                    title: title,
                    message: message,
                    position: position,
                    timeout: 10000,
                    type: "danger",
                    //thumbnail: '<img width="40" height="40" style="display: inline-block;" src="assets/img/profiles/avatar2x.jpg" data-src="assets/img/profiles/avatar.jpg" ui-jq="unveil" data-src-retina="assets/img/profiles/avatar2x.jpg" alt="">'
                }).show();

            };

            var _success = function (message, title, position, style) {

                if (typeof (title) === 'undefined') title = "Success";
                if (typeof (position) === 'undefined') position = "bottom-right";
                if (typeof (style) === 'undefined') style = "circle";

                $('body').pgNotification({
                    style: style,
                    title: title,
                    message: message,
                    position: position,
                    timeout: 5000,
                    type: "success",
                    //thumbnail: '<img width="40" height="40" style="display: inline-block;" src="assets/img/profiles/avatar2x.jpg" data-src="assets/img/profiles/avatar.jpg" ui-jq="unveil" data-src-retina="assets/img/profiles/avatar2x.jpg" alt="">'
                }).show();

            };

            var _info = function (message, title, position, style) {

                if (typeof (title) === 'undefined') title = "Xpath";
                if (typeof (position) === 'undefined') position = "bottom-right";
                if (typeof (style) === 'undefined') style = "circle";

                $('body').pgNotification({
                    style: style,
                    title: title,
                    message: message,
                    position: position,
                    timeout: 5000,
                    type: "info",
                    //thumbnail: '<img width="40" height="40" style="display: inline-block;" src="assets/img/profiles/avatar2x.jpg" data-src="assets/img/profiles/avatar.jpg" ui-jq="unveil" data-src-retina="assets/img/profiles/avatar2x.jpg" alt="">'
                }).show();

            };

            var _warning = function (message, title, position, style) {

                if (typeof (title) === 'undefined') title = "Warning";
                if (typeof (position) === 'undefined') position = "bottom-right";
                if (typeof (style) === 'undefined') style = "circle";

                $('body').pgNotification({
                    style: style,
                    title: title,
                    message: message,
                    position: position,
                    timeout: 5000,
                    type: "warning",
                    //thumbnail: '<img width="40" height="40" style="display: inline-block;" src="assets/img/profiles/avatar2x.jpg" data-src="assets/img/profiles/avatar.jpg" ui-jq="unveil" data-src-retina="assets/img/profiles/avatar2x.jpg" alt="">'
                }).show();

            };

            NotificationServiceFactory.alert = _alert;
            NotificationServiceFactory.success = _success;
            NotificationServiceFactory.info = _info;
            NotificationServiceFactory.warning = _warning;
            return NotificationServiceFactory;
        }
    ]);