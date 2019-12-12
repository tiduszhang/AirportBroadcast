(function () {
    //设备管理
    appModule.controller('common.views.adioSets.playSetGlobal.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants',
        'abp.services.app.tenantSettings',
        function ($scope, $uibModal, $stateParams, uiGridConstants, tenantSettings) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.entity = {};
            vm.getAll = function () {
                vm.loading = true;
                tenantSettings.getPlayTimesAndCanPlayLanguages()
                    .then(function (result) {
                        vm.entity = result.data; 
                       
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.save = function () {
                abp.message.confirm(
                    "",
                    "确定设置？",
                    function (isConfirmed) {
                        if (isConfirmed) {
                            tenantSettings.setPlayTimesAndCanPlayLanguages(vm.entity).then(function () {
                                vm.getAll();
                                abp.notify.success("设置成功！");
                            });
                        }
                    }
                );

            };
              

            vm.getAll();
        }]);
})();