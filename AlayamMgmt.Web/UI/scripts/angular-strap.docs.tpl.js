/**
 * angular-strap
 * @version v2.3.8 - 2016-05-09
 * @link http://mgcrea.github.io/angular-strap
 * @author Olivier Louvignes <olivier@mg-crea.com> (https://github.com/mgcrea)
 * @license MIT License, http://www.opensource.org/licenses/MIT
 */
(function (window, document, undefined) {
    'use strict';
    angular.module('mgcrea.ngStrap').run(['$templateCache', function ($templateCache) {
        $templateCache.put('alert/docs/alert.demo.tpl.html', '<div class="alert" tabindex="-1" ng-class="[type ? \'alert-\' + type : null]"><button type="button" class="close" ng-click="$hide()">&times;</button> <strong ng-bind="title"></strong>&nbsp;<span ng-bind-html="content"></span></div>');
    }]);
    angular.module('mgcrea.ngStrap').run(['$templateCache', function ($templateCache) {
        $templateCache.put('aside/docs/aside.demo.tpl.html', '<div class="aside" tabindex="-1" role="dialog"><div class="aside-dialog"><div class="aside-content"><div class="aside-header" ng-show="title"><button type="button" class="close" ng-click="$hide()">&times;</button><h4 class="aside-title" ng-bind-html="title"></h4></div><div class="aside-body" ng-show="content"><h4>Text in aside</h4><p ng-bind-html="content"></p><pre>2 + 3 = {{ 2 + 3 }}</pre><h4>Popover in aside</h4><p>This <button type="button" class="btn btn-default popover-test" data-title="A Title" data-content="And here\'s some amazing content. It\'s very engaging. right?" bs-popover>button</button> should trigger a popover on click.</p><h4>Tooltips in aside</h4><p><a href="#" class="tooltip-test" data-title="Tooltip" bs-tooltip>This link</a> and <a href="#" class="tooltip-test" data-title="Tooltip" bs-tooltip>that link</a> should have tooltips on hover.</p></div><div class="aside-footer"><button type="button" class="btn btn-default" ng-click="$hide()">Close</button> <button type="button" class="btn btn-primary" ng-click="$hide()">Save changes</button></div></div></div></div>');
    }]);
    angular.module('mgcrea.ngStrap').run(['$templateCache', function ($templateCache) {
        $templateCache.put('modal/docs/modal.demo.tpl.html', '<div class="modal" tabindex="-1" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header" ng-show="title"><button type="button" class="close" ng-click="$hide()">&times;</button><h4 class="modal-title" ng-bind-html="title"></h4></div><div class="modal-body" ng-show="content"><h4>Text in a modal</h4><p ng-bind-html="content"></p><pre>2 + 3 = {{ 2 + 3 }}</pre><h4>Popover in a modal</h4><p>This <a href="#" role="button" class="btn btn-default popover-test" data-title="A Title" data-content="And here\'s some amazing content. It\'s very engaging. right?" bs-popover>button</a> should trigger a popover on click.</p><h4>Tooltips in a modal</h4><p><a href="#" class="tooltip-test" data-title="Tooltip" bs-tooltip>This link</a> and <a href="#" class="tooltip-test" data-title="Tooltip" bs-tooltip>that link</a> should have tooltips on hover.</p></div><div class="modal-footer"><button type="button" class="btn btn-default" ng-click="$hide()">Close</button> <button type="button" class="btn btn-primary" ng-click="$hide()">Save changes</button></div></div></div></div>');
    }]);
    angular.module('mgcrea.ngStrap').run(['$templateCache', function ($templateCache) {
        $templateCache.put('popover/docs/popover-content.demo.tpl.html', '<form name="popoverForm"><p ng-bind-html="content" style="min-width:300px"></p><pre>2 + 3 = <span ng-cloak>{{ 2 + 3 }}</span></pre><div class="form-group"><div class="input-group"><div class="input-group-addon">@</div><input class="form-control" type="email" placeholder="Enter email"></div></div><div class="form-actions"><button type="button" class="btn btn-danger" ng-click="$hide()">Close</button> <button type="button" class="btn btn-primary" ng-click="popover.saved=true;$hide()">Save changes</button></div></form>');
    }]);
    angular.module('mgcrea.ngStrap').run(['$templateCache', function ($templateCache) {
        $templateCache.put('popover/docs/popover.demo.tpl.html', '<div class="popover" tabindex="-1" ng-show="content"><div class="arrow"></div><h3 class="popover-title" ng-bind-html="title" ng-show="title"></h3><div class="popover-content"><form name="popoverForm"><p ng-bind-html="content" style="min-width:300px"></p><pre>2 + 3 = <span ng-cloak>{{ 2 + 3 }}</span></pre><div class="form-group"><div class="input-group"><div class="input-group-addon">@</div><input class="form-control" type="email" placeholder="Enter email"></div></div><div class="form-actions"><button type="button" class="btn btn-danger" ng-click="$hide()">Close</button> <button type="button" class="btn btn-primary" ng-click="popover.saved=true;$hide()">Save changes</button></div></form></div></div>');
    }]);
    angular.module('mgcrea.ngStrap').run(['$templateCache', function ($templateCache) {
        $templateCache.put('tooltip/docs/tooltip.demo.tpl.html', '<div class="tooltip tooltip-info" ng-show="title"><div class="tooltip-arrow"></div><div class="tooltip-inner" ng-bind-html="title"></div></div>');
    }]);
    angular.module('mgcrea.ngStrap').run(['$templateCache', function ($templateCache) {
        $templateCache.put('typeahead/docs/typeahead.demo.tpl.html', '<pre>{{pane.content}}</pre>');
    }]);
})(window, document);