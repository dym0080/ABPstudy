var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('MyEventCloud');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);